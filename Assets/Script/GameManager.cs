using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject mainMenuPanel;
    public GameObject gamePanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public Button startButton;
    public Button restartButton;
    public Button resumeButton;
    public Button mainMenuButton;
    public Button quitButton;
    public Button pauseButton;

    private ChessBoard chessBoard;

    public void Start()
    {
        chessBoard = FindFirstObjectByType<ChessBoard>();
        
        // Setup button listeners
        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartGame);
        resumeButton.onClick.AddListener(ResumeGame);
        mainMenuButton.onClick.AddListener(ShowMainMenu);
        quitButton.onClick.AddListener(QuitGame);
        pauseButton.onClick.AddListener(PauseGame);

        ShowMainMenu();
    }

    void Update()
    {
        // Update current player display during gameplay
        if (gamePanel.activeInHierarchy && chessBoard != null)
        {
            string playerName = chessBoard.CurrentPlayer == "x" ? "Player X" : "Player O";
        }
    }

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        gamePanel.SetActive(true);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);

        // THÊM DÒNG NÀY ĐỂ KHỞI TẠO KHI BẮT ĐẦU GAME
        if (chessBoard != null)
        {
            chessBoard.RestartGame();
        }
        var uiManager = FindFirstObjectByType<UIManager>();
        if (uiManager != null) uiManager.OnGameRestart();

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
        gamePanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        
        if (chessBoard != null)
        {
            // SỬA DÒNG NÀY
            chessBoard.RestartGame();
        }

        gameOverPanel.SetActive(false);
        gamePanel.SetActive(true);
        
        var uiManager = FindFirstObjectByType<UIManager>();
        if (uiManager != null) uiManager.OnGameRestart();
    }

    public void ShowGameOver(string winner)
    {
        gameOverPanel.SetActive(true);
    }

    void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}