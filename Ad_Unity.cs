using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using GoogleMobileAds;

public class Ad_Unity : MonoBehaviour
{
    RewardBasedVideoAd rewardBasedVideo;
    public InterstitialAd fullad;
    public Button AdButton;
    string text = "감사합니다.\n300G 적립 완료!";

    void Awake()
    {
        string adfullId = "ca-app-pub-2915301137740963/3316790739"; //full id
        fullad = new InterstitialAd(adfullId);
        AdRequest request = new AdRequest.Builder().Build();
        fullad.LoadAd(request);

        string rewardAdId = "ca-app-pub-2915301137740963/4837223137";
        rewardBasedVideo = RewardBasedVideoAd.Instance;
        AdRequest request2 = new AdRequest.Builder().Build();
        rewardBasedVideo.LoadAd(request, rewardAdId);
    }
    public void ShowAd()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
            rewardBasedVideo.Show();


        }
        /*if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideoZone", new ShowOptions() { resultCallback = HandleAdResult });
        }*/
    }
    void HandleRewardBasedVideoRewarded(object sender,Reward args)
{
    string type = args.Type;
    PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + 300);

    AdButton.GetComponentInChildren<Text>().text = text;

}
/*
    void HandleAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                text = "감사합니다.\n300G 적립 완료!";
                AdButton.GetComponentInChildren<Text>().text = text;
                PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + 300);
                break;
            case ShowResult.Skipped:
                text = "끝까지 봐주세요";
                AdButton.GetComponentInChildren<Text>().text = text;
                break;
            case ShowResult.Failed:
                text = "인터넷 연결이 필요합니다.";
                AdButton.GetComponentInChildren<Text>().text = text;
                break;
        }
    }
    */
}