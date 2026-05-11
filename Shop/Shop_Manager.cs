using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Shop_Manager : MonoBehaviour
{
    public Text totalgold;                  // 내 현재 골드 텍스트
    public Text howmanyitem;                // 아이템 개수 몇개 사나 텍스트
    public Text helpitemdescription;        // 아이템 설명 텍스트
    public Text upgradedescriptiontext;     // 업그레이드 설명 텍스트
    public Text tobuyitempaygold;           // 필요한 골드
    public Text toupgradepaygold;
    public GameObject itemimage;            // 구매 창의 아이템 이미지
    public GameObject upgradeimage;         // 업그레이드 창의 이미지
    public Sprite[] itemsprite;             // 아이템 스프라이트
    public Sprite[] upgradesprite;
    public static int[] itemprice = { 30,15 };
    public int[,] upgradeprice = { { 1000, 2000, 3000, 4000, 5000 } };
    string itemnumstring;
    string[] itemdescription = { "시작 시 공을 하나 더 가지고 시작합니다.", "공을 2회 막아주는 쉴드를 생성합니다." };
    string[] upgradedescription = { "쉴드 횟수를 1 증가시킵니다.\n 기본 쉴드량 1\n 무한 모드에도 적용됨" };
    int itemnum;
    int upgradenum;
    public GameObject purchaseitemform;
    public GameObject upgradeform;
    public GameObject confirmpurchaseitem;
    public GameObject confirmupgrade;
    public Text myitemnum;
    public GameObject itemimage2;
    public GameObject maxitem;
    public GameObject maxupgrade;
    public AudioClip[] se;
    AudioSource ase;
    public GameObject[] page;
    void Start()
    {
        totalgold.text = PlayerPrefs.GetInt("Gold").ToString();
        ase = GetComponent<AudioSource>();
    }
    void SelectItem(int n)
    {
        itemnum = n;
        itemimage.GetComponent<Image>().sprite = itemsprite[n];
        helpitemdescription.text = itemdescription[n];
        tobuyitempaygold.text = (itemprice[n] * int.Parse(howmanyitem.text)).ToString();
        if(PlayerPrefs.GetInt("Item"+ itemnum) == 99)
            maxitem.SetActive(true);
        else
            purchaseitemform.SetActive(true);
    }
    void SelectUpgrade(int n)
    {
        upgradenum = n;
        upgradeimage.GetComponent<Image>().sprite = upgradesprite[n];
        upgradedescriptiontext.text = upgradedescription[n];
        if (PlayerPrefs.GetInt("Upgrade" + n) == 5)
            maxupgrade.SetActive(true);
        else
        { 
        upgradeform.SetActive(true);
            toupgradepaygold.text = upgradeprice[n, PlayerPrefs.GetInt("Upgrade" + n)].ToString();
        }
    }
    public void GoUpgradePage()
    {
        page[0].SetActive(false);
        page[1].SetActive(true);
    }
    public void GoItemPage()
    {
        page[0].SetActive(true);
        page[1].SetActive(false);
    }
    public void increaseitemnum1()
    {
        if (PlayerPrefs.GetInt("Gold") - (itemprice[itemnum] * (int.Parse(howmanyitem.text) + 1)) > 0)
        {
            howmanyitem.text = (int.Parse(howmanyitem.text) + 1).ToString();
            if (int.Parse(howmanyitem.text) + PlayerPrefs.GetInt("Item" + itemnum) > 99)
                howmanyitem.text = (99 - PlayerPrefs.GetInt("Item" + itemnum)).ToString();
            tobuyitempaygold.text = (itemprice[itemnum] * int.Parse(howmanyitem.text)).ToString();
        }
    }
    public void increaseitemnum10()
    {
        if (PlayerPrefs.GetInt("Gold") - (itemprice[itemnum] * (int.Parse(howmanyitem.text) + 10)) > 0)
        {
            howmanyitem.text = (int.Parse(howmanyitem.text) + 10).ToString();
            if (int.Parse(howmanyitem.text) + PlayerPrefs.GetInt("Item" + itemnum) > 99)
                howmanyitem.text = (99 - PlayerPrefs.GetInt("Item" + itemnum)).ToString();
            tobuyitempaygold.text = (itemprice[itemnum] * int.Parse(howmanyitem.text)).ToString();
        }
    }
    public void decreaseitemnum1()
    {
        howmanyitem.text = (int.Parse(howmanyitem.text) - 1).ToString();
        if (int.Parse(howmanyitem.text) < 1)
            howmanyitem.text = "1";
        tobuyitempaygold.text = (itemprice[itemnum] * int.Parse(howmanyitem.text)).ToString();
    }
    public void decreaseitemnum10()
    {
        howmanyitem.text = (int.Parse(howmanyitem.text) - 10).ToString();
        if (int.Parse(howmanyitem.text) < 1)
            howmanyitem.text = "1";
        tobuyitempaygold.text = (itemprice[itemnum] * int.Parse(howmanyitem.text)).ToString();
    }
    public void PurchaseItem()
    {
        if (PlayerPrefs.GetInt("Gold") - int.Parse(tobuyitempaygold.text) > 0)
        {
            PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") - int.Parse(tobuyitempaygold.text));
            PlayerPrefs.SetInt("Item" + itemnum, PlayerPrefs.GetInt("Item" + itemnum) + int.Parse(howmanyitem.text));
            purchaseitemform.SetActive(false);
            itemimage2.GetComponent<Image>().sprite = itemsprite[itemnum];
            myitemnum.text = PlayerPrefs.GetInt("Item" + itemnum).ToString();
            GameObject.Find(itemnum.ToString()).SendMessage("RefreshItemNum");
            totalgold.text = PlayerPrefs.GetInt("Gold").ToString();
            howmanyitem.text = "1";
            ase.PlayOneShot(se[0]);
            confirmpurchaseitem.SetActive(true);
        }
    }
    public void Upgrade()
    {
        if(PlayerPrefs.GetInt("Gold")- int.Parse(toupgradepaygold.text) > 0)
        {
            PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") - int.Parse(toupgradepaygold.text));
            PlayerPrefs.SetInt("Upgrade" + upgradenum, PlayerPrefs.GetInt("Upgrade" + upgradenum) + 1);
            upgradeform.SetActive(false);
            ase.PlayOneShot(se[0]);
            GameObject.Find(upgradenum.ToString()).SendMessage("RefreshUpgradeNum");
            totalgold.text = PlayerPrefs.GetInt("Gold").ToString();
            confirmupgrade.SetActive(true);
        }
    }
    public void ConfirmUpgrade()
    {
        confirmupgrade.SetActive(false);
    }
    public void ConfrimPurchaseitem()
    {
        confirmpurchaseitem.SetActive(false);
    }
    public void PurchaseItemCancle()
    {
        purchaseitemform.SetActive(false);
        howmanyitem.text = "1";
    }
    public void UpgradeCancle()
    {
        upgradeform.SetActive(false);
    }
    public void ConfirmMaxItem()
    {
        maxitem.SetActive(false);
    }
    public void ConfirmUpgradeMax()
    {
        maxupgrade.SetActive(false);
    }
}
