using UnityEngine;
using System.Collections;

public class Ball_Control_Multi : MonoBehaviour
{

    Rigidbody2D rb;
    TrailRenderer tr;
    SpriteRenderer sr;
    AudioSource ase;
    float ball_MinSpeed;
    float[] ball_MaxSpeed;
    int touchcount;
    Vector2 ball_speed;
    //public BallEffect[] balleffect;
    public Material[] effect15;
    public Material[] effect20;
    public Material[] effect30;
    public Material[] effect40;
    public Material[] effect50;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        sr = GetComponent<SpriteRenderer>();
        ase = GetComponent<AudioSource>();
        ball_MinSpeed = 20f;
        ball_MaxSpeed = new float[] { 30f, 40f, 48f, 55f, 60f, 65f };
        if (Game_Option_Multi.getball1p)
        {
            rb.velocity = new Vector2(7f, -7f);
        }
        else
        {
            rb.velocity = new Vector2(7f, 7f);
        }
        Game_Option_Multi.getball1p = !Game_Option_Multi.getball1p;
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        ase.Play();
        ball_speed = rb.velocity;
        float temp = ball_speed.magnitude;
        if (temp < 25)
        {
            tr.enabled = false;
        }
        else if (temp < 30)
        {
            tr.enabled = true;
            tr.materials = effect15;
        }
        else if (temp < 40)
        {
            tr.enabled = true;
            tr.materials = effect20;
        }
        else if (temp < 50)
        {
            tr.enabled = true;
            tr.materials = effect30;
        }
        else if (temp < 60)
        {
            tr.enabled = true;
            tr.materials = effect40;
        }
        else
        {
            tr.enabled = true;
            tr.materials = effect50;
        }
        ase.PlayOneShot(ase.clip);
        if (coll.gameObject.CompareTag("Bar1"))
        {
            if (!coll.gameObject.GetComponent<Bar_Control1>().bar_stat)
            {
                if (touchcount < 5)
                {
                    touchcount++;
                }
            }
            else {
                if (touchcount > 0)
                {
                    touchcount--;
                }
            }
        }
        else if (coll.gameObject.CompareTag("Bar2"))
        {
            if (!coll.gameObject.GetComponent<Bar_Control2>().bar_stat)
            {
                if (touchcount < 5)
                {
                    touchcount++;
                }
            }
            else {
                if (touchcount > 0)
                {
                    touchcount--;
                }
            }
        }
        if (Mathf.Abs(ball_speed.x) > Mathf.Abs(ball_speed.y * 3))
        {
            ball_speed.y *= 2;
            ball_speed.Normalize();
            ball_speed *= temp;
            rb.velocity = ball_speed;
        }
        if (temp < ball_MinSpeed)
        {
            ball_speed.Normalize();
            ball_speed *= ball_MinSpeed;
            rb.velocity = ball_speed;
        }
        else if (temp > ball_MaxSpeed[touchcount])
        {
            ball_speed.Normalize();
            ball_speed *= ball_MaxSpeed[touchcount];
            rb.velocity = ball_speed;

        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bar1") || other.gameObject.CompareTag("Bar2"))
        {
            Vector3 vel = Vector3.zero;
            vel.x = (transform.position.x - other.transform.position.x) * 5.0f;
            vel.y = rb.velocity.y;
            vel.Normalize();
            vel *= ball_speed.magnitude;
            rb.velocity = vel;
        }
    }
}
