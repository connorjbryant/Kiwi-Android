using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kiwi_StartMenu : MonoBehaviour
{
    private float rotationSpeed;
    private float fallSpeed;
    private float scaleSpeed;
    private float scaleLimit;
    private Rigidbody2D rb;
    private Vector2 originalScale;

    public float LifeTime;

    //MiniGame
    private StartMenuAnimated startAi;
    private AudioSource audioSource;
    public AudioClip popSound;
    public AudioClip gameOverSound;
    

    // Start is called before the first frame update
    void Start()
    {
        startAi = GameObject.Find("Animation_AI").gameObject.GetComponent<StartMenuAnimated>();
        if (Random.Range(0, 2) == 0)
        {
            rotationSpeed = Random.Range(-120f, -60f);
        }
        else
        {
            rotationSpeed = Random.Range(60f, 120f);
        }
        int currentScore = startAi.score;
        fallSpeed = Random.Range(1f + currentScore/25, 2f + currentScore/25);
        scaleSpeed = Random.Range(1f + currentScore/25, 3f + currentScore/25);
        scaleLimit = Random.Range(2.5f, 4.5f);
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Spinning
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        //Falling
        rb.gravityScale = fallSpeed;

        //Loop Scale Size
        transform.localScale = new Vector3(originalScale.x + Mathf.Sin(scaleSpeed * Time.time) / scaleLimit,
            originalScale.y + Mathf.Sin(scaleSpeed * Time.time) / scaleLimit, 1);

        //Destroy after a while
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnMouseDown()
    {
        startAi.score++;
        startAi.StartMiniGame = true;
        audioSource.clip = popSound;
        audioSource.Play();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        Destroy(gameObject,1);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            startAi.score = 0;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            Destroy(gameObject,1);
        }
    }
}
