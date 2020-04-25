using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.Advertisements; 

public class GoogleAds : MonoBehaviour
{
    // Dành cho quảng cáo google 
    private string appId = "ca-app-pub-9129937445162354~4582236201";
    private string rewardAdsId = "ca-app-pub-4515972558419147/2693063715";
    private string interstitalId = "ca-app-pub-4515972558419147/8360500978";
    private RewardBasedVideoAd rewardBasedVideo;
    private InterstitialAd interstitial;

    // Start is called before the first frame update
    void Start()
    {
        // Unity advertises
        #region 
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("3123469", testMode: false);
            
        }
        #endregion
        MobileAds.Initialize(appId);
       

        this.rewardBasedVideo = RewardBasedVideoAd.Instance;
        this.RequestRewardBasedVideo(); 
        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

        // Khởi tạo quảng cáo xen kẽ 
        PrepareInterestitialAd();
        StartCoroutine(Adss());
    }
    //Tạo luồng quảng cáo unity
   
    IEnumerator Adss()
    {
        for (int i = 0; i < 1000; i++)
        {
            // Load the rewarded video ad with the request.
            if (!this.rewardBasedVideo.IsLoaded())
            {
                AdRequest request1 = new AdRequest.Builder().Build();
                this.rewardBasedVideo.LoadAd(request1, rewardAdsId);
            }

            // Load the interstitial with the request.
            if (!this.interstitial.IsLoaded())
            {
                AdRequest request = new AdRequest.Builder().Build();
                this.interstitial.LoadAd(request);
            }      
            yield return new WaitForSeconds(5);
        }
    }
    private void RequestRewardBasedVideo()
    {
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, rewardAdsId);
    }

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        this.RequestRewardBasedVideo(); 
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
       
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().AdReward();
       // this.RequestRewardBasedVideo(); 
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }
    // quangr caos thuong 
    public void ShowAd()
    {
        
            rewardBasedVideo.Show(); 
        
       // this.RequestRewardBasedVideo();
 
    }

    // Phần này dành cho quảng cáo xen kẽ 
    #region
    public void PrepareInterestitialAd()
    {
        this.interstitial = new InterstitialAd(interstitalId);
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;
    }
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
        this.interstitial.Destroy();
        this.interstitial = new InterstitialAd(interstitalId);
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }
    #endregion
    public void ShowinterstitialAds()
    {
        if(this.interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }
    // phần này dành cho quảng cáo của Unity 
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().AdReward();

                break;
            case ShowResult.Skipped:

                break;
            case ShowResult.Failed:

                break;
        }
    }
    
    public void ShowUnityAds()
    {
       
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options); 
        
    }
}

