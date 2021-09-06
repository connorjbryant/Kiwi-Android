using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingEffect : MonoBehaviour
{
    public bool startBlinking;
    public float numOfIFrames = 5f;
    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (startBlinking)
        {
            numOfIFrames -= Time.deltaTime;
            if (numOfIFrames > 0)
            {
                if ((numOfIFrames * 100) % 2 >= 0 && (numOfIFrames * 100) % 2 <= 1f)
                {
                    sprite.enabled = false;
                }
                else
                {
                    sprite.enabled = true;
                }
            }
            else
            {
                sprite.enabled = true;
                startBlinking = false;
            }
        }
    }
}
