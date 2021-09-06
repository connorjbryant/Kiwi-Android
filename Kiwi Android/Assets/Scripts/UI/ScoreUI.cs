using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public static ScoreUI instance;

    public float float_score;
    public int score = 0;
    public float highScore;
    public float scoreMultiplier = 1;

    public int CoinPoints = 25;
    public int killPoints = 50;

    private TextMeshProUGUI scoreUI;
    public bool isTutorial;

    // Start is called before the first frame update
    public void Start()
    {
        float_score = 0;
        scoreUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public void Update()
    {
        //score += Time.deltaTime * 1;
        //score += (int)Time.time;
        //float scoreRounded = Mathf.Round(score) * scoreMultiplier;
        //int myScore = (int)(score + 0.5f);
        /*
        if (score >= PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            //PlayfabManager.SendLeaderboard(score);
        }
        */
        //scoreUI.text = "Score: " + scoreRounded.ToString();
        //scoreUI.text = Mathf.FloorToInt(scoreRounded).ToString();
        float_score += Time.deltaTime;
        float scoreRounded = Mathf.Round(float_score);
        scoreUI.text = scoreRounded.ToString();
    }
}
