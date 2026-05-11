using UnityEngine;
using System.Collections;

public class Item_plus2_Stage : MonoBehaviour {

    public GameObject newball;
    float dmg;
    void Start()
    {
        dmg = 1;
    }
    void FixedUpdate()
    {
        transform.Translate(Vector2.down * Time.deltaTime * 10f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bar")
        {
            GameObject temp = Instantiate(newball, new Vector2(transform.position.x - 0.1f, transform.position.y), Quaternion.identity) as GameObject;
            GameObject temp2 = Instantiate(newball, new Vector2(transform.position.x + 0.1f, transform.position.y), Quaternion.identity) as GameObject;
            Destroy(gameObject);
            Stage_Manager.ballnum++;
            Stage_Manager.ballnum++;
            temp.SendMessage("SetDMG", dmg);
            other.SendMessage("PlusBall2");
            temp2.SendMessage("SetDMG", dmg);
        }
    }
}
