using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public PlayerMove move;

    public GameObject KiwiFruit;
    public int numKiwiAmmo;
    public int maxNumKiwiAmmo;
    public float kiwiBulletSpeed;

    public float fireRate;
    private float tempFireRate;
    public bool shoot;

    public Transform shootPoint; //where to spawn the kiwi gun

    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip[] throwSounds;
    public AudioClip addedKiwi;
    public AudioClip maxKiwi;

    public GameObject three_KiwiBelt_0;
    public GameObject three_KiwiBelt_1;
    public GameObject three_KiwiBelt_2;
    public GameObject three_KiwiBelt_3;
    public GameObject four_KiwiBelt_0;
    public GameObject four_KiwiBelt_1;
    public GameObject four_KiwiBelt_2;
    public GameObject four_KiwiBelt_3;
    public GameObject four_KiwiBelt_4;
    public bool hasKiwiBelt;


    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<PlayerMove>();
        tempFireRate = fireRate;
        audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("Powerup_KiwiBelt") == 1)
        {
            hasKiwiBelt = true;
            numKiwiAmmo = 4;
        }
        else
        {
            numKiwiAmmo = 3;
        }
        maxNumKiwiAmmo = numKiwiAmmo;
        changeAmmo();
        shoot = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (fireRate > 0)
        {
            fireRate -= Time.deltaTime;
        }
        //Throw a Kiwi Fruit
        if ((Input.GetKeyDown(KeyCode.Space) || shoot) && numKiwiAmmo > 0 && fireRate < 0
            && !move.isGameOver && Time.timeScale > 0.01f)
        {
            GameObject kiwiBullet = Instantiate(KiwiFruit, shootPoint.transform.position, Quaternion.identity);
            kiwiBullet.GetComponent<KiwiBullet>().kiwiPlayer = gameObject;
            numKiwiAmmo--;
            changeAmmo();
            fireRate = tempFireRate;
        }
        shoot = false;
    }

    public void shootKiwi()
    {
        shoot = true;
    }

    private void changeAmmo()
    {
        if (four_KiwiBelt_0 == null || three_KiwiBelt_0 == null)
        {
            return;
        }
        if (hasKiwiBelt)
        {
            if (numKiwiAmmo == 4)
            {
                four_KiwiBelt_0.SetActive(false);
                four_KiwiBelt_1.SetActive(false);
                four_KiwiBelt_2.SetActive(false);
                four_KiwiBelt_3.SetActive(false);
                four_KiwiBelt_4.SetActive(true);
            }
            else if (numKiwiAmmo == 3)
            {
                four_KiwiBelt_0.SetActive(false);
                four_KiwiBelt_1.SetActive(false);
                four_KiwiBelt_2.SetActive(false);
                four_KiwiBelt_3.SetActive(true);
                four_KiwiBelt_4.SetActive(false);
            }
            else if (numKiwiAmmo == 2)
            {
                four_KiwiBelt_0.SetActive(false);
                four_KiwiBelt_1.SetActive(false);
                four_KiwiBelt_2.SetActive(true);
                four_KiwiBelt_3.SetActive(false);
                four_KiwiBelt_4.SetActive(false);
            }
            else if (numKiwiAmmo == 1)
            {
                four_KiwiBelt_0.SetActive(false);
                four_KiwiBelt_1.SetActive(true);
                four_KiwiBelt_2.SetActive(false);
                four_KiwiBelt_3.SetActive(false);
                four_KiwiBelt_4.SetActive(false);
            }
            else
            {
                four_KiwiBelt_0.SetActive(true);
                four_KiwiBelt_1.SetActive(false);
                four_KiwiBelt_2.SetActive(false);
                four_KiwiBelt_3.SetActive(false);
                four_KiwiBelt_4.SetActive(false);
            }
        }
        else 
        {
            if (numKiwiAmmo == 3)
            {
                three_KiwiBelt_0.SetActive(false);
                three_KiwiBelt_1.SetActive(false);
                three_KiwiBelt_2.SetActive(false);
                three_KiwiBelt_3.SetActive(true);
            }
            else if (numKiwiAmmo == 2)
            {
                three_KiwiBelt_0.SetActive(false);
                three_KiwiBelt_1.SetActive(false);
                three_KiwiBelt_2.SetActive(true);
                three_KiwiBelt_3.SetActive(false);
            }
            else if (numKiwiAmmo == 1)
            {
                three_KiwiBelt_0.SetActive(false);
                three_KiwiBelt_1.SetActive(true);
                three_KiwiBelt_2.SetActive(false);
                three_KiwiBelt_3.SetActive(false);
            }
            else
            {
                three_KiwiBelt_0.SetActive(true);
                three_KiwiBelt_1.SetActive(false);
                three_KiwiBelt_2.SetActive(false);
                three_KiwiBelt_3.SetActive(false);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "KiwiFruit" || collision.tag == "KiwiWeapon")
        {
            if (numKiwiAmmo == maxNumKiwiAmmo)
            {
                audioSource.time = 0f;
                audioSource.clip = maxKiwi;
                audioSource.Play();
            }
            if (numKiwiAmmo < maxNumKiwiAmmo)
            {
                audioSource.time = 0f;
                audioSource.clip = addedKiwi;
                audioSource.Play();
                numKiwiAmmo++;
                changeAmmo();
            }
           
            Destroy(collision.gameObject);
        }
    }
}
