using UnityEngine;

public class BatAttack : MonoBehaviour
{
    public float swingDuration = 0.15f;

    private bool swinging;
    private float swingTimer;

    private Vector3 startPosition = new Vector3(-0.1f, -0.05f, 0f);
    private Vector3 attackPosition = new Vector3(0.1f, -0.05f, 0f);

    private Quaternion startRotation = Quaternion.Euler(0f, 0f, 90f);
    private Quaternion attackRotation = Quaternion.Euler(0f, 0f, 270f);

    void Start()
    {
        transform.localPosition = startPosition;
        transform.localRotation = startRotation;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !swinging)
        {
            swinging = true;
            swingTimer = 0f;
        }

        if (swinging)
        {
            swingTimer += Time.deltaTime;

            float progress = swingTimer / swingDuration;

            if (progress < 0.5f)
            {
                float t = progress * 2f;

                transform.localPosition =
                    Vector3.Lerp(startPosition, attackPosition, t);

                transform.localRotation =
                    Quaternion.Euler(0f, 0f, 90f + 180f * t);
            }
            else
            {
                float t = (progress - 0.5f) * 2f;

                transform.localPosition =
                    Vector3.Lerp(attackPosition, startPosition, t);

                transform.localRotation =
                    Quaternion.Euler(0f, 0f, 270f + 180f * t);
            }

            if (progress >= 1f)
            {
                swinging = false;

                transform.localPosition = startPosition;
                transform.localRotation = startRotation;
            }
        }
    }
}