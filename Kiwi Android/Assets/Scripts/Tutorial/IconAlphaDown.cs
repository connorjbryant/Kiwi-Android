using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconAlphaDown : MonoBehaviour
{
    private PlayerMove move;
    private Image image;
    public float alphaFadeSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        move = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (move.isWitch)
        {
            gameObject.SetActive(false);
        }
        else
        {
            image.color -= new Color(0, 0, 0, Time.deltaTime * alphaFadeSpeed);

            if (image.color.a <= 0.005f)
                gameObject.SetActive(false);
        }
    }
}
