using UnityEngine;
using UnityEngine.UI;
using YG;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameStarted;
    public GameObject startingText;
    public GameObject SwipeToMoveText;
    public GameObject ArrowR;
    public GameObject ArrowL;

    public static int numberOfCheese;
    public Text cheeseText;
    public Text highScore;

    private const string YandexLeaderBoardName = "CheeseScore";

    public void StartGame()
    {
        Destroy(startingText);
        Destroy(SwipeToMoveText);
        Destroy(ArrowR);
        Destroy(ArrowL);
        isGameStarted = true;
        UpdateHighScore();
    }
    public void StopGame()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }
    private void OnEnable()
    {
        PlayerController.gameEnded += StopGame;
        PlayerController.CollectedAmountChanged += ChangeCollectedAmount;
    }
    private void OnDisable()
    {
        PlayerController.gameEnded -= StopGame;
        PlayerController.CollectedAmountChanged -= ChangeCollectedAmount;
    }
    private void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;
        numberOfCheese = 0;

        /*playerForCoordinates = GameObject.FindGameObjectWithTag("Player");*/
    }
    private void ChangeCollectedAmount()
    {
        cheeseText.text = numberOfCheese.ToString();
        CheckHighScore();
    }

    private void CheckHighScore()
    {
        if (numberOfCheese > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", numberOfCheese);
            UpdateHighScore();
            YandexGame.NewLeaderboardScores(YandexLeaderBoardName, numberOfCheese);
        }
    }
    
    private void UpdateHighScore() => highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
}