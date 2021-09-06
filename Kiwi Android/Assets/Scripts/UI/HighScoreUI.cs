using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreUI : MonoBehaviour
{
    private TextMeshProUGUI highScoreUI;

    // Start is called before the first frame update
    public void Start()
    {
        highScoreUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public void Update()
    {
        highScoreUI.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
        //PlayfabManager.SendLeaderboard(ScoreUI.instance.score);
    }

}
