using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageInfo : MonoBehaviour
{
    public LeveLoader levelLoader;
    public Text kiwiText; //For endlessMode only

    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip playSound;

    // Start is called before the first frame update
    void Start()
    {
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LeveLoader>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = playSound;
    }

    public void playStageNum(int stageNum)
    {
        audioSource.Play();
        if (stageNum == 1)
            levelLoader.LoadNextLevel("Stage 1 Scene");
        else if (stageNum == 2)
            levelLoader.LoadNextLevel("Stage 2 Scene");
        else if (stageNum == 3)
            levelLoader.LoadNextLevel("Stage 3 Scene");
        else if (stageNum == 4)
        {
            if (EndlessModeSettings.numberOfIncludedLevels == 0)
            {
                kiwiText.text = "Need to have at least one level!";
                return;
            }
            levelLoader.LoadNextLevel("EndlessMode Scene");
        }
    }

    public void returnToStageSelection()
    {
        Time.timeScale = 1;
        audioSource.Play();
        levelLoader.LoadNextLevel("StageSelection");
    }

    public void returnToStageSelection2()
    {
        Time.timeScale = 1;
        audioSource.Play();
        levelLoader.LoadNextLevel("StageSelection");
    }

    public void goToCutsceneTransition_1_to_2()
    {
        Time.timeScale = 1;
        audioSource.Play();
        print("A");
        levelLoader.LoadNextLevel("Level_1_to_2");
    }

    public void goToCutsceneTransition_2_to_3()
    {
        Time.timeScale = 1;
        audioSource.Play();
        print("B");
        levelLoader.LoadNextLevel("Level_2_to_3");
    }

    public void goToCutsceneTransition_3_to_end()
    {
        Time.timeScale = 1;
        audioSource.Play();
        print("C");
        levelLoader.LoadNextLevel("Level_3_to_end");
    }
}
