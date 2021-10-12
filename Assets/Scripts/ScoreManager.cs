using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI highScoreText;

    private TextMeshProUGUI scoreText;

    private int score;
    private int highScore;
    private int combo;

    public int Combo { get => combo; set => combo = value; }
    public int Score { get => score; set => score = value; }
    private void Awake()
    {
        if (Instance is null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        score = 0;
        combo = 0;
        highScoreText.text = "Highscore : " + highScore.ToString();
    }
    private void OnLevelWasLoaded(int level)
    {
        switch (level)
        {
            case 0:
                if (highScoreText == null)
                    highScoreText = GameObject.Find("Canvas/Highscore").GetComponent<TextMeshProUGUI>();
                highScore = PlayerPrefs.GetInt("HighScore", 0);
                highScoreText.text = "Highscore : " + highScore.ToString();
                break;
            default:
                scoreText = GameObject.Find("Hud/Score").GetComponent<TextMeshProUGUI>();
                UpdateScore();
                break;
        }
    }
    public void ScoreUp()
    {
        score += 1 + combo;
        iTween.ShakePosition(scoreText.gameObject, Vector3.one * 5f, 0.2f);
        combo++;
        UpdateScore();
    }
    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    }
    public void CheckScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            highScore = score;
            GameManager.Instance.hud.EnableNewHighScoreText();
            GameManager.Instance.hud.EnableNewHighScoreTextInCredits();
        }

        PlayerPrefs.SetInt("HighScore", highScore);
    }
    public void BossHit()
    {
        score += 1 + combo;
        iTween.ShakePosition(scoreText.gameObject, Vector3.one * 5f, 0.2f);
        UpdateScore();
    }
    public void BossKilled()
    {
        score *= 10;
        iTween.ShakePosition(scoreText.gameObject, Vector3.one * 5f, 2f);
        UpdateScore();
    }
}
