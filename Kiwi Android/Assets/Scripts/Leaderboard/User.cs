using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public static int currentScore = 0;

    public void Start()
    {
        currentScore = PlayerPrefs.GetInt("HighScore");
    }

    public static User instance;

    public string Name;
    public string Score;
    //public int Score2 = currentScore;
    public int Score2 = PlayerPrefs.GetInt("HighScore");
    public string Date;
    private int tempUserScore;
    //public int userRank;
    //private int tempScore;

    /*public void Start()
    {
        tempScore = PlayerPrefs.GetInt("HighScore");
    }*/

    public User()
    {
        Name = PlayerScores.playerName;
        //Score2 = tempScore;
        Score = PlayerScores.playerScore;
        Date = PlayerScores.playerDate;
        //userRank = PlayerPrefs.GetInt(".score");
    }
}
