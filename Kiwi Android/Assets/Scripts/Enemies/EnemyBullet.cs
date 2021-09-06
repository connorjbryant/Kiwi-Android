using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 v = GetComponent<Rigidbody2D>().velocity;
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            DestroyItself();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet" ||
            collision.gameObject.tag == "Enemies" ||
            collision.gameObject.tag == "Coin" ||
            collision.gameObject.tag == "Wind")
        {
            return;
        }
        else
        {
            DestroyItself();
        }
    }

    public void DestroyItself()
    {
        Destroy(gameObject);
    }
}
