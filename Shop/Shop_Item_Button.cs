using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Shop_Item_Button : MonoBehaviour {
    int itemnum;
    Text[] info;
	void Start ()
    {
        itemnum = int.Parse(transform.name);
        info = gameObject.GetComponentsInChildren<Text>();
        info[0].text = Shop_Manager.itemprice[itemnum].ToString() + "G";
        info[1].text = PlayerPrefs.GetInt("Item" + itemnum).ToString()+"개";
    }
    void RefreshItemNum()
    {
        info[1].text = PlayerPrefs.GetInt("Item" + itemnum).ToString();
    }
    public void SelectItem()
    {
        GameObject.FindGameObjectWithTag("GM").SendMessage("SelectItem", itemnum);
    }
}
