using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Dir_Generic : MonoBehaviour
{
    // This AI_Director script will be the parent for each AI_Director script for each level

    [HideInInspector] public GameObject player;
    [HideInInspector] public PlayerMove kiwiMove;

    [Header("Testing Levels")]
    public bool isTestingLevel = false;
    public int SetLevel = 0;

    [Header("Generic - Applies to all or most levels")]
    public bool spawnedEndPlatform = false;
    public bool isLastLevel = false;
    public GameObject endPlatform; //If not in endless mode, this marks the end of a stage.
    public static int currentLevel = 1;
    public static int tempCurrentLevel;
    [HideInInspector] public bool willDelay;
    [HideInInspector] public float delayTime;
    [HideInInspector] public float randomNum;
    
    [Header("General Variables for Wind")]
    public GameObject wind;
    [HideInInspector] public bool willMakeWind;
    [HideInInspector] public Transform wind_spawn_location;
    [HideInInspector] public float wind_spawn_location_y_offset;
    public float min_wind_spawn_rate = 5f; //Wind Spawning Min time
    public float max_wind_spawn_rate = 8f; //Wind Spawning Max time
    [HideInInspector] public float randomWindSpawnRate;

    [Header("General Variables for powerUps/Items")]
    public GameObject kiwiFruit;
    public Transform kiwiFruitSpawnLocation;
    public static bool willMakeItem;
    
    public GameObject coins;
    [HideInInspector] public bool spawnCoin = false;
    [HideInInspector] public Transform coinSpawnLocation;

    [Header("General Variables for obstacles")]
    public float timeTillHaveToSpawnPillar = 4f;
    public static bool willMakeObstacle;
    public static float randomObstacleSpawnRate;
    [HideInInspector] public int randomObstacleID;
    [HideInInspector] public bool haveToSpawnPillar;
    [HideInInspector] public float tempTimeTillHaveToSpawnPillar;

    public GameObject[] lvl_obstacles;
    public Transform[] lvl_obstacles_spawn_location;
    [HideInInspector] public float lvl_obstacles_spawn_location_y_offset;
    public float lvl_obstacles_min_spawn_rate;
    public float lvl_obstacles_max_spawn_rate;

    [Header("General Variables for Enemies")]
    public static bool willMakeEnemies;
    public static float randomEnemySpawnRate;
    [HideInInspector] public int randomEnemyID;

    public GameObject[] lvl_enemies;
    public Transform[] lvl_enemies_spawn_location;
    [HideInInspector] public float lvl_enemies_spawn_location_y_offset;
    public float lvl_enemies_min_spawn_rate;
    public float lvl_enemies_max_spawn_rate;


    protected virtual void Start()
    {
        if (isTestingLevel)
        {
            currentLevel = SetLevel;
        }

        Time.timeScale = 1f;
        player = GameObject.FindGameObjectWithTag("Player");
        kiwiMove = player.GetComponent<PlayerMove>();
        wind_spawn_location = transform.Find("WindSpawnLocation").transform;
        coinSpawnLocation = transform.Find("CoinSpawnLocation").transform;
        willMakeObstacle = false;
        willMakeEnemies = false;
        willMakeItem = false;
        tempTimeTillHaveToSpawnPillar = timeTillHaveToSpawnPillar;
        randomNum = 0f;
    }

    public void assignStart()
    {
        
    }

    public void SpawningWinds()
    {
        if (!willMakeWind)
        {
            wind_spawn_location_y_offset = Random.Range(-6f, 6f);
            randomWindSpawnRate = Random.Range(min_wind_spawn_rate, max_wind_spawn_rate);
            willMakeWind = true;
        }
        else
        {
            randomWindSpawnRate -= Time.deltaTime;
            if (randomWindSpawnRate <= 0)
            {
                Instantiate(wind,
                    new Vector3(wind_spawn_location.position.x,
                    wind_spawn_location.position.y + wind_spawn_location_y_offset,
                    wind_spawn_location.transform.position.z), Quaternion.identity);
                willMakeWind = false;
            }
        }
    }

    public void SpawningWinds(float minY, float maxY)
    {
        if (!willMakeWind)
        {
            wind_spawn_location_y_offset = Random.Range(minY, maxY);
            randomWindSpawnRate = Random.Range(min_wind_spawn_rate, max_wind_spawn_rate);
            willMakeWind = true;
        }
        else
        {
            randomWindSpawnRate -= Time.deltaTime;
            if (randomWindSpawnRate <= 0)
            {
                Instantiate(wind,
                    new Vector3(wind_spawn_location.position.x,
                    wind_spawn_location.position.y + wind_spawn_location_y_offset,
                    wind_spawn_location.transform.position.z), Quaternion.identity);
                willMakeWind = false;
            }
        }
    }

    public void DelayTime()
    {
        if (willDelay)
        {
            delayTime -= Time.deltaTime;
            if (delayTime < 0) { willDelay = false; }
            return;
        }
    }
}
