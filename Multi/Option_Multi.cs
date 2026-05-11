using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Option_Multi : MonoBehaviour {
    public Text ballnumtext;
    public Text timetext;
    public void GameStart()             
    {
        if (timetext.text == "")
            Game_Option_Multi.time = 120;
        else
            Game_Option_Multi.time = int.Parse(timetext.text);
        Game_Option_Multi.minballnum = int.Parse(ballnumtext.text);
        gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("GM").GetComponent<Game_Manager_Multi>().SendMessage("StartGame");
    }
    public void BallNumUp()
    {
        int num;
        num = int.Parse(ballnumtext.text);
        if(num<3)
        num++;
        ballnumtext.text = num.ToString();
    }

    public void BallNumDown()
    {
        int num;
        num = int.Parse(ballnumtext.text);
        if (num > 1)
            num--;
        ballnumtext.text = num.ToString();
    }                   // 공 개수 설정하는 버튼 함수
}
