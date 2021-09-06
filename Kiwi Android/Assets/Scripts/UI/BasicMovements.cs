using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovements : MonoBehaviour
{
    public bool willSpin;
    public bool willScaleLoop;

    public float rotationSpeed = -120f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (willSpin)
        {
            transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
        }
    }
}
