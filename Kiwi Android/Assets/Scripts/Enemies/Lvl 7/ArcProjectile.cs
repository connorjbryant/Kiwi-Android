using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcProjectile : BaseEnemies
{
    public float rotationSpeed = 50f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        if (health <= 0)
        {
            DestroyItself();
        }
    }
}
