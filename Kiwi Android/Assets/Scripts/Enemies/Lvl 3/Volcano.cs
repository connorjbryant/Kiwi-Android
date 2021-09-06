using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volcano : MonoBehaviour
{
    public GameObject fireBullet;
    public GameObject kiwiHole;
    public float y_speed;
    public float x_speed;
    public float x_speed_Mult;
    public Transform spawnPoint;
    public float fireRate;
    private float tempFireRate;
    public AudioSource audioSource;
    public AudioClip popSound;
    public AudioClip shootSound;

    public bool can_shoot;
    public Transform kiwiTarget;

    // Start is called before the first frame update
    void Start()
    {
        tempFireRate = fireRate;
        fireRate = 0.1f;
        can_shoot = true;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (can_shoot)
        {
            fireRate -= Time.deltaTime;
            if (fireRate <= 0)
            {
                x_speed = Random.Range(-1, 3) * x_speed_Mult;
                y_speed = Random.Range(30f, 60f);
                GameObject spawnedFireBullet = Instantiate(fireBullet, spawnPoint.position, Quaternion.identity);
                spawnedFireBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(x_speed, y_speed));
                audioSource.clip = shootSound;
                audioSource.Play();
                fireRate = tempFireRate;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "KiwiWeapon" && can_shoot)
        {
            audioSource.clip = popSound;
            audioSource.Play();
            Destroy(collision.gameObject);
            can_shoot = false;
            kiwiHole.SetActive(true);
        }
    }
}
