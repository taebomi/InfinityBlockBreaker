using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Select_Level_Button : MonoBehaviour {
    string objectname;
    public Sprite[] medal;
    void Start()
    {
        objectname = transform.name;
        if (PlayerPrefs.GetInt("PlayStage") >= int.Parse(objectname))
        {
            if(PlayerPrefs.GetInt("Medal"+ objectname) == 2)
                gameObject.GetComponent<Button>().image.overrideSprite = medal[3];
            else if (PlayerPrefs.GetInt("Medal" + objectname) == 1)
                gameObject.GetComponent<Button>().image.overrideSprite = medal[2];
            else if(PlayerPrefs.GetInt("PlayStage")==int.Parse(objectname))
                gameObject.GetComponent<Button>().image.overrideSprite = medal[0];
            else
                gameObject.GetComponent<Button>().image.overrideSprite = medal[1];
        }
    }
    public void ButtonClick()
    {
        if (PlayerPrefs.GetInt("PlayStage") >= int.Parse(objectname))
            GameObject.FindGameObjectWithTag("GM").SendMessage("StageInfoCheck",objectname);
    }
}
