using UnityEngine;
using System.Collections;

public class Block_Main : MonoBehaviour {
    public int health = 1;
    int dmg;
    GameObject gm;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM");
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            dmg = (int)other.gameObject.GetComponent<Ball_Main>().dmg;
            health -= dmg;
            if (health < 0)
            {
                gm.SendMessage("BreakAll");
                Destroy(gameObject);
                EasterEgg_Main.blocknum--;
            }
        }
    }
}
