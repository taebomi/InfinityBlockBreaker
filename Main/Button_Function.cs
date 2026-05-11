using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;
public class Button_Function : MonoBehaviour
{
    public GameObject help;
    public GameObject option;
    public GameObject cc;
    public GameObject bgmtoggle;
    public GameObject bgctoggle;
    public void ShowLeaderBoard()
    {
        Social.ShowLeaderboardUI();
    }
    public void ShowAchievements()
    {
        Social.ShowAchievementsUI();
    }
    public void GPLogin()
    {
        Social.localUser.Authenticate((bool success) => {
        });
    }
    public void CC()
    {
        cc.SetActive(true);
    }
    public void Help()
    {
        help.SetActive(true);
    }
    public void ExitHelp()
    {
        help.SetActive(false);
    }
    public void Option()
    {
        if (PlayerPrefs.GetInt("BGM") == 0)
            bgmtoggle.GetComponent<Toggle>().isOn = true;
        else
            bgmtoggle.GetComponent<Toggle>().isOn = false;
        /*
            if (PlayerPrefs.GetInt("BG") == 1)
            bgtoggle.GetComponent<Toggle>().isOn = true;
        else
            bgtoggle.GetComponent<Toggle>().isOn = false;
            */
        if (PlayerPrefs.GetInt("BGC") != 0)
            bgctoggle.GetComponent<Toggle>().isOn = true;
        else
            bgctoggle.GetComponent<Toggle>().isOn = false;
        option.SetActive(true);
    }
    public void ExitCC()
    {
        cc.SetActive(false);
    }
    public void ExitOption()
    {
        option.SetActive(false);
        if (PlayerPrefs.GetInt("BGC") == 1)
        {
            GetComponent<Game_System>().CheckBG();
        }
        if (PlayerPrefs.GetInt("BGM") == 1)
        {
            GetComponent<Game_System>().CheckBGM();
        }
    }
    public void CheckBGM()
    {
        if (bgmtoggle.GetComponent<Toggle>().isOn)
        {
            PlayerPrefs.SetInt("BGM", 0);
        }
        else
        {
            PlayerPrefs.SetInt("BGM", 1);
        }
    }
    /*public void CheckBG()
    {
        if (bgtoggle.GetComponent<Toggle>().isOn)
        {
            PlayerPrefs.SetInt("BG", 1);
        }
        else
        {
            PlayerPrefs.SetInt("BG", 0);
        }
    }*/
    public void CheckBGColor()
    {
        if (bgctoggle.GetComponent<Toggle>().isOn)
        {
            PlayerPrefs.SetInt("BGC", 1);
        }
        else
        {
            PlayerPrefs.SetInt("BGC", 0);
        }
    }
    public void StartSingleGame()
    {
        SceneManager.LoadScene("Singleplay");
    }
    public void Start2PGame()
    {
        SceneManager.LoadScene("Multiplay_2p");
    }
    public void StartStageGame()
    {
        SceneManager.LoadScene("SelectLevel");
    }
    public void GoShop()
    {
        SceneManager.LoadScene("Shop");
    }
}
