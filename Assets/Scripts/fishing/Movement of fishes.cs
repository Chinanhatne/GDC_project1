using UnityEngine;

public class FishSwim : MonoBehaviour
{
    [Header("Speed")]
    public float minSpeed = 1.5f;
    public float maxSpeed = 3.5f;
    private float speed;

    [Header("Rare Fish")]
    [Range(0f, 1f)] public float rareChance = 0.2f;
    public float rareSpeedMultiplier = 0.5f; // cá hiếm chậm hơn

    [Header("Movement")]
    public bool useCurve;
    public float curveAmplitude = 0.5f;
    public float curveFrequency = 2f;

    [Header("Direction")]
    public bool moveRight = true;

    [Header("Direction Change")]
    public float changeDirChance = 0.3f; // xác suất đổi hướng
    public float minChangeTime = 2f;
    public float maxChangeTime = 5f;
    private float changeTimer;

    [Header("Bounds")]
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -5f;
    public float maxY = 1f;

    private float startY;
    private float timeOffset;
    private bool isRare = false;

    void Start()
    {
        // random speed
        speed = Random.Range(minSpeed, maxSpeed);

        // cá hiếm
        if (Random.value < rareChance)
        {
            isRare = true;
            speed *= rareSpeedMultiplier;
        }

        // random kiểu bơi
        useCurve = Random.value > 0.5f;

        // random hướng
        moveRight = Random.value > 0.5f;

        // timer đổi hướng
        changeTimer = Random.Range(minChangeTime, maxChangeTime);

        startY = transform.position.y;
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

        // di chuyển ngang
        transform.Translate(Vector2.right * dir * speed * Time.deltaTime);

        // bơi cong
        if (useCurve)
        {
            float yOffset = Mathf.Sin(Time.time * curveFrequency + timeOffset) * curveAmplitude;
            float newY = Mathf.Clamp(startY + yOffset, minY, maxY);
            transform.position = new Vector2(transform.position.x, newY);
        }
    }

    void ChangeDirection()
    {
        changeTimer -= Time.deltaTime;

        if (changeTimer <= 0f && Random.value < changeDirChance)
        {
            moveRight = !moveRight;
            Flip();

            changeTimer = Random.Range(minChangeTime, maxChangeTime);
        }
    }

    void CheckDestroy()
    {
        if (transform.position.x > maxX + 1 ||
            transform.position.x < minX - 1 ||
            transform.position.y > maxY + 1 ||
            transform.position.y < minY - 1)
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