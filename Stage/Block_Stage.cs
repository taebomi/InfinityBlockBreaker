using UnityEngine;
using System.Collections;

public class Block_Stage : MonoBehaviour {

    public GameObject[] breakeffect;
    public Sprite[] blockcolor;
    SpriteRenderer sr;
    public int health = 1;
    public int mycolor;
    int dmg;
    GameObject gm;
    public int itemnum;
    public GameObject[] item;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM");
        sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = blockcolor[mycolor - 1];
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            dmg = (int)other.gameObject.GetComponent<Ball_Stage>().dmg;
            if (health - dmg < 0)
            {
                other.SendMessage("PlaySE");
                if (itemnum != 0)
                {
                    Instantiate(item[itemnum-1], transform.position, Quaternion.identity);
                }
                gm.SendMessage("CheckClear");
                Destroy(gameObject);
                GameObject effect = Instantiate(breakeffect[mycolor-1], transform.position, Quaternion.identity) as GameObject;
                Destroy(effect, 0.5f);
            }
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            dmg = (int)other.gameObject.GetComponent<Ball_Stage>().dmg;
            health -= dmg;
            if (health > 0)
            {
                mycolor -= dmg;
                while (mycolor < 1)
                {
                    mycolor = mycolor + 13;
                }
                sr.sprite = blockcolor[mycolor-1];
            }
            else
            {
                if (itemnum != 0)
                {
                    Instantiate(item[itemnum-1], transform.position, Quaternion.identity);
                }
                gm.SendMessage("CheckClear");
                Destroy(gameObject);
                GameObject effect = Instantiate(breakeffect[mycolor-1], transform.position, Quaternion.identity) as GameObject;
                Destroy(effect, 0.5f);
            }
        }
    }
}
