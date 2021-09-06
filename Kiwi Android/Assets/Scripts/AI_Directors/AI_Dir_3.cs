using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Dir_3 : AI_Dir_Generic
{
    [Header("Level 3")]

    public float kiwiFruitLvl3_Spawnchance = 50f;
    float originalSpawnRate;

    /* Obstacles Index:
     * 0 - Pillar
     * 1 - Wave
     * 2 - Volcano
     */

    /* Obstacles Index:
     * 0 - Meteor
     */

    private bool willMakeMeteors;
    public GameObject[] lvl3_meteors;
    public Transform[] lvl3_meteors_spawn_locations;
    public Transform[] lvl3_meteors_aim_locations;
    public float meteorSpawnRate;
    private float tempMeteorSpawnRate;
    public float minMeteorSpawnRate;
    public float maxMeteorSpawnRate;
    public float meteorSpeed;
    private GameObject randomMeteor;
    private Transform spawnPoint;
    private Transform aimPoint;
    public GameObject bottomFireWave;
    public GameObject topFireWave;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        tempMeteorSpawnRate = meteorSpawnRate;
    }

    private void FixedUpdate()
    {
        if (Level_Transition.transitionTime > 0) return;
        if (currentLevel == 3)
        {
            bottomFireWave.SetActive(true);
            topFireWave.SetActive(true);
        }
        else
        {
            bottomFireWave.SetActive(false);
            topFireWave.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLevel != 3) return;

        //Spawn the end goal pillars
        if ((Level_Transition.gameTime >= Level_Transition.staticLevelSwitchTime - 1)
            && !spawnedEndPlatform
            && isLastLevel)
        {
            Instantiate(endPlatform, lvl_obstacles_spawn_location[0].transform.position + new Vector3(0, 0f), Quaternion.identity);
            spawnedEndPlatform = true;
        }
        if (spawnedEndPlatform)
            return;

        //Spawning Winds
        SpawningWinds();

        //Delay Time
        DelayTime();

        //Spawning Meteors
        if (!willMakeMeteors)
        {
            meteorSpawnRate = Random.Range(minMeteorSpawnRate, maxMeteorSpawnRate);
            willMakeMeteors = true;
        }
        else
        {
            meteorSpawnRate -= Time.deltaTime;
            if (meteorSpawnRate <= 0)
            {
                randomMeteor = lvl3_meteors[Random.Range(0, lvl3_meteors.Length)];
                spawnPoint = lvl3_meteors_spawn_locations[Random.Range(0, lvl3_meteors_spawn_locations.Length)];
                if (Random.Range(0, 4) == 0)
                {
                    aimPoint = player.transform;
                }
                else
                {
                    aimPoint = lvl3_meteors_aim_locations[Random.Range(0, lvl3_meteors_aim_locations.Length)];
                }
                GameObject madeMeteor = Instantiate(randomMeteor, spawnPoint.position, Quaternion.identity);
                madeMeteor.GetComponent<Rigidbody2D>().velocity = (aimPoint.position - spawnPoint.position).normalized * (meteorSpeed += Random.Range(0, 0.25f));
                willMakeMeteors = false;
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
            }
            else
            {
                randomNum = Random.Range(0f, 100);
                /*
                 * 1-5: Pillar
                 * 6-75: Fire Wave
                 * 76-100: Valcano
                 */
                if (randomNum >= 0f && randomNum <= 5)
                {
                    randomObstacleID = 0; //Pillar
                    lvl_obstacles_spawn_location_y_offset = Random.Range(-2.75f, 0);
                }
                else if (randomNum >= 5f && randomNum <= 30)
                {
                    randomObstacleID = 1; //Top Fire Wave Starter
                    lvl_obstacles_spawn_location_y_offset = 0;
                }
                else if (randomNum >= 31f && randomNum <= 75f)
                {
                    randomObstacleID = 2; //Bottom Fire Wave Starter
                    lvl_obstacles_spawn_location_y_offset = 0;
                }
                else
                {
                    randomObstacleID = 3; //Volcano 
                    lvl_obstacles_spawn_location_y_offset = Random.Range(-2.75f, 0);
                }
            }
            randomObstacleSpawnRate = Random.Range(lvl_obstacles_min_spawn_rate, lvl_obstacles_max_spawn_rate);
            willMakeObstacle = true;
        }
        else
        {
            if (randomObstacleSpawnRate < 0)
            {
                GameObject obstacleToSpawn = lvl_obstacles[randomObstacleID];

                GameObject madeObstacle = Instantiate(obstacleToSpawn,
                    new Vector3(lvl_obstacles_spawn_location[randomObstacleID].position.x,
                    lvl_obstacles_spawn_location[randomObstacleID].position.y + lvl_obstacles_spawn_location_y_offset,
                    lvl_obstacles_spawn_location[randomObstacleID].position.z), Quaternion.identity);

                //Spawn a kiwiFruit above the pillar
                if (randomObstacleID == 0)
                {
                    if (Random.Range(0, 100) <= kiwiFruitLvl3_Spawnchance)
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
                willMakeObstacle = false;
                haveToSpawnPillar = false;
                randomObstacleSpawnRate = 0;
            }
            else
            {
                randomObstacleSpawnRate -= Time.deltaTime;
            }

            //Difficulty: DescreaseAllSpawnRates for all
            if (minMeteorSpawnRate > 1.75f)
            {
                minMeteorSpawnRate -= Time.deltaTime / (StaticBaseVars.difficultyScale * 2);
            }
            if (maxMeteorSpawnRate > 2f)
            {
                maxMeteorSpawnRate -= Time.deltaTime / (StaticBaseVars.difficultyScale * 2);
            }

            //Difficulty: Descrease Obstacle SpawnRates
            if (lvl_obstacles_min_spawn_rate > 1.75f)
                lvl_obstacles_min_spawn_rate -= Time.deltaTime / StaticBaseVars.difficultyScale;
            if (lvl_obstacles_max_spawn_rate > 3f)
                lvl_obstacles_max_spawn_rate -= Time.deltaTime / StaticBaseVars.difficultyScale;
        }
    }
}
