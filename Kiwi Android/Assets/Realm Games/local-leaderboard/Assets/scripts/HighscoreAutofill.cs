using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreAutofill : MonoBehaviour
{
    private Text highScoreUI;

    // Start is called before the first frame update
    void Start()
    {
        highScoreUI = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        highScoreUI.text = "Enter " + PlayerPrefs.GetInt("HighScore").ToString();
    }
}
