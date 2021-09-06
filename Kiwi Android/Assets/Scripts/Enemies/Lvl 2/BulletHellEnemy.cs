using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellEnemy : BaseEnemies
{
    public int BulletHell_EnemyID = 0;
    private GameObject playerKiwi;

    [Header("Enemy that shoots in 4 directions and rotates")]
    public GameObject enemyBullet;
    public GameObject[] aimPoints;
    public float fireRate;
    public float rotationSpeed;
    public float bulletSpeed;
    private float tempFireRate;
    public bool shotBullet;

    public GameObject[] madeBullets;


    // Start is called before the first frame update
    void Start()
    {
        tempFireRate = fireRate;
        playerKiwi = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        fireRate -= Time.deltaTime;
        switch (BulletHell_EnemyID)
        {
            case 1:
                transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

                if (Vector2.Distance(transform.position, playerKiwi.transform.position) > 7f &&
                    transform.position.x - 6f > playerKiwi.transform.position.x)
                {
                    if (fireRate < 0)
                    {
                        madeBullets[0] = Instantiate(enemyBullet, transform.position, Quaternion.identity);
                        madeBullets[1] = Instantiate(enemyBullet, transform.position, Quaternion.identity);
                        madeBullets[2] = Instantiate(enemyBullet, transform.position, Quaternion.identity);
                        madeBullets[3] = Instantiate(enemyBullet, transform.position, Quaternion.identity);

                        madeBullets[0].GetComponent<Rigidbody2D>().velocity =
                            (aimPoints[0].transform.position - transform.position) * bulletSpeed;
                        madeBullets[1].GetComponent<Rigidbody2D>().velocity =
                            (aimPoints[1].transform.position - transform.position) * bulletSpeed;
                        madeBullets[2].GetComponent<Rigidbody2D>().velocity =
                            (aimPoints[2].transform.position - transform.position) * bulletSpeed;
                        madeBullets[3].GetComponent<Rigidbody2D>().velocity =
                            (aimPoints[3].transform.position - transform.position) * bulletSpeed;

                        fireRate = tempFireRate;
                    }
                }
                else
                {
                    GetComponent<Rigidbody2D>().velocity = Vector2.left * 3f;
                }

                break;
            case 2:
                break;
            default:
                print("Unknown Bullet Hell Enemy #");
                break;
        }

        if (health <= 0)
        {
            DestroyItself();
        }
    }
}
