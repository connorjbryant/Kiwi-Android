using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Director : MonoBehaviour
{
    [Header("Level Transitions")]
    public int stageNum = 0;
    public bool willChangeLevel;

    private PlayerMove kiwiMove;
    public GameObject transitionClouds;
    public float transitionCloudSpeed;
    public SpriteRenderer[] backgrounds;
    public Transform transitionCloudsSpawnLocation;
    public bool spawnedClouded;
    public float gameTime;
    public float levelSwitchTime = 60f;
    public float transitionTime;
    private float tempTransitionTime = 5f;
    public int numOfLevels;

    private AudioSource levelMusic;
    public float initalLevelMusicVolume;
    public AudioClip level_1_music;
    public AudioClip level_2_music;
    public AudioClip level_3_music;
    public AudioClip level_3_music_2;

    [Header("Generic - Applies to all or most levels")]
    public int currentLevel = 1;
    //Change this to static
    private int tempCurrentLevel;
    private bool willDelay;
    private float delayTime;
    private float randomNum;
    public GameObject coins;
    public GameObject player;
    
    [Header("General Variables for Wind")]
    private bool willMakeWind;
    public Transform wind_spawn_location;
    private float wind_spawn_location_y_offset;
    public GameObject wind;
    public float min_wind_spawn_rate; //Wind Spawning Min time
    public float max_wind_spawn_rate; //Wind Spawning Max time
    private float randomWindSpawnRate;

    [Header("General Variables for powerUps")]
    private bool willMakeItem;
    public GameObject kiwiFruit;
    public Transform kiwiFruitSpawnLocation;

    //Add to level 1,2 script instead
    public float kiwiFruitLvl1_Spawnchance = 75; //Kiwi spawning on pillar
    public float kiwiFruitLvl2_SpawnRate = 5; //Kiwi will spawn from the air every n seconds
    private float temp_kiwiFruitLvl2_SpawnRate;

    public Transform coinSpawnLocation;
    public float coinLvl2_spawnRate = 4;
    private float temp_coinLvl2_SpawnRate;
    private bool spawnCoin = false;

    [Header("General Variables for obstacles")]
    private bool willMakeObstacle;
    private int randomObstacleID;
    private float randomObstacleSpawnRate;
    private bool haveToSpawnPillar;
    public float timeTillHaveToSpawnPillar = 4f;
    private float tempTimeTillHaveToSpawnPillar;

    [Header("General Variables for Enemies")]
    private bool willMakeEnemies;
    private int randomEnemyID;
    private float randomEnemySpawnRate;

    [Header("New Stuff")]
    public bool isEndlessMode = false;
    public bool spawnedEndPlatform = false;
    public GameObject endPlatform; //If not in endless mode, this marks the end of a stage.

    /*---------------------------------------------------------------------*/

    [Header("Level 1")]

    /* Obstacles Index:
     * 0 - Pillar
     * 1 - Top Vine
     * 2 - Bottom Stump 2
     * 3 - Top Vine and Bottom Stump
     */
    
    public GameObject[] lvl1_obstacles;
    public Transform[] lvl1_obstacles_spawn_location;
    private float lvl1_obstacles_spawn_location_y_offset;
    public float lvl1_obstacles_min_spawn_rate;
    public float lvl1_obstacles_max_spawn_rate;
    public GameObject[] extra_lvl1_pillars;

    /* Index:
     * 0 - Hover Enemy 
     */

    public GameObject[] lvl1_enemies;
    public Transform[] lvl1_enemies_spawn_location;
    private float lvl1_enemies_spawn_location_y_offset;
    public float lvl1_enemies_min_spawn_rate;
    public float lvl1_enemies_max_spawn_rate;

    /*---------------------------------------------------------------------*/

    [Header("Level 2")]

    /* Obstacles Index:
     * 0 - Plane
     */

    public GameObject[] lvl2_obstacles;
    public Transform[] lvl2_obstacles_spawn_location;
    private float lvl2_obstacles_spawn_location_y_offset;
    public float lvl2_obstacles_min_spawn_rate;
    public float lvl2_obstacles_max_spawn_rate;

    /* Enemy Index:
     * 0 - Chase Enemy
     * 1 - Shoot Enemy
     * 2 - Bullet Hell (BH) Enemy
     */

    public GameObject[] lvl2_enemies;
    public Transform[] lvl2_enemies_spawn_location;
    private float lvl2_enemies_spawn_location_y_offset;
    public float lvl2_enemies_min_spawn_rate;
    public float lvl2_enemies_max_spawn_rate;

    public bool spawn_2_Bullet_Hell_enemies;

    /*---------------------------------------------------------------------*/

    [Header("Level 3")]

    public float kiwiFruitLvl3_Spawnchance = 50f;
    float originalSpawnRate;

    /* Obstacles Index:
     * 0 - Pillar
     * 1 - Wave
     * 2 - Volcano
     */

    public GameObject[] lvl3_obstacles;
    public Transform[] lvl3_obstacles_spawn_location;
    private float lvl3_obstacles_spawn_location_y_offset;
    public float lvl3_obstacles_min_spawn_rate;
    public float lvl3_obstacles_max_spawn_rate;

    /* Obstacles Index:
     * 0 - Meteor
     */

    public bool willMakeMeteors;
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

    /*---------------------------------------------------------------------*/

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        player = GameObject.FindGameObjectWithTag("Player");
        kiwiMove = player.GetComponent<PlayerMove>();
        willMakeObstacle = false;
        willMakeEnemies = false;
        willMakeItem = false;
        willMakeMeteors = false;
        tempTimeTillHaveToSpawnPillar = timeTillHaveToSpawnPillar;
        temp_kiwiFruitLvl2_SpawnRate = kiwiFruitLvl2_SpawnRate;
        temp_coinLvl2_SpawnRate = coinLvl2_spawnRate;
        tempMeteorSpawnRate = meteorSpawnRate;
        randomNum = 0f;
        levelMusic = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<AudioSource>();
        initalLevelMusicVolume = levelMusic.volume;
        spawnedEndPlatform = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Changing Levels
        if (currentLevel == 0)
        {
            if (transitionTime > 0)
            {
                //Spawn screen covering clouds
                levelMusic.volume = Mathf.Lerp(levelMusic.volume, 0, 0.0075f);
                transitionTime -= Time.deltaTime;
                if (!spawnedClouded)
                {
                    GameObject spawnCloud = Instantiate(transitionClouds, new Vector3(transitionCloudsSpawnLocation.position.x,
                        transitionCloudsSpawnLocation.position.y, -10f), Quaternion.identity);
                    spawnCloud.GetComponent<Rigidbody2D>().velocity = new Vector2(-transitionCloudSpeed, 0);
                    spawnedClouded = true;
                }
            }
            else
            {
                gameTime = 0;
                willMakeObstacle = false;
                willMakeEnemies = false;
                willMakeItem = false;
                randomObstacleSpawnRate = 0.5f;
                randomEnemySpawnRate = 1f;
                currentLevel = tempCurrentLevel + 1;
                if (currentLevel > numOfLevels && isEndlessMode)
                {
                    currentLevel = 1;   
                }
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

                //Changing Backgrounds
                foreach (SpriteRenderer background in backgrounds)
                {
                    background.enabled = false;
                }

                if (currentLevel == 1)
                {
                    levelMusic.clip = level_1_music;
                    levelMusic.Play();
                    backgrounds[0].enabled = true;
                    backgrounds[1].enabled = true;
                }
                else if (currentLevel == 2)
                {
                    levelMusic.clip = level_2_music;
                    levelMusic.Play();
                    backgrounds[2].enabled = true;
                    backgrounds[3].enabled = true;
                }
                else if (currentLevel == 3)
                {
                    levelMusic.clip = level_3_music;
                    levelMusic.Play();
                    backgrounds[4].enabled = true;
                    backgrounds[5].enabled = true;
                }

                kiwiMove.flightMeterMult = 1;
                kiwiMove.flightMeter = kiwiMove.tempflightMeter;
}
            return;
        }

        if (willChangeLevel)
        {
            
            if (gameTime > levelSwitchTime)
            {
                if (currentLevel == 3 && !isEndlessMode)
                    return;
                spawnedClouded = false;
                transitionTime = tempTransitionTime;
                tempCurrentLevel = currentLevel;
                currentLevel = 0;
                kiwiMove.flightMeterMult = 0f;
                initalLevelMusicVolume = levelMusic.volume;
                return;
            }
            else
            {
                gameTime += Time.deltaTime;
                levelMusic.volume = Mathf.Lerp(levelMusic.volume, initalLevelMusicVolume, 0.0075f);
            }
        }
        

        //Spawning Winds
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

        //Spawning Kiwi Powerups/Coins for level 2
        if (currentLevel == 2)
        {
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
        }

        //Delay Time
        if (willDelay)
        {
            delayTime -= Time.deltaTime;
            if (delayTime < 0) { willDelay = false; }

            return;
        }

        /*Level 1*/
        switch (currentLevel)
        {

            /********************** LEVEL 1 ***********************/

            case 1:
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
                        lvl1_enemies_spawn_location_y_offset = Random.Range(-12f, 12f);
                    }
                    randomEnemySpawnRate = Random.Range(lvl1_enemies_min_spawn_rate, lvl1_enemies_max_spawn_rate);
                    willMakeEnemies = true;
                }
                else
                {
                    randomEnemySpawnRate -= Time.deltaTime;
                    if (randomEnemySpawnRate <= 0)
                    {
                        GameObject madeEnemy = Instantiate(lvl1_enemies[randomEnemyID],
                            new Vector3(lvl1_enemies_spawn_location[randomEnemyID].position.x,
                            lvl1_enemies_spawn_location[randomEnemyID].position.y + lvl1_enemies_spawn_location_y_offset,
                            lvl1_enemies_spawn_location[randomEnemyID].position.z), Quaternion.identity);
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
                        lvl1_obstacles_spawn_location_y_offset = Random.Range(-2.75f, 2.75f);
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
                            lvl1_obstacles_spawn_location_y_offset = Random.Range(-2.75f, 0.25f);
                        }
                        else if (randomNum >= 11f && randomNum <= 40)
                        {
                            randomObstacleID = 1; //Top Vine
                            lvl1_obstacles_spawn_location_y_offset = Random.Range(4.75f, 6f);
                        }
                        else if (randomNum >= 41f && randomNum <= 70)
                        {
                            randomObstacleID = 2; //Bottom Tree Stump
                            lvl1_obstacles_spawn_location_y_offset = Random.Range(-6.5f, -4.5f);
                        }
                        else
                        {
                            randomObstacleID = 3; //Double Vertical Vine
                            lvl1_obstacles_spawn_location_y_offset = Random.Range(-2.75f, 2.75f);
                        }
                    }
                    randomObstacleSpawnRate = Random.Range(lvl1_obstacles_min_spawn_rate, lvl1_obstacles_max_spawn_rate);
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
                                obstacleToSpawn = lvl1_obstacles[randomObstacleID];
                            }
                        }
                        else
                        {
                            obstacleToSpawn = lvl1_obstacles[randomObstacleID];
                        }
                        
                        GameObject madeObstacle = Instantiate(obstacleToSpawn, 
                            new Vector3 (lvl1_obstacles_spawn_location[randomObstacleID].position.x,
                            lvl1_obstacles_spawn_location[randomObstacleID].position.y + lvl1_obstacles_spawn_location_y_offset,
                            lvl1_obstacles_spawn_location[randomObstacleID].position.z), Quaternion.identity);

                        if (Random.Range(0,2) == 0) { spawnCoin = true; }

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
                        else if(randomObstacleID == 1)
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
                if (lvl1_obstacles_min_spawn_rate > 1.35f)
                {
                    lvl1_obstacles_min_spawn_rate -= Time.deltaTime / StaticBaseVars.difficultyScale;
                }
                if (lvl1_obstacles_max_spawn_rate > 1.75f)
                {
                    lvl1_obstacles_max_spawn_rate -= Time.deltaTime / StaticBaseVars.difficultyScale;
                }
                break;


            /********************** LEVEL 2 ***********************/


            case 2:
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
                        lvl2_enemies_spawn_location_y_offset = Random.Range(-5f, 5f);
                    }
                    else if (randomNum >= 36 && randomNum <= 70)
                    {
                        randomEnemyID = 1; //Shoot Enemy
                        lvl2_enemies_spawn_location_y_offset = Random.Range(-4f, 4f);
                    }
                    else if (randomNum >= 71 && randomNum <= 75) // Chase Enemy Spawner
                    {
                        randomEnemyID = 2;
                        //randomEnemyID = 3; 
                        lvl2_enemies_spawn_location_y_offset = 0;
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
                        lvl2_enemies_spawn_location_y_offset = 0;
                    }
                    else if (randomNum >= 81 && randomNum <= 95) //Bullet Hell Enemy
                    {
                        randomEnemyID = 6;
                        lvl2_enemies_spawn_location_y_offset = Random.Range(-1f, 1f);
                    }
                    else if (randomNum >= 96 && randomNum <= 100) //2 Bullet Hell Enemies
                    {
                        randomEnemyID = 6;
                        spawn_2_Bullet_Hell_enemies = true;
                    }

                    randomEnemySpawnRate = Random.Range(lvl2_enemies_min_spawn_rate, lvl2_enemies_max_spawn_rate);
                    willMakeEnemies = true;
                }
                else
                {
                    randomEnemySpawnRate -= Time.deltaTime;
                    if (randomEnemySpawnRate <= 0)
                    {
                        if (spawn_2_Bullet_Hell_enemies)
                        {
                            GameObject madeEnemy = Instantiate(lvl2_enemies[randomEnemyID],
                                new Vector3(lvl2_enemies_spawn_location[randomEnemyID].position.x,
                                lvl2_enemies_spawn_location[randomEnemyID].position.y + 5,
                                lvl2_enemies_spawn_location[randomEnemyID].position.z), Quaternion.identity);
                            madeEnemy.GetComponent<BulletHellEnemy>().rotationSpeed = 45;
                            madeEnemy.GetComponent<BulletHellEnemy>().fireRate = 0.55f;

                            madeEnemy = Instantiate(lvl2_enemies[randomEnemyID],
                                new Vector3(lvl2_enemies_spawn_location[randomEnemyID].position.x,
                                lvl2_enemies_spawn_location[randomEnemyID].position.y - 5,
                                lvl2_enemies_spawn_location[randomEnemyID].position.z), Quaternion.identity);
                            madeEnemy.GetComponent<BulletHellEnemy>().rotationSpeed = -45;
                            madeEnemy.GetComponent<BulletHellEnemy>().fireRate = 0.55f;
                        }
                        else
                        {
                            GameObject madeEnemy = Instantiate(lvl2_enemies[randomEnemyID],
                                new Vector3(lvl2_enemies_spawn_location[randomEnemyID].position.x,
                                lvl2_enemies_spawn_location[randomEnemyID].position.y + lvl2_enemies_spawn_location_y_offset,
                                lvl2_enemies_spawn_location[randomEnemyID].position.z), Quaternion.identity);
                            
                            if (randomEnemyID == 2)
                            {
                                //Both Chase Enemy Spawners spawn at once
                                madeEnemy = Instantiate(lvl2_enemies[randomEnemyID + 1],
                                    new Vector3(lvl2_enemies_spawn_location[randomEnemyID + 1].position.x,
                                    lvl2_enemies_spawn_location[randomEnemyID + 1].position.y + lvl2_enemies_spawn_location_y_offset,
                                    lvl2_enemies_spawn_location[randomEnemyID + 1].position.z), Quaternion.identity);
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
                        randomObstacleID = Random.Range(1,5); //Clouds
                    }
                    else
                    {
                        randomObstacleID = 0; //Plane
                        lvl2_obstacles_spawn_location_y_offset = Random.Range(-2.5f, 0f);
                    }
                    randomObstacleSpawnRate = Random.Range(lvl2_obstacles_min_spawn_rate, lvl2_obstacles_max_spawn_rate);
                    willMakeObstacle = true;
                }
                else
                {
                    randomObstacleSpawnRate -= Time.deltaTime;
                    if (randomObstacleSpawnRate <= 0)
                    {
                        Instantiate(lvl2_obstacles[randomObstacleID],
                            new Vector3(lvl2_obstacles_spawn_location[randomObstacleID].position.x,
                            lvl2_obstacles_spawn_location[randomObstacleID].position.y + lvl2_obstacles_spawn_location_y_offset,
                            lvl2_obstacles_spawn_location[randomObstacleID].position.z), Quaternion.identity);
                        willMakeObstacle = false;
                    }
                }

                //Difficulty: DescreaseAllSpawnRates for enemy
                if (lvl2_enemies_min_spawn_rate > 1f)
                {
                    lvl2_enemies_min_spawn_rate -= Time.deltaTime / StaticBaseVars.difficultyScale;
                }
                if (lvl2_enemies_max_spawn_rate > 2f)
                {
                    lvl2_enemies_max_spawn_rate -= Time.deltaTime / StaticBaseVars.difficultyScale;
                }
                break;


            /********************** LEVEL 3 ***********************/


            case 3:
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
                        if (Random.Range(0,4) == 0)
                        {
                            aimPoint = player.transform;
                        }
                        else
                        {
                            aimPoint = lvl3_meteors_aim_locations[Random.Range(0, lvl3_meteors_aim_locations.Length)];
                        }
                        GameObject madeMeteor = Instantiate(randomMeteor, spawnPoint.position, Quaternion.identity);
                        madeMeteor.GetComponent<Rigidbody2D>().velocity = (aimPoint.position - spawnPoint.position).normalized * (meteorSpeed += Random.Range(0, 1f));
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
                        lvl3_obstacles_spawn_location_y_offset = Random.Range(-2.75f, 2.75f);
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
                            lvl3_obstacles_spawn_location_y_offset = Random.Range(-2.75f, 0);
                            //randomObstacleSpawnRate = Random.Range(lvl3_obstacles_min_spawn_rate, lvl3_obstacles_max_spawn_rate);
                        }
                        else if (randomNum >= 5f && randomNum <= 30)
                        {
                            randomObstacleID = 1; //Top Fire Wave Starter
                            lvl3_obstacles_spawn_location_y_offset = 0;
                            //randomObstacleSpawnRate = Random.Range(lvl3_obstacles_min_spawn_rate, lvl3_obstacles_max_spawn_rate / 2);
                        }
                        else if (randomNum >= 31f && randomNum <= 75f)
                        {
                            randomObstacleID = 2; //Bottom Fire Wave Starter
                            lvl3_obstacles_spawn_location_y_offset = 0;
                            //randomObstacleSpawnRate = Random.Range(lvl3_obstacles_min_spawn_rate, lvl3_obstacles_max_spawn_rate / 2);
                        }
                        else
                        {
                            randomObstacleID = 3; //Volcano 
                            lvl3_obstacles_spawn_location_y_offset = Random.Range(-2.75f, 0);
                            //randomObstacleSpawnRate = Random.Range(lvl3_obstacles_min_spawn_rate * 1.25f, lvl3_obstacles_max_spawn_rate / 2);
                        }
                    }
                    randomObstacleSpawnRate = Random.Range(lvl3_obstacles_min_spawn_rate, lvl3_obstacles_max_spawn_rate);
                    willMakeObstacle = true;
                }
                else
                {
                    if (randomObstacleSpawnRate < 0)
                    {
                        GameObject obstacleToSpawn = lvl3_obstacles[randomObstacleID];

                        GameObject madeObstacle = Instantiate(obstacleToSpawn,
                            new Vector3(lvl3_obstacles_spawn_location[randomObstacleID].position.x,
                            lvl3_obstacles_spawn_location[randomObstacleID].position.y + lvl3_obstacles_spawn_location_y_offset,
                            lvl3_obstacles_spawn_location[randomObstacleID].position.z), Quaternion.identity);

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

                    if (lvl3_obstacles_min_spawn_rate > 1.75f)
                    {
                        lvl3_obstacles_min_spawn_rate -= Time.deltaTime / StaticBaseVars.difficultyScale;
                    }
                    if (lvl3_obstacles_max_spawn_rate > 3f)
                    {
                        lvl3_obstacles_max_spawn_rate -= Time.deltaTime / StaticBaseVars.difficultyScale;
                    }
                }

                //Spawn the end goal pillars
                if ((gameTime >= levelSwitchTime)
                    && !spawnedEndPlatform 
                    && !isEndlessMode)
                {
                    Instantiate(endPlatform, lvl3_obstacles_spawn_location[0].transform.position + new Vector3(0, 0f), Quaternion.identity);
                    spawnedEndPlatform = true;
                }
                if (spawnedEndPlatform)
                    return;

                break;
            case 0:
                Debug.Log("AI Director is now transitioning levels");
                break;
            default:
                print("Unknown level #");
                break;
        }
        
    }
}
