using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFive : MonoBehaviour
{
    public GameObject[] obstaclePatterns;

    private float timeBtwSpawn;

    public float startTimeBtwSpawn;
    public float decreaseTime;
    public float minTime = 1f;

    private void Update()
    {

        if (timeBtwSpawn <= 0)
        {
            int rand = Random.Range(0, obstaclePatterns.Length);
            Instantiate(obstaclePatterns[rand], transform.position, Quaternion.identity);
            timeBtwSpawn = startTimeBtwSpawn;

            if (startTimeBtwSpawn > minTime)
            {
                startTimeBtwSpawn -= decreaseTime;
            }



        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }
}
