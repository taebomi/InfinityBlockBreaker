using UnityEngine;
using System.Collections;

public class Block_Manager : MonoBehaviour
{
    public GameObject[] breakeffect;
    public Sprite[] blockcolor;
    SpriteRenderer sr;
    public int health = 1;
    float dmg;
    int score;
    int colornumber;
    bool warn = false;
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        health = Game_Manager.level;
        score = Game_Manager.level * 5 + 5;
        if (health % 13 == 0)
        {
            sr.sprite = blockcolor[12];
            colornumber = 12;
        }
        else {
            sr.sprite = blockcolor[health % 13 - 1];
            colornumber = health % 13 - 1;
        }
    }
    void FixedUpdate()
    {
        transform.Translate(Vector2.down * Time.deltaTime * 0.4f);
    }
   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            dmg = other.gameObject.GetComponent<Ball_Control>().dmg;
            if (health - dmg < 0)
            {
                other.SendMessage("PlaySE");
                if(warn== true)
                {
                    Zone_Warning.count--;
                }
                other.gameObject.GetComponent<Ball_Control>().touchcount++;
                Destroy(gameObject);    
                Game_Manager.score += score;
                Game_Manager.ScoreRefresh();
                GameObject effect = Instantiate(breakeffect[colornumber], transform.position, Quaternion.identity) as GameObject;
                Destroy(effect, 0.5f);
            }
        }
        else if (other.CompareTag("WarnZone"))
        {
            if(warn==false) 
            Zone_Warning.count++;
            warn = true;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            dmg = other.gameObject.GetComponent<Ball_Control>().dmg;
            health -= (int)dmg;
            if (health > 0)
            {
                if (health % 13 == 0)
                {
                    sr.sprite = blockcolor[12];
                    colornumber = 1;
                }
                else {
                    sr.sprite = blockcolor[health % 13 - 1];
                    colornumber = health % 13 - 1;
                }
            }
            else
            {
                if (warn == true)
                {
                    Zone_Warning.count--;
                }
                Game_Manager.score += score;
                Game_Manager.ScoreRefresh();
                GameObject effect = Instantiate(breakeffect[colornumber], transform.position, Quaternion.identity) as GameObject;
                Destroy(effect, 0.5f);
                Destroy(gameObject);
            }
        }
    }
}
