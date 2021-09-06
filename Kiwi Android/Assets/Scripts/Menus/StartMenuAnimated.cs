using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartMenuAnimated : MonoBehaviour
{
    public GameObject[] kiwiFruits;
    public Transform kiwiSpawnLocation;
    public float kiwiSpawnXOffset;
    public float kiwiSpawnRate;
    private float tempKiwiSpawnRate;

    public GameObject[] clouds;
    public Transform cloudsSpawnLocation;
    public float cloudsSpawnYOffset;
    public float cloudsSpawnRate;
    private float tempCloudsSpawnRate;

    public AudioSource introAudioSource;

    //MiniGame
    public bool StartMiniGame;
    public GameObject scoreUIObj;
    public TextMeshProUGUI scoreText;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        Time.timeScale = 1f;
        tempKiwiSpawnRate = kiwiSpawnRate;
        tempCloudsSpawnRate = cloudsSpawnRate;
        kiwiSpawnRate = 0.1f;
        cloudsSpawnRate = 0.1f;

        //Modifying the Intro Music
        introAudioSource.time = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        //Spawning the Kiwis
        kiwiSpawnRate -= Time.deltaTime;
        if (kiwiSpawnRate <= 0)
        {
            kiwiSpawnXOffset = Random.Range(-50, 50);
            Instantiate(kiwiFruits[Random.Range(0, kiwiFruits.Length)],
                new Vector2(kiwiSpawnLocation.position.x + kiwiSpawnXOffset,
                kiwiSpawnLocation.position.y), Quaternion.identity);
            kiwiSpawnRate = tempKiwiSpawnRate;
        }

        //Spawning the Clouds
        cloudsSpawnRate -= Time.deltaTime;
        if (cloudsSpawnRate <= 0)
        {
            cloudsSpawnYOffset = Random.Range(-10, 10);
            Instantiate(clouds[Random.Range(0, clouds.Length)],
                new Vector2(cloudsSpawnLocation.position.x,
                cloudsSpawnLocation.position.y + cloudsSpawnYOffset), Quaternion.identity);
            cloudsSpawnRate = tempCloudsSpawnRate;
        }

        //Minigame
        if (StartMiniGame)
        {
            scoreUIObj.SetActive(true);
            scoreText.text = score.ToString();
        }
    }
}
