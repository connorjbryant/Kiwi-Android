using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : BaseEnemies
{
    public GameObject Enemy_Bullet;
    private GameObject playerKiwi;

    public float fireRate;
    public float tempFireRate;
    public float bulletSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        tempFireRate = fireRate;
        playerKiwi = GameObject.FindGameObjectWithTag("Player");
        fireRate = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, playerKiwi.transform.position) > 5f &&
            transform.position.x - 5f > playerKiwi.transform.position.x)
        {
            if (fireRate > 0)
            {
                fireRate -= Time.deltaTime;
            }
            else
            {
                GameObject bullet = Instantiate(Enemy_Bullet, transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity =
                    (playerKiwi.transform.position - transform.position).normalized * bulletSpeed;
                fireRate = tempFireRate;
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.left * 3f;
        }

        if (health <= 0)
        {
            DestroyItself();
        }
    }
}
