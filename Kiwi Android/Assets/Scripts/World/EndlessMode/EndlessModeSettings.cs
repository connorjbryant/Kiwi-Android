using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndlessModeSettings : MonoBehaviour
{
    public AudioSource audioSource;
    public Text kiwiText;
    public static int numberOfIncludedLevels;
    public List<GameObject> level_X_marks = new List<GameObject>(); //UI info for selected level or not

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("Starting_EndlessMode_Level") == 0)
        {
            for (int i = 1; i <= 7; i++)
                PlayerPrefs.SetInt("EndlessMode_Level" + i, 1);
            PlayerPrefs.SetInt("Starting_EndlessMode_Level", 1);
        }

        for(int i = 1; i <= 7; i++)
        {
            if (PlayerPrefs.GetInt("EndlessMode_Level" + i) == 0)
            {
                level_X_marks[i - 1].SetActive(true);
            }
            else if (PlayerPrefs.GetInt("EndlessMode_Level" + i) == 1)
            {
                level_X_marks[i - 1].SetActive(false);
            }
        }

        countNumOfIncludedLevels();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            printLevelMarks();
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            List<int> excludedLevelList = new List<int>();
            for (int i = 1; i <= 7; i++)
            {
                if (PlayerPrefs.GetInt("EndlessMode_Level" + i) == 0)
                {
                    excludedLevelList.Add(i);
                }
            }
            TestingIncludedLevelsList(excludedLevelList);
        }
    }

    public void toggleLevel(int level_num)
    {
        audioSource.Play();

        if (PlayerPrefs.GetInt("EndlessMode_Level" + level_num) == 0)
        {
            //I want to play this level
            print("Endless Mode: I want to play: " + level_num);
            PlayerPrefs.SetInt("EndlessMode_Level" + level_num, 1);
            level_X_marks[level_num - 1].SetActive(false);
        }
        else if (PlayerPrefs.GetInt("EndlessMode_Level" + level_num) == 1)
        {
            //Fack this level
            print("Endless Mode: Fuck this level: " + level_num);
            PlayerPrefs.SetInt("EndlessMode_Level" + level_num, 0);
            level_X_marks[level_num - 1].SetActive(true);
        }

        countNumOfIncludedLevels();
    }

    public void printLevelMarks()
    {
        for (int i = 1; i <= 7; i++)
        {
            print("Level " + i + ": " + PlayerPrefs.GetInt("EndlessMode_Level" + i));
        }
    }

    public void TestingIncludedLevelsList(List<int> exceptNums)
    {
        List<int> levelList = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
        foreach (int num in exceptNums)
        {
            levelList.Remove(num);
        }

        int resultIndex = Random.Range(0, levelList.Count);
        print("Result Level: " + levelList[resultIndex]);
    }

    public void countNumOfIncludedLevels()
    {
        numberOfIncludedLevels = 0;
        for (int i = 1; i <= 7; i++)
        {
            if (PlayerPrefs.GetInt("EndlessMode_Level" + i) == 1)
            {
                numberOfIncludedLevels++;
            }
        }
        print("numberOfIncludedLevels: " + numberOfIncludedLevels);
    }
}
