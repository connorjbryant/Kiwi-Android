using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Dir_6 : AI_Dir_Generic
{
    [Header("Level 6")]

    /* Obstacles Index:
     * 0 - Tall non-lethal trees with lethal hanging vines
     */

    /* Enemy Index:
     * 0 - Charging Land Rhinos
     * 1 - Jumping Kangaroos 
     * 2 - Slow moving tall Giraffes
     */

    public GameObject ground;
    public float lvl6_ground_spawnRate = 1f;
    public float temp_lvl6_ground_spawnRate;
    public Transform groundSpawnLocation;

    public float coinLvl6_spawnRate = 4;
    private float temp_coinLvl6_SpawnRate;

    [Header("Level 6 Tree Components")]
    public GameObject vines;
    public GameObject koala;
    private int numOfVines = 1;
    private Transform vineSpawnPoint;
    private float vineSpawnOffset = -2.5f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        temp_coinLvl6_SpawnRate = coinLvl6_spawnRate;
        temp_lvl6_ground_spawnRate = lvl6_ground_spawnRate;
        lvl6_ground_spawnRate = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLevel != 6) return;

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

        //Spawning Winds
        SpawningWinds(-2.0f, 6.0f);

        //Spawning the Ground Platforms
        lvl6_ground_spawnRate -= Time.deltaTime;
        if (lvl6_ground_spawnRate <= 0)
        {
            GameObject groundObj = Instantiate(ground, groundSpawnLocation.position, Quaternion.identity);
            lvl6_ground_spawnRate = temp_lvl6_ground_spawnRate;
        }

        //Delay Time
        DelayTime();

        //Making Enemies
        if (!willMakeEnemies)
        {
            randomNum = Random.Range(0f, 100f);
            /*
             * 0-30: Rhinos
             * 31-70: Kangaroos
             * 71-100: Giraffes
             */
            if (randomNum >= 0f && randomNum <= 30f)
            {
                randomEnemyID = 0; //Charging Rhinos Enemy
                lvl_enemies_spawn_location_y_offset = Random.Range(0, 0.5f);
            }
            else if (randomNum >= 31f && randomNum <= 70f)
            {
                randomEnemyID = 1; //Jumping Kangaroo Enemy
                lvl_enemies_spawn_location_y_offset = -4.0f;
                willDelay = true;
                delayTime = 0.75f;
            }
            else
            {
                randomEnemyID = 2; //Tall Giraffes
                lvl_enemies_spawn_location_y_offset = Random.Range(0, 0.5f);
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

        //Making Obstacles
        if (!willMakeObstacle)
        {
            randomNum = Random.Range(0f, 100f);
            /*
             * 0-100: Trees with Vines 
             */
            if (randomNum >= 0f && randomNum <= 100)
            {
                randomObstacleID = 0; //Tree with Vines
                lvl_obstacles_spawn_location_y_offset = Random.Range(-1f, 0f);
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
                    //Spawned a tree, need to make some mods

                    vineSpawnPoint = madeObstacle.transform.Find("Leaves_Sprite").Find("VinesSpawnPoint").transform;
                    numOfVines = Random.Range(1, 4);
                    for (int i = 0; i < numOfVines; i++)
                    {
                        vineSpawnOffset = Random.Range(-2.5f, 0.25f);
                        Vector2 vineSpawn = vineSpawnPoint.transform.position + new Vector3(Random.Range(-4.0f, 4.0f), vineSpawnOffset);
                        GameObject vineObj = Instantiate(vines, vineSpawn, Quaternion.identity);
                        vineObj.transform.SetParent(madeObstacle.transform);

                        //75% for a Koala; 25% for a kiwi fruit
                        randomNum = Random.Range(0, 100);
                        if (randomNum < 60)
                        {
                            GameObject koalaObj = Instantiate(koala, vineObj.transform.position + new Vector3(0.25f, Random.Range(-1.0f, 1.0f)),
                                Quaternion.identity);
                            koalaObj.transform.SetParent(madeObstacle.transform);
                        }
                        else
                        {
                            GameObject kiwiFruitObj = Instantiate(kiwiFruit, vineObj.transform.Find("Bottom").transform.position, Quaternion.identity);
                            kiwiFruitObj.GetComponent<Rigidbody2D>().gravityScale = 0;
                            kiwiFruitObj.GetComponent<AutoScrollSpeed>().enabled = false;
                            kiwiFruitObj.transform.SetParent(madeObstacle.transform);
                        }
                    }
                }

                willMakeObstacle = false;
            }
        }

        //Difficulty: Descrease Enemy SpawnRates
        if (lvl_enemies_min_spawn_rate > 2.5f)
            lvl_enemies_min_spawn_rate -= Time.deltaTime / StaticBaseVars.difficultyScale;
        if (lvl_enemies_min_spawn_rate > 3.5f)
            lvl_enemies_min_spawn_rate -= Time.deltaTime / StaticBaseVars.difficultyScale;
    }
}
