using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEnemies : BaseEnemies
{
    public GameObject kiwiPlayer;
    public float fallGravity = 1;
    public float xDistanceTillFall = 0.5f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        kiwiPlayer = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(kiwiPlayer.transform.position.x - transform.position.x) < xDistanceTillFall)
        {
            rb.gravityScale = fallGravity;
        }
        if (health <= 0)
        {
            DestroyItself();
        }
    }
}
