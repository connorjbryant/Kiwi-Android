using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance;
    public LeveLoader levelLoader;

    [Header("Touch Controls")]
    public bool leftArrow;
    public bool rightArrow;
    public bool upArrow;
    public bool downArrow;
    public bool allowTiltControls;

    [Header("Flying")]
    public int movementType = -1;
    /*
     * 1 - Land Walking
     * 2 - Normal Flying
     * 3 - Witch Gliding
     * 4 - Swimming
     */

    public bool canMove;
    private Rigidbody2D rb;
    public float originalGravityScale;
    public float rotationSpeed;

    //Flight Meter
    public float flightMeter;
    public float tempflightMeter;
    public float flightMeterMult;

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
    public float heavierMaxNegativeVelocity = -7.5f;
    private float originalMaxNegativeVelocity;

    public ParticleSystem windEffectParticle;

    [Header("Heat Meter")]
    public GameObject heatMeterMeterUI;
    public GameObject bgSunRays;
    public bool isTouchingLightRay;
    public float heatMeter;
    private float tempheatMeter;

    [Header("Landing")]
    public bool isLanding;
    public bool canJump;
    public float jumpForce;
    public float bounceForce = 5f;

    [Header("Swimming")]
    public bool isSwimming;
    public bool isWet;
    public float wetTimer = 3f;
    float tempWetTimer;
    public float maxNegativeVelocityInWater = -1f;
    public bool touchingSurface;
    public ParticleSystem splashEffect;
    public ParticleSystem bubbleTrailEffect;
    public ParticleSystem wetEffect;
    public ParticleSystem rayEffect;

    public GameObject waterWaveSurface;
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;

    [Header("Audio")]
    public AudioSource musicSource;
    private AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip glideSound;
    public AudioClip kiwiHurtSound;
    public AudioClip gameOverSound;
    public AudioClip bounceSound;
    public AudioClip splashSound;
    public AudioClip burnSound;
    public bool playHurtSoundOnce;

    [Header("GameOver")]
    public ScoreUI scoreUI;
    public bool isGameOver;
    public bool gotGameOver;
    public bool adDone;
    public GameObject resultScreen;
    public TextMeshProUGUI scoreText;

    [Header("PowerUps")]
    public GameObject normalKiwiSprite;
    public GameObject witchKiwiSprite;
    public bool isWitch;
    public float witchDuration;
    private float tempWitchDuration;
    public float kiwiWitchYSpeed = 5f;
    public bool infinteWitch;

    public GameObject bubble;
    public bool hasBubble;
    public bool lostBubble;
    public float iFrame = 5f;
    float tempIFrame = 5f;
    public BlinkingEffect blinkObj;
    public BlinkingEffect blinkObjForSkin;
    public SpriteRenderer normalSprite;
    public SpriteRenderer witchSprite;
    public GameObject normalTouchControls;
    public GameObject witchTouchControls;

    [Header("Tutorial Settings")]
    public bool isInTutorial;
    public bool startGamePaused;
    public bool infiniteFlightMeter;

    [Header("Cosmetics")]
    public GameObject HeadSkin;
    public GameObject OutfitSkin;

    // Start is called before the first frame update
    void Start()
    {
        gotGameOver = false;
        adDone = false;

        //FrameRate
        Application.targetFrameRate = 60;
        if (startGamePaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LeveLoader>();
        canMove = true;
        flightMeterMult = 1;
        rb = GetComponent<Rigidbody2D>();
        originalGravityScale = rb.gravityScale;
        tempflightMeter = flightMeter;
        tempheatMeter = heatMeter;
        heatMeter = 0;
        currentXpoint = transform.position.x;
        audioSource = GetComponent<AudioSource>();
        audioSource.time = 0f;
        tempWitchDuration = witchDuration;
        tempIFrame = iFrame;
        playHurtSoundOnce = false;
        originalMaxNegativeVelocity = maxNegativeVelocity;
        tempWetTimer = wetTimer;

        //Powerups
        if (SceneManager.GetActiveScene().name == "1_Tutorial" ||
            SceneManager.GetActiveScene().name == "2_Tutorial" ||
            SceneManager.GetActiveScene().name == "3_Tutorial" ||
            SceneManager.GetActiveScene().name == "4_Tutorial")
        {}
        else
        {
            //Witch
            if (PlayerPrefs.GetInt("Powerup_WitchHat") == 1)
            {
                isWitch = true;
                witchKiwiSprite.SetActive(true);
                normalKiwiSprite.SetActive(false);
                witchTouchControls.SetActive(true);
                normalTouchControls.SetActive(false);

                //Disable Hat and Outfit Sprites
                HeadSkin.SetActive(false);
                OutfitSkin.SetActive(false);
            }
            else
            {
                //Enable Hat and Outfit Sprites
                HeadSkin.SetActive(true);
                OutfitSkin.SetActive(true);
            }

            //Bubble
            if (PlayerPrefs.GetInt("Powerup_Bubble") == 1)
            {
                bubble.SetActive(true);
                hasBubble = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Powerups
        if (lostBubble)
        {
            iFrame -= Time.deltaTime;
            if (iFrame < 0)
            {
                hasBubble = false;
            }
        }

        //GameOver
        if (isGameOver && !gotGameOver)
        {
            if (isInTutorial)
            {
                levelLoader.LoadNextLevel(SceneManager.GetActiveScene().name);
                return;
            }

            //Remove all powerups if gameOver
            PlayerPrefs.SetInt("Powerup_WitchHat", 0);
            PlayerPrefs.SetInt("Powerup_Bubble", 0);
            PlayerPrefs.SetInt("Powerup_KiwiBelt", 0);

            GameObject.Find("Canvas").transform.Find("PauseButton").gameObject.SetActive(false);
            canMove = false;
            resultScreen.SetActive(true);
            Time.timeScale = 0f;
            if (PlayerPrefs.GetInt("HighScore") < (int)scoreUI.float_score)
            {
                PlayerPrefs.SetInt("HighScore", (int)scoreUI.float_score);
            }
            if (scoreText != null)
            {
                scoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
            }
            gotGameOver = true;
        }
        if (gotGameOver && !adDone && !isInTutorial)
        {
            Time.timeScale = 0;
        }
            

        if (!canMove)
        {
            return;
        }

        //Kiwi's horizontal movement
        if (Input.GetKey(KeyCode.D) || rightArrow || (allowTiltControls && Input.acceleration.x > 0.5f))
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(currentXpoint + xOffset, transform.position.y, transform.position.z), horizontalOffsetSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A) || leftArrow || (allowTiltControls && Input.acceleration.x < -0.5f))
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
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -90), Time.deltaTime * rotationSpeed * 4);
        }

        //Velocity Limit
        if (isTouchingWind && rb.velocity.y > maxPositiveVelocityWithWind)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(rb.velocity.y, maxPositiveVelocityWithWind, 0.1f));
        }
        else if (!isTouchingWind && rb.velocity.y > maxPositiveVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(rb.velocity.y, maxPositiveVelocity, 0.2f));
        }
        else if (rb.velocity.y < maxNegativeVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(rb.velocity.y, maxNegativeVelocity, 0.2f));
        }

        //Heat Effect
        if (heatMeter >= tempheatMeter) GameOver();
        if (isTouchingLightRay) heatMeter += Time.deltaTime * 2.5f;
        else heatMeter -= Time.deltaTime / 1.5f;
        if (heatMeterMeterUI != null)
        {
            if (heatMeter < 0)
            {
                heatMeterMeterUI.SetActive(false);
                heatMeter = 0;
            }
            else
            {
                heatMeterMeterUI.SetActive(true);
            }
        }

        //Wet Effect
        if (isWet)
        {
            wetTimer -= Time.deltaTime;
            if (wetTimer <= 0) 
            { 
                isWet = false; 
                wetEffect.Stop();
                maxNegativeVelocity = originalMaxNegativeVelocity;
            }
        }

        //Determining the movement Type:
        if (Time.timeScale < 0.01f)
            return;

        if (isWitch) { movementType = 3; }
        else if (isSwimming) {movementType = 4; }
        else if (canJump) { movementType = 1; }
        else { movementType = 2; }

        if (infiniteFlightMeter)
            flightMeter = 100;

        switch (movementType)
        {
            case (1): //Kiwi is on land
                if (flightMeter < tempflightMeter)
                {
                    flightMeter += Time.deltaTime * 15f;
                }
                if ((Input.GetKey(KeyCode.W) || upArrow))
                {
                    rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                    if (Input.GetKeyDown(KeyCode.W) || upArrow)
                    {
                        windEffectParticle.Play();
                        audioSource.time = 0f;
                        audioSource.clip = jumpSound;
                        audioSource.Play();
                    }
                }
                break;
            case (2):
                //Flying controls - Gliding
                maxNegativeVelocity = originalMaxNegativeVelocity;
                if ((Input.GetKey(KeyCode.W) || upArrow) && flightMeter > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(rb.velocity.y, 0, Time.deltaTime));
                    if (isTouchingWind)
                    {
                        rb.gravityScale = -(originalGravityScale * 1.25f);
                    }
                    else
                    {
                        rb.gravityScale = -(originalGravityScale / 1.25f);
                        if (isWet)
                            flightMeter -= Time.deltaTime * flightMeterMult * 2f;
                        else
                            flightMeter -= Time.deltaTime * flightMeterMult;
                    }
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -45), Time.deltaTime * rotationSpeed);
                }
                //Flying controls - Not Gliding
                else
                {
                    if (isTouchingWind)
                    {
                        rb.gravityScale = originalGravityScale / 1.5f;
                        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -75), Time.deltaTime * rotationSpeed);
                    }
                    else
                    {
                        rb.gravityScale = originalGravityScale;
                        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -150), Time.deltaTime * rotationSpeed);
                    }
                }
                break;
            case (3): //Witch Flying
                if (Input.GetKey(KeyCode.W) || upArrow)
                {
                    rb.velocity = new Vector2(0, Mathf.Lerp(rb.velocity.y, kiwiWitchYSpeed, 0.1f));
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -45), Time.deltaTime * rotationSpeed);
                }
                else if (Input.GetKey(KeyCode.S) || downArrow)
                {
                    rb.velocity = new Vector2(0, Mathf.Lerp(rb.velocity.y, -kiwiWitchYSpeed, 0.1f));
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -135), Time.deltaTime * rotationSpeed);
                }
                else
                {
                    rb.velocity = new Vector2(0, Mathf.Lerp(rb.velocity.y, 0, 0.1f));
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -90), Time.deltaTime * rotationSpeed * 4);
                }
                rb.gravityScale = 0;
                break;
            case (4): //Swimming
                maxNegativeVelocity = maxNegativeVelocityInWater;
                
                if ((Input.GetKey(KeyCode.W) || upArrow) && flightMeter > 0)
                {
                    flightMeter -= Time.deltaTime * 0.75f;
                    rb.gravityScale = -(originalGravityScale * 0.5f);
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -60), Time.deltaTime * rotationSpeed);
                }
                else
                {
                    flightMeter -= Time.deltaTime * 0.5f;
                    if (flightMeter <= 0)
                        rb.gravityScale = originalGravityScale;
                    else
                        rb.gravityScale = originalGravityScale * 0.5f;
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -120), Time.deltaTime * rotationSpeed);
                }

                break;
            default:
                break;
        }
    }

    public void holdLeft()
    {
        leftArrow = true;
    }
    public void holdRight()
    {
        rightArrow = true;
    }
    public void holdUp()
    {
        upArrow = true;
    }
    public void holdDown()
    {
        downArrow = true;
    }
    public void letgoLeft()
    {
        leftArrow = false;
    }
    public void letgoRight()
    {
        rightArrow = false;
    }
    public void letgoUp()
    {
        upArrow = false;
    }
    public void letgoDown()
    {
        downArrow = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "KiwiFruit")
        {
            flightMeter += 2.5f;
            if (flightMeter > tempflightMeter)
            {
                flightMeter = tempflightMeter;
            }
        }
        if (collision.tag == "Ray")
        {
            rayEffect.Play();
            audioSource.time = 0f;
            audioSource.clip = burnSound;
            audioSource.Play();
        }
        if (collision.tag == "WaterWave")
        {
            canJump = false;
            isLanding = false;
            isSwimming = true;

            splashEffect.Play();
            windEffectParticle.Stop();
            bubbleTrailEffect.Play();

            audioSource.time = 0.1f;
            audioSource.clip = splashSound;
            audioSource.Play();
        }
        if (collision.tag == "Ground" || collision.tag == "BounceCloud")
        {
            isLanding = true;
        }
        else if (collision.tag == "Wind")
        {
            isTouchingWind = true;
        }
        if (collision.tag == "DeathZone" || collision.tag == "Enemies" || collision.tag == "Meteors"
            || collision.tag == "FireWave")
        {
            Debug.Log("Trigger: Kiwi was killed by " + collision.name);
            GameOver();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ray")
        {
            isTouchingLightRay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "WaterWave")
        {
            splashEffect.Play();
            isSwimming = false;
            windEffectParticle.Play();
            bubbleTrailEffect.Stop();

            //Im wet 
            isWet = true;
            wetTimer = tempWetTimer;
            maxNegativeVelocity = heavierMaxNegativeVelocity;
            wetEffect.Play();

            audioSource.time = 0.1f;
            audioSource.clip = splashSound;
            audioSource.Play();
        }
        if (collision.tag == "Ray")
        {
            isTouchingLightRay = false;
            rayEffect.Stop();
        }
        if (collision.tag == "Ground" || collision.tag == "BounceCloud")
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
            windEffectParticle.Stop();
            if (collision.gameObject.name.Contains("Basin_Full"))
            {
                heatMeter = 0;
                splashEffect.Play();
                audioSource.time = 0.1f;
                audioSource.clip = splashSound;
                audioSource.Play();
            }
        }
        if (collision.gameObject.tag == "BounceCloud")
        {
            flightMeter += 2.5f;
            rb.AddForce(transform.up * bounceForce);
            audioSource.clip = bounceSound;
            audioSource.Play();
        }
        if (collision.gameObject.tag == "Enemies" || collision.gameObject.tag == "DeathZone")
        {
            Debug.Log("Collision: Kiwi was killed by " + collision.gameObject.name);
            GameOver();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            windEffectParticle.Play();
            canJump = false;

            if (collision.gameObject.name.Contains("Basin_Full"))
            {
                splashEffect.Play();
                audioSource.time = 0.1f;
                audioSource.clip = splashSound;
                audioSource.Play();
            }
        }
    }

    public void GameOver()
    {
        if (isGameOver)
            return;

        if (hasBubble)
        {
            if (!playHurtSoundOnce)
            {
                audioSource.clip = kiwiHurtSound;
                audioSource.Play();
                playHurtSoundOnce = true;
            }

            PlayerPrefs.SetInt("Powerup_Bubble", 0);
            lostBubble = true;
            bubble.SetActive(false);

            if (!isWitch)
            {
                if (HeadSkin.GetComponent<SpriteRenderer>().sprite != null)
                    blinkObjForSkin.sprite = HeadSkin.GetComponent<SpriteRenderer>();
                else if (OutfitSkin.GetComponent<SpriteRenderer>().sprite != null)
                    blinkObjForSkin.sprite = OutfitSkin.GetComponent<SpriteRenderer>();
                
                blinkObj.sprite = normalSprite;
            }
            else
            {
                blinkObj.sprite = normalSprite;
                blinkObjForSkin.sprite = witchSprite;
            }
            blinkObj.startBlinking = true;
            blinkObj.numOfIFrames = iFrame;
            blinkObjForSkin.startBlinking = true;
            blinkObjForSkin.numOfIFrames = iFrame;
            flightMeter = tempflightMeter;
            heatMeter = 0;
        }
        else
        {
            audioSource.clip = gameOverSound;
            audioSource.Play();
            musicSource.Stop();
            Debug.Log("Game Over");
            isGameOver = true;
        }
    }

    public IEnumerator HeatMeter()
    {
        heatMeterMeterUI.SetActive(true);
        yield return new WaitForSeconds(1);
        heatMeterMeterUI.SetActive(false);
    }

    public IEnumerator BG_Rays()
    {
        bgSunRays.SetActive(true);
        yield return new WaitForSeconds(5);
        heatMeterMeterUI.SetActive(false);
    }
}
