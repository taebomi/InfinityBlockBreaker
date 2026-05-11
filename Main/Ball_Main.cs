using UnityEngine;
using System.Collections;

public class Ball_Main : MonoBehaviour
{
    Rigidbody2D rb;
    TrailRenderer tr;
    SpriteRenderer sr;
    AudioSource ase;
    public Vector2 ball_speed;
    float ball_MinSpeed;
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
        rb.velocity = new Vector2(0, -3f);
    }
    void PlaySE()
    {
        ase.Play();
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        PlaySE();
        ball_speed = rb.velocity;
        if (coll.gameObject.tag == "Bar")
        {
            Vector3 vel = Vector3.zero;
            vel.x = (transform.position.x - coll.transform.position.x) * 5.0f;
            vel.y = rb.velocity.y;
            vel.Normalize();
            vel *= ball_speed.magnitude;
            rb.velocity = vel;
        }
        if (Mathf.Abs(ball_speed.x) > Mathf.Abs(ball_speed.y * 3))
        {
            ball_speed.y *= 2;
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
}
