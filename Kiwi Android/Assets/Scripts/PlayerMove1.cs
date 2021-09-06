using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMove1 : MonoBehaviour
{
    [Header("Flying")]
    public bool canMove;
    private Rigidbody2D rb;
    public float originalGravityScale;
    public float rotationSpeed;
    public float flightMeter;
    float tempflightMeter;
    public bool isTouchingWind;
    public float horizontalOffsetSpeed;
    public float currentXpoint;
    public float xOffset = 10f;
    public int flyingSpeedCondition; 
    /*
     * 0 - Slow speed
     * 1 - Normal speed
     * 2 - Fast speed
     */

    public float maxPositiveVelocity = 4f;
    public float maxPositiveVelocityWithWind = 5f;
    public float maxNegativeVelocity = -5f;

    public ParticleSystem windEffectParticle;

    [Header("Landing")]
    public bool isLanding;
    public bool canJump;
    public float jumpForce;

    [Header("Audio")]
    public AudioSource musicSource;
    private AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip glideSound;
    public AudioClip kiwiHurtSound;
    public AudioClip gameOverSound;

    [Header("GameOver")]
    public ScoreUI scoreUI;
    public bool isGameOver;
    public GameObject resultScreen;
    public TextMeshProUGUI scoreText;


    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
        originalGravityScale = rb.gravityScale;
        tempflightMeter = flightMeter;
        currentXpoint = transform.position.x;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //GameOver
        if (isGameOver)
        {
            canMove = false;
            resultScreen.SetActive(true);
            Time.timeScale = 0f;
            scoreText.text = "Final Score: " + Mathf.Round(scoreUI.score).ToString();
        }

        if (!canMove)
        {
            return;
        }

        //Kiwi's horizontal movement
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(currentXpoint + xOffset, transform.position.y, transform.position.z), horizontalOffsetSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(currentXpoint - xOffset, transform.position.y, transform.position.z), horizontalOffsetSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(currentXpoint, transform.position.y, transform.position.z), Time.deltaTime);
        }

        //When Kiwi is about to land
        if (isLanding)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -30), Time.deltaTime * rotationSpeed * 4);
        }

        //Kiwi is on land
        if (canJump && flightMeter < tempflightMeter)
        {
            flightMeter += Time.deltaTime * 15f;
        }
        if (canJump && Input.GetKey(KeyCode.W))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            if (Input.GetKeyDown(KeyCode.W))
            {
                windEffectParticle.Play();
                audioSource.clip = jumpSound;
                audioSource.Play();
            }
        }

        //Flying controls - Gliding
        if (Input.GetKey(KeyCode.W) && flightMeter > 0)
        {   
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(rb.velocity.y, 0, Time.deltaTime));
            if (isTouchingWind)
            {
                rb.gravityScale = -(originalGravityScale*1.25f);
            }
            else
            {
                rb.gravityScale = -(originalGravityScale/1.25f);
                flightMeter -= Time.deltaTime;
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -45), Time.deltaTime * rotationSpeed);
        }
        //Flying controls - Not Gliding
        else
        {
            if (isTouchingWind)
            {
                rb.gravityScale = originalGravityScale/1.5f;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -75), Time.deltaTime * rotationSpeed);
            }
            else
            {
                rb.gravityScale = originalGravityScale;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -150), Time.deltaTime * rotationSpeed);
            }
        }

        //Velocity Limit
        if (isTouchingWind && rb.velocity.y > maxPositiveVelocityWithWind)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(rb.velocity.y, maxPositiveVelocityWithWind, 0.2f));
        }
        else if (!isTouchingWind && rb.velocity.y > maxPositiveVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(rb.velocity.y, maxPositiveVelocity, 0.2f));
        }
        else if (rb.velocity.y < maxNegativeVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(rb.velocity.y, maxNegativeVelocity, 0.2f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "KiwiFruit")
        {
            flightMeter += 1.5f;
            if (flightMeter > tempflightMeter)
            {
                flightMeter = tempflightMeter;
            }
        }
        if (collision.tag == "Ground")
        {
            isLanding = true;
        }
        else if (collision.tag == "Wind")
        {
            isTouchingWind = true;
        }
        if (collision.tag == "DeathZone" || collision.tag == "Enemies")
        {
            Debug.Log("Trigger: Kiwi was killed by " + collision.name);
            GameOver();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            isLanding = false;
        }
        else if (collision.tag == "Wind")
        {
            isTouchingWind = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }
        if (collision.gameObject.tag == "DeathZone")
        {
            Debug.Log("Collision: Kiwi was killed by " + collision.gameObject.name);
            GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            windEffectParticle.Stop();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            windEffectParticle.Play();
            canJump = false;
        }
    }

    public void GameOver()
    {
        //audioSource.clip = kiwiHurtSound;
        //audioSource.Play();
        audioSource.clip = gameOverSound;
        audioSource.Play();
        musicSource.Stop();
        Debug.Log("Game Over");
        isGameOver = true;
    }
}
