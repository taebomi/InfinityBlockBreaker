using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CheckZone1P : MonoBehaviour {
    public Text score;
    AudioSource ase;
    void Start() {
        ase=GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            ase.PlayOneShot(ase.clip);
            Game_Option_Multi.count--;
            Game_Option_Multi.score_2p++;
            score.text = Game_Option_Multi.score_2p.ToString();
            Game_Option_Multi.getball1p = true;
            if (Game_Option_Multi.gamefinish && Game_Option_Multi.count == 0)
                GameObject.FindGameObjectWithTag("GM").GetComponent<Game_Manager_Multi>().SendMessage("FinishGame");
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Destroy(other.gameObject);
        }
    }
}
