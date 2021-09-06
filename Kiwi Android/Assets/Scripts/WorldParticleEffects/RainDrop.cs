using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDrop : MonoBehaviour
{
    public float lifeTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        lifeTime = Random.Range(1.5f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
