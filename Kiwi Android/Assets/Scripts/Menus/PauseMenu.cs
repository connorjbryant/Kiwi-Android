using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public LeveLoader levelLoader;
    public PlayerMove move;

    public static bool GamePaused = false;
    public GameObject pauseMenu;
    public GameObject pauseButton;

    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip generalSound;
    public AudioClip quitSound;
    public AudioClip tickSound;
    private bool playTickOnce;

    //Code for the three second timer after pausing the game
    [Header("Unpausing the Game will have a 3-sec timer")]
    public GameObject UnPauseTextObj;
    private TextMeshProUGUI timerText;
    public bool unPausedGame;
    public float unPausedTimer;
    private float tempUnPausedTimer;

    private void Start()
    {
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LeveLoader>();
        audioSource = GetComponent<AudioSource>();
        timerText = UnPauseTextObj.GetComponent<TextMeshProUGUI>();
        tempUnPausedTimer = unPausedTimer;
        playTickOnce = false;
        move = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        pauseButton = GameObject.Find("Canvas").transform.Find("PauseButton").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (move.gotGameOver)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                //Will resume the game - Need 3 second timer
                UnPauseGame();
            }
            else if (!GamePaused && !unPausedGame)
            {
                //Will pause the game
                PauseGame();
            }
        }

        if (unPausedGame)
        {
            pauseButton.SetActive(false);
            Time.timeScale = 0.00001f;
            unPausedTimer -= Time.deltaTime * 100000;
            timerText.text = ((int)unPausedTimer + 1).ToString();

            if (!playTickOnce)
            {
                audioSource.clip = tickSound;
                audioSource.Play();
                playTickOnce = true;
            }
            
            if (unPausedTimer <= 0)
            {
                pauseButton.SetActive(true);
                ResumeGame();
                UnPauseTextObj.SetActive(false);
                unPausedGame = false;
            }
        }
    }

    public void UnPauseGame()
    {
        playTickOnce = false;
        unPausedTimer = tempUnPausedTimer;
        pauseMenu.SetActive(false);
        UnPauseTextObj.SetActive(true);
        unPausedGame = true;
    }

    public void ResumeGame()
    {
        if (move.gotGameOver)
            return;

        Time.timeScale = 1f;
        audioSource.clip = generalSound;
        audioSource.Play();
        pauseMenu.SetActive(false);
        GamePaused = false;
    }

    public void PauseGame()
    {
        audioSource.clip = generalSound;
        audioSource.Play();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void RestartGame()
    {
        move.adDone = true;
        move.isGameOver = false;
        move.gotGameOver = false;
        Time.timeScale = 1f;
        audioSource.clip = generalSound;
        audioSource.Play();
        GamePaused = false;

        levelLoader.LoadNextLevel(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        move.adDone = true;
        move.isGameOver = false;
        move.gotGameOver = false;
        Time.timeScale = 1f;
        audioSource.clip = quitSound;
        audioSource.Play();
        GamePaused = false;

        levelLoader.LoadNextLevel("StartMenu");
    }

    public void GoToTutorialSelectionScene()
    {
        move.isGameOver = false;
        move.gotGameOver = false;
        Time.timeScale = 1f;
        audioSource.clip = quitSound;
        audioSource.Play();
        GamePaused = false;
        levelLoader.LoadNextLevel("TutorialPreview");
    }

    public void GoToStore()
    {
        move.adDone = true;
        move.isGameOver = false;
        move.gotGameOver = false;
        Time.timeScale = 1f;
        audioSource.clip = generalSound;
        audioSource.Play();
        GamePaused = false;

        levelLoader.LoadNextLevel("Store");

        /*
        if (move.isGameOver)
            SceneManager.LoadScene("Store");
        else
            levelLoader.LoadNextLevel("Store");
        */
    }
}
