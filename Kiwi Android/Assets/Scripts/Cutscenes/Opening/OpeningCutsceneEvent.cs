using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningCutsceneEvent : MonoBehaviour
{
    public GameObject[] pictures;
    public GameObject Scene_3_2;
    public float changeRate;
    private float tempChangeRate;
    private int pictures_index;
    private int numberOfPictures;
    public bool changedImageFiveTime;
    private AsyncOperation _asyncOperation;

    public GameObject skipButton;
    public GameObject loadingScreen;
    public bool doneSkipping;
    public float loadTime = 1f;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //loadTime = 1f;
        doneSkipping = false;
        tempChangeRate = changeRate;
        changeRate = 0.1f;
        pictures_index = -1;
        numberOfPictures = pictures.Length;
        changedImageFiveTime = false;

        skipButton.SetActive(false);
        loadingScreen.SetActive(true);
        this._asyncOperation = SceneManager.LoadSceneAsync("StartMenu");
        this._asyncOperation.allowSceneActivation = false;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Loading 
        if (!doneSkipping)
        {
            if (loadTime <= 0)
            {
                loadingScreen.SetActive(false);
                skipButton.SetActive(true);
                doneSkipping = true;
            }
            else
            {
                loadTime -= Time.deltaTime;
            }
        }

        //Cutscene Setup
        changeRate -= Time.deltaTime;
        if (pictures_index == -1 && changeRate <= 0)
        {
            pictures_index++;
        }

        if (pictures_index == 3)
        {
            Scene_3_2.SetActive(true);
        }
        else
        {
            Scene_3_2.SetActive(false);
        }

        if (pictures_index == 5 && !changedImageFiveTime)
        {
            changeRate = 1f;
            changedImageFiveTime = true;
        }

        if (changeRate <= 0 && pictures_index < numberOfPictures && pictures_index >= 0)
        {
            if (pictures_index == 0)
            {
                pictures[pictures_index].SetActive(true);
            }
            else
            {
                pictures[pictures_index - 1].SetActive(false);
                pictures[pictures_index].SetActive(true);
            }
            changeRate = tempChangeRate;
            pictures_index++;
        }
        //Move to start
        if (pictures_index >= numberOfPictures && changeRate <= 0)
        {
            GoToStartMenu();
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            GoToStartMenu();
        }
    }

    public void GoToStartMenu()
    {
        audioSource.Play();
        this._asyncOperation.allowSceneActivation = true;
    }
}
