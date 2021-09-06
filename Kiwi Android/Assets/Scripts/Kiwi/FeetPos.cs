using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetPos : MonoBehaviour
{
    public PlayerMove playerMove;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            playerMove.canJump = true;
            playerMove.windEffectParticle.Stop();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            playerMove.canJump = false;
            playerMove.windEffectParticle.Play();
        }
    }
}
