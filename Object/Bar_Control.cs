using UnityEngine;
using System.Collections;
using CnControls;

public class Bar_Control : MonoBehaviour
{
    Transform tf;
    AudioSource ase;
    Rigidbody2D rb;
    public AudioClip[] se;
    public float speed = 20f;
    Vector3 touchposition;
    void Start()
    {
        tf = GetComponent<Transform>();
        ase = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        /*
        if (Input.touchCount > 0)
        {
            touchposition = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, 0, 0));
            tf.Translate(new Vector2((touchposition.x - transform.position.x)*speed*Time.deltaTime, 0));
        }
        */
        if (Input.touchCount > 0)
        {
            touchposition = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, 0, 0));
            if (touchposition.x > tf.position.x+1f&&tf.position.x<8)
                tf.Translate(new Vector2((speed * Time.deltaTime*1.4f),0));
            else if (touchposition.x < tf.position.x-1f && tf.position.x > -8)
                tf.Translate(new Vector2((-speed * Time.deltaTime*1.4f), 0));
            else
                tf.Translate(new Vector2((touchposition.x - transform.position.x) * speed*1.4f* Time.deltaTime, 0));
        }
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * speed*Time.deltaTime);
    }
    IEnumerator Reverse()
    {
        speed *= -1;
        yield return new WaitForSeconds(5f);
        speed *=-1;
    }
    void PlusBall()
    {
        ase.PlayOneShot(se[3]);
    }
    IEnumerator PlusBall2() {
        ase.PlayOneShot(se[3]);
        yield return new WaitForSeconds(0.1f);
        ase.PlayOneShot(se[3]);
    }
    IEnumerator Bigger()
    {
        ase.PlayOneShot(se[0]);
        tf.localScale += new Vector3(0.6f, 0f, 0f);
        yield return new WaitForSeconds(40f);
        ase.PlayOneShot(se[1]);
        tf.localScale += new Vector3(-0.6f, 0f, 0f);
    }
    IEnumerator Shorter()
    {
        ase.PlayOneShot(se[1]);
        tf.localScale -= new Vector3(0.6f, 0f, 0f);
        yield return new WaitForSeconds(20f);
        ase.PlayOneShot(se[0]);
        tf.localScale -= new Vector3(-0.6f, 0f, 0f);
    }
    IEnumerator Invisible()
    {
        ase.PlayOneShot(se[2]);
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject go in temp)
        {
            go.GetComponent<Renderer>().material.color = new Color(go.GetComponent<Renderer>().material.color.r, go.GetComponent<Renderer>().material.color.g, go.GetComponent<Renderer>().material.color.b, 0);
        }
        yield return new WaitForSeconds(1f);
        temp = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject go in temp)
        {
            go.GetComponent<Renderer>().material.color = new Color(go.GetComponent<Renderer>().material.color.r, go.GetComponent<Renderer>().material.color.g, go.GetComponent<Renderer>().material.color.b, 1);
        }
    }
    IEnumerator Slow()
    {
        speed *= 0.5f;
        yield return new WaitForSeconds(3f);
        speed *= 2f;
    }
}
