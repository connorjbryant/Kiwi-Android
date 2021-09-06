using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

public class AdManager : MonoBehaviour
{
    public DressingRoom dressing;
    private BannerView bannerAd;
    private InterstitialAd interstitial;
    RewardedAd rewardAd;
    string rewardId;
    public bool ad = true;

    public static AdManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        dressing = FindObjectOfType<DressingRoom>();
        MobileAds.Initialize(InitializationStatus => { });
        // Get singleton reward based video ad reference
        RequestRewardedAd();
        //this.RequestBanner();

        //Remove Ad function
        if (PlayerPrefs.GetInt("DisabledAds") == 1)
        {
            gameObject.SetActive(false);
        }

        //Show Ad function
        if (PlayerPrefs.GetInt("DisabledAds") == 0)
        {
            gameObject.SetActive(true);
        }
    }

    void RequestRewardedAd()
    {
        #if UNITY_ANDROID
                rewardId = "ca-app-pub-9500067308309897/9236159050";
#elif UNITY_IPHONE
                rewardId = "ca-app-pub-9500067308309897/9236159050";
#else
                rewardId = null;
#endif

        rewardAd = new RewardedAd(rewardId);

        //call events
        rewardAd.OnAdLoaded += HandleRewardAdLoaded;
        //rewardAd.OnAdFailedToLoad += HandleRewardAdFailedToLoad;
        rewardAd.OnAdOpening += HandleRewardAdOpening;
        rewardAd.OnAdFailedToShow += HandleRewardAdFailedToShow;
        rewardAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardAd.OnAdClosed += HandleRewardAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        rewardAd.LoadAd(request); //load & show the banner ad
    }

    //attach to a button that plays ad if ready
    public void ShowRewardedAd()
    {
        if (rewardAd.IsLoaded())
        {
            rewardAd.Show();
        }
    }

    //call events
    public void HandleRewardAdLoaded(object sender, EventArgs args)
    {
        //do this when ad loads
    }

    public void HandleRewardAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        //do this when ad fails to loads
        Debug.Log("Ad failed to load" + args.Message);
    }

    public void HandleRewardAdOpening(object sender, EventArgs args)
    {
        //do this when ad is opening
    }

    public void HandleRewardAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        //do this when ad fails to show
    }

    public void HandleUserEarnedReward(object sender, EventArgs args)
    {
        //reward the player here
        //RevivePlayer();
    }

    public void HandleRewardAdClosed(object sender, EventArgs args)
    {
        //do this when ad is closed
        RequestRewardedAd();

        //Set a playerpref here so players dont need to watch it again
        ad = false;
        Debug.Log("Tourist selected");
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    private void RequestBanner()
    {
        string adUnitId = "ca-app-pub-9500067308309897/2309780325";
        this.bannerAd = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
        this.bannerAd.LoadAd(this.CreateAdRequest());
    }

    public void RequestInterstitial()
    {
        string adUnitId = "ca-app-pub-9500067308309897/4805959458";
        // Clean up interstitial ad before creating a new one.
        if (this.interstitial != null)
            this.interstitial.Destroy();

        // Create an interstitial.
        this.interstitial = new InterstitialAd(adUnitId);

        //Load an interstitial ad.
        this.interstitial.LoadAd(this.CreateAdRequest());
    }

    public void ShowInterstitial()
    {
        if(this.interstitial.IsLoaded())
        {
            interstitial.Show();
        }
        else
        {
            Debug.Log("Interstitial Ad is not ready yet");
        }
    }

}
