using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
public class Stage_Info
{
    public static string[] leaderboardid = {
        "CggI_8yryScQAhAV", "CggI_8yryScQAhAW", "CggI_8yryScQAhAX", "CggI_8yryScQAhAY", "CggI_8yryScQAhAZ",
        "CggI_8yryScQAhAa", "CggI_8yryScQAhAb", "CggI_8yryScQAhAc", "CggI_8yryScQAhAd", "CggI_8yryScQAhAe",
        "CggI_8yryScQAhAm", "CggI_8yryScQAhAn", "CggI_8yryScQAhAo", "CggI_8yryScQAhAq", "CggI_8yryScQAhAp",
        "CggI_8yryScQAhAr", "CggI_8yryScQAhAs", "CggI_8yryScQAhAt", "CggI_8yryScQAhAu", "CggI_8yryScQAhAv",
        "CggI_8yryScQAhAw", "CggI_8yryScQAhAx", "CggI_8yryScQAhAy", "CggI_8yryScQAhAz", "CggI_8yryScQAhA0",
        "CggI_8yryScQAhA1", "CggI_8yryScQAhA2", "CggI_8yryScQAhA3", "CggI_8yryScQAhA4", "CggI_8yryScQAhA5"
    };
    public static int[,] cleartime = {
        { 35, 55 }, { 50, 70 }, { 50, 70 }, { 60, 80 }, { 50, 70 }, { 50, 70 }, { 70, 90 }, { 50, 70 }, { 40, 60 }, { 60, 80 }
      , {40,80 } , {40,80 }  , {60,100 } , {30,60 } , {65,110 } , {65,110 } , {65,110 } , {75,120 } , {60,110 } , {70,120 }
      , {60,120 } , {40,80 }  , {50,100 } , {50,100 } , {40,80 } , {60,120 } , {50,100 } , {45,90 } , {60,120 } , {40,50 }};
    public static int stagenum;
    public static int maxstagenum = 30;
    public static bool[] itemon = { false, false };
}
public class Stage_System : MonoBehaviour
{
    string objectname;
    int objectnameint;
    public GameObject stageinfo;
    public GameObject[] stage;
    public Text stagename;
    public Text[] cleartime;
    int stagenumber = 1;
    int checkgoldmedal;
    int checksilvermedal;
    void Start()
    {
        if (Stage_Info.stagenum / 10 > 0)
            stagenumber = (Stage_Info.stagenum - 1) / 10;
        else stagenumber = 0;
        stage[stagenumber].SetActive(true);
        GameObject.FindGameObjectWithTag("MainCamera").transform.Translate(new Vector2(42.6f * stagenumber, 0));

        if (PlayerPrefs.GetInt("GoNextLevel") == 1)
        {
            string sn;
            if ((Stage_Info.stagenum + 1) / 100 > 0)
            {
                sn = (Stage_Info.stagenum + 1).ToString();
            }
            else if ((Stage_Info.stagenum + 1) / 10 > 0)
            {
                sn = "0" + (Stage_Info.stagenum + 1);
            }
            else
            {
                sn = "00" + (Stage_Info.stagenum + 1);
            }
            Stage_Info.stagenum++;
            PlayerPrefs.SetInt("GoNextLevel", 0);
            StageInfoCheck(sn);
        }
        if (PlayerPrefs.GetInt("PlayStage") >= 30)
        {

            Social.ReportProgress("CggI_8yryScQAhA6", 100.0f, (bool success) => { });
        }
        else if (PlayerPrefs.GetInt("PlayStage") >= 20)
        {

            Social.ReportProgress("CggI_8yryScQAhAj", 100.0f, (bool success) => { });
        }
        else if (PlayerPrefs.GetInt("PlayStage") >= 10)
        {

            Social.ReportProgress("CggI_8yryScQAhAg", 100.0f, (bool success) => { });
        }   // 스테이지 모두 클리어 했나 체크
        checkgoldmedal = 0;
        checksilvermedal = 0;
        for (int i = 1; i < 10; i++)
        {
            if (PlayerPrefs.GetInt("Medal00" + i) == 2)
            {
                checkgoldmedal++;
                checksilvermedal++;
            }
            else if (PlayerPrefs.GetInt("Medal00" + i) == 1)
                checksilvermedal++;
            else
                break;
        }

        if (PlayerPrefs.GetInt("Medal010") == 2)
        {
            checkgoldmedal++;
            checksilvermedal++;
        }
        else if (PlayerPrefs.GetInt("Medal010") == 1)
            checksilvermedal++;
        if (checkgoldmedal == 10)
            Social.ReportProgress("CggI_8yryScQAhAi", 100.0f, (bool success) => { });
        if (checksilvermedal == 10)
            Social.ReportProgress("CggI_8yryScQAhAh", 100.0f, (bool success) => { });
        checkgoldmedal = 0;
        checksilvermedal = 0;
        for (int i = 11; i < 21; i++)
        {
            if (PlayerPrefs.GetInt("Medal0" + i) == 2)
            {
                checkgoldmedal++;
                checksilvermedal++;
            }
            else if (PlayerPrefs.GetInt("Medal0" + i) == 1)
                checksilvermedal++;
            else
                break;
        }
        if (checkgoldmedal == 10)
            Social.ReportProgress("CggI_8yryScQAhAl", 100.0f, (bool success) => { });
        if (checksilvermedal == 10)
            Social.ReportProgress("CggI_8yryScQAhAk", 100.0f, (bool success) => { });
        checkgoldmedal = 0;
        checksilvermedal = 0;
        for (int i = 21; i < 31; i++)
        {
            if (PlayerPrefs.GetInt("Medal0" + i) == 2)
            {
                checkgoldmedal++;
                checksilvermedal++;
            }
            else if (PlayerPrefs.GetInt("Medal0" + i) == 1)
                checksilvermedal++;
            else
                break;
        }
        if (checkgoldmedal == 10)
            Social.ReportProgress("CggI_8yryScQAhA8", 100.0f, (bool success) => { });
        if (checksilvermedal == 10)
            Social.ReportProgress("CggI_8yryScQAhA7", 100.0f, (bool success) => { });   // 메달 체크
    }
    void StageInfoCheck(string name)
    {
        objectname = name;
        objectnameint = int.Parse(name);
        Stage_Info.stagenum = objectnameint;
        stagename.text = "스테이지 " + ((objectnameint - 1) / 10 + 1) + " - ";
        if (objectnameint % 10 == 0)
            stagename.text += "10";
        else stagename.text += objectname.Substring(2);
        cleartime[0].text = Stage_Info.cleartime[objectnameint - 1, 0].ToString();
        cleartime[1].text = Stage_Info.cleartime[objectnameint - 1, 1].ToString();
        cleartime[2].text = PlayerPrefs.GetFloat("Level" + objectname).ToString();
        stageinfo.SetActive(true);
        stage[stagenumber].SetActive(false);
    }
    public void Cancel()
    {
        stageinfo.SetActive(false);
        stage[stagenumber].SetActive(true);
    }
    public void GoLevel()
    {
        if (PlayerPrefs.GetInt("PlayStage") >= objectnameint)
        {
            SceneManager.LoadScene("Level" + objectname);
        }
    }
    public void ShowLeaderBoard()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(Stage_Info.leaderboardid[objectnameint - 1]);
    }
    public void NextStage()
    {
        GameObject.FindGameObjectWithTag("MainCamera").transform.Translate(new Vector2(42.6f, 0));
        stage[stagenumber].SetActive(false);
        stagenumber++;
        stage[stagenumber].SetActive(true);
    }
    public void PrevStage()
    {
        GameObject.FindGameObjectWithTag("MainCamera").transform.Translate(new Vector2(-42.6f, 0));
        stage[stagenumber].SetActive(false);
        stagenumber--;
        stage[stagenumber].SetActive(true);
    }
}
