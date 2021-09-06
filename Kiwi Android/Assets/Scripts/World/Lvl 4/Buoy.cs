using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoy : MonoBehaviour
{
    [Header("Particle Effects")]
    public ParticleSystem splashEffect;

    [Header("Audio Source")]
    public AudioSource audioSource;
    public AudioClip splashSound;

    private Rigidbody2D rb;
    public Vector3 originalRotation;
    public float rotationSpeed = 1f;
    public float rotationAngleLimit = 45f;
    public float lifeTime;

    public GameObject waterSurface;
    public bool touchedWaterOnce;
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;
    private float originalGrav;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = Random.Range(0.25f, 0.75f);
        originalGrav = rb.gravityScale;
        touchedWaterOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (waterSurface == null) return;
        if (waterSurface.activeSelf == false) return;
        if (Lvl4_Wave.wavePhase == 3) return;
        if (AI_Dir_Generic.currentLevel != 4) return;

        /*
        if (transform.position.y < waterSurface.transform.position.y)
        {
            float displacementMultipler = Mathf.Clamp01(-transform.position.y / depthBeforeSubmerged) * displacementAmount;
            rb.AddForce(new Vector2(0, Mathf.Abs(Physics.gravity.y) * displacementMultipler), ForceMode2D.Force);
        }*/

        lifeTime += Time.deltaTime;
        transform.eulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 
            Mathf.Sin(lifeTime * rotationSpeed) * rotationAngleLimit);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "WaterWave")
        {
            rb.gravityScale = -originalGrav;
            if (!touchedWaterOnce)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                touchedWaterOnce = true;
            }

            waterSurface = collision.transform.Find("Surface").gameObject;
            /*
            splashEffect.Play();
            audioSource.time = 0.15f;
            audioSource.clip = splashSound;
            audioSource.Play();
            */
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "WaterWave")
        {
            rb.gravityScale = originalGrav;
            /*
            splashEffect.Play();
            audioSource.time = 0.15f;
            audioSource.clip = splashSound;
            audioSource.Play();
            */
        }
    }
}
