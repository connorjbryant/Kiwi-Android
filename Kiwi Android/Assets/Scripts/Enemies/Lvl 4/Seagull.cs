using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seagull : BaseEnemies
{
    private Rigidbody2D rb;
    public GameObject kiwiPlayer;
    public float normalSpeed = 3f;
    public float chargeSpeed = 7f;
    private float y_distance_from_kiwi = 0;
    public float y_distance_from_kiwi_till_charge = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        kiwiPlayer = GameObject.FindGameObjectWithTag("Player");
        rb.velocity = new Vector2(-normalSpeed, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        y_distance_from_kiwi = Mathf.Abs(kiwiPlayer.transform.position.y - transform.position.y);
        if (y_distance_from_kiwi < y_distance_from_kiwi_till_charge)
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, -chargeSpeed, Time.deltaTime), 0);
        }

        if (health <= 0)
        {
            DestroyItself();
        }
    }
}
