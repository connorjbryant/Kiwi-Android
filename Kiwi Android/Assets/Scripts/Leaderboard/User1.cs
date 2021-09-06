using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class User1
{
    public static User1 instance;

    public string userName;
    public int userScore;
    public string userDate;
    private int tempUserScore;
    //public int userRank;

    public User1()
    {
        userName = PlayerScores.playerName;
        userScore = PlayerPrefs.GetInt("HighScore");
        userDate = PlayerScores.playerDate;
        //userRank = PlayerPrefs.GetInt(".score");
    }
}
