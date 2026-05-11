using UnityEngine;
using System.Collections;

public class Item_Longer_Stage : MonoBehaviour {
    void FixedUpdate()
    {
        transform.Translate(Vector2.down * Time.deltaTime * 10f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bar")
        {
            Destroy(gameObject);
            other.SendMessage("Bigger");
        }
    }
}
