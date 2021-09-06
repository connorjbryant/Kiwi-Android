using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBaseVars : MonoBehaviour
{
    // Start is called before the first frame update
    public static float windSpeed = -5f;
    public static float obstacleSpeed = -3f;
    public static float enemySpeed = -4f;
    public static float chaseEnemySpeedMult = 1;

    public float originalWindSpeed = -5f;
    public float originalObstacleSpeed = -3f;
    public float originalEnemySpeed = -4f;
    public float originalChaseEnemySpeedMult = 1;

    public static float difficultyScale = 150f; //Modifies game speed and spawn speed. 

    //Level 3 - FireWaves
    public static float y_height = 5f;
    public static float y_speed = 5f;

    void Start()
    {
        difficultyScale = 150f;
        windSpeed = originalWindSpeed;
        obstacleSpeed = originalObstacleSpeed;
        enemySpeed = originalEnemySpeed;
        chaseEnemySpeedMult = originalChaseEnemySpeedMult;
    }

    // Update is called once per frame
    void Update()
    {
        /*Autospeed will increase over time*/
        windSpeed -= Time.deltaTime/ difficultyScale;
        obstacleSpeed -= Time.deltaTime/ difficultyScale;
        enemySpeed -= (Time.deltaTime * 1.5f) / difficultyScale;
        if (chaseEnemySpeedMult <= 2.5f)
        {
            chaseEnemySpeedMult += (Time.deltaTime * 0.05f) / difficultyScale;
        }
    }
}
