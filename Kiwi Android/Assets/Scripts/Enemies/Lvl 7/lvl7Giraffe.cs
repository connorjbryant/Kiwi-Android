using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl7Giraffe : MonoBehaviour
{
    //He just moves up and down
    public float moveSpeed = 1f;
    public float yRange = 2.5f;
    float originalYPos;

    // Start is called before the first frame update
    void Start()
    {
        originalYPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x,
             originalYPos + yRange * Mathf.Sin(moveSpeed * Mathf.PI * Time.time), transform.position.z);
    }
}
