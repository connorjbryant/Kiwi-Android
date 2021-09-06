using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicClass : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject[] other;
    private bool NotFirst = false;

    private void Awake()
    {
        other = GameObject.FindGameObjectsWithTag("Music");

        foreach (GameObject oneOther in other)
        {
            if (oneOther.scene.buildIndex == -1)
            {
                NotFirst = true;
            }
        }

        if (NotFirst == true)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (audioSource.isPlaying) return;
            audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
