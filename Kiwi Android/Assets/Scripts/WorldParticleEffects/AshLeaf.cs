using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshLeaf : MonoBehaviour
{
    public float lifeTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        lifeTime = Random.Range(4.5f, 5.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
