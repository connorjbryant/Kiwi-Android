using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TazmanianDevil : BaseEnemies
{
    public float speed;
    public float rightSpeed = 5f;
    public float leftSpeed = 2f; //This is for a bug where he moves lefts much faster
    public bool movingRight;

    private void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {
        transform.Translate(Vector2.right * -speed * Time.deltaTime);

        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyWall")
        {
            movingRight = !movingRight;
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                speed = rightSpeed;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                speed = leftSpeed;
            }
        }

        if (collision.tag == "KiwiWeapon")
        {
            health--;
        }
    }
}
