using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Dir_5 : AI_Dir_Generic
{
    [Header("Level 5")]

    //Light Rays 
    public GameObject lightRay;
    public Transform lightRaySpawnLocation;
    private float rayAngle;
    public float minRayAngle = -60f;
    public float maxRayAngle = 60f;
    private float raySpawnRate;
    public float minRaySpawnRate = 2.5f;
    public float maxRaySpawnRate = 4f;

    /* Obstacles Index:
     * 0 - Water Pillars
     */

    /* Enemy Index:
     * 0 - T. Devil
     */

    //Others 
    public float coinLvl6_spawnRate = 4;
    private float temp_coinLvl6_SpawnRate;
    private bool start;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        start = false;
        raySpawnRate = minRaySpawnRate;
        temp_coinLvl6_SpawnRate = coinLvl6_spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLevel != 5) return;
        if (!start)
        {
            willMakeObstacle = true;
            randomObstacleSpawnRate = 0.01f;
            lvl_obstacles_spawn_location_y_offset = Random.Range(-2f, -1f);
            start = true;
        }

        //Spawning Winds
        SpawningWinds();

        //Spawning Coins
        

        //Delay Time
        DelayTime();

        //Spawning Light
        if (raySpawnRate <= 0)
        {
            GameObject lightRayObj = Instantiate(lightRay, lightRaySpawnLocation.position, Quaternion.identity);

            randomNum = Random.Range(0, 2);
            if (randomNum == 0)
                rayAngle = Random.Range(minRayAngle, minRayAngle / 2);
            else
                rayAngle = Random.Range(maxRayAngle / 2, maxRayAngle);

            lightRayObj.transform.eulerAngles = new Vector3(0, 0, rayAngle);
            raySpawnRate = Random.Range(minRaySpawnRate, maxRaySpawnRate);
        }
        else
        {
            raySpawnRate -= Time.deltaTime;
        }

        //Spawning Obstacles
        if (!willMakeObstacle)
        {
            randomNum = Random.Range(0f, 100f);
            /*
             * 0-15: Water Pillar to remove heat
             * 16-100: Regular Pillar
             */
            if (randomNum >= 0f && randomNum <= 15)
            {
                randomObstacleID = 0; //Water Pillar
                lvl_obstacles_spawn_location_y_offset = Random.Range(-2.5f, -1.5f);
            }
            else
            {
                randomObstacleID = 1; //Normal Pillar
                lvl_obstacles_spawn_location_y_offset = Random.Range(-2.5f, -1.5f);
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

                if (randomObstacleID == 1)
                {
                    randomNum = Random.Range(0f, 100f);
                    if (randomNum > 0 && randomNum < 35)
                    {
                        //Spawn T. Devil with 35 percent chance
                        Instantiate(lvl_enemies[0],
                                madeObstacle.transform.position + new Vector3(0, 8f, 0), Quaternion.identity);
                    }

                    //Spawn Coin
                    Instantiate(coins,
                            madeObstacle.transform.position + new Vector3(Random.Range(-3f, 3f), 7f, 0), Quaternion.identity);
                }
                else
                {
                    //Spawn Kiwi Fruit
                    Instantiate(kiwiFruit,
                        madeObstacle.transform.position + new Vector3(0, 5f, 0), Quaternion.identity) ;
                }
                willMakeObstacle = false;
            }
        }

        //Difficulty: Descrease Ray SpawnRates
        if (minRaySpawnRate > 2.5f)
            minRaySpawnRate -= Time.deltaTime / StaticBaseVars.difficultyScale;
        if (maxRaySpawnRate > 4f)
            maxRaySpawnRate -= Time.deltaTime / StaticBaseVars.difficultyScale;
    }
}
