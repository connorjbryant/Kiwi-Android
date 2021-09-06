using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_Dir_7 : AI_Dir_Generic
{
    [Header("Level 7")]

    /*Obstacle Index:
     * 0 - Bamboo Pillars
     */

    /*Enemy Index
     * 0 - Arc Projectile Throwing Enemy
     * 1 - Falling Enemy if you get close
     */

    public GameObject transparentBamboo; //This is for the arc throwing enemy to hang on

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLevel != 7) return;

        //Spawning Winds
        SpawningWinds();

        //Spawn the end goal pillars
        if ((Level_Transition.gameTime >= Level_Transition.staticLevelSwitchTime - 1)
            && !spawnedEndPlatform
            && isLastLevel)
        {
            Instantiate(endPlatform, lvl_obstacles_spawn_location[0].transform.position + new Vector3(-7.5f, 0), Quaternion.identity);
            spawnedEndPlatform = true;
        }
        if (spawnedEndPlatform)
            return;

        //Delay Time
        DelayTime();

        //Making Enemies
        if (!willMakeEnemies)
        {
            randomNum = Random.Range(0f, 100f);
            /*
             * 0-40: Arc Throwing Enemies
             * 41-70: Giraffe Enemy
             * 71-100: Panther Enemy on Tree
             */
            if (randomNum >= 0f && randomNum <= 40f)
            {
                randomEnemyID = 0; //Arc Throwing Enemies
                lvl_enemies_spawn_location_y_offset = Random.Range(-4.5f, 4.5f);
            }
            else if (randomNum >= 41f && randomNum <= 70f)
            {
                randomEnemyID = 1; //Giraffe Enemy
                lvl_enemies_spawn_location_y_offset = Random.Range(0f, 2.0f);
            }
            else
            {
                randomEnemyID = 2; //Panther Enemy on Tree
                lvl_enemies_spawn_location_y_offset = Random.Range(-1.5f, 2.0f);
            }
            randomEnemySpawnRate = Random.Range(lvl_enemies_min_spawn_rate, lvl_enemies_max_spawn_rate);
            willMakeEnemies = true;
        }
        else
        {
            randomEnemySpawnRate -= Time.deltaTime;
            if (randomEnemySpawnRate <= 0)
            {
                randomNum = Random.Range(0f, 100f);
                if (randomEnemyID == 0)
                {
                    //Create invisible Bamboo
                    Instantiate(transparentBamboo,
                        new Vector3(lvl_enemies_spawn_location[0].position.x, 0, 0), Quaternion.identity);

                    //Create Coins
                    GameObject coinObj;
                    for (int i = 0; i < 2; i++)
                    {
                        if (i == 0)
                        {
                            coinObj = Instantiate(coins,
                                new Vector3(lvl_enemies_spawn_location[0].position.x, Random.Range(-5.0f, -1.0f), 0), Quaternion.identity);
                        }
                        else
                        {
                            coinObj = Instantiate(coins,
                                new Vector3(lvl_enemies_spawn_location[0].position.x, Random.Range(1.0f, 5.0f), 0), Quaternion.identity);
                        }
                        coinObj.GetComponent<AutoScrollSpeed>().hasCustomSpeed = true;
                        coinObj.GetComponent<AutoScrollSpeed>().autoScrollSpeed = transparentBamboo.GetComponent<AutoScrollSpeed>().autoScrollSpeed;
                    }
                        

                    //Arc Throwing Enemy
                    if (randomNum >= 0f && randomNum <= 75f)
                    {
                        lvl_enemies_spawn_location_y_offset = Random.Range(-4.0f, 4.0f);
                        GameObject madeEnemy = Instantiate(lvl_enemies[randomEnemyID],
                            new Vector3(lvl_enemies_spawn_location[randomEnemyID].position.x,
                            lvl_enemies_spawn_location[randomEnemyID].position.y + lvl_enemies_spawn_location_y_offset,
                            lvl_enemies_spawn_location[randomEnemyID].position.z), Quaternion.identity);
                    }
                    else
                    {
                        //Double Arc Throwing Enemy Chance
                        lvl_enemies_spawn_location_y_offset = Random.Range(1.5f, 4.5f);
                        GameObject madeEnemy = Instantiate(lvl_enemies[randomEnemyID],
                            new Vector3(lvl_enemies_spawn_location[randomEnemyID].position.x,
                            lvl_enemies_spawn_location[randomEnemyID].position.y + lvl_enemies_spawn_location_y_offset,
                            lvl_enemies_spawn_location[randomEnemyID].position.z), Quaternion.identity);

                        lvl_enemies_spawn_location_y_offset = Random.Range(-4.5f, -1.5f);
                        madeEnemy = Instantiate(lvl_enemies[randomEnemyID],
                            new Vector3(lvl_enemies_spawn_location[randomEnemyID].position.x,
                            lvl_enemies_spawn_location[randomEnemyID].position.y + lvl_enemies_spawn_location_y_offset,
                            lvl_enemies_spawn_location[randomEnemyID].position.z), Quaternion.identity);
                    }
                }
                else if (randomEnemyID == 1)
                {
                    //Giraffe Enemy
                    GameObject madeEnemy = Instantiate(lvl_enemies[randomEnemyID],
                            new Vector3(lvl_enemies_spawn_location[randomEnemyID].position.x,
                            lvl_enemies_spawn_location[randomEnemyID].position.y + lvl_enemies_spawn_location_y_offset,
                            lvl_enemies_spawn_location[randomEnemyID].position.z), Quaternion.identity);
                }
                else
                {
                    //Panther Enemy on Tree
                    GameObject madeEnemy = Instantiate(lvl_enemies[randomEnemyID],
                            new Vector3(lvl_enemies_spawn_location[randomEnemyID].position.x,
                            lvl_enemies_spawn_location[randomEnemyID].position.y + lvl_enemies_spawn_location_y_offset,
                            lvl_enemies_spawn_location[randomEnemyID].position.z), Quaternion.identity);
                }
                willMakeEnemies = false;
            }
        }

        //Spawning Obstacles
        if (!willMakeObstacle)
        {
            randomNum = Random.Range(0f, 100f);
            /*
             * 0-100: Lamp Pillar
             */
            if (randomNum >= 0f && randomNum <= 100)
            {
                randomObstacleID = 0; //Lamp Pillar
                lvl_obstacles_spawn_location_y_offset = Random.Range(-7.5f, -5f);
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

                //Spawn Kiwi
                if (randomObstacleID == 0)
                {
                    randomNum = Random.Range(0f, 100f);
                    if (randomNum >= 0f && randomNum <= 100f)
                    {
                        Instantiate(kiwiFruit,
                            new Vector3(lvl_obstacles_spawn_location[randomObstacleID].position.x,
                            lvl_obstacles_spawn_location[randomObstacleID].position.y + lvl_obstacles_spawn_location_y_offset + 8f,
                            lvl_obstacles_spawn_location[randomObstacleID].position.z), Quaternion.identity);
                    }
                }

                willMakeObstacle = false;
            }
        }

        //Difficulty Increase
        if (lvl_enemies_min_spawn_rate >= 2f)
        {
            lvl_enemies_min_spawn_rate -= Time.deltaTime / StaticBaseVars.difficultyScale;
        }
        if (lvl_enemies_max_spawn_rate >= 3.5f)
        {
            lvl_enemies_max_spawn_rate -= Time.deltaTime / StaticBaseVars.difficultyScale;
        }
    }
}
