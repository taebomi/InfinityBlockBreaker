using UnityEngine;
using System.Collections;

public class Item_Shield : MonoBehaviour {
    void FixedUpdate()
    {
        transform.Translate(Vector2.down * Time.deltaTime * 0.4f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bar")
        {
            GameObject.FindGameObjectWithTag("Shield").SendMessage("TurnOn");
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Ball")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -8f);
        }
    }
}
