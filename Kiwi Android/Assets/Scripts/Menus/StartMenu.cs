using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public LeveLoader levelLoader;
    public GameObject TutorialMenu;

    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip playSound;
    public AudioClip quitSound;
    public AudioClip hoveringSound;
    

    private void Start()
    {
        //Testing playerPrefs
        //PlayerPrefs.DeleteAll();

        if (PlayerPrefs.GetInt("GotStartingCoins") == 0)
        {
            PlayerPrefs.SetInt("numCoins", 100);
            PlayerPrefs.SetInt("GotStartingCoins", 1);
        }

        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LeveLoader>();
        audioSource = GetComponent<AudioSource>();

        //Set player pref quality
        if (PlayerPrefs.GetInt("FirstSettingQualityLevel") == 0)
        {
            QualitySettings.SetQualityLevel(2);
            PlayerPrefs.SetInt("CurrentQualityLevel", 2); //2 is medium default settings
            PlayerPrefs.SetInt("FirstSettingQualityLevel", 1);
        }
        else
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("CurrentQualityLevel"));
        }

        //Initial Tutorial PopIp
        if (PlayerPrefs.GetInt("FirstTutorialPopUp") == 0)
        {
            TutorialMenu.SetActive(true);
            PlayerPrefs.SetInt("FirstTutorialPopUp", 1);
        }
    }

    public void GoToStartMenu()
    {
        levelLoader.LoadNextLevel("StartMenu");
    }

    public void PlayGame()
    {
        audioSource.clip = playSound;
        audioSource.Play();

        levelLoader.LoadNextLevel("StageSelection");
    }

    public void GoToStore()
    {
        audioSource.clip = playSound;
        audioSource.Play();

        levelLoader.LoadNextLevel("Store");
    }

    public void GoToDressingRoom()
    {
        audioSource.clip = playSound;
        audioSource.Play();

        levelLoader.LoadNextLevel("DressingRoom");
    }

    public void GoToLeaderboard()
    {
        audioSource.clip = playSound;
        audioSource.Play();
        //SceneManager.LoadScene(12);
        levelLoader.LoadNextLevel("Leaderboard");
    }

    public void QuitGame()
    {
        audioSource.clip = quitSound;
        audioSource.Play();

        Application.Quit();
    }

    public void Hovering()
    {
        audioSource.clip = hoveringSound;
        audioSource.Play();
    }

    public void ButtonPressed()
    {
        audioSource.clip = playSound;
        audioSource.Play();
    }

    public void GoBack()
    {
        audioSource.clip = quitSound;
        audioSource.Play();
    }

    public void Tutorial()
    {
        audioSource.clip = playSound;
        audioSource.Play();

        levelLoader.LoadNextLevel("TutorialPreview");
    }
}


