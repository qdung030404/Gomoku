using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Game Info UI")]
    public Text moveCountText;
    public Text timerText;
    public Slider difficultySlider;
    public Text difficultyText;
    

    private ChessBoard chessBoard;
    private float gameTime;
    private int moveCount;
    private bool isTiming;

    void Start()
    {
        chessBoard = FindFirstObjectByType<ChessBoard>();
        
        // Initialize UI elements
        difficultySlider.onValueChanged.AddListener(OnDifficultyChanged);
        
        ResetGameStats();
    }

    void Update()
    {
        if (isTiming)
        {
            gameTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    public void StartGameTimer()
    {
        isTiming = true;
        gameTime = 0;
        moveCount = 0;
        UpdateMoveCount();
    }

    public void StopGameTimer()
    {
        isTiming = false;
    }

    public void IncrementMoveCount()
    {
        moveCount++;
        UpdateMoveCount();
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void UpdateMoveCount()
    {
        moveCountText.text = $"Moves: {moveCount}";
    }

    private void OnDifficultyChanged(float value)
    {
        difficultyText.text = $"Difficulty: {(int)value}";
        // You can adjust bot difficulty here
    }

    private void OnBoardSizeChanged(int index)
    {
        int[] sizes = { 10, 15, 19 }; // Common board sizes
        if (chessBoard != null)
        {
            // You might want to add a method to resize the board
        }
    }

    public void ResetGameStats()
    {
        gameTime = 0;
        moveCount = 0;
        UpdateTimerDisplay();
        UpdateMoveCount();
    }
    public void OnGameRestart()
    {
        ResetGameStats();
        StartGameTimer();
    }
}