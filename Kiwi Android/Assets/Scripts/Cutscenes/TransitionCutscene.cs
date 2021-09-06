using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionCutscene : MonoBehaviour
{
    public GameObject[] pictures;
    public float changeRate;
    private float tempChangeRate;
    private int pictures_index;
    private int numberOfPictures;
    private AsyncOperation _asyncOperation;

    public GameObject skipButton;

    private AudioSource audioSource;
    public AudioClip popSound;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        tempChangeRate = changeRate;
        changeRate = 0.1f;
        pictures_index = -1;
        numberOfPictures = pictures.Length;

        skipButton.SetActive(true);
        this._asyncOperation = SceneManager.LoadSceneAsync("StageSelection");
        this._asyncOperation.allowSceneActivation = false;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Cutscene Setup
        changeRate -= Time.deltaTime;
        if (pictures_index == -1 && changeRate <= 0)
        {
            pictures_index++;
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
        if (pictures_index >= numberOfPictures && changeRate <= -1.5f)
        {
            GoToSceneSelectionMenu();
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            GoToSceneSelectionMenu();
        }
    }

    public void GoToSceneSelectionMenu()
    {
        audioSource.clip = popSound;
        audioSource.Play();
        this._asyncOperation.allowSceneActivation = true;
    }
}
