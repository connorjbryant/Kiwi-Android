using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_2_Script : MonoBehaviour
{
    [Header("General Variables for obstacles")]
    public float timeTillHaveToSpawnPillar = 4f;
    public static bool willMakeObstacle;
    public static float randomObstacleSpawnRate;
    [HideInInspector] public float tempTimeTillHaveToSpawnPillar;

    public GameObject[] lvl_obstacles;
    public Transform[] lvl_obstacles_spawn_location;
    [HideInInspector] public float lvl_obstacles_spawn_location_y_offset;
    public float lvl_obstacles_min_spawn_rate;
    public float lvl_obstacles_max_spawn_rate;

    // Start is called before the first frame update
    void Start()
    {
        willMakeObstacle = false;
    }

    // Update is called once per frame
    void Update()
    {
        //AI selecting what obstacle to spawn
        if (!willMakeObstacle)
        {
            lvl_obstacles_spawn_location_y_offset = Random.Range(-2.75f, 0.25f);

            randomObstacleSpawnRate = Random.Range(lvl_obstacles_min_spawn_rate, lvl_obstacles_max_spawn_rate);
            willMakeObstacle = true;
        }
        else
        {
            randomObstacleSpawnRate -= Time.deltaTime;
            if (randomObstacleSpawnRate <= 0)
            {
                GameObject madeObstacle = Instantiate(lvl_obstacles[0],
                    new Vector3(lvl_obstacles_spawn_location[0].position.x,
                    lvl_obstacles_spawn_location[0].position.y + lvl_obstacles_spawn_location_y_offset,
                    lvl_obstacles_spawn_location[0].position.z), Quaternion.identity);

                willMakeObstacle = false;
            }
        }
    }
}
