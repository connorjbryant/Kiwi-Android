using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    private BoxCollider2D colliderBox;
    private Rigidbody2D rigidBody;
    private float width;
    public float scrollSpeed = -2f;

    // Start is called before the first frame update
    void Start()
    {
        colliderBox = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();

        width = colliderBox.size.x;
        colliderBox.enabled = false;

        rigidBody.velocity = new Vector3(scrollSpeed, 0, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= -width) 
        {
            Vector3 resetPosition = new Vector3(width * 2f, 0, 10);
            transform.position = (Vector3)transform.position + resetPosition;
        }
    }
}
