using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shop_Upgrade_Button : MonoBehaviour {
    int upgradenum;
    Text info;
    void Start()
    {
        upgradenum = int.Parse(transform.name);
        info = gameObject.GetComponentInChildren<Text>();
        info.text = PlayerPrefs.GetInt("Upgrade" + upgradenum).ToString();
    }
    void RefreshUpgradeNum()
    {
        info.text = PlayerPrefs.GetInt("Upgrade" + upgradenum).ToString();
    }
    public void SelectUpgrade()
    {
        GameObject.FindGameObjectWithTag("GM").SendMessage("SelectUpgrade", upgradenum);
    }
}
