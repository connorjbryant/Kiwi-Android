using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Dir_4 : AI_Dir_Generic
{
    [Header("Level 4")]

    /* Obstacles Index:
     * 0 - Wooden Raft (Pillar substitution)
     * 1 - Buoy
     */

    /* Enemy Index:
     * 0 - Normal Fish
     * 1 - Jumping Fish
     * 2 - Seagull
     */

    public GameObject lvl4_Wave; //This is the parent of normal fish
    public float kiwiFruitLvl4_SpawnRate = 5; //Kiwi will spawn from the air every n seconds
    private float temp_kiwiFruitLvl4_SpawnRate;
    public float coinLvl4_spawnRate = 4;
    private float temp_coinLvl4_SpawnRate;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        
        lvl4_Wave = GameObject.Find("World").transform.Find("Lvl4_Wave").gameObject;
        temp_kiwiFruitLvl4_SpawnRate = kiwiFruitLvl4_SpawnRate;
        temp_coinLvl4_SpawnRate = coinLvl4_spawnRate;

        if (QualitySettings.GetQualityLevel() == 1 || QualitySettings.GetQualityLevel() == 2)
        {
            //If settings are low/med, disable the wave shader
            lvl4_Wave.transform.Find("WaterRender").Find("RenderCamera").gameObject.SetActive(false);
        }

        if (currentLevel != 4) return;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLevel != 4) return;

        //Spawning Winds
        if (!willMakeWind)
        {
            wind_spawn_location_y_offset = Random.Range(4f, 6f);
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

        //Spawning Kiwi Fruits
        if (kiwiFruitLvl4_SpawnRate <= 0)
        {
            GameObject kiwiPowerup = Instantiate(kiwiFruit,
                new Vector3(kiwiFruitSpawnLocation.position.x,
                kiwiFruitSpawnLocation.position.y + Random.Range(4f, 5f),
                kiwiFruitSpawnLocation.position.z), Quaternion.identity);
            kiwiPowerup.GetComponent<Rigidbody2D>().gravityScale = 0.75f;
            kiwiPowerup.GetComponent<AutoScrollSpeed>().hasCustomSpeed = true;
            kiwiFruitLvl4_SpawnRate = temp_kiwiFruitLvl4_SpawnRate + Random.Range(0f, 2.5f);
        }
        else
        {
            kiwiFruitLvl4_SpawnRate -= Time.deltaTime;
        }

        //Spawning Coins
        if (coinLvl4_spawnRate <= 0)
        {
            GameObject coinObj = Instantiate(coins,
                new Vector3(coinSpawnLocation.position.x,
                coinSpawnLocation.position.y + Random.Range(4f, 6f),
                coinSpawnLocation.position.z), Quaternion.identity);
            coinObj.GetComponent<Rigidbody2D>().gravityScale = 0;
            coinObj.GetComponent<AutoScrollSpeed>().hasCustomSpeed = true;
            coinLvl4_spawnRate = temp_coinLvl4_SpawnRate + Random.Range(-2f, 1.5f);
        }
        else
        {
            coinLvl4_spawnRate -= Time.deltaTime;
        }

        //Delay Time
        DelayTime();

        //Spawning Enemies
        if (!willMakeEnemies)
        {
            randomNum = Random.Range(0f, 100f);
            /*
             * 0-15: Normal Fish Enemy
             * 16-40: Jumping Fish Enemy
             * 71-100: Seagull
             */
            if (randomNum >= 0f && randomNum <= 15f)
            {
                randomEnemyID = 0; //Normal Fish Enemy
                lvl_enemies_spawn_location_y_offset = Random.Range(0, 0.5f);
            }
            else if (randomNum >= 16f && randomNum <= 40f)
            {
                randomEnemyID = 1; //Jumping Fish Enemy
                lvl_enemies_spawn_location_y_offset = Random.Range(0, 0.5f);
                willDelay = true;
                delayTime = 0.75f;
            }
            else
            {
                randomEnemyID = 2; //Seagull Enemy
                lvl_enemies_spawn_location_y_offset = Random.Range(-1.5f, 1.5f);
            }
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
                
                if (randomEnemyID == 0)
                {
                    madeEnemy.transform.SetParent(lvl4_Wave.transform);
                }

                willMakeEnemies = false;
            }
        }

        //Spawning Obstacles
        //Must spawn in a pillar within a certain time
        if (timeTillHaveToSpawnPillar > 0)
        {
            timeTillHaveToSpawnPillar -= Time.deltaTime;
        }
        else
        {
            haveToSpawnPillar = true;
        }
        //AI selecting what obstacle to spawn
        if (!willMakeObstacle)
        {
            if (haveToSpawnPillar)
            {
                randomObstacleID = 0;
                lvl_obstacles_spawn_location_y_offset = 0;
                timeTillHaveToSpawnPillar = tempTimeTillHaveToSpawnPillar;
                haveToSpawnPillar = false;
            }
            else
            {
                randomNum = Random.Range(0f, 100f);
                /*
                 * 0-10: Wooden Raft
                 * 11-100: Buoy 
                 */
                if (randomNum >= 0f && randomNum <= 20f)
                {
                    randomObstacleID = 0; //Wooden Raft
                    lvl_obstacles_spawn_location_y_offset = Random.Range(-0.25f, 0.25f);
                }
                else
                {
                    randomObstacleID = 1; //Buoy
                    lvl_obstacles_spawn_location_y_offset = Random.Range(-0.25f, 0.25f);
                }
            }
            randomObstacleSpawnRate = Random.Range(lvl_obstacles_min_spawn_rate, lvl_obstacles_max_spawn_rate);
            willMakeObstacle = true;
        }
        else
        {
            randomObstacleSpawnRate -= Time.deltaTime;
            if (randomObstacleSpawnRate <= 0)
            {
                GameObject madeObstacle = Instantiate(lvl_obstacles[randomObstacleID],
                    new Vector3(lvl_obstacles_spawn_location[randomObstacleID].position.x,
                    lvl_obstacles_spawn_location[randomObstacleID].position.y + lvl_obstacles_spawn_location_y_offset,
                    lvl_obstacles_spawn_location[randomObstacleID].position.z), Quaternion.identity);

                if (randomObstacleID == 0)
                {
                    madeObstacle.transform.SetParent(lvl4_Wave.transform);
                }

                else if (randomObstacleID == 1)
                {
                    //Delay if spawned buoy
                    willDelay = true;
                    delayTime = 3f;
                }

                willMakeObstacle = false;
            }
        }

        //Difficulty: Descrease Enemy SpawnRates
        if (lvl_enemies_min_spawn_rate > 2f)
            lvl_enemies_min_spawn_rate -= Time.deltaTime / StaticBaseVars.difficultyScale;
        if (lvl_enemies_min_spawn_rate > 3f)
            lvl_enemies_min_spawn_rate -= Time.deltaTime / StaticBaseVars.difficultyScale;
    }
}
