using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverEnemy : BaseEnemies
{
    public float x_speed = -2f;
    public float y_speed = 5f;
    public float height = 0.5f;
    private Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        height = Random.Range(0.35f, 0.55f);
        originalPos = transform.position;
        x_speed = StaticBaseVars.enemySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            DestroyItself();
        }
        float newY = Mathf.Sin(Time.time * y_speed);
        transform.position = new Vector3(transform.position.x + x_speed * Time.deltaTime, (originalPos.y + newY) * height, transform.position.z);
    }
}
