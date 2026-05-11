using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EasterEgg_Main : MonoBehaviour
{
    public Text hey;
    public static int blocknum;
    void Start()
    {
        if (PlayerPrefs.GetInt("Season") == 0)
        {
            PlayerPrefs.SetInt("Season",1);
            PlayerPrefs.SetInt("HighScore", 0);
            PlayerPrefs.SetInt("HighLevel",0);
        }
        blocknum = 61;
    }
    void BreakAll()
    {
        if (blocknum == 20)
        {
            hey.gameObject.SetActive(true);
        }
        else if (blocknum == 0)
        {
            Social.ReportProgress("CggI_8yryScQAhAf", 100.0f, (bool success) => { });
            hey.text = "이거라도 받으세요.\n이제 게임해주실래요?";
        }
    }
}