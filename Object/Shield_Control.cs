using UnityEngine;
using System.Collections;

public class Shield_Control : MonoBehaviour {
    public static int shieldcount;
    SpriteRenderer sr;
    BoxCollider2D bc;
    AudioSource ase;
    public AudioClip[] se;
	void Start () {
        shieldcount = 0;
        ase = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
	}

    void TurnOn()
    {
        if (ase == null || bc == null || sr == null)
        {
            ase = GetComponent<AudioSource>();
            sr = GetComponent<SpriteRenderer>();
            bc = GetComponent<BoxCollider2D>();
        }
        shieldcount = shieldcount + PlayerPrefs.GetInt("Upgrade0")+1;
        sr.enabled = true;
        bc.enabled = true;
        sr.color = new Color(sr.color.r, sr.color.g-0.1f*(PlayerPrefs.GetInt("Upgrade0") + 1), sr.color.b);
        ase.PlayOneShot(se[0]);

    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            shieldcount--;
            sr.color = new Color(sr.color.r, sr.color.g + 0.1f, sr.color.b);
            if (shieldcount == 0)
            {
                sr.enabled = false;
                bc.enabled = false;
            }
            ase.PlayOneShot(se[1]);
        }
    }
}
