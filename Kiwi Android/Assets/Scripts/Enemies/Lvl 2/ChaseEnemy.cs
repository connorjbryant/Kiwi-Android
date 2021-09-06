using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : BaseEnemies
{
    private Rigidbody2D rb;
    public GameObject kiwiPlayer;
    private Vector3 finalPosition;
    private Vector3 finalPlayerPosition;
    private bool willCharge; //Trigger after the enemy stops chasing
    public float chaseTillDistance; //Enemy Stops fully chasing player at a certain distance
    public float speed;
    public bool isDone;

    //Move Straight then dive down 
    public bool diveMode;
    public float diveDistance;

    // Start is called before the first frame update
    void Start()
    {
        kiwiPlayer = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        willCharge = false;
        isDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!diveMode)
        {
            if (!willCharge)
            {
                if (Vector2.Distance(transform.position, kiwiPlayer.transform.position) > chaseTillDistance)
                {
                    transform.position = Vector3.MoveTowards(transform.position, kiwiPlayer.transform.position, speed * Time.deltaTime * StaticBaseVars.chaseEnemySpeedMult);
                }
                else
                {
                    finalPlayerPosition = kiwiPlayer.transform.position;
                    finalPosition = transform.position;
                    willCharge = true;
                }
            }
            else
            {
                rb.velocity = (finalPlayerPosition - finalPosition).normalized * speed * 1.25f * StaticBaseVars.chaseEnemySpeedMult;
            }
        }
        else
        {
            if (!willCharge)
            {
                if (Mathf.Abs(transform.position.x - kiwiPlayer.transform.position.x) > diveDistance)
                {
                    rb.velocity = new Vector2(-speed * StaticBaseVars.chaseEnemySpeedMult, 0);
                }
                else
                {
                    finalPlayerPosition = kiwiPlayer.transform.position;
                    finalPosition = transform.position;
                    willCharge = true;
                }
            }
            else
            {
                rb.velocity = (finalPlayerPosition - finalPosition).normalized * speed * StaticBaseVars.chaseEnemySpeedMult;
            }
        }

        if (health <= 0)
        {
            DestroyItself();
        }
    }
}
