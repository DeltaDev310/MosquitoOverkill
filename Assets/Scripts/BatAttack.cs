using UnityEngine;
using System.Collections;

public class BatAttack : MonoBehaviour
{
    public float swingAngle = 180f;
    public float swingDuration = 0.25f;

    private bool isSwinging = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isSwinging)
        {
            StartCoroutine(Swing());
        }
    }

    IEnumerator Swing()
    {
        isSwinging = true;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector2 direction = mousePosition - transform.position;

        float aimAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Player faces DOWN by default.
        // The bat sprite itself has a 90° rotation,
        // so compensate for that here.
        float centerAngle = aimAngle - 90f;

        // 180° swing: left side → front → right side
        float startAngle = centerAngle + 90f;
        float endAngle = centerAngle - 90f;

        float elapsed = 0f;

        while (elapsed < swingDuration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / swingDuration;
            t = Mathf.SmoothStep(0f, 1f, t);

            float currentAngle = Mathf.Lerp(startAngle, endAngle, t);

            transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);

            yield return null;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, endAngle);

        isSwinging = false;
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
    if (!isSwinging)
        return;

    if (other.CompareTag("Mosquito"))
    {
        Mosquito mosquito = other.GetComponent<Mosquito>();

        if (mosquito != null)
        {
            mosquito.Die();
        }
    }
}	
}