using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemyBarrage : MonoBehaviour
{
    public GameObject shootEnemy;
    public float fireRate;
    private float tempFireRate;
    public float lifeTime = 10f;
    public float speed = 3f;
    public float shootEnemyGravityScale;

    public int patternSelection;
    /*
     * 1 - It will go from up to down
     * 2 - It will go from down to up
     */

    // Start is called before the first frame update
    void Start()
    {
        tempFireRate = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        fireRate -= Time.deltaTime;
        if (fireRate <= 0)
        {
            GameObject enemy = Instantiate(shootEnemy, transform.position, Quaternion.identity);
            enemy.GetComponent<Rigidbody2D>().gravityScale = shootEnemyGravityScale;
            enemy.GetComponent<CircleCollider2D>().enabled = false;
            fireRate = tempFireRate;
        }

        switch (patternSelection)
        {
            case 1:
                transform.Translate(new Vector2(0, -1) * speed * Time.deltaTime);
                break;
            case 2:
                transform.Translate(new Vector2(0, 1) * speed * Time.deltaTime);
                break;
            default:
                break;
        }

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
