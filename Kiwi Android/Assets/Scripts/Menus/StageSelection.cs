using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StageSelection : MonoBehaviour
{
    public LeveLoader levelLoader;

    //Increment this by 1 for time a unique stage is complete - Call in level_Trans script?
    [Header("Stage Management - Locked")]
    public GameObject stage2;
    public GameObject stage3;
    public GameObject endlessStage;

    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip playSound;

    

    // Start is called before the first frame update
    void Start()
    {
        //Testing
        //PlayerPrefs.SetInt("numberOfUnlockedStages", 4);

        if (PlayerPrefs.GetInt("numberOfUnlockedStages") == 0)
            PlayerPrefs.SetInt("numberOfUnlockedStages", 1);

        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LeveLoader>();

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = playSound;

        //Unlock avalible stages
        if (PlayerPrefs.GetInt("numberOfUnlockedStages") >= 2)
        {
            stage2.GetComponent<Image>().color = new Color(255, 255, 255, 255);
            stage2.GetComponent<Button>().interactable = true;
        }
        if (PlayerPrefs.GetInt("numberOfUnlockedStages") >= 3)
        {
            stage3.GetComponent<Image>().color = new Color(255, 255, 255, 255);
            stage3.GetComponent<Button>().interactable = true;
        }
        if (PlayerPrefs.GetInt("numberOfUnlockedStages") >= 4)
        {
            endlessStage.GetComponent<Image>().color = new Color(255, 255, 255, 255);
            endlessStage.GetComponent<Button>().interactable = true;
            endlessStage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Endless Mode";
            //Unlock Endless Stage
        }
    }

    public void selectedStage_1()
    {
        audioSource.Play();
        levelLoader.LoadNextLevel("New Zealand Selected");
    }

    public void selectedStage_2()
    {
        //if (numberOfUnlockedStages < 2) return;
        audioSource.Play();
        levelLoader.LoadNextLevel("Australia Selected");
    }

    public void selectedStage_3()
    {
        //if (numberOfUnlockedStages < 3) return;
        audioSource.Play();
        levelLoader.LoadNextLevel("Japan Selected");
    }

    public void selectedEndlessMode()
    {
        audioSource.Play();
        levelLoader.LoadNextLevel("EndlessMode");
    }

    public void returnToStartMenu()
    {
        audioSource.Play();
        levelLoader.LoadNextLevel("StartMenu");
    }
}
