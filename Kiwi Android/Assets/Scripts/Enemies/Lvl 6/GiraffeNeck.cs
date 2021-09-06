using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiraffeNeck : MonoBehaviour
{
    public bool isHitByKiwi = false;

    private void Start()
    {
        isHitByKiwi = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "KiwiWeapon")
        {
            isHitByKiwi = true;
        }
    }
}
