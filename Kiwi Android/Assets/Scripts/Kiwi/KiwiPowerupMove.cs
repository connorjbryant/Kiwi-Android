using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiwiPowerupMove : MonoBehaviour
{
    private int startingLevel;
    private Rigidbody2D rb;
    public float x_speed = -2f;
    public float y_speed = 5f;
    public float height = 0.75f;
    private Vector3 originalPos;

    //Water Level
    public bool touchedWaterOnce = false;
    public GameObject waterSurface;
    public AudioClip splashSound;
    public bool inWater = false;
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;
    private float newGravityScale;
    private float originalGravityScale;

    //Tutorial
    public bool isInTutorial;

    // Start is called before the first frame update
    void Start()
    {
        touchedWaterOnce = false;
        rb = GetComponent<Rigidbody2D>();
        originalGravityScale = rb.gravityScale;
        height = Random.Range(0.35f, 0.55f);
        originalPos = transform.position;
        newGravityScale = rb.gravityScale;
        startingLevel = AI_Dir_Generic.currentLevel;

        if (AI_Dir_Generic.currentLevel == 4)
        {
            CircleCollider2D[] cols = GetComponents<CircleCollider2D>();
            foreach (CircleCollider2D col in cols)
            {
                if (!col.isTrigger) col.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startingLevel == 2 || isInTutorial)
        {
            float newY = Mathf.Sin(Time.time * y_speed);
            transform.position = new Vector3(transform.position.x + x_speed * Time.deltaTime, (originalPos.y + newY) * height, transform.position.z);
        }
        else if (AI_Dir_Generic.currentLevel == 4 && inWater && transform.position.y + 0.15f < waterSurface.transform.position.y)
        {
            if (Lvl4_Wave.wavePhase == 3) return;
            float displacementMultipler = Mathf.Clamp01(-transform.position.y / depthBeforeSubmerged) * displacementAmount;
            rb.AddForce(new Vector2(0, Mathf.Abs(Physics.gravity.y) * displacementMultipler), ForceMode2D.Force);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "WaterWave")
        {
            rb.gravityScale = 0.5f;
            if (!touchedWaterOnce)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                touchedWaterOnce = true;
            }
            inWater = true;
            waterSurface = collision.transform.Find("Surface").gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "WaterWave")
        {
            rb.gravityScale = newGravityScale;
            inWater = false;
        }
    }
}
