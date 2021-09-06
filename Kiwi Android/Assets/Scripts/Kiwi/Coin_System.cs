using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_System : MonoBehaviour
{
    public int numOfCoins;
    public ParticleSystem coinPickUpEffect;
    public ScoreUI scoreUI;
    private AudioSource audioSource;
    public AudioClip coinClip;

    public void Start()
    {
        numOfCoins = PlayerPrefs.GetInt("numCoins");
        audioSource = GetComponent<AudioSource>();

        //Testing Coins:
        if (PlayerPrefs.GetInt("GotInitialCoins") == 0)
        {
            PlayerPrefs.SetInt("numCoins", 100);
            PlayerPrefs.SetInt("GotInitialCoins", 1);
        }
        //PlayerPrefs.SetInt("numCoins", 1000);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            audioSource.time = 0f;
            audioSource.clip = coinClip;
            audioSource.Play();
            scoreUI.float_score += scoreUI.CoinPoints;
            numOfCoins++;
            PlayerPrefs.SetInt("numCoins", numOfCoins);
            PlayerPrefs.Save();
            coinPickUpEffect.Play();
            Destroy(collision.gameObject);
        }
    }

    public void BuyCoins(int numOfCoins)
    {
        //Open In-App Purchases
        //Decrease dollars 

        PlayerPrefs.SetInt("numCoins", PlayerPrefs.GetInt("numCoins") + numOfCoins);
    }
}
