using UnityEngine;
using System.Collections;

public class Zone_Death: MonoBehaviour {

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Block")
        {
            GameObject.FindGameObjectWithTag("GM").SendMessage("GameOver");
        }
    }
}
