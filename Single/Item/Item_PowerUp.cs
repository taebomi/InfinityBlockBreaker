using UnityEngine;
using System.Collections;

public class Item_PowerUp : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Translate(Vector2.down * Time.deltaTime * 0.4f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            other.SendMessage("PowerUp");
            Destroy(gameObject);
        }
    }
}
