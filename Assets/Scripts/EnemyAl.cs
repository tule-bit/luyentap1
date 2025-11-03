using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class EnemyAl : MonoBehaviour
{
    [Header("Enemy AI Settings")]
    public float speed = 2f;
    public float moveDisance = 5f;
    private Vector2 starPos;
    private bool movingRight = true;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        starPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveHorizontal();
    }
    void MoveHorizontal()
    {
        float distance = Mathf.Abs(transform.position.x - starPos.x);
        if(distance >= moveDisance)
        {
            movingRight = !movingRight;
        }
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;
        rb.linearVelocity = direction * speed;
        Vector3 scale = transform.localScale;
        scale .x = movingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.left * moveDisance, transform.position + Vector3.right * moveDisance);
    }
}

