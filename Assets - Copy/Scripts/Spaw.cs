using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [Header("Cấu hình spawn")]
    public GameObject enemyPrefab;
    public Transform spawnPoint;            // Vị trí spawn
    public float spawnInterval = 2.0f;      // Khoảng cách giữa các lần spawn (giây)
    public int quantityPerSpawn = 10;       // Số lượng enemy spawn mỗi lần
    public float spawnRadius = 3.0f;        // Bán kính random vị trí spawn

    private float lastSpawnTime;

    void Start()
    {
        lastSpawnTime = Time.time;
    }

    void Update()
    {
        if (Time.time - lastSpawnTime >= spawnInterval)
        {
            StartCoroutine(SpawnEnemiesSmooth());
            lastSpawnTime = Time.time;
        }
    }

    IEnumerator SpawnEnemiesSmooth()
    {
        if (enemyPrefab == null)
        {
            Debug.LogWarning("⚠️ Chưa gán prefab enemy!");
            yield break;
        }

        Vector3 basePos = spawnPoint != null ? spawnPoint.position : transform.position;
        Quaternion baseRot = spawnPoint != null ? spawnPoint.rotation : transform.rotation;

        for (int i = 0; i < quantityPerSpawn; i++)
        {
            // Random quanh điểm spawn
            Vector3 offset = Random.insideUnitSphere * spawnRadius;
            offset.y = 0; // giữ enemy trên mặt phẳng

            Instantiate(enemyPrefab, basePos + offset, baseRot);

            // Giúp Unity không bị lag khi spawn số lượng lớn
            if (i % 50 == 0) yield return null;
        }
    }
}
