using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Dir_2 : AI_Dir_Generic
{
    [Header("Level 2")]

    /* Obstacles Index:
     * 0 - Plane
     */

    /* Enemy Index:
     * 0 - Chase Enemy
     * 1 - Shoot Enemy
     * 2 - Bullet Hell (BH) Enemy
     */

    //Others
    private bool spawn_2_Bullet_Hell_enemies;
    public float kiwiFruitLvl2_SpawnRate = 5; //Kiwi will spawn from the air every n seconds
    private float temp_kiwiFruitLvl2_SpawnRate;
    public float coinLvl2_spawnRate = 4;
    private float temp_coinLvl2_SpawnRate;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        temp_kiwiFruitLvl2_SpawnRate = kiwiFruitLvl2_SpawnRate;
        temp_coinLvl2_SpawnRate = coinLvl2_spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLevel != 2) return;

        //Spawning Winds
        SpawningWinds();

        //Spawning Kiwi Fruits
        if (kiwiFruitLvl2_SpawnRate <= 0)
        {
            GameObject kiwiPowerup = Instantiate(kiwiFruit,
                new Vector3(kiwiFruitSpawnLocation.position.x,
                kiwiFruitSpawnLocation.position.y + Random.Range(-5f, 5f),
                kiwiFruitSpawnLocation.position.z), Quaternion.identity);
            kiwiPowerup.GetComponent<Rigidbody2D>().gravityScale = 0;
            kiwiPowerup.GetComponent<AutoScrollSpeed>().hasCustomSpeed = true;
            kiwiFruitLvl2_SpawnRate = temp_kiwiFruitLvl2_SpawnRate + Random.Range(0f, 2.5f);
        }
        else
        {
            kiwiFruitLvl2_SpawnRate -= Time.deltaTime;
        }

        //Spawning Coins
        if (coinLvl2_spawnRate <= 0)
        {
            GameObject coinObj = Instantiate(coins,
                new Vector3(coinSpawnLocation.position.x,
                coinSpawnLocation.position.y + Random.Range(-6f, 6f),
                coinSpawnLocation.position.z), Quaternion.identity);
            coinObj.GetComponent<Rigidbody2D>().gravityScale = 0;
            coinObj.GetComponent<AutoScrollSpeed>().hasCustomSpeed = true;
            coinLvl2_spawnRate = temp_coinLvl2_SpawnRate + Random.Range(0f, 1.5f);
        }
        else
        {
            coinLvl2_spawnRate -= Time.deltaTime;
        }

        //Delay Time
        DelayTime();

        //Spawning Enemies
        if (!willMakeEnemies)
        {
            randomNum = Random.Range(0, 100f);
            /*
             * 1-35: Chase Enemy
             * 36-70: Shoot Enemy
             * 71-75: Chase Enemy Spawner 
             * 76-80: Shoot Enemy Spawner 
             * 81-95: Bullet Hell Enemy
             * 96-100: 2 Bullet Hell Enemy
             */
            if (randomNum >= 0f && randomNum <= 35)
            {
                randomEnemyID = 0; //Chase Enemy
                lvl_enemies_spawn_location_y_offset = Random.Range(-5f, 5f);
            }
            else if (randomNum >= 36 && randomNum <= 70)
            {
                randomEnemyID = 1; //Shoot Enemy
                lvl_enemies_spawn_location_y_offset = Random.Range(-4f, 4f);
            }
            else if (randomNum >= 71 && randomNum <= 75) // Chase Enemy Spawner
            {
                randomEnemyID = 2;
                //randomEnemyID = 3; 
                lvl_enemies_spawn_location_y_offset = 0;
            }
            else if (randomNum >= 75 && randomNum <= 80) // Shoot Enemy Spawner 
            {
                if (Random.Range(0, 2) == 0)
                {
                    randomEnemyID = 4;
                }
                else
                {
                    randomEnemyID = 5;
                }
                lvl_enemies_spawn_location_y_offset = 0;
            }
            else if (randomNum >= 81 && randomNum <= 95) //Bullet Hell Enemy
            {
                randomEnemyID = 6;
                lvl_enemies_spawn_location_y_offset = Random.Range(-1f, 1f);
            }
            else if (randomNum >= 96 && randomNum <= 100) //2 Bullet Hell Enemies
            {
                randomEnemyID = 6;
                spawn_2_Bullet_Hell_enemies = true;
            }

            randomEnemySpawnRate = Random.Range(lvl_enemies_min_spawn_rate, lvl_enemies_max_spawn_rate);
            willMakeEnemies = true;
        }
        else
        {
            randomEnemySpawnRate -= Time.deltaTime;
            if (randomEnemySpawnRate <= 0)
            {
                if (spawn_2_Bullet_Hell_enemies)
                {
                    GameObject madeEnemy = Instantiate(lvl_enemies[randomEnemyID],
                        new Vector3(lvl_enemies_spawn_location[randomEnemyID].position.x,
                        lvl_enemies_spawn_location[randomEnemyID].position.y + 5,
                        lvl_enemies_spawn_location[randomEnemyID].position.z), Quaternion.identity);
                    madeEnemy.GetComponent<BulletHellEnemy>().rotationSpeed = 45;
                    madeEnemy.GetComponent<BulletHellEnemy>().fireRate = 0.55f;

                    madeEnemy = Instantiate(lvl_enemies[randomEnemyID],
                        new Vector3(lvl_enemies_spawn_location[randomEnemyID].position.x,
                        lvl_enemies_spawn_location[randomEnemyID].position.y - 5,
                        lvl_enemies_spawn_location[randomEnemyID].position.z), Quaternion.identity);
                    madeEnemy.GetComponent<BulletHellEnemy>().rotationSpeed = -45;
                    madeEnemy.GetComponent<BulletHellEnemy>().fireRate = 0.55f;
                }
                else
                {
                    GameObject madeEnemy = Instantiate(lvl_enemies[randomEnemyID],
                        new Vector3(lvl_enemies_spawn_location[randomEnemyID].position.x,
                        lvl_enemies_spawn_location[randomEnemyID].position.y + lvl_enemies_spawn_location_y_offset,
                        lvl_enemies_spawn_location[randomEnemyID].position.z), Quaternion.identity);

                    if (randomEnemyID == 2)
                    {
                        //Both Chase Enemy Spawners spawn at once
                        madeEnemy = Instantiate(lvl_enemies[randomEnemyID + 1],
                            new Vector3(lvl_enemies_spawn_location[randomEnemyID + 1].position.x,
                            lvl_enemies_spawn_location[randomEnemyID + 1].position.y + lvl_enemies_spawn_location_y_offset,
                            lvl_enemies_spawn_location[randomEnemyID + 1].position.z), Quaternion.identity);
                    }
                }

                if (randomEnemyID >= 2 && randomEnemyID <= 6)
                {
                    willDelay = true;
                    if (spawn_2_Bullet_Hell_enemies)
                    {
                        delayTime = 7.5f;
                    }
                    else
                    {
                        delayTime = 6f;
                    }
                }
                spawn_2_Bullet_Hell_enemies = false;
                willMakeEnemies = false;
            }
        }

        //Spawning Obstacles
        if (!willMakeObstacle)
        {
            randomNum = Random.Range(0f, 100f);
            /*
             * 1-60: Plane
             */
            if (randomNum >= 0f && randomNum <= 60)
            {
                randomObstacleID = Random.Range(1, 5); //Clouds
            }
            else
            {
                randomObstacleID = 0; //Plane
                lvl_obstacles_spawn_location_y_offset = Random.Range(-2.5f, 0f);
            }
            randomObstacleSpawnRate = Random.Range(lvl_obstacles_min_spawn_rate, lvl_obstacles_max_spawn_rate);
            willMakeObstacle = true;
        }
        else
        {
            randomObstacleSpawnRate -= Time.deltaTime;
            if (randomObstacleSpawnRate <= 0)
            {
                Instantiate(lvl_obstacles[randomObstacleID],
                    new Vector3(lvl_obstacles_spawn_location[randomObstacleID].position.x,
                    lvl_obstacles_spawn_location[randomObstacleID].position.y + lvl_obstacles_spawn_location_y_offset,
                    lvl_obstacles_spawn_location[randomObstacleID].position.z), Quaternion.identity);
                willMakeObstacle = false;
            }
        }

        //Difficulty: DescreaseAllSpawnRates for enemy
        if (lvl_enemies_min_spawn_rate > 1f)
            lvl_enemies_min_spawn_rate -= Time.deltaTime / StaticBaseVars.difficultyScale;
        if (lvl_enemies_max_spawn_rate > 2f)
            lvl_enemies_max_spawn_rate -= Time.deltaTime / StaticBaseVars.difficultyScale;
    }
}
