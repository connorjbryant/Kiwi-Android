using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEnemy : BaseEnemies
{
    [Header("Particle Effects")]
    public ParticleSystem splashEffect;

    [Header("Audio Clip")]
    public AudioClip splashSound;

    public bool inWater;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = rb.gravityScale * Random.Range(1f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (inWater)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, 
                new Vector2(rb.velocity.x, 0), Time.deltaTime * 10);
        }
        if (health <= 0)
        {
            DestroyItself();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "WaterWave")
        {
            inWater = true;
            rb.gravityScale = 0;
            PlayAudioClip(splashSound, 0.15f); ;
            splashEffect.Play();
        }
        if (collision.tag == "KiwiWeapon")
        {
            health--;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "WaterWave")
        {
            PlayAudioClip(splashSound, 0.15f);
            splashEffect.Play();
        }
    }
}
