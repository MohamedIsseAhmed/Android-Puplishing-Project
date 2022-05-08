using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GoogleMobileAds.Api;
public class MonetizationManager : MonoBehaviour
{

    public Text status;
    public static MonetizationManager instance;

    private string App_Id = "ca-app-pub-1487520234070489~5089508091";
    private string bannerAdId = "ca-app-pub-3940256099942544/2934735716";
    private string interstialId = "ca-app-pub-3940256099942544/4411468910";
    private string rewardedVideoAddID = "ca-app-pub-3940256099942544/1712485313";

    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;
    public bool isAddFinished;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {

        StartCoroutine(LoadBannerAd());
      
    }
    IEnumerator LoadBannerAd()
    {
        yield return new WaitForSeconds(1);
        ShowBannerAd();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            RequestBanner();
            ShowBannerAd();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            RequestInterstitial();
            Showinterstitial();

        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            RequestRewardVideoAd();
            ShowVideRewardVideoAdd();
        }
    }
    public void RequestBanner()
    {
        this.bannerView = new BannerView(bannerAdId, AdSize.Banner, AdPosition.Bottom);
    }
    public void ShowBannerAd()
    {
        RequestBanner();
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;

        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;

        this.bannerView.OnAdOpening += this.HandleOnAdOpened;

        this.bannerView.OnAdClosed += this.HandleOnAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        this.bannerView.LoadAd(request);
        
    }

    private void RequestInterstitial()
    {

        this.interstitial = new InterstitialAd(interstialId);

        this.interstitial.OnAdLoaded += HandleOnAdLoaded;

        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;

        this.interstitial.OnAdOpening += HandleOnAdOpened;

        this.interstitial.OnAdClosed += HandleOnAdClosed;
        AdRequest request = new AdRequest.Builder().Build();

        this.interstitial.LoadAd(request);
    }

    public void Showinterstitial()
    {
        RequestInterstitial();
        if (this.interstitial.IsLoaded())
        {
            print("intersitial Ad");
            this.interstitial.Show();
        }
    }
    public void RequestRewardVideoAd()
    {
        rewardedAd = new RewardedAd(rewardedVideoAddID);


        AdRequest request = new AdRequest.Builder().Build();

        this.rewardedAd.LoadAd(request);
    }
    public void ShowVideRewardVideoAdd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            print("Rewarded Ad");
            this.rewardedAd.Show();
        }
    }
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        //status.text = "HandleAdLoaded event received";
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        status.text = "HandleFailedToReceiveAd event received with message";
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message:");
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        //status.text = "HandleAdOpened event received";
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        //status.text = "HandleAdClosed event received";
        MonoBehaviour.print("HandleAdClosed event received");
        isAddFinished = true;
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        //status.text = "HandleAdLeavingApplication event received";
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }
}
