using UnityEngine;

public class Mosquito : MonoBehaviour
{
    public float speed = 3f;

    private Rigidbody2D rb;
    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PickNewDirection();
    }

    void FixedUpdate()
    {
        rb.MovePosition(
            rb.position + direction * speed * Time.fixedDeltaTime
        );
    }

    void PickNewDirection()
    {
        direction = Random.insideUnitCircle.normalized;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        PickNewDirection();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("MOSQUITO HIT " + collision.gameObject.name);

        PickNewDirection();
    }
}