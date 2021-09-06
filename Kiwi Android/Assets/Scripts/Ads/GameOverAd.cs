using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverAd : MonoBehaviour
{
    //public GameObject gameOverAdPopUp;
    public PlayerMove move;
    public int numberOfDeathsTillAd = 3;
    public AdManager adManager;

    // Start is called before the first frame update
    void Start()
    {
        //Ad Manager
        //AdManager.instance.RequestInterstitial();
        adManager = GetComponent<AdManager>();
        adManager.RequestInterstitial();
        move = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
    }

    void Update()
    {
        /*
        if(PlayerMove.instance.isGameOver == true)
        {
            AdManager.instance.ShowInterstitial();
            //gameOverAdPopUp.SetActive(true);
        }
        */

        if (move.isGameOver == true)
        {
            //AdManager.instance.ShowInterstitial();
            adManager.ShowInterstitial();
        }
    }

    // Update is called once per frame
    void gameOverAd()
    {
        AdManager.instance.ShowInterstitial();
    }
}
