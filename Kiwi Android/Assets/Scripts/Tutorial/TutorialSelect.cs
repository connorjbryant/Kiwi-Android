using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialSelect : MonoBehaviour
{
    public LeveLoader levelLoader;
    private AudioSource audioSource;

    public Text kiwiText;
    public GameObject tutorial_1_playButton;
    public string tutorial_1_info;
    public GameObject tutorial_2_playButton;
    public string tutorial_2_info;
    public GameObject tutorial_3_playButton;
    public string tutorial_3_info;
    public GameObject tutorial_4_playButton;
    public string tutorial_4_info;

    // Start is called before the first frame update
    void Start()
    {
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LeveLoader>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TutorialInfo(int tutorialNumber)
    {
        audioSource.Play();
        if (tutorialNumber == 1)
        {
            kiwiText.text = tutorial_1_info;
            SetPlayButtonsOff();
            tutorial_1_playButton.SetActive(true);
        }
        else if (tutorialNumber == 2)
        {
            kiwiText.text = tutorial_2_info;
            SetPlayButtonsOff();
            tutorial_2_playButton.SetActive(true);
        }
        else if (tutorialNumber == 3)
        {
            kiwiText.text = tutorial_3_info;
            SetPlayButtonsOff();
            tutorial_3_playButton.SetActive(true);
        }
        else if (tutorialNumber == 4)
        {
            kiwiText.text = tutorial_4_info;
            SetPlayButtonsOff();
            tutorial_4_playButton.SetActive(true);
        }
    }

    public void SetPlayButtonsOff()
    {
        tutorial_1_playButton.SetActive(false);
        tutorial_2_playButton.SetActive(false);
        tutorial_3_playButton.SetActive(false);
        tutorial_4_playButton.SetActive(false);
    }

    public void goToTutorial_num(int tutorialNumber)
    {
        audioSource.Play();
        if (tutorialNumber == 1)
        {
            //SceneManager.LoadScene("1_Tutorial");
            levelLoader.LoadNextLevel("1_Tutorial");
        }
        else if (tutorialNumber == 2)
        {
            //SceneManager.LoadScene("2_Tutorial");
            levelLoader.LoadNextLevel("2_Tutorial");
        }
        else if (tutorialNumber == 3)
        {
            //SceneManager.LoadScene("3_Tutorial");
            levelLoader.LoadNextLevel("3_Tutorial");
        }
        else if (tutorialNumber == 4)
        {
            //SceneManager.LoadScene("4_Tutorial");
            levelLoader.LoadNextLevel("4_Tutorial");
        }
    }

    public void returnToStartMenu()
    {
        audioSource.Play();
        //SceneManager.LoadScene("StartMenu");
        levelLoader.LoadNextLevel("StartMenu");
    }
}
