using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Dir_1 : AI_Dir_Generic
{
    [Header("Level 1")]

    /* Obstacles Index:
     * 0 - Pillar
     * 1 - Top Vine
     * 2 - Bottom Stump 2
     * 3 - Top Vine and Bottom Stump
     */

    /* Index:
     * 0 - Hover Enemy 
     */

    //Others
    public GameObject[] extra_lvl1_pillars;
    public float kiwiFruitLvl1_Spawnchance = 75; //Kiwi spawning on pillar

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLevel != 1) return;

        //Spawning Winds
        SpawningWinds();

        //Delay Time
        DelayTime();

        //Spawning Enemies
        if (!willMakeEnemies)
        {
            randomNum = Random.Range(0f, 100f);
            /*
             * 1-100: Hover Enemy
             */
            if (randomNum >= 0f && randomNum <= 100f)
            {
                randomEnemyID = 0; //Hover Enemy
                lvl_enemies_spawn_location_y_offset = Random.Range(-12f, 12f);
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
                lvl_obstacles_spawn_location_y_offset = Random.Range(-2.75f, 2.75f);
                timeTillHaveToSpawnPillar = tempTimeTillHaveToSpawnPillar;
                haveToSpawnPillar = false;
            }
            else
            {
                randomNum = Random.Range(0f, 100f);
                /*
                 * 1-10: Pillar
                 * 11-40: Vine
                 * 41-70: Tree Stump
                 * 71-100: Vine and tree
                 */
                if (randomNum >= 0f && randomNum <= 10)
                {
                    randomObstacleID = 0; //Pillar
                    lvl_obstacles_spawn_location_y_offset = Random.Range(-2.75f, 0.25f);
                }
                else if (randomNum >= 11f && randomNum <= 40)
                {
                    randomObstacleID = 1; //Top Vine
                    lvl_obstacles_spawn_location_y_offset = Random.Range(4.75f, 6f);
                }
                else if (randomNum >= 41f && randomNum <= 70)
                {
                    randomObstacleID = 2; //Bottom Tree Stump
                    lvl_obstacles_spawn_location_y_offset = Random.Range(-6.5f, -4.5f);
                }
                else
                {
                    randomObstacleID = 3; //Double Vertical Vine
                    lvl_obstacles_spawn_location_y_offset = Random.Range(-2.75f, 2.75f);
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
                int randomPillar = Random.Range(0, 3);
                GameObject obstacleToSpawn;
                if (randomObstacleID == 0)
                {
                    if (randomPillar == 0)
                    {
                        obstacleToSpawn = extra_lvl1_pillars[0];
                    }
                    else if (randomPillar == 1)
                    {
                        obstacleToSpawn = extra_lvl1_pillars[1];
                    }
                    else
                    {
                        obstacleToSpawn = lvl_obstacles[randomObstacleID];
                    }
                }
                else
                {
                    obstacleToSpawn = lvl_obstacles[randomObstacleID];
                }

                GameObject madeObstacle = Instantiate(obstacleToSpawn,
                    new Vector3(lvl_obstacles_spawn_location[randomObstacleID].position.x,
                    lvl_obstacles_spawn_location[randomObstacleID].position.y + lvl_obstacles_spawn_location_y_offset,
                    lvl_obstacles_spawn_location[randomObstacleID].position.z), Quaternion.identity);

                if (Random.Range(0, 2) == 0) { spawnCoin = true; }

                //Spawning Items
                if (randomObstacleID == 0)
                {
                    //Spawn a kiwiFruit above the pillar
                    if (Random.Range(0, 100) <= kiwiFruitLvl1_Spawnchance)
                    {
                        Instantiate(kiwiFruit, new Vector3(madeObstacle.transform.position.x,
                            madeObstacle.transform.position.y + 10f,
                            madeObstacle.transform.position.z), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(coins, new Vector3(madeObstacle.transform.position.x,
                            madeObstacle.transform.position.y + 6f,
                            madeObstacle.transform.position.z), Quaternion.identity);
                    }
                }
                else if (randomObstacleID == 1)
                {
                    if (spawnCoin)
                        Instantiate(coins, new Vector3(madeObstacle.transform.position.x,
                            madeObstacle.transform.position.y - 7.5f,
                            madeObstacle.transform.position.z), Quaternion.identity);
                }
                else if (randomObstacleID == 2)
                {
                    if (spawnCoin)
                        Instantiate(coins, new Vector3(madeObstacle.transform.position.x,
                            madeObstacle.transform.position.y + 8f,
                            madeObstacle.transform.position.z), Quaternion.identity);
                }
                else if (randomObstacleID == 3)
                {
                    //Double Obstacle
                    GameObject topVine = madeObstacle.transform.GetChild(0).gameObject;
                    topVine.transform.position = new Vector3(topVine.transform.position.x,
                    topVine.transform.position.y + Random.Range(-1f, 1f),
                    topVine.transform.position.z);
                    GameObject bottomVine = madeObstacle.transform.GetChild(1).gameObject;
                    bottomVine.transform.position = new Vector3(bottomVine.transform.position.x,
                    bottomVine.transform.position.y + Random.Range(-1f, 1f),
                    bottomVine.transform.position.z);

                    if (spawnCoin)
                        Instantiate(coins, new Vector3(topVine.transform.position.x,
                            (topVine.transform.position.y - 9f),
                            topVine.transform.position.z), Quaternion.identity);
                }
                willMakeObstacle = false;
                spawnCoin = false;
            }
        }

        //Difficulty: DescreaseAllSpawnRates
        if (lvl_obstacles_min_spawn_rate > 1.35f)
            lvl_obstacles_min_spawn_rate -= Time.deltaTime / StaticBaseVars.difficultyScale;
        if (lvl_obstacles_max_spawn_rate > 1.75f)
            lvl_obstacles_max_spawn_rate -= Time.deltaTime / StaticBaseVars.difficultyScale;
    }
}
