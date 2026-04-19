using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [Header("Fish Prefabs")]
    public GameObject[] fishPrefabs;

    [Header("Spawn Time")]
    public float minTime = 1f;
    public float maxTime = 3f;

    [Header("Spawn Area")]
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -5f;
    public float maxY = 1f;

    [Header("Limit")]
    public int maxFishAlive = 10;

    private float timer;
    private int currentFish = 0;

    void Start()
    {
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
        if (currentFish >= maxFishAlive) return;

        int spawnCount = Random.Range(1, 3); // số cá mỗi lần spawn

        for (int i = 0; i < spawnCount; i++)
        {
            if (currentFish >= maxFishAlive) break;

            SpawnFish();
        }
    }

    void SpawnFish()
    {
        GameObject prefab = fishPrefabs[Random.Range(0, fishPrefabs.Length)];

        float y = Random.Range(minY, maxY);

        // spawn từ trái hoặc phải
        bool fromLeft = Random.value > 0.5f;
        float x = fromLeft ? minX : maxX;

        GameObject fish = Instantiate(prefab, new Vector2(x, y), Quaternion.identity);

        FishSwim fishScript = fish.GetComponent<FishSwim>();
        if (fishScript != null)
        {
            fishScript.minX = minX;
            fishScript.maxX = maxX;
            fishScript.minY = minY;
            fishScript.maxY = maxY;

            fishScript.moveRight = fromLeft;
        }

        currentFish++;

        // theo dõi khi cá bị destroy
        StartCoroutine(TrackFish(fish));
    }

    System.Collections.IEnumerator TrackFish(GameObject fish)
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