using UnityEngine;
using System.Collections;

public class Ball_Delete : MonoBehaviour {
    AudioSource gmasmute;
    void Start()
    {
        gmasmute = GameObject.FindGameObjectWithTag("GM").GetComponent<AudioSource>();
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ball")
        {
            Game_Manager.ballcount--;
            if (gmasmute.mute)
                gmasmute.mute = false;
        }
        Destroy(other.gameObject);
        if (Game_Manager.ballcount == 0)
        {
            GameObject.FindGameObjectWithTag("GM").SendMessage("GameOver");
        }
    }
}
