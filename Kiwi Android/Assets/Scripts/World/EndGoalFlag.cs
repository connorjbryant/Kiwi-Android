using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndGoalFlag : MonoBehaviour
{
    public int stageNum;
    private GameObject kiwiPlayer;
    private AutoScrollSpeed autoScrollSpeedScript;
    public GameObject winGameScreen;
    private bool isDone;

    // Start is called before the first frame update
    void Start()
    {
        isDone = false;
        kiwiPlayer = GameObject.FindGameObjectWithTag("Player");
        autoScrollSpeedScript = transform.parent.GetComponent<AutoScrollSpeed>();
        winGameScreen = GameObject.Find("Canvas").transform.Find("YouWin").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (kiwiPlayer.transform.position.x >= transform.position.x)
        {
            //autoScrollSpeedScript.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isDone)
                return;

            GetComponent<AudioSource>().Play();

            Debug.Log("You Won Stage " + stageNum);
            Debug.Log("You Unlocked Stage " + (stageNum + 1));

            if (stageNum == 1 && PlayerPrefs.GetInt("numberOfUnlockedStages") < 2)
            {
                //Unlock Stage 2
                PlayerPrefs.SetInt("numberOfUnlockedStages", 2);
            }
            else if (stageNum == 2 && PlayerPrefs.GetInt("numberOfUnlockedStages") < 3)
            {
                //Unlock stage 3
                PlayerPrefs.SetInt("numberOfUnlockedStages", 3);
            }
            else if (stageNum == 3 && PlayerPrefs.GetInt("numberOfUnlockedStages") < 4)
            {
                //Unlock endless mode
                PlayerPrefs.SetInt("numberOfUnlockedStages", 4);
            }
            
            winGameScreen.SetActive(true);
            Time.timeScale = 0;
            isDone = true;
        }
    }
}
