using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float TimeDestroy = 2.0f;
    public int Damage = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, TimeDestroy);
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            IcanTakeDamge takeDamge = collision.GetComponent<IcanTakeDamge>();
            if (takeDamge != null)
            {
                takeDamge.TakeDamage(Damage, Vector2.zero, gameObject);
            }
            Destroy(gameObject);
        }
        
    }
}
