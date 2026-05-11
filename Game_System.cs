using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;

public class Game_Option
{
    public static bool main = true;
}
public class Game_System : MonoBehaviour
{
    public GameObject quit;
    public GameObject resumebutton;           // 일시 정지 버튼
    public GameObject pausebutton;          // 재개 버튼 
    public GameObject bg;
    void Awake()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = false;
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>
        {
        });
        if (PlayerPrefs.GetInt("PlayStage") == 0)
        {
            PlayerPrefs.SetInt("PlayStage", 1);
        }
    }
    void Start()
    {
        CheckBG();
        CheckBGM();
    }
    public void CheckBGM()
    {
        if (PlayerPrefs.GetInt("BGM") == 1)
        {
            GetComponent<AudioSource>().Stop();
        }
    }
    public void CheckBG()
    {
        if (PlayerPrefs.GetInt("BGC") == 1)
        {
            if (bg != null)
                bg.SetActive(false);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            quit.SetActive(true);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void NoQuitGame()
    {
        Time.timeScale = 1;
        if (Game_Manager.playing == false)
            Time.timeScale = 0;
        else if (Stage_Manager.playing == false)
            Time.timeScale = 0;
        else if (Game_Option.main)
            Time.timeScale = 1;
        quit.SetActive(false);
    }
    public void InfinityNoQuitGame()
    {
        Time.timeScale = Game_Manager.timescale;
        quit.SetActive(false);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        if(Stage_Info.itemon[0]== true)
        {
            if (PlayerPrefs.GetInt("Item0") == 0)
                Stage_Info.itemon[0] = false;
        }
        if (Stage_Info.itemon[1] == true)
        {
            if (PlayerPrefs.GetInt("Item1") == 0)
                Stage_Info.itemon[1] = false;
        }
        SceneManager.LoadScene(gameObject.scene.name);
    }
    public void GoMain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
    }
    public void ShowLeaderBoard()
    {
        Social.ShowLeaderboardUI();
    }
    public void ShowThisLevelLeaderBoard()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(Stage_Info.leaderboardid[Stage_Info.stagenum - 1]);
    }
    public void ShowAchievements()
    {
        Social.ShowAchievementsUI();
    }
    public void GoNextLevel()
    {
        PlayerPrefs.SetInt("GoNextLevel", 1);
        SceneManager.LoadScene("SelectLevel");
    }
    public void GoSelectLevel()
    {
        SceneManager.LoadScene("SelectLevel");
    }
    public void Resume()
    {
        Time.timeScale = 1;
        pausebutton.SetActive(true);
        resumebutton.SetActive(false);
    }
    public void InfinityResume()
    {
        Time.timeScale = Game_Manager.timescale;
        pausebutton.SetActive(true);
        resumebutton.SetActive(false);

    }
    public void Pause()
    {
        Time.timeScale = 0;
        resumebutton.SetActive(true);
        pausebutton.SetActive(false);
    }
}
