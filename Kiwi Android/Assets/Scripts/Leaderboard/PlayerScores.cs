using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FullSerializer;
using UnityEngine.UI;

public class PlayerScores : MonoBehaviour
{
    public static PlayerScores instance;
    //private int playerScore;
    public static string playerScore;
    public static string playerDate;
    public static string playerName;
    public InputField nameText;
    public InputField nameText3;
    public Text scoreText;
    public Text nameText2;
    public Text playerDate2;
    User user = new User();
    //private int playerRank;

    public static fsSerializer serializer = new fsSerializer();

    // Start is called before the first frame update
    void Start()
    {
        //playerScore = PlayerPrefs.GetInt("HighScore");
        //playerScore.ToString();
        playerScore = "";
        playerScore = playerScore.ToString();
        playerDate = DateTime.Now.ToString();
        //scoreText.text = playerScore.ToString();
        //playerRank = PlayerPrefs.GetInt(".score");
    }

    public void OnSubmit()
    {
        playerScore = nameText3.text;
        playerName = nameText.text;
        if (playerScore == PlayerPrefs.GetInt("HighScore").ToString())
        {
            Debug.Log("Correct Highscore");
        }
        else if (nameText3 = null)
        {
            playerScore = PlayerPrefs.GetInt("HighScore").ToString();
        }
        else
        {
            playerScore = PlayerPrefs.GetInt("HighScore").ToString();
        }
        PostToDatabase();
    }

    public void OnGetScore()
    {
        playerName = nameText.text;
        playerScore = nameText3.text;
        //playerScore = PlayerPrefs.GetInt("HighScore");
        playerDate = DateTime.Now.ToString();
        RetrieveFromDatabase();
    }

    private void UpdateScore()
    {
        scoreText.text = user.Score.ToString();
        nameText2.text = user.Name;
        playerDate2.text = user.Date;
    }

    private void PostToDatabase()
    {
        User user = new User();
        RestClient.Put("https://kiwi-bf6ba-default-rtdb.firebaseio.com/Highscore_Leaderboard/ " + playerName + " .json", user);
    }

    private void RetrieveFromDatabase()
    {
        RestClient.Get<User>("https://kiwi-bf6ba-default-rtdb.firebaseio.com/Highscore_Leaderboard/ " + nameText.text + " .json").Then(response =>
        {
            user = response;
            UpdateScore();
        });
    }

    /*private void RetrieveFromDatabase2()
    {
        RestClient.Get<User[]>("https://kiwi-bf6ba-default-rtdb.firebaseio.com/Highscore_Leaderboard/ " + " .json").Then(onResolved: response =>
        {
            fsData userData = fsJsonParser.Parse(response.ToString());

            User[] users = null;
            serializer.TryDeserialize(userData, ref users);

            foreach (var user in users)
            {
                
            }

            UpdateScore();
        });
    }*/
}
