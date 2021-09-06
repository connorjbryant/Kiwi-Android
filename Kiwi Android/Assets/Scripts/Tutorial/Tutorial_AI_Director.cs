using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_AI_Director : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject pillar;
    public GameObject kiwiFruits;
    public GameObject hoverEnemy;

    public Transform pillarSpawnLocation;
    public Transform kiwiFruitsSpawnLocation;
    public Transform hoverEnemySpawnLocation;

    public float pillarSpawnRate;
    private float tempPillarSpawnRate;

    public float kiwiFruitRate;
    private float tempKiwiFruitSpawnRate;

    public float hoverEnemySpawnRate;
    private float tempHoverEnemySpawnRate;

    void Start()
    {
        Time.timeScale = 1f;
        tempPillarSpawnRate = pillarSpawnRate;
        tempKiwiFruitSpawnRate = kiwiFruitRate;
        tempHoverEnemySpawnRate = hoverEnemySpawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        pillarSpawnRate -= Time.deltaTime;
        if (pillarSpawnRate <= 0)
        {
            Instantiate(pillar, pillarSpawnLocation.position, Quaternion.identity);
            pillarSpawnRate = tempPillarSpawnRate;
        }

        kiwiFruitRate -= Time.deltaTime;
        if (kiwiFruitRate <= 0)
        {
            Instantiate(kiwiFruits, kiwiFruitsSpawnLocation.position, Quaternion.identity);
            kiwiFruitRate = tempKiwiFruitSpawnRate;
        }

        hoverEnemySpawnRate -= Time.deltaTime;
        if (hoverEnemySpawnRate <= 0)
        {
            Instantiate(hoverEnemy, hoverEnemySpawnLocation.position, Quaternion.identity);
            hoverEnemySpawnRate = tempHoverEnemySpawnRate;
        }
    }
}
