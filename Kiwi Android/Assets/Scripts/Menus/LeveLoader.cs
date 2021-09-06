using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeveLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTIme = 1f;

    public bool WillResumeTimeAfterTransition = false;
    private float time = 0;
    private GameObject player;
    private PlayerMove move;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        player = GameObject.FindGameObjectWithTag("Player");
        if  (player != null)
        {
            move = player.GetComponent<PlayerMove>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (WillResumeTimeAfterTransition)
        {
            Time.timeScale = 1f;
            time += Time.deltaTime;
            if (time > transitionTIme)
            {
                Time.timeScale = 0f;
                WillResumeTimeAfterTransition = false;
            }
        }
    }

    public void LoadNextLevel(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    IEnumerator LoadLevel(string sceneName)
    {
        //Play Anime
        Time.timeScale = 1f;
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSecondsRealtime(transitionTIme);

        //Load Scene
        SceneManager.LoadScene(sceneName);
    }
}
