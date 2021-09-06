using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDestroyer : MonoBehaviour
{
    public LayerMask onlyAffectEnemyBullets;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.layer == 16) //16 is OnlyAffectEnemyBullet
        {
            if (collision.gameObject.tag == "Enemies")
            {
                Destroy(collision.gameObject);
            }
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.layer == 16)
        {
            if (collision.tag == "Enemies")
            {
                Destroy(collision.gameObject);
            }
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
