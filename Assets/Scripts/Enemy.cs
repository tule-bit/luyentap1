using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Enemy : MonoBehaviour, IcanTakeDamge
{
    public int health = 100;
    public int currentHealth;
    public bool isDeath = false;
    private Rigidbody2D rb;

    public void Start()
    {
        currentHealth = health;
        rb = GetComponent<Rigidbody2D>();
    }
    public void TakeDamage(int damage, Vector2 HitPoint, GameObject hitdirection)
    {
        if(isDeath) return;
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            isDeath = true;
            Die();
        }
    }
    public void Die()
    {
        rb.linearVelocity = Vector2.zero;
        Debug.Log("Enemy Died");
        Destroy(gameObject);
        
    }
}
