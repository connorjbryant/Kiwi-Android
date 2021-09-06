using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hovering : MonoBehaviour
{
    public float y_height = 5f;
    public float y_speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //print("Item Hovering");
        transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(y_speed * Time.time) * y_height, transform.position.z);
    }
}
