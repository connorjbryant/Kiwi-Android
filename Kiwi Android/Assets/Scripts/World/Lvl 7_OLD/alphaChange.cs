using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alphaChange : MonoBehaviour
{
    //Apply this to objects that will be present in level 7 with sprite Renderer
    public SpriteRenderer[] sprites;
    private bool isTransparent = false;
    public float alphaChangeRate = 1.5f;
    
    private Color tmpColor;

    // Start is called before the first frame update
    void Start()
    {
        tmpColor = sprites[0].color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTransparent)
            tmpColor.a = Mathf.MoveTowards(tmpColor.a, 0, alphaChangeRate * Time.deltaTime);
        else
            tmpColor.a = Mathf.MoveTowards(tmpColor.a, 1, alphaChangeRate * Time.deltaTime * 2);

        foreach (SpriteRenderer sprite in sprites)
            sprite.color = tmpColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FadeOut")
            isTransparent = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "FadeOut")
            isTransparent = false;
    }
}
