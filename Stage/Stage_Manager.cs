using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using GoogleMobileAds;

public class Stage_Manager : MonoBehaviour
{
    public static int blocknum;
    public static int ballnum;
    public GameObject startbutton;
    public GameObject gameoverscreen;
    public GameObject gameclearscreen;
    public GameObject buttonafterresult;
    public GameObject buttonnewxtgame;
    public InterstitialAd fullad;
    public Text cleartime;
    public Text timer;
    public Text earngold;
    public Text totalgold;
    AudioSource ase;
    public AudioClip[] se;
    float time;
    bool finish = false;
    public static bool playing = true;
    public GameObject medal;
    public GameObject ball;
    int medalnum;
    int maxballcount;
    void Start()
    {
        Time.timeScale = 0;
        string adfullId = "ca-app-pub-2915301137740963/3316790739"; //full id
        fullad = new InterstitialAd(adfullId);
        AdRequest request = new AdRequest.Builder().Build();
        fullad.LoadAd(request);
        medalnum = 0;
        finish = false;
        playing = false;
        time = 0;
        timer.text = "0";
        ase = GetComponent<AudioSource>();
        blocknum = GameObject.FindGameObjectWithTag("Blocks").transform.childCount;
        ballnum = GameObject.FindGameObjectWithTag("Balls").transform.childCount - 1;
        if (Stage_Info.itemon[0])
        {
            PlayerPrefs.SetInt("Item0", PlayerPrefs.GetInt("Item0") - 1);
            if (PlayerPrefs.GetInt("Item0") == 0)
                Stage_Info.itemon[0] = false;
            ballnum++;
            ball.SetActive(true);
        }
        if (Stage_Info.itemon[1])
        {
            PlayerPrefs.SetInt("Item1", PlayerPrefs.GetInt("Item1") - 1);
            if (PlayerPrefs.GetInt("Item1") == 0)
                Stage_Info.itemon[1] = false;
            GameObject.FindGameObjectWithTag("Shield").SendMessage("TurnOn");
        }
        StartCoroutine(CheckMaxBall());
    }
    IEnumerator CheckMaxBall()
    {
        while (true)
        {
            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
            maxballcount = balls.Length;
            yield return new WaitForSeconds(3.0f);
        }
    }
    void Update()
    {
        if (!finish)
        {
            time += Time.deltaTime;
            timer.text = time.ToString("N1");
        }
    }
    void CheckClear()
    {
        blocknum--;
        if (blocknum == 0)
        {
            StartCoroutine(Clear());
        }
    }
    IEnumerator Clear()
    {
        finish = true;
        GetComponent<Game_System>().pausebutton.SetActive(false);
        ase.Stop();
        ase.PlayOneShot(se[0]);
        gameclearscreen.SetActive(true);
        totalgold.text = PlayerPrefs.GetInt("Gold").ToString();
        cleartime.text = time.ToString("N3");
        yield return new WaitForSeconds(2.0f);
        medal.SetActive(true);
        if (Stage_Info.cleartime[Stage_Info.stagenum - 1, 0] >= time)
        {
            medal.GetComponent<Animator>().SetInteger("go", 3);
            medalnum = 2;
        }
        else if (Stage_Info.cleartime[Stage_Info.stagenum - 1, 1] >= time)
        {
            medal.GetComponent<Animator>().SetInteger("go", 2);
            medalnum = 1;
        }
        if (medalnum >= 0)
        {
            yield return new WaitForSeconds(0.5f);
            ase.PlayOneShot(se[2]);
            if (medalnum >= 1)
            {
                yield return new WaitForSeconds(0.6f);
                ase.PlayOneShot(se[2]);
                if (medalnum == 2)
                {
                    yield return new WaitForSeconds(0.6f);
                    ase.PlayOneShot(se[2]);
                }
            }
        }
        yield return new WaitForSeconds(0.2f);
        if (medalnum == 0)
        {
            ase.PlayOneShot(se[3]);
        }
        else if (medalnum == 1)
        {
            ase.PlayOneShot(se[4]);
        }
        else
        {
            ase.PlayOneShot(se[5]);
        }
        SaveData();
    }
    void SaveData()
    {
        string snum;
        string mnum;
        if (Stage_Info.stagenum / 100 > 0)
        {
            snum = "Level" + Stage_Info.stagenum;
            mnum = "Medal" + Stage_Info.stagenum;
        }
        else if (Stage_Info.stagenum / 10 > 0)
        {
            snum = "Level0" + Stage_Info.stagenum;
            mnum = "Medal0" + Stage_Info.stagenum;
        }
        else {
            snum = "Level00" + Stage_Info.stagenum;
            mnum = "Medal00" + Stage_Info.stagenum;
        }
        if (PlayerPrefs.GetFloat(snum) == 0)
        {
            if (Stage_Info.cleartime[Stage_Info.stagenum - 1, 0] >= time)
            {
                PlayerPrefs.SetInt(mnum, 2);
                PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + (Stage_Info.stagenum / 10 + 1) * 20 + 20);
                earngold.text = ((Stage_Info.stagenum / 10 + 1) * 70 + 20).ToString();
            }
            else if (Stage_Info.cleartime[Stage_Info.stagenum - 1, 1] >= time)
            {
                PlayerPrefs.SetInt(mnum, 1);
                PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + (Stage_Info.stagenum / 10 + 1) * 10 + 10);
                earngold.text = ((Stage_Info.stagenum / 10 + 1) * 60 + 10).ToString();
            }
            else
                earngold.text = ((Stage_Info.stagenum / 10 + 1) * 50).ToString();
            PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + (Stage_Info.stagenum / 10 + 1) * 50);
            PlayerPrefs.SetFloat(snum, float.Parse(cleartime.text));
        }
        else if (PlayerPrefs.GetFloat(snum) > float.Parse(cleartime.text))
        {
            if (Stage_Info.cleartime[Stage_Info.stagenum - 1, 0] >= time)
            {
                PlayerPrefs.SetInt(mnum, 2);
                PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + (Stage_Info.stagenum / 10 + 1) * 20 + 20);
                earngold.text = ((Stage_Info.stagenum / 10 + 1) * 20 + 20).ToString();
            }
            else if (Stage_Info.cleartime[Stage_Info.stagenum - 1, 1] >= time)
            {
                PlayerPrefs.SetInt(mnum, 1);
                PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + (Stage_Info.stagenum / 10 + 1) * 10 + 10);
                earngold.text = ((Stage_Info.stagenum / 10 + 1) * 10 + 10).ToString();
            }
            PlayerPrefs.SetFloat(snum, float.Parse(cleartime.text));
        }
        else
        {
            if (Stage_Info.cleartime[Stage_Info.stagenum - 1, 0] >= time)
            {
                PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + (Stage_Info.stagenum / 10 + 1) * 20 + 20);
                earngold.text = ((Stage_Info.stagenum / 10 + 1) * 20 + 20).ToString();
            }
            else if (Stage_Info.cleartime[Stage_Info.stagenum - 1, 1] >= time)
            {
                PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + (Stage_Info.stagenum / 10 + 1) * 10 + 10);
                earngold.text = ((Stage_Info.stagenum / 10 + 1) * 10 + 10).ToString();
            }
        }
        ase.PlayOneShot(se[6]);
        totalgold.text = (PlayerPrefs.GetInt("Gold")).ToString();
        if (PlayerPrefs.GetInt("PlayStage") == Stage_Info.stagenum)
            PlayerPrefs.SetInt("PlayStage", PlayerPrefs.GetInt("PlayStage") + 1);
        Social.ReportScore((int)(time * 1000), Stage_Info.leaderboardid[Stage_Info.stagenum - 1], (bool success) => { });
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
        }
        PlayerPrefs.SetInt("PlayTime", PlayerPrefs.GetInt("PlayTime") + 1);   // 플레이 1회 추가
        CheckAchievement();
        buttonafterresult.SetActive(true);
        if (Stage_Info.maxstagenum == Stage_Info.stagenum)
        {
            buttonnewxtgame.SetActive(false);
        }
    }
    void CheckAchievement()
    {
        int playtime = PlayerPrefs.GetInt("PlayTime");
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
        }
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
        }
    }
    void GameOver()
    {
        if (!finish)
        {
            Time.timeScale = 0;
            GetComponent<Game_System>().pausebutton.SetActive(false);
            ase.Stop();
            ase.PlayOneShot(se[1]);
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
            }
            gameoverscreen.SetActive(true);
        }
    }
    public void StageStart()
    {
        Time.timeScale = 1;
        playing = true;
        startbutton.SetActive(false);
        GetComponent<Game_System>().pausebutton.SetActive(true);
    }
    public void NotPlaying()
    {
        playing = false;
    }
    public void Playing()
    {
        playing = true;
    }
}
