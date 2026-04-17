using UnityEngine;
using System.Collections;

public class JellyfishSpawner : MonoBehaviour
{
    public GameObject jellyfishPrefab;

    public float minTime = 1f;
    public float maxTime = 3f;

    // vùng nước (từ hình của bạn)
    public float minY = -5f;
    public float maxY = 1f;

    public float spawnXLeft = -10f;
    public float spawnXRight = 10f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            float time = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(time);

            Spawn();
        }
    }

    void Spawn()
{
    float y = Random.Range(minY, maxY);

    bool spawnRight = Random.value > 0.5f;
    float x = spawnRight ? spawnXRight : spawnXLeft;

    GameObject jelly = Instantiate(jellyfishPrefab, new Vector2(x, y), Quaternion.identity);

    if (jelly != null)
    {
        JellyfishMove move = jelly.GetComponent<JellyfishMove>();

        if (move != null)
        {
            move.direction = spawnRight ? -1 : 1;
        }
    }
}
}