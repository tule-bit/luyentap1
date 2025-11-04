using System.Collections;
using UnityEngine;

public class EnemyZoneAttack : MonoBehaviour, IcanTakeDamge
{
    [Header("Attack Settings")]
    public int damageAmount = 10;
    public float attackInterval = 1.0f;
    public string playerTag = "Player";

    [Header("Chase Settings")]
    public float moveSpeed = 3f;
    public float stopDistance = 1.5f;

    [Header("Optional Settings")]
    public Animator anim;

    private Coroutine attackCoroutine;
    private Transform currentTarget;
    private Rigidbody2D rb;
    private EnemyAl enemyPatrol; // ← tham chiếu tới script EnemyAl
    private bool chasing = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyPatrol = GetComponent<EnemyAl>();
    }

    public void TakeDamage(int damage, Vector2 hitPoint, GameObject hitDirection)
    {
        Debug.Log($"{gameObject.name} nhận {damage} damage!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            currentTarget = collision.transform;
            chasing = true;

            // Tắt patrol trong EnemyAl
            if (enemyPatrol != null)
                enemyPatrol.enabled = false;

            if (attackCoroutine == null)
                attackCoroutine = StartCoroutine(ChaseAndAttack());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            chasing = false;
            currentTarget = null;

            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }

            rb.linearVelocity = Vector2.zero;

            // Bật lại patrol
            if (enemyPatrol != null)
                enemyPatrol.enabled = true;
        }
    }

    private IEnumerator ChaseAndAttack()
    {
        while (currentTarget != null)
        {
            float distance = Vector2.Distance(transform.position, currentTarget.position);

            if (distance > stopDistance)
            {
                // Di chuyển tới Player
                Vector2 direction = (currentTarget.position - transform.position).normalized;
                rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

                // Lật hướng theo hướng di chuyển
                if (direction.x != 0)
                    transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
            }
            else
            {
                // Dừng lại & tấn công
                rb.linearVelocity = Vector2.zero;

                var playerHealth = currentTarget.GetComponent<IcanTakeDamge>();
                if (playerHealth != null)
                    playerHealth.TakeDamage(damageAmount, currentTarget.position, gameObject);

                if (anim != null)
                    anim.SetTrigger("Attack");

                yield return new WaitForSeconds(attackInterval);
            }

            yield return null;
        }

        rb.linearVelocity = Vector2.zero;
    }
}
