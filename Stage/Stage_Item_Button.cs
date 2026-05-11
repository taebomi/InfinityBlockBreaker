using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Stage_Item_Button : MonoBehaviour {
    string buttonname;
    bool hi = false;
    void Start()
    {
        buttonname = transform.name;
        if (PlayerPrefs.GetInt("Item" + buttonname) == 0)
        {
            GetComponent<Toggle>().image.color = new Vector4(0.5f, 0.5f, 0.5f, 1f);
            GetComponent<Toggle>().enabled = false;
        }
        else if(Stage_Info.itemon[int.Parse(buttonname)])
            GetComponent<Toggle>().isOn = true;

    }

    public void ItemOn()
    {
        if (PlayerPrefs.GetInt("Item" + buttonname) > 0 && GetComponent<Toggle>().isOn)
        {
            Stage_Info.itemon[int.Parse(buttonname)] = true;
        }
        else if (PlayerPrefs.GetInt("Item" + buttonname) > 0 && !GetComponent<Toggle>().isOn)
        {
            Stage_Info.itemon[int.Parse(buttonname)] = false;
        }
    }
}
