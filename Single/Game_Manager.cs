using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GoogleMobileAds.Api;
using GoogleMobileAds;

public class Game_Manager : MonoBehaviour
{
    public GameObject block_set;             // 블록 세트
    public static int ballcount = 0;         // 현재 공의 수
    public static int level = 1;             // 현재 레벨
    public static int score = 0;             //      점수  
    int maxballcount = 0;                    // 최대 공 개수
    int highscore;                           // 최고 점수
    int highlevel;                           //      레벨
    public Text score_t;
    public static Text score_T;                     // 점수 텍스트
    public Text level_T;                     // 레벨 텍스트
    public Text highscore_T;                 // 최고 점수 텍스트
    public Text highlevel_T;                 // 최고 레벨 텍스트                            // 텍스트 UI
    public GameObject resultscreen;          // 결과 창
    GameObject temp;                         // 블록 생성 임시 객체
    public static bool playing = true;       // 플레이 중인지 확인 후 배경음악 정지    
    public InterstitialAd fullad;
    public AudioClip[] sound;
    public GameObject goodscoretext;
    AudioSource ase;
    public Text earngold;
    public Text totalgold;
    public static float timescale;
    public GameObject speedup;
    void Start()
    {
        timescale = 1.0f;
        ase = GetComponent<AudioSource>();
        string adfullId = "ca-app-pub-2915301137740963/3316790739"; //full id
        fullad = new InterstitialAd(adfullId);
        AdRequest request = new AdRequest.Builder().Build();
        fullad.LoadAd(request);
        ballcount = 0;
        maxballcount = 0;
        level = 1;
        score = 0;
        LoadData();                         // 최고 기록 출력
        playing = true;
        score_T = score_t;
        StartCoroutine(Block_making());
    }
    public static void ScoreRefresh()
    {
           score_T.text = score.ToString();                                  // 점수 출력
    }
    IEnumerator Block_making()              // 블록 생성 함수
    {
        while (true)
        {
            temp = Instantiate(block_set, transform.position, Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(3.7f);
            Destroy(temp);
            if (maxballcount < ballcount)
            {
                maxballcount = ballcount;
            }
            temp = Instantiate(block_set, transform.position, Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(3.7f);
            Destroy(temp);
            if (maxballcount < ballcount)
            {
                maxballcount = ballcount;
            }
            temp = Instantiate(block_set, transform.position, Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(3.7f);
            Destroy(temp);
            if (maxballcount < ballcount)
            {
                maxballcount = ballcount;
            }
            level++;
            level_T.text = level.ToString();
            if (level % 5 == 0)
            {
                Time.timeScale += 0.1f;
                timescale += 0.1f;
                StartCoroutine(SpeedUp());
            }
        }
    }   // 블록 생성 함수
    IEnumerator SpeedUp()
    {
        speedup.SetActive(true);
        yield return new WaitForSeconds(2.4f);
        speedup.SetActive(false);
    }
    void CheckAchievements()
    {
        int playtime = PlayerPrefs.GetInt("PlayTime");
        if (highlevel >= 50)
        {
            Social.ReportProgress("CggI_8yryScQAhAH", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAG", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAF", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAE", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAD", 100.0f, (bool success) => { });
        }
        else if (highlevel >= 40 && highlevel < 50)
        {
            Social.ReportProgress("CggI_8yryScQAhAG", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAF", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAE", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAD", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAH", 0.0f, (bool success) => { });
        }
        else if (highlevel >= 30 && highlevel < 40)
        {
            Social.ReportProgress("CggI_8yryScQAhAF", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAE", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAD", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAG", 0.0f, (bool success) => { });
        }
        else if (highlevel >= 20 && highlevel < 30)
        {
            Social.ReportProgress("CggI_8yryScQAhAE", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAD", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAF", 0.0f, (bool success) => { });
        }
        else if (highlevel >= 10 && highlevel < 20)
        {
            Social.ReportProgress("CggI_8yryScQAhAD", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAE", 0.0f, (bool success) => { });
        }       // 최고 단계
        if (maxballcount >= 5 && maxballcount < 10)
        {
            Social.ReportProgress("CggI_8yryScQAhAJ", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAK", 0.0f, (bool success) => { });

        }
        else if (maxballcount >= 10 && maxballcount < 15)
        {
            Social.ReportProgress("CggI_8yryScQAhAK", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAJ", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAL", 0.0f, (bool success) => { });

        }
        else if (maxballcount >= 15 && maxballcount < 20)
        {
            Social.ReportProgress("CggI_8yryScQAhAL", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAK", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAJ", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAM", 0.0f, (bool success) => { });

        }
        else if (maxballcount >= 20 && maxballcount < 25)
        {
            Social.ReportProgress("CggI_8yryScQAhAM", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAL", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAK", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAJ", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAN", 0.0f, (bool success) => { });

        }
        else if (maxballcount >= 25 && maxballcount < 30)
        {
            Social.ReportProgress("CggI_8yryScQAhAN", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAM", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAL", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAK", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAJ", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAO", 0.0f, (bool success) => { });

        }
        else if (maxballcount >= 30)
        {
            Social.ReportProgress("CggI_8yryScQAhAO", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAN", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAM", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAL", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAK", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAJ", 100.0f, (bool success) => { });
        }       // 최대 공 개수
        if (playtime >= 1 && playtime < 10)
        {
            Social.ReportProgress("CggI_8yryScQAhAP", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAQ", 0f, (bool success) => { });
        }
        else if (playtime >= 10 && playtime < 50)
        {
            Debug.Log(PlayerPrefs.GetInt("PlayTime"));
            Social.ReportProgress("CggI_8yryScQAhAP", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAQ", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAR", 0f, (bool success) => { });
        }
        else if (playtime >= 50 && playtime < 200)
        {
            Social.ReportProgress("CggI_8yryScQAhAP", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAQ", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAR", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAS", 0f, (bool success) => { });
        }
        else if (playtime >= 200 && playtime < 1000)
        {
            Social.ReportProgress("CggI_8yryScQAhAS", 100.0f, (bool success) => { });
            Social.ReportProgress("CggI_8yryScQAhAT", 0f, (bool success) => { });
        }
        else if (playtime >= 1000)
        {
            Social.ReportProgress("CggI_8yryScQAhAT", 100.0f, (bool success) => { });
        }       // 플레이 횟수
    }
    void SaveData()                                                           // 데이터 세이브
    {
        if (PlayerPrefs.GetInt("AdCount") == 1)
        {
            if (fullad.IsLoaded())
            {
                fullad.Show();
            }
            PlayerPrefs.SetInt("AdCount", 0);
        }
        else
        {
            PlayerPrefs.SetInt("AdCount", 1);
        }   // 2회 플레이당 광고
        if (score > highscore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highscore = score;
        }
        if (level > highlevel)
        {
            PlayerPrefs.SetInt("HighLevel", level);
            highlevel = level;
        }
        if (score > highscore || level > highlevel)
        {
            goodscoretext.SetActive(true);
        }
        else
        {
            ase.PlayOneShot(sound[0]);
        }   // 최고 점수 단계 기록
        earngold.text = (score / 50).ToString();
        PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + score / 50);
        totalgold.text = PlayerPrefs.GetInt("Gold").ToString();
        Social.ReportScore(highscore, "CggI_8yryScQAhA9", (bool success) =>
        {
        });
        Social.ReportScore(highlevel, "CggI_8yryScQAhA-", (bool success) =>
        {
        });
        level = 1;
        PlayerPrefs.SetInt("PlayTime", PlayerPrefs.GetInt("PlayTime") + 1);   // 플레이 1회 추가
        CheckAchievements();
    }
    void LoadData()
    {
        if (PlayerPrefs.GetInt("HighScore") != 0)
        {
            highscore = PlayerPrefs.GetInt("HighScore");
            highscore_T.text = highscore.ToString();
        }
        if (PlayerPrefs.GetInt("HighLevel") != 0)
        {
            highlevel = PlayerPrefs.GetInt("HighLevel");
            highlevel_T.text = highlevel.ToString();
        }
    }       // 최고기록 로드
    void GameOver()
    {
        ase.Stop();
        playing = false;
        timescale = 1f;
        Time.timeScale = 0;
        Zone_Warning.count = 0;
        resultscreen.SetActive(true);
        GetComponent<Game_System>().pausebutton.SetActive(false);
        SaveData();
    }
    public void Playing()
    {
        playing = true;
    }
    public void NotPlaying()
    {
        playing = false;
    }
}
