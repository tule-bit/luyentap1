using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [Header("Fire Settings")]
    public float bulletSpeed = 10f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private PlayerController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        shoot();
    }
    void shoot()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if(playerController.FaceRight())
            {
                rb.linearVelocity = Vector2.right * bulletSpeed;
                Debug.Log("Fired Right");
            }
            else
            {
                rb.linearVelocity = Vector2.left * bulletSpeed;
                Debug.Log("Fired Left");
            }
        }
    }
}
