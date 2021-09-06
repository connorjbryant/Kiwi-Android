using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialEvent : MonoBehaviour
{
    public float eventTime;
    public GameObject[] eventTriggers;
    public GameObject[] tutorialTexts;

    public GameObject obstacleArrows;
    public GameObject PillarArrows;
    public GameObject KiwiArrows;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        eventTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        eventTime += Time.deltaTime;

        //Welcome to Kiwi!
        if (eventTime > 0 && eventTime <= 5)
        {
            eventTriggers[0].SetActive(true);
        }
        //Here is how to play the game!
        else if (eventTime > 5 && eventTime <= 10)
        {
            eventTriggers[0].SetActive(false);
            eventTriggers[1].SetActive(true);
        }
        //Press "W" to glide
        else if (eventTime > 10 && eventTime <= 15)
        {
            eventTriggers[1].SetActive(false);
            eventTriggers[2].SetActive(true);
        }
        //Press "A" and "D" to Strafe
        else if (eventTime > 15 && eventTime <= 20)
        {
            eventTriggers[2].SetActive(false);
            eventTriggers[3].SetActive(true);
        }
        //Avoid the obstacles
        else if (eventTime > 20 && eventTime <= 30)
        {
            eventTriggers[3].SetActive(false);
            eventTriggers[4].SetActive(true);
            obstacleArrows.SetActive(true);
        }
        //Regain flight meter
        else if (eventTime > 35 && eventTime <= 40)
        {
            eventTriggers[4].SetActive(false);
            eventTriggers[5].SetActive(true);
            obstacleArrows.SetActive(false);
            PillarArrows.SetActive(true);
        }
        //Pick up kiwis!
        else if (eventTime > 40 && eventTime <= 45)
        {
            eventTriggers[5].SetActive(false);
            eventTriggers[6].SetActive(true);
            PillarArrows.SetActive(false);
            KiwiArrows.SetActive(true);
        }
        //Throw them at enemies!
        else if (eventTime > 45 && eventTime <= 50)
        {
            eventTriggers[6].SetActive(false);
            eventTriggers[7].SetActive(true);
            KiwiArrows.SetActive(false);
        }
        //Done
        else if (eventTime > 50 && eventTime <= 54)
        {
            eventTriggers[7].SetActive(false);
            eventTriggers[8].SetActive(true);
        }
        else if (eventTime >= 55)
        {
            SceneManager.LoadScene("StartMenu");
        }
    }
}
