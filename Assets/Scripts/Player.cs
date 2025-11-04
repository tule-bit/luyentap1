using UnityEngine;

public class Player : MonoBehaviour, IcanTakeDamge
{
    public int health = 100;
    public int currentHealth;
    private int IsDeadId;
    private Animator anim;
    private PlayerController playerController;
    private PlayerFire playerFire;
    private bool isDead = false;
    void Start()
        {
            currentHealth = health;
            anim = GetComponentInChildren<Animator>();
            playerController = GetComponent<PlayerController>();
            playerFire = GetComponent<PlayerFire>();
            IsDeadId = Animator.StringToHash("4_Death");

        }
    public void TakeDamage(int damage, Vector2 HitPoint, GameObject hitdirection)
    {
        if (isDead) return;
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            Die();
        }
    }
    private void Die()
    {
        anim.SetBool(IsDeadId, true);
        if (playerController != null) { playerController.enabled = false; }
        playerFire.enabled = false;
        Debug.Log("Game Over");
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    
}
