using UnityEngine;

public class Mosquito : MonoBehaviour
{
    public float speed = 3f;

    private Rigidbody2D rb;
    private Vector2 direction;
    
    private Vector2 targetDirection;
    
    public GameObject mosquitoPrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PickNewDirection();
    }

    void FixedUpdate()
    {
        direction = Vector2.Lerp(
            direction,
            targetDirection,
            5f * Time.fixedDeltaTime
        ).normalized;

        rb.MovePosition(
            rb.position + direction * speed * Time.fixedDeltaTime
        );
    }

    void PickNewDirection()
    {
        direction = Random.insideUnitCircle.normalized;
        targetDirection = direction;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 wallNormal = collision.contacts[0].normal;

        targetDirection = Quaternion.Euler(
            0,
            0,
            Random.Range(-60f, 60f)
        ) * wallNormal;
    }
    public void Die()
{
    if (MosquitoManager.Instance.CanSpawn())
    {
        for (int i = 0; i < 2; i++)
        {
            if (!MosquitoManager.Instance.CanSpawn())
                break;

            Instantiate(
                mosquitoPrefab,
                transform.position,
                Quaternion.identity
            );

            MosquitoManager.Instance.RegisterSpawn();
        }
    }

    MosquitoManager.Instance.RegisterDeath();
    Destroy(gameObject);
}
}