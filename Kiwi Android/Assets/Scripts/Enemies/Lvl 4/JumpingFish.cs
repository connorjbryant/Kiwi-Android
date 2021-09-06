using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingFish : BaseEnemies
{
    [Header("Particle Effects")]
    public ParticleSystem splashEffect;

    [Header("Audio Source")]
    public AudioClip splashSound;

    private Rigidbody2D rb;
    private float originalGrav;
    private float newGravityScale;
    
    public float maxSpeed = 2f;
    public float rotationSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGrav = rb.gravityScale;
        newGravityScale = rb.gravityScale * 1.25f;
        audioSource = GetComponent<AudioSource>();
        //transform.position = new Vector2(transform.position.x, waveSurfaceTransform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        else
        {
            if (rb.velocity.y > 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -45), Time.deltaTime * rotationSpeed * Mathf.Pow(rb.velocity.magnitude, 2));
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 45), Time.deltaTime * rotationSpeed * Mathf.Pow(rb.velocity.magnitude, 2));
            }
        }
        //transform.position = new Vector2(transform.position.x,
        //    waveSurfaceTransform.position.y + Mathf.Sin(Time.time * waveSpeed) * waveHeight);

        if (health <= 0)
        {
            DestroyItself();
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "WaterWave")
        {
            rb.gravityScale = -originalGrav;
            PlayAudioClip(splashSound, 0.15f);
            splashEffect.Play();
        }
        if (collision.tag == "KiwiWeapon")
        {
            health--;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "WaterWave")
        {
            rb.gravityScale = newGravityScale;
            PlayAudioClip(splashSound, 0.15f);
            splashEffect.Play();
        }
    }
}
