using UnityEngine;
using System.Collections;

public class Item_invisible : MonoBehaviour {
    void FixedUpdate()
    {
        transform.Translate(Vector2.down * Time.deltaTime * 0.4f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -8f);
        }
        else if(other.gameObject.tag == "Bar")
        {
            other.SendMessage("Invisible");
            Destroy(gameObject);
        }
    }
    
}
