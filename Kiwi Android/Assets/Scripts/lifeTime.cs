using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeTime : MonoBehaviour
{
    public float life_Time = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        life_Time -= Time.deltaTime;
        if (life_Time <= 0)
        {
            Destroy(gameObject);
        }
    }
}
