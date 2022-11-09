namespace mainspace
{
#if admob
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.SceneManagement;


public class AdsManger : MonoBehaviour
{
    public static AdsManger instance;

    public RewardedAd rewardedAd;
    public InterstitialAd interstitial;
    public BannerView bannerView;
    AdRequest request;

    string admob_android_ban = "";
    string admob_android_bay = "";
    string admob_android_rewarded = "";

    string admob_ios_ban = "";
    string admob_ios_bay = "";
    string admob_ios_rewarded = "";

    private void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        GameData gameData = FindObjectOfType<GameData>();

        admob_android_ban = gameData.admob_android_banner;
        admob_android_bay = gameData.admob_android_Interstitial;
        admob_android_rewarded = gameData.admob_android_rewarded;

        admob_ios_ban = gameData.admob_ios_banner;
        admob_ios_bay = gameData.admob_ios_Interstitial;
        admob_ios_rewarded = gameData.admob_ios_rewarded;

        int step = PlayerPrefs.GetInt("step", 1);

        if (PlayerPrefs.GetInt("step", 1) > 0)
        {
            RequestConfiguration requestConfiguration = new RequestConfiguration.Builder()
             // .SetMaxAdContentRating(MaxAdContentRating.MA)
               .build();
            MobileAds.SetRequestConfiguration(requestConfiguration);
        }
        else
        {
            RequestConfiguration requestConfiguration = new RequestConfiguration.Builder()
              .SetMaxAdContentRating(MaxAdContentRating.T)
               .build();
            MobileAds.SetRequestConfiguration(requestConfiguration);
        }
        request = new AdRequest.Builder().Build();

        CreateAndLoadInterstitialAd();
        CreateAndLoadRewardedAd();
        RequestBanner();

        InvokeRepeating("checkAds", 60f, 60f);  //1s delay, repeat every 1s

    }

    void checkAds()
    {

        if (PlayerPrefs.GetInt("step", 1) > 0)
        {
            RequestConfiguration requestConfiguration = new RequestConfiguration.Builder()
              .SetMaxAdContentRating(MaxAdContentRating.MA)
               .build();
            MobileAds.SetRequestConfiguration(requestConfiguration);
        }
        else
        {
            RequestConfiguration requestConfiguration = new RequestConfiguration.Builder()
              .SetMaxAdContentRating(MaxAdContentRating.T)
               .build();
            MobileAds.SetRequestConfiguration(requestConfiguration);
        }
        request = new AdRequest.Builder().Build();
        if (interstitial != null && !interstitial.IsLoaded())
        {
            CreateAndLoadInterstitialAd();
        }
        if (rewardedAd != null && !rewardedAd.IsLoaded())
        {
            CreateAndLoadRewardedAd();
        }

    }


    public void RequestBanner()
    {

#if UNITY_ANDROID
        string adUnitId = admob_android_ban;
#elif UNITY_IPHONE
                string adUnitId = admob_ios_ban;
#else
                string adUnitId = "unexpected_platform";
#endif

        AdSize adSize = new AdSize(320, 50);
        bannerView = new BannerView(adUnitId, adSize, AdPosition.Bottom);

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    private void CreateAndLoadInterstitialAd()
    {//string adUnitId = "ca-app-pub-8798151215907977/3196629852";
     //string adUnitId = "ca-app-pub-3940256099942544/1033173712"; //for test
        if (this.interstitial != null && this.interstitial.IsLoaded()) return;

#if UNITY_ANDROID
        string adUnitId = admob_android_bay;
#elif UNITY_IPHONE
            string adUnitId = admob_ios_bay;
#else
            string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);
        interstitial.OnAdClosed += HandleOnAdClosed;

        // Load the interstitial with the request.
        interstitial.LoadAd(request);

    }
    public void showInterstitial()
    {

        if (interstitial.IsLoaded() && PlayerPrefs.GetInt("step", 0) > 8)
        {
            Manger_base.instance.panel_loading.SetActive(true);
            interstitial.Show();
        }
        else
        {
            SceneManager.LoadScene("Map");
        }
    }



    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        Manger_base.instance.panel_loading.SetActive(false);

        SceneManager.LoadScene("Map");
    }


    private void CreateAndLoadRewardedAd()
    {//string adUnitId = "ca-app-pub-8798151215907977/6560647103";
     //string adUnitId = "ca-app-pub-3940256099942544/5224354917"; //for test
        if (this.rewardedAd != null && this.rewardedAd.IsLoaded()) return;
#if UNITY_ANDROID
        string adUnitId = admob_android_rewarded;
#elif UNITY_IPHONE
                string adUnitId = admob_ios_rewarded;
#else
                string adUnitId = "unexpected_platform";
#endif
        this.rewardedAd = new RewardedAd(adUnitId);

        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.LoadAd(request);

    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        finshVideo();
    }

    public void showVideo()
    {
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
        else
        {
            Manger_base.instance.panel_no_video.SetActive(true);
        }
    }

    private void finshVideo()
    {
        string ActiveSceneName = SceneManager.GetActiveScene().name;
        if (ActiveSceneName == "Game1")
        {
            FindObjectOfType<Manger1>().help(10);
        }

        else if (ActiveSceneName == "Game3")
        {
            FindObjectOfType<Manger3>().showHint();

        }
        else if (ActiveSceneName == "Game4")
        {
            FindObjectOfType<Manger4>().showChar();

        }

    }
}
#else


    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class AdsManger : MonoBehaviour
    {
        public static AdsManger instance;

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); return; }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void showInterstitial()
        {

            SceneManager.LoadScene("Map");

        }

        public void showVideo()
        {
            Manger_base.instance.panel_no_video.SetActive(true);

        }
    }
#endif
}