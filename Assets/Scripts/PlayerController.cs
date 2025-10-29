using Unity.Mathematics;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    public Animator anim;
    public int move;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool FacingRight = true;
    public float Radius = 0.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        move = Animator.StringToHash("1_Move");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(Horizontal * speed, rb.linearVelocity.y);
        if(Horizontal > 0 && ! FacingRight || (Horizontal < 0 && FacingRight)){
               flip();
        }
        if (math.abs(rb.linearVelocity.x) > 0.1f)
        {
            anim.SetBool(move, true);
        }
        else
        {
            anim.SetBool(move, false);
        }
    }
    private void flip(){
        FacingRight = !FacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
