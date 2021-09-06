using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcProjectileThrowingEnemies : BaseEnemies
{
    public GameObject arcProjectile;
    public float throwRate = 3f;
    float tempThrowRate;
    public Vector2 throwForce = new Vector2(-2.5f, 4);

    [Header("Sounds")]
    public AudioClip throwSound;

    // Start is called before the first frame update
    void Start()
    {
        tempThrowRate = throwRate;
        throwRate = Random.Range(0f, tempThrowRate);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        throwRate -= Time.deltaTime;
        if (throwRate <= 0)
        {
            GameObject arcProjectileObj = Instantiate(arcProjectile, transform.position, Quaternion.identity);
            arcProjectileObj.GetComponent<Rigidbody2D>().AddForce(throwForce);
            throwRate = tempThrowRate;

            /*
            audioSource.clip = throwSound;
            audioSource.time = 0f;
            audioSource.Play();
            */
        }
        if (health <= 0)
        {
            DestroyItself();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "KiwiWeapon")
        {
            health--;
        }
        if (collision.tag == "Coin")
        {
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("Plane"))
        {
            DestroyItself();
        }
    }
}
