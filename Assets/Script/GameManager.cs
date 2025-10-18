using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject mainMenuPanel;
    public GameObject gamePanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public Text currentPlayerText;
    public Text gameModeText;
    public Button startButton;
    public Button restartButton;
    public Button resumeButton;
    public Button mainMenuButton;
    public Button quitButton;
    public Button pauseButton;

    [Header("Game Mode Selection")]
    public GameObject gameModeSelectionPanel;
    public Button pvpButton;
    public Button pveButton;
    public Button backToMenuButton;

    private ChessBoard chessBoard;
    private string currentGameMode = "pvp";

    public void Start()
    {
        chessBoard = FindFirstObjectByType<ChessBoard>();
        if (chessBoard != null)
        {
            chessBoard.OnGameWin += ShowGameOver;
        }
        // Setup button listeners
        startButton.onClick.AddListener(ShowGameModeSelection);
        restartButton.onClick.AddListener(RestartGame);
        resumeButton.onClick.AddListener(ResumeGame);
        mainMenuButton.onClick.AddListener(ShowMainMenu);
        quitButton.onClick.AddListener(QuitGame);
        pauseButton.onClick.AddListener(PauseGame);

        // Game mode buttons
        pvpButton.onClick.AddListener(() => StartGameWithMode("pvp"));
        pveButton.onClick.AddListener(() => StartGameWithMode("pve"));
        backToMenuButton.onClick.AddListener(ShowMainMenu);

        ShowMainMenu();
    }

    void Update()
    {
        // Update current player display during gameplay
        if (gamePanel.activeInHierarchy && chessBoard != null && currentPlayerText != null)
        {
            string playerName = chessBoard.CurrentPlayer == "x" ? "Player X" : "Player O";
            string modeInfo = currentGameMode == "pve" && chessBoard.CurrentPlayer == "o" ? " (Bot)" : "";
            currentPlayerText.text = $"Current: {playerName}{modeInfo}";
            
            // Update game mode text
            if (gameModeText != null)
                gameModeText.text = currentGameMode == "pvp" ? "Mode: PvP" : "Mode: PvE";
        }
    }

    public void ShowGameModeSelection()
    {
        mainMenuPanel.SetActive(false);
        gameModeSelectionPanel.SetActive(true);
    }

    public void StartGameWithMode(string gameMode)
    {
        currentGameMode = gameMode;
        gameModeSelectionPanel.SetActive(false);
        gamePanel.SetActive(true);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);

        if (chessBoard != null)
        {
            chessBoard.SetGameMode(gameMode);
            chessBoard.RestartGame();
        }
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void ShowMainMenu()
    {
        Time.timeScale = 1;
        mainMenuPanel.SetActive(true);
        gameModeSelectionPanel.SetActive(false);
        gamePanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        
        if (chessBoard != null)
        {
            chessBoard.RestartGame();
        }

        gameOverPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void ShowGameOver(string winner)
    {
        Debug.Log($"GameOver called with winner: {winner}");
        
        gameOverPanel.SetActive(true);
        
        if (winner == "x")
            gameOverText.text = currentGameMode == "pvp" ? "Player X Wins!" : "You Win!";
        else if (winner == "o")
            gameOverText.text = currentGameMode == "pvp" ? "Player O Wins!" : "Bot Wins!";
        else
            gameOverText.text = "It's a Draw!";
    }

    void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}