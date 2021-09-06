using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koala : MonoBehaviour
{
    private float climbSpeed;
    public float minClimbSpeed = 1.0f;
    public float maxClimbSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        climbSpeed = Random.Range(minClimbSpeed, maxClimbSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * climbSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Lvl6_Vines")
        {
            climbSpeed *= -1;
        }
    }
}
