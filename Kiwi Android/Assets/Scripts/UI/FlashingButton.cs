using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingButton : MonoBehaviour
{
    public float flashSpeed = 0.025f;
    public bool is_Cyan_Flash;
    public Image image;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_Cyan_Flash)
        {
            time += Time.deltaTime * flashSpeed;
            image.color = new Color(Mathf.Abs(Mathf.Sin(time * 180 / Mathf.PI)) * 1, 1, 1, 1);
        }
    }
}
