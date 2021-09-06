using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_3_Script : MonoBehaviour
{
    [Header("General Variables for obstacles")]
    public float timeTillHaveToSpawnPillar = 4f;
    public static bool willMakeObstacle;
    public static float randomObstacleSpawnRate;
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

    private int randomNum;

    // Start is called before the first frame update
    void Start()
    {
        willMakeObstacle = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Spawning Enemies
        if (!willMakeEnemies)
        {
            randomEnemyID = 0; //Hover Enemy
            lvl_enemies_spawn_location_y_offset = Random.Range(-12f, 12f);
            randomEnemySpawnRate = Random.Range(lvl_enemies_min_spawn_rate, lvl_enemies_max_spawn_rate);
            willMakeEnemies = true;
        }
        else
        {
            randomEnemySpawnRate -= Time.deltaTime;
            if (randomEnemySpawnRate <= 0)
            {
                GameObject madeEnemy = Instantiate(lvl_enemies[randomEnemyID],
                    new Vector3(lvl_enemies_spawn_location[randomEnemyID].position.x,
                    lvl_enemies_spawn_location[randomEnemyID].position.y + lvl_enemies_spawn_location_y_offset,
                    lvl_enemies_spawn_location[randomEnemyID].position.z), Quaternion.identity);
                willMakeEnemies = false;
            }
        }

        //AI selecting what obstacle to spawn
        if (!willMakeObstacle)
        {
            randomObstacleSpawnRate = Random.Range(lvl_obstacles_min_spawn_rate, lvl_obstacles_max_spawn_rate);
            willMakeObstacle = true;
        }
        else
        {
            randomObstacleSpawnRate -= Time.deltaTime;
            if (randomObstacleSpawnRate <= 0)
            {
                randomNum = Random.Range(0, 2);

                GameObject madeObstacle = Instantiate(lvl_obstacles[randomNum],
                    new Vector3(lvl_obstacles_spawn_location[randomNum].position.x,
                    lvl_obstacles_spawn_location[randomNum].position.y,
                    lvl_obstacles_spawn_location[randomNum].position.z), Quaternion.identity);

                willMakeObstacle = false;
            }
        }
    }
}
