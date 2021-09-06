using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_1_Script : MonoBehaviour
{
    [Header("General Variables for Wind")]
    public GameObject wind;
    [HideInInspector] public bool willMakeWind;
    public Transform wind_spawn_location;
    [HideInInspector] public float wind_spawn_location_y_offset;
    public float min_wind_spawn_rate = 5f; //Wind Spawning Min time
    public float max_wind_spawn_rate = 8f; //Wind Spawning Max time
    [HideInInspector] public float randomWindSpawnRate;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
    }
}
