using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScrollSpeed : MonoBehaviour
{
    public float autoScrollSpeed = -2f;
    public bool hasCustomSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (!hasCustomSpeed)
        {
            if (gameObject.tag == "Wind")
            {
                autoScrollSpeed = StaticBaseVars.windSpeed;
            }
            else if (gameObject.tag == "Enemies")
            {
                autoScrollSpeed = StaticBaseVars.enemySpeed;
            }
            else
            {
                //Obstacles
                autoScrollSpeed = StaticBaseVars.obstacleSpeed;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(autoScrollSpeed, 0, 0) * Time.deltaTime, Space.World);
        
        if (hasCustomSpeed)
        {
            autoScrollSpeed -= Time.deltaTime / 150f;
        }
    }
}
