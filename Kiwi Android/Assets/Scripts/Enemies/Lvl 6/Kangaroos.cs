using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kangaroos : BaseEnemies
{
    private Rigidbody2D rb;
    
    public float minJumpForce = 8f;
    public float maxJumpForce = 16f;
    private float jumpForce;

    public float minjumpSpeed = 3f;
    public float maxjumpSpeed = 6f;
    private float jumpSpeed;
    
    public bool isOnLand = false;
    public float landTime = 0.5f;
    private float tempLandTime;

    [Header("AudioClip")]
    public AudioClip jumpSound;

    [Header("Particle Effect")]
    public ParticleSystem dustEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        tempLandTime = landTime;

        jumpForce = Random.Range(minJumpForce, maxJumpForce);
        jumpSpeed = Random.Range(minjumpSpeed, maxjumpSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            DestroyItself();
        }

        if (!isOnLand) return;
        landTime -= Time.deltaTime;
        if (landTime <= 0)
        {
            PlayAudioClip(jumpSound);
            rb.AddForce(new Vector2(-jumpSpeed, jumpForce), ForceMode2D.Impulse);
            landTime = tempLandTime;
            isOnLand = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            dustEffect.Play();
            isOnLand = true;
        }
    }
}
