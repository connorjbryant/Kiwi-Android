using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemies : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource audioSource;

    public int health = 1;
    //public LayerMask planeObject; //Die when hits plane

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (health <= 0)
        {
            DestroyItself();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "KiwiWeapon")
        {
            health--;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Plane"))
        {
            DestroyItself();
        }
    }
    public void DestroyItself()
    {
        Destroy(gameObject, 1 * Time.deltaTime);
    }

    public void PlayAudioClip(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void PlayAudioClip(AudioClip audioClip, float time)
    {
        audioSource.time = time;
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
