using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play_Stop_Music : MonoBehaviour
{
    public bool willStopMusic;
    private GameObject musicPlayer;

    // Start is called before the first frame update
    void Start()
    {
        musicPlayer = GameObject.FindGameObjectWithTag("Music");

        if (musicPlayer == null)
            return;

        if (willStopMusic)
            musicPlayer.GetComponent<MusicClass>().StopMusic();
        else
            musicPlayer.GetComponent<MusicClass>().PlayMusic();
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == SceneManager.GetSceneByName("StartMenu").buildIndex ||
            level == SceneManager.GetSceneByName("StageSelection").buildIndex)
        {
            GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>().PlayMusic();
        }
    }
}
