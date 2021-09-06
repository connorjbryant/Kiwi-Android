using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Transition : MonoBehaviour
{
    private PlayerMove kiwiMove;
    private GameObject player;

    [Header("Level Transitions")]
    public bool isEndlessMode = false;

    public SpriteRenderer[] backgrounds;
    public bool willChangeLevel = true;
    public GameObject transitionClouds;
    public float transitionCloudSpeed = 7f;
    [HideInInspector] public Transform transitionCloudsSpawnLocation;
    private bool spawnedClouded;
    public static float gameTime = 0f;
    public static float staticLevelSwitchTime;
    public float levelSwitchTime = 60f;
    public int stageNum = 1;
    public static float transitionTime = 5f;
    private float tempTransitionTime;
    public int numOfLevels = 3;

    [Header("Audio Transitions")]
    private AudioSource levelMusic;
    private float initalLevelMusicVolume;
    public AudioClip[] level_1_musics;
    public AudioClip[] level_2_musics;
    public AudioClip[] level_3_musics;
    public AudioClip[] level_4_musics;
    public AudioClip[] level_5_musics;
    public AudioClip[] level_6_musics;
    public AudioClip[] level_7_musics;

    [Header("Level Specific Stuff")]
    public GameObject lvl4_wave;
    public GameObject lvl5_lightings;

    // Start is called before the first frame update
    void Start()
    {
        switch (stageNum)
        {
            case (1):
                AI_Dir_Generic.currentLevel = 1;
                break;
            case (2):
                AI_Dir_Generic.currentLevel = 4;
                break;
            case (3):
                AI_Dir_Generic.currentLevel = 7;
                break;
            default:
                break;
        }

        gameTime = 0;

        AI_Dir_Generic.tempCurrentLevel = AI_Dir_Generic.currentLevel;
        staticLevelSwitchTime = levelSwitchTime;
        player = GameObject.FindGameObjectWithTag("Player");
        kiwiMove = player.GetComponent<PlayerMove>();
        transitionCloudsSpawnLocation = transform.Find("TransitionCloudLocation").transform;

        transitionTime = 5f;
        tempTransitionTime = transitionTime;
        levelMusic = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<AudioSource>();
        initalLevelMusicVolume = levelMusic.volume;

        if (isEndlessMode)
        {
            AI_Dir_Generic.currentLevel = 0;
            AI_Dir_Generic.tempCurrentLevel = 0;
            transitionTime = 0; //Switch scenes immediately

            if (EndlessModeSettings.numberOfIncludedLevels == 1)
            {
                willChangeLevel = false;
            }
            else
            {
                willChangeLevel = true;
            }
        }
    }

    //Return random num except
    public int randomIntExcept(List<int> exceptNums)
    {
        List<int> levelList = new List<int> { 1, 2, 3, 4, 5, 6, 7};
        foreach (int num in exceptNums)
        {
            levelList.Remove(num);
        }
        int resultIndex = Random.Range(0, levelList.Count);
        return levelList[resultIndex];
    }


    // Update is called once per frame
    void Update()
    {
        //Changing Levels
        if (AI_Dir_Generic.currentLevel == 0)
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
                AI_Dir_Generic.willMakeObstacle = false;
                AI_Dir_Generic.willMakeEnemies = false;
                AI_Dir_Generic.willMakeItem = false;
                AI_Dir_Generic.randomObstacleSpawnRate = 0.5f;
                AI_Dir_Generic.randomEnemySpawnRate = 1f;

                //Changing to the right level for endless mode or not
                if (isEndlessMode)
                {
                    //Min is level 1, Max is level 7
                    List<int> excludedLevelList = new List<int>();
                    for (int i = 1; i <= 7; i++)
                    {
                        if (PlayerPrefs.GetInt("EndlessMode_Level" + i) == 0)
                        {
                            excludedLevelList.Add(i);
                            if (willChangeLevel)
                                excludedLevelList.Add(AI_Dir_Generic.tempCurrentLevel);
                        }
                    }

                    AI_Dir_Generic.currentLevel = randomIntExcept(excludedLevelList);
                    print("New Level in Endless Mode is: " + AI_Dir_Generic.currentLevel);
                }
                else
                    AI_Dir_Generic.currentLevel = AI_Dir_Generic.tempCurrentLevel + 1;

                //Changing Backgrounds + Music + Others
                foreach (SpriteRenderer background in backgrounds)
                {
                    background.enabled = false;
                }
                switch (AI_Dir_Generic.currentLevel)
                {
                    case (1):
                        levelMusic.clip = level_1_musics[Random.Range(0, level_1_musics.Length)];
                        break;
                    case (2):
                        levelMusic.clip = level_2_musics[Random.Range(0, level_2_musics.Length)];
                        break;
                    case (3):
                        levelMusic.clip = level_3_musics[Random.Range(0, level_3_musics.Length)];
                        break;
                    case (4):
                        levelMusic.clip = level_4_musics[Random.Range(0, level_4_musics.Length)];
                        break;
                    case (5):
                        levelMusic.clip = level_5_musics[Random.Range(0, level_5_musics.Length)];
                        break;
                    case (6):
                        levelMusic.clip = level_6_musics[Random.Range(0, level_6_musics.Length)];
                        break;
                    case (7):
                        levelMusic.clip = level_7_musics[Random.Range(0, level_7_musics.Length)];
                        break;
                    default:
                        Debug.Log("Unknown level num");
                        break;
                }
                levelMusic.Play();
                backgrounds[AI_Dir_Generic.currentLevel * 2 - 2].enabled = true;
                backgrounds[AI_Dir_Generic.currentLevel * 2 - 1].enabled = true;

                kiwiMove.flightMeterMult = 1;
                kiwiMove.flightMeter = kiwiMove.tempflightMeter;

                /*Level Specific Modifications*/

                //Level 4 wave
                if (lvl4_wave != null)
                {
                    if (AI_Dir_Generic.currentLevel == 4)
                    {
                        lvl4_wave.SetActive(true);
                        Lvl4_Wave.wavePhaseTime = 0;
                        Lvl4_Wave.wavePhase = 0;
                    }
                    else lvl4_wave.SetActive(false);
                }
                
                //Level 5 lightings
                if (lvl5_lightings != null)
                {
                    if (AI_Dir_Generic.currentLevel == 5)
                        lvl5_lightings.SetActive(true);
                    else lvl5_lightings.SetActive(false);
                }
            }
            return;
        }

        gameTime += Time.deltaTime;
        if (willChangeLevel)
        {
            if (gameTime > levelSwitchTime)
            {
                if (AI_Dir_Generic.currentLevel == 3 && !isEndlessMode)
                    return;
                if (AI_Dir_Generic.currentLevel == 6 && !isEndlessMode)
                    return;
                spawnedClouded = false;
                transitionTime = tempTransitionTime;
                AI_Dir_Generic.tempCurrentLevel = AI_Dir_Generic.currentLevel;
                AI_Dir_Generic.currentLevel = 0;
                kiwiMove.flightMeterMult = 0f;
                initalLevelMusicVolume = levelMusic.volume;
                return;
            }
            else
            {
                levelMusic.volume = Mathf.Lerp(levelMusic.volume, initalLevelMusicVolume, 0.0075f);
            }
        }
    }
}
