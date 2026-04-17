using UnityEngine;

public class FishSwim : MonoBehaviour
{
    [Header("Speed")]
    public float minSpeed = 1.5f;
    public float maxSpeed = 3.5f;
    private float speed;

    [Header("Rare Fish")]
    [Range(0f, 1f)] public float rareChance = 0.2f;
    public float rareSpeedMultiplier = 0.5f;

    [Header("Movement")]
    public bool useCurve;
    public float curveAmplitude = 0.5f;
    public float curveFrequency = 2f;

    [Header("Direction")]
    public bool moveRight = true;

    [Header("Direction Change")]
    public float changeDirChance = 0.3f;
    public float minChangeTime = 2f;
    public float maxChangeTime = 5f;
    private float changeTimer;

    [Header("Bounds")]
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private float baseY;
    private float timeOffset;
    private bool isRare = false;

    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);

        // cá hiếm
        if (Random.value < rareChance)
        {
            isRare = true;
            speed *= rareSpeedMultiplier;
        }

        useCurve = Random.value > 0.5f;

        changeTimer = Random.Range(minChangeTime, maxChangeTime);

        baseY = transform.position.y;
        timeOffset = Random.Range(0f, 100f);

        Flip();
    }

    void Update()
    {
        Move();
        ChangeDirection();
        CheckDestroy();
    }

    void Move()
    {
        float dir = moveRight ? 1 : -1;

        float newX = transform.position.x + dir * speed * Time.deltaTime;
        float newY = transform.position.y;

        if (useCurve)
        {
            float yOffset = Mathf.Sin(Time.time * curveFrequency + timeOffset) * curveAmplitude;
            newY = Mathf.Clamp(baseY + yOffset, minY, maxY);
        }

        transform.position = new Vector2(newX, newY);
    }

    void ChangeDirection()
    {
        changeTimer -= Time.deltaTime;

        // ❗ CHỈ cho đổi hướng khi còn trong màn hình
        if (changeTimer <= 0f &&
            transform.position.x > minX &&
            transform.position.x < maxX &&
            Random.value < changeDirChance)
        {
            moveRight = !moveRight;
            Flip();

            changeTimer = Random.Range(minChangeTime, maxChangeTime);
        }
    }

    void CheckDestroy()
    {
        // ra khỏi màn hình thì xoá
        if (transform.position.x > maxX + 2 ||
            transform.position.x < minX - 2 ||
            transform.position.y > maxY + 2 ||
            transform.position.y < minY - 2)
        {
            Destroy(gameObject);
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = moveRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}