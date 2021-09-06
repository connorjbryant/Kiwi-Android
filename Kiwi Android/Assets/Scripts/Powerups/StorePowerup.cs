using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePowerup : MonoBehaviour
{
    public int costOfWitchHat = 1000;
    public int costOfBubble = 250;
    public int costOfBelt = 50;

    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip purchaseSound;
    public AudioClip notEnoughCoins;

    int numberOfCoins;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableWitchHat()
    {
        if (PlayerPrefs.GetInt("Powerup_WitchHat") == 0)
        {
            numberOfCoins = PlayerPrefs.GetInt("numCoins");
            if (numberOfCoins >= costOfWitchHat)
            {
                audioSource.clip = purchaseSound;
                audioSource.Play();

                numberOfCoins -= costOfWitchHat;
                PlayerPrefs.SetInt("numCoins", numberOfCoins);
                PlayerPrefs.SetInt("Powerup_WitchHat", 1);
            }
            else
            {
                audioSource.clip = notEnoughCoins;
                audioSource.Play();
            }
        }
    }

    public void EnableBubble()
    {
        if (PlayerPrefs.GetInt("Powerup_Bubble") == 0)
        {
            numberOfCoins = PlayerPrefs.GetInt("numCoins");
            if (numberOfCoins >= costOfBubble)
            {
                audioSource.clip = purchaseSound;
                audioSource.Play();

                numberOfCoins -= costOfBubble;
                PlayerPrefs.SetInt("numCoins", numberOfCoins);
                PlayerPrefs.SetInt("Powerup_Bubble", 1);
            }
            else
            {
                audioSource.clip = notEnoughCoins;
                audioSource.Play();
            }
        }
    }

    public void EnableKiwiBelt()
    {
        if (PlayerPrefs.GetInt("Powerup_KiwiBelt") == 0)
        {
            numberOfCoins = PlayerPrefs.GetInt("numCoins");
            if (numberOfCoins > costOfBelt)
            {
                audioSource.clip = purchaseSound;
                audioSource.Play();

                numberOfCoins -= costOfBelt;
                PlayerPrefs.SetInt("numCoins", numberOfCoins);
                PlayerPrefs.SetInt("Powerup_KiwiBelt", 1);
            }
            if (numberOfCoins < costOfBelt)
            {
                audioSource.clip = notEnoughCoins;
                audioSource.Play();
            }
        }
    }
}
