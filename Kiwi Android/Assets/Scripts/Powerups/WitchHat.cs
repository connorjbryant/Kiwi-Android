using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchHat : BasePowerup
{
    public float duration = 30f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        move = player.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerPowerup)
        {
            move.isWitch = true;
            move.witchDuration = duration;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            triggerPowerup = true;
        }
    }
}
