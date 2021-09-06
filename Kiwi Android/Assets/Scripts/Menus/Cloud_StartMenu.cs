using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud_StartMenu : MonoBehaviour
{
    private float cloudSpeed;
    public float LifeTime;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = transform.localScale * Random.Range(1f, 2f);
        cloudSpeed = Random.Range(7.5f, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(cloudSpeed * Time.deltaTime, 0 ,0);

        //Destroy after a while
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
