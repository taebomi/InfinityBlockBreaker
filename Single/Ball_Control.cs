using UnityEngine;
using System.Collections;
public class Ball_Control : MonoBehaviour
{
    Rigidbody2D rb;
    TrailRenderer tr;
    SpriteRenderer sr;
    AudioSource ase;
    public Vector2 ball_speed;
    float ball_MinSpeed;
    public int touchcount;
    public float dmg;
    float mindmg;
    public Sprite[] ballcolor;
    public Material[] effect50;
    public Material[] effect15;
    public AudioClip se;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        ase = GetComponent<AudioSource>();
    }
    void Start()
    {
        ball_MinSpeed = 15f;
        rb.velocity = new Vector2(0, -ball_MinSpeed);
        touchcount = 0;
    }
    void SetDMG(float d)
    {
        mindmg = d;
        dmg = mindmg;
    }
    void PlaySE()
    {
        ase.Play();
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        PlaySE();
        ball_speed = rb.velocity;
        if (coll.gameObject.tag == "Block")
        {
            touchcount++;
            if (touchcount % 10 == 0)
            {
                mindmg++;
                dmg = mindmg;
            }
            if (touchcount < 100)
            {
                sr.sprite = ballcolor[(touchcount/10)];
                if (touchcount == 30)
                {
                    tr.materials = effect50;
                    tr.enabled = true;
                }
            }
        }
        if (Mathf.Abs(ball_speed.x) > Mathf.Abs(ball_speed.y * 3))
        {
            ball_speed.y *=2;
            ball_speed.Normalize();
            ball_speed *= ball_MinSpeed;
            rb.velocity = ball_speed;
        }
        if (ball_speed.magnitude < ball_MinSpeed)
        {
            ball_speed.Normalize();
            ball_speed *= ball_MinSpeed;
            rb.velocity = ball_speed;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bar"))
        {
            Vector3 vel = Vector3.zero;
            vel.x = (transform.position.x - other.transform.position.x) * 5.0f;
            vel.y = rb.velocity.y;
            vel.Normalize();
            vel *= ball_speed.magnitude;
            rb.velocity = vel;
        }
    }
    IEnumerator PowerUp()
    {
        tr.enabled = true;
        if (touchcount >= 50)
        {
            tr.materials = effect15;
        }
        mindmg *= 2f;
        mindmg += 3;
        dmg = mindmg;
        ball_speed = rb.velocity;
        ball_speed *= 2;
        rb.velocity = ball_speed;
        ase.PlayOneShot(se);
        GameObject.FindGameObjectWithTag("GM").GetComponent<AudioSource>().mute = true;
        yield return new WaitForSeconds(5.0f);
        GameObject.FindGameObjectWithTag("GM").GetComponent<AudioSource>().mute = false;
        mindmg -= 3;
        mindmg /= 2f;
        dmg = mindmg;
        if (touchcount < 30)
        {
            tr.enabled = false;
        }
        else
        {
            tr.materials = effect50;
        }
        ball_speed = rb.velocity;
        ball_speed /= 2;
        rb.velocity = ball_speed;
    }
}
