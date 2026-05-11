using UnityEngine;
using System.Collections;

public class Item_plus : MonoBehaviour
{
    public GameObject newball;
    float dmg;
    void Start()
    {
        dmg = (int)(Game_Manager.level / 2) + 1;
    }
    void FixedUpdate()
    {
        transform.Translate(Vector2.down * Time.deltaTime * 0.4f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bar")
        {
            GameObject temp = Instantiate(newball, transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
            Game_Manager.ballcount++;
            temp.SendMessage("SetDMG", dmg);
            other.SendMessage("PlusBall");
        }
        else if (other.gameObject.tag == "Ball")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -8f);
        }
    }
}
