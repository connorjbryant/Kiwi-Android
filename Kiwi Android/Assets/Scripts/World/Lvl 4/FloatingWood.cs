using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingWood : MonoBehaviour
{
    [Header("Swimming")]
    private Rigidbody2D rb;
    public GameObject waterWaveSurface;
    public bool inWater;
    private float originalGrav;
    public bool touchedWaterOnce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGrav = rb.gravityScale;
        touchedWaterOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (waterWaveSurface == null) { return; }
        transform.position = Vector2.Lerp(transform.position, 
            new Vector2(transform.position.x, waterWaveSurface.transform.position.y), 
            Time.deltaTime / 2);
        */
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "WaterWave")
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            /*
            inWater = true;
            rb.gravityScale = -originalGrav;
            if (!touchedWaterOnce)
            {
                waterWaveSurface = collision.transform.Find("Surface").gameObject;
                touchedWaterOnce = true;
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
            */
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "WaterWave")
        {
            rb.gravityScale = originalGrav;
            //inWater = false;
        }
    }
}
