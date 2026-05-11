using UnityEngine;
using System.Collections;

public class Ball_Delete_Stage : MonoBehaviour {

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ball")
        {
            Stage_Manager.ballnum--;
        }
        Destroy(other.gameObject);
        if (Stage_Manager.ballnum == 0)
        {
            GameObject.FindGameObjectWithTag("GM").SendMessage("GameOver");
        }
    }
}
