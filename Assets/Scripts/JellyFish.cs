using UnityEngine;

public class JellyfishMove : MonoBehaviour
{
    public float speed = 2f;
    public int direction = -1; // -1 = trái, 1 = phải

    void Start()
    {
        // Lật sprite theo hướng
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }

    void Update()
{
    if (this == null) return;

    transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

    if (transform.position.x < -12f || transform.position.x > 12f)
    {
        Destroy(gameObject);
    }
}
}