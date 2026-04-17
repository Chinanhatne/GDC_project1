using UnityEngine;
using System.Collections;

public class FishSpawner : MonoBehaviour
{
    [Header("Fish Prefabs")]
    public GameObject[] fishPrefabs;

    [Header("Spawn Time")]
    public float minTime = 1f;
    public float maxTime = 3f;

    [Header("Limit")]
    public int maxFishAlive = 10;

    private float timer;
    private int currentFish = 0;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        ResetTimer();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            TrySpawn();
            ResetTimer();
        }
    }

    void TrySpawn()
    {
        if (fishPrefabs == null || fishPrefabs.Length == 0) return;
        if (currentFish >= maxFishAlive) return;

        int spawnCount = Random.Range(1, 3);

        for (int i = 0; i < spawnCount; i++)
        {
            if (currentFish >= maxFishAlive) break;

            SpawnFish();
        }
    }

    void SpawnFish()
    {
        GameObject prefab = fishPrefabs[Random.Range(0, fishPrefabs.Length)];

        if (prefab == null)
        {
            Debug.LogWarning("Prefab bị null!");
            return;
        }

        // 🎯 LẤY BOUNDS THEO CAMERA
        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        float minX = cam.transform.position.x - camWidth;
        float maxX = cam.transform.position.x + camWidth;
        float minY = cam.transform.position.y - camHeight;
        float maxY = cam.transform.position.y + camHeight;

        float y = Random.Range(minY, maxY);

        bool fromLeft = Random.value > 0.5f;

        // 👉 lấy size cá
        float halfWidth = 0.5f;
        SpriteRenderer sr = prefab.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            halfWidth = sr.bounds.size.x / 2f;
        }

        // 👉 spawn ngoài màn hình 1 chút
        float x = fromLeft ? minX - halfWidth : maxX + halfWidth;

        GameObject fish = Instantiate(prefab, new Vector2(x, y), Quaternion.identity);

        // setup script
        FishSwim fishScript = fish.GetComponent<FishSwim>();
        if (fishScript != null)
        {
            fishScript.minX = minX;
            fishScript.maxX = maxX;
            fishScript.minY = minY;
            fishScript.maxY = maxY;

            // ⚠️ QUAN TRỌNG
            fishScript.moveRight = fromLeft;
        }

        currentFish++;

        StartCoroutine(TrackFish(fish));
    }

    IEnumerator TrackFish(GameObject fish)
    {
        while (fish != null)
        {
            yield return null;
        }

        currentFish--;
    }

    void ResetTimer()
    {
        timer = Random.Range(minTime, maxTime);
    }
}