using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectAi_Director : MonoBehaviour
{
    public int currentLevel;
    public float spawnRate;
    private float tempSpawnRate;

    public GameObject rainDrop;
    public Transform rainDropSpawner;

    public GameObject feather;
    public Transform featherSpawner;

    public GameObject ashLeaf;
    public Transform ashLeafSpawner;

    public GameObject cherryBlossom;
    public Transform cherryBlossomSpawner;

    public bool isSpawning = true;

    private GameObject spawningObject;
    private Transform spawnObjectSpawner;

    // Start is called before the first frame update
    void Start()
    {
        tempSpawnRate = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawning)
        {
            return;
        }
        currentLevel = AI_Dir_Generic.currentLevel;

        //Spawning the items
        if (spawnRate < 0)
        {
            switch (currentLevel)
            {
                case (1):
                    tempSpawnRate = 0.1f;
                    spawningObject = rainDrop;
                    spawnObjectSpawner = rainDropSpawner;
                    Instantiate(spawningObject, new Vector3(spawnObjectSpawner.transform.position.x + Random.Range(-12f, 12f),
                        spawnObjectSpawner.transform.position.y, spawnObjectSpawner.transform.position.z), Quaternion.identity);
                    break;
                case (2):
                    tempSpawnRate = 0.3f;
                    spawningObject = feather;
                    spawnObjectSpawner = featherSpawner;
                    Instantiate(spawningObject, new Vector3(spawnObjectSpawner.transform.position.x,
                        spawnObjectSpawner.transform.position.y + Random.Range(-5f, 5f),
                        spawnObjectSpawner.transform.position.z), Quaternion.identity);
                    break;
                case (3):
                    tempSpawnRate = 0.25f;
                    spawningObject = ashLeaf;
                    spawnObjectSpawner = ashLeafSpawner;
                    Instantiate(spawningObject, new Vector3(spawnObjectSpawner.transform.position.x + Random.Range(-13f, 12f),
                        spawnObjectSpawner.transform.position.y,
                        spawnObjectSpawner.transform.position.z), Quaternion.identity);
                    break;
                case (7):
                    /*
                    tempSpawnRate = 0.3f;
                    spawningObject = cherryBlossom;
                    spawnObjectSpawner = cherryBlossomSpawner;
                    Instantiate(spawningObject, new Vector3(spawnObjectSpawner.transform.position.x,
                        spawnObjectSpawner.transform.position.y + Random.Range(-5f, 5f),
                        spawnObjectSpawner.transform.position.z), Quaternion.identity);
                    */
                    break;
            }
            spawnRate = tempSpawnRate;
        }
        else
        {
            spawnRate -= Time.deltaTime;
        }
    }
}
