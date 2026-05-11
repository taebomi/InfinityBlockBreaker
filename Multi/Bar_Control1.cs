using UnityEngine;
using System.Collections;
using CnControls;

public class Bar_Control1 : MonoBehaviour {

    public float speed;
    float move;
    Vector3 bar_position;
    public bool bar_stat = true;
    Rigidbody2D rb;
    Transform tf;
    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
    }
    void Start()
    {
        bar_position = transform.position;
    }
    void Update()
    {
        tf.Translate(new Vector2(CnInputManager.GetAxis("Horizontal") * 30f * Time.deltaTime, 0));
        // x축 움직임 최대치
        if (transform.position.x > 10)
        {
            tf.position = new Vector2(10, transform.position.y);
        }
        if (transform.position.x < -10)
        {
            tf.position = new Vector2(-10, transform.position.y);
        }
    }
    void FixedUpdate()
    {
        // 앞으로 팡!
        if (bar_stat && CnInputManager.GetButton("Fire1"))
        {
            bar_stat = false;
            rb.velocity = new Vector2(0f, 3f);
        }
        // 뒤로 오자
        if (transform.position.y > bar_position.y + 1.0f)
        {
            rb.velocity = new Vector2(0f, -2f);
        }
        // 너무 뒤로 갔다.
        if (transform.position.y < bar_position.y)
        {
            bar_stat = true;
            tf.position = new Vector2(transform.position.x, bar_position.y);
            rb.velocity = new Vector2(0f, 0f);
        }
    }
}
