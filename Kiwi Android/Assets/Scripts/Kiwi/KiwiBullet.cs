using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiwiBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject kiwiPlayer;
    public ScoreUI scoreUI;
    public int numOfKills;
    public float speed;
    public float lifeTime = 3f;
    public float rotationSpeed;

    //Rescaling
    public Vector2 originalSize;
    public float increaseScaleSize = 1f;

    //Sound Effects

    public GameObject audioSourceObject;
    private AudioSource audioSource;
    public AudioClip kiwiHitSound;
    public AudioClip[] throwSounds;

    //Water Level
    public GameObject waterSurface;
    public AudioClip splashSound;
    public bool inWater = false;
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;
    private float newGravityScale;
    private float originalGravityScale;

    //To prevent any sounds from spamming too much
    private int soundLimit;
    public int maxSoundLimit = 3;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravityScale = rb.gravityScale;
        newGravityScale = rb.gravityScale;
        numOfKills = 1;
        audioSource = GetComponent<AudioSource>();
        kiwiPlayer = GameObject.FindGameObjectWithTag("Player");
        Transform originalKiwiBirdPosition = kiwiPlayer.transform;
        Transform originalKiwiBulletPosition = gameObject.transform;
        rb.velocity = (originalKiwiBulletPosition.position - originalKiwiBirdPosition.position) * speed;
        scoreUI = GameObject.FindGameObjectWithTag("ScoreUI").GetComponent<ScoreUI>();
        soundLimit = 0;

        originalSize = transform.localScale;
        transform.localScale = originalSize / 5;

        AudioClip selectedThrowSound = throwSounds[Random.Range(0, throwSounds.Length)];
        audioSource.time = 0f;
        audioSource.clip = selectedThrowSound;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject, 5);
        }
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        if (transform.localScale.x < originalSize.x && transform.localScale.y < originalSize.y)
        {
            transform.localScale += new Vector3(increaseScaleSize * Time.deltaTime, increaseScaleSize * Time.deltaTime);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (soundLimit <= maxSoundLimit)
        {
            audioSource.time = 0f;
            audioSource.clip = kiwiHitSound;
            audioSource.Play();
            soundLimit++;
        }
        explode();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (soundLimit > maxSoundLimit)
            return;
            
        if (collision.tag == "Enemies")
        {
            soundLimit++;
            print("Kiwi Fruit hit enemy");
            scoreUI.float_score += scoreUI.killPoints * numOfKills++;
            audioSource.clip = kiwiHitSound;
            audioSource.Play();
            explode();
        }
        else if (collision.tag == "WaterWave")
        {
            soundLimit++;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y/2);
            rb.gravityScale = 0.1f;
            inWater = true;
            audioSource.time = 0.125f;
            audioSource.clip = splashSound;
            audioSource.Play();
            waterSurface = collision.transform.Find("Surface").gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "WaterWave")
        {
            rb.gravityScale = newGravityScale;
            inWater = false;
        }
    }

    public void explode()
    {        
        Destroy(gameObject, 10);
    }
}
