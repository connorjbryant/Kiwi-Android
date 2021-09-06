using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWave : MonoBehaviour
{
    public GameObject fire;
    public Transform startPoint;
    public Transform endPoint;
    public float speedToEndPoint;
    public float height;
    public bool reachedEndPoint;
    public bool isWaveMoving;
    public bool isWaveGoingUp;

    public AudioSource audioSource;
    public AudioClip fireWaveSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        speedToEndPoint = StaticBaseVars.y_speed;
        height = StaticBaseVars.y_height;

        startPoint.position = fire.transform.position;
        if (isWaveGoingUp)
        {
            endPoint.position = new Vector3(startPoint.position.x,
                startPoint.position.y + height, startPoint.position.z);
        }
        else
        {
            endPoint.position = new Vector3(startPoint.position.x,
                startPoint.position.y - height, startPoint.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaveMoving && !reachedEndPoint)
        {
            fire.transform.position = Vector2.Lerp(fire.transform.position, endPoint.position, speedToEndPoint * Time.deltaTime);
            if (Vector2.Distance(fire.transform.position, endPoint.position) <= 0.01f)
            {
                reachedEndPoint = true;
            }
        }
        else if (isWaveMoving && reachedEndPoint)
        {
            fire.transform.position = Vector2.Lerp(fire.transform.position, startPoint.position, speedToEndPoint * Time.deltaTime);
            if (Vector2.Distance(fire.transform.position, startPoint.position) <= 0.01f)
            {
                isWaveMoving = false;
                reachedEndPoint = false;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FireWaveStarter")
        {
            reachedEndPoint = false;
            isWaveMoving = true;
            audioSource.clip = fireWaveSound;
            audioSource.Play();
        }    
    }
}
