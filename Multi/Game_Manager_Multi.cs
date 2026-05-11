using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using GoogleMobileAds;
public class Game_Option_Multi
{
    public static float time;               // 플레이어 설정 값
    public static int minballnum;           // 플레이어 설정 값
    public static int score_1p;             // 초기 0
    public static int score_2p;             // 초기 0
    public static int count;                // 초기 0
    public static bool getball1p=true;      // true
    public static bool gamefinish;
}
public class Game_Manager_Multi : MonoBehaviour
{
    public GameObject wineffect;
    public GameObject resultscreen;
    public Text result1p;
    public Text result2p;
    public Text timer_text;
    public Text countdown_text;
    public GameObject ball;
    public AudioClip[] sound;
    AudioSource ase;
    Coroutine ballcreate;
    Coroutine timer;
    public InterstitialAd fullad;
    IEnumerator StartGame()
    {
        Game_Option_Multi.gamefinish = false;
        timer_text.text = Game_Option_Multi.time.ToString();
        ase.PlayOneShot(sound[1]);
        yield return new WaitForSeconds(1.0f);
        countdown_text.text = "2";
        ase.PlayOneShot(sound[1]);
        yield return new WaitForSeconds(1.0f);
        countdown_text.text = "1";
        ase.PlayOneShot(sound[1]);
        yield return new WaitForSeconds(1.0f);
        countdown_text.text = "START!";
        ase.PlayOneShot(sound[0]);
        yield return new WaitForSeconds(0.5f);
        countdown_text.gameObject.SetActive(false);
        timer = StartCoroutine(Timer());
        ballcreate = StartCoroutine(BallCreate());
        yield return new WaitForSeconds(3.0f);
    }
    void Start()
    {
        string adfullId = "ca-app-pub-2915301137740963/3316790739"; //full id
        fullad = new InterstitialAd(adfullId);
        AdRequest request = new AdRequest.Builder().Build();
        fullad.LoadAd(request);
        Game_Option_Multi.score_1p = 0;
        Game_Option_Multi.score_2p = 0;
        Game_Option_Multi.count = 0;
        ase = GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("BGM") == 1)
        {
            ase.Stop();
        }
    }
    void GameFinish()
    {
        StopCoroutine(ballcreate);
        StopCoroutine(timer);
        Game_Manager.playing = false;
        Game_Option_Multi.gamefinish = true;
        timer_text.text = "0";
    }
    void FinishGame()
    {
        if (fullad.IsLoaded())
        {
            fullad.Show();
        }
        Time.timeScale = 0;
        resultscreen.SetActive(true);
        if(Game_Option_Multi.score_1p > Game_Option_Multi.score_2p)
        {
            result1p.text = "승리!!";
            result2p.text = "패배ㅠㅠ";
        }
        else if(Game_Option_Multi.score_1p < Game_Option_Multi.score_2p)
        {
            result2p.text = "승리!!";
            result1p.text = "패배ㅠㅠ";
        }
        else
        {
            result1p.text = "무승부... 한판더?";
            result2p.text = "무승부... 한판더?";
        }
    }
    IEnumerator BallCreate()
    {
        while (true)
        {
            if (Game_Option_Multi.count < Game_Option_Multi.minballnum)
            {
                if (Game_Option_Multi.getball1p)
                {
                    Instantiate(ball, new Vector2(-11.5f, -5.3f), Quaternion.identity);
                }
                else
                {
                    Instantiate(ball, new Vector2(-11.5f, 5.3f), Quaternion.identity);
                }
                Game_Option_Multi.count++;

            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator Timer()
    {
        while (true)
        {
            Game_Option_Multi.time -= 0.1f;
            timer_text.text = Game_Option_Multi.time.ToString("N1");
            if (Game_Option_Multi.time <= 0)
            {
                GameFinish();
                timer_text.text = "0";
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}