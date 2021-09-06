using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteors : MonoBehaviour
{
    public bool isDestroyed;
    Animator animator;
    AnimationClip animationClip;
    public AudioSource audioSource;
    public AudioClip meteorSound;
    public AudioClip explosionSound;
    public bool playClipOnce;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        isDestroyed = false;
        animator.enabled = false;
        playClipOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playClipOnce && Vector2.Distance(player.transform.position, transform.position) <= 2.5f)
        {
            audioSource.clip = meteorSound;
            audioSource.Play();
            playClipOnce = true;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "KiwiWeapon")
        {
            isDestroyed = true;
            DestroyMeteor();
        }
    }

    public void DestroyMeteor()
    {
        animator.enabled = true;
        animator.Play("MeteorBreaking");
        audioSource.clip = explosionSound;
        audioSource.Play();
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }
}
