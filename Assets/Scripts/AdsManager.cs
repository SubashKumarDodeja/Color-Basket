using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    [SerializeField]
    string AppId;
    [SerializeField]
    string Interstitial;
    [SerializeField]
    string BigBanner;
    [SerializeField]
    string SmartBanner;
    [SerializeField]
    string Rewarded;


    public static AdsManager Instance;
    private BannerView bannerView;
    private InterstitialAd interstitial;
    private BannerView smartBannerView;
    private RewardedAd rewardedAd;

    
  

    private void Awake()
	{
    

		if (Instance == null)
		{
			Instance = this;
		}

		else if (Instance != this)
		{
			Destroy(gameObject);
		}


		DontDestroyOnLoad(gameObject);
	}
	private void Start()
    {
        MobileAds.Initialize(initStatus => { });

        ReqInterstitial();
        RequestRewardedAd();


    }
    public void ShowBanner()
    {
        #if UNITY_ANDROID
                string adUnitId = BigBanner;
        #elif UNITY_IPHONE
                    string adUnitId = "ca-app-pub-3940256099942544/2934735716";
        #else
                    string adUnitId = "unexpected_platform";
        #endif

                // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);


        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);

    }


    public void HideBanner()
    {
        this.bannerView.Hide();
    }


    public void ShowSmartBanner()
    {
        #if UNITY_ANDROID
                string adUnitId = BigBanner;
        #elif UNITY_IPHONE
                            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
        #else
                            string adUnitId = "unexpected_platform";
        #endif

        
        this.smartBannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);
        this.smartBannerView.OnAdLoaded += this.HandleOnAdLoaded;
        this.smartBannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.smartBannerView.LoadAd(request);

    }
    public void HideSmartBanner()
    {
        this.smartBannerView.Hide();
    }

    public void ReqInterstitial()
    {
        #if UNITY_ANDROID
        string adUnitId = Interstitial;
        #elif UNITY_IPHONE
                string adUnitId = "ca-app-pub-3940256099942544/4411468910";
        #else
                string adUnitId = "unexpected_platform";
        #endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

       

        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;



        AdRequest request = new AdRequest.Builder().Build();
     
        this.interstitial.LoadAd(request);
    }


    public void ShowInterstitialAd()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        } 
        else
        {
            this.ReqInterstitial();
        }

    }

    public void RequestRewardedAd()
    {
        this.rewardedAd = new RewardedAd(Rewarded);
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);

        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
    }
    public void ShowRewardAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        } else
        {

            this.RequestRewardedAd();
        }
 
    }
    #region Handle
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        ReqInterstitial();
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
        RequestRewardedAd();

    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);


        GameManager gm = FindObjectOfType<GameManager>();
        gm.Reward();

    }

    #endregion
}
