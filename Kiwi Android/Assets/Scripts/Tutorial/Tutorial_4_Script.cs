using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_4_Script : MonoBehaviour
{
    [Header("General Variables for Enemies")]
    public static bool willMakeEnemies;
    public static float randomEnemySpawnRate;
    [HideInInspector] public int randomEnemyID;

    public GameObject[] lvl_enemies;
    public Transform[] lvl_enemies_spawn_location;
    [HideInInspector] public float lvl_enemies_spawn_location_y_offset;
    public float lvl_enemies_min_spawn_rate;
    public float lvl_enemies_max_spawn_rate;

    [Header("General Variables for Spawn Kiwi Fruit")]
    public GameObject kiwiFruit;
    public Transform kiwiFruitSpawnLocation;
    public static bool willMakeItem;

    public float kiwiFruit_SpawnRate = 5;
    private float temp_KiwiFruit_SpawnRate;

    // Start is called before the first frame update
    void Start()
    {
        temp_KiwiFruit_SpawnRate = kiwiFruit_SpawnRate;
        kiwiFruit_SpawnRate = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Spawning Enemies
        if (!willMakeEnemies)
        {
            randomEnemyID = Random.Range(0, lvl_enemies.Length); //Hover Enemy
            lvl_enemies_spawn_location_y_offset = Random.Range(-10f, 10f);
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

        //Spawning Kiwi Fruit
        if (kiwiFruit_SpawnRate <= 0)
        {
            GameObject kiwiPowerup = Instantiate(kiwiFruit,
                new Vector3(kiwiFruitSpawnLocation.position.x,
                kiwiFruitSpawnLocation.position.y + Random.Range(-5f, 5f),
                kiwiFruitSpawnLocation.position.z), Quaternion.identity);
            kiwiPowerup.GetComponent<Rigidbody2D>().gravityScale = 0;
            kiwiPowerup.GetComponent<AutoScrollSpeed>().hasCustomSpeed = true;
            kiwiPowerup.GetComponent<KiwiPowerupMove>().isInTutorial = true;
            kiwiFruit_SpawnRate = temp_KiwiFruit_SpawnRate + Random.Range(0f, 2.5f);
        }
        else
        {
            kiwiFruit_SpawnRate -= Time.deltaTime;
        }
    }
}
