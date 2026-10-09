using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public float lifetime = 2f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("BULLET HIT: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Mosquito"))
        {
            Debug.Log("BULLET HIT MOSQUITO");

            collision.gameObject.GetComponent<Mosquito>().Die();

            Destroy(gameObject);
        }
    }
}