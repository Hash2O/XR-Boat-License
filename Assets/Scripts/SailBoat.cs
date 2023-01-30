using UnityEngine;

public class SailBoat : MonoBehaviour
{
    public float maxTiltAngle = 30f;
    public float tiltSpeed = 5f;

    private float currentTilt = 0f;
    private float targetTilt = 0f;
    private float tiltDirection = 1f;

    [SerializeField] GameObject _direction;
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        targetTilt = horizontal * maxTiltAngle;

        if (Mathf.Abs(currentTilt - targetTilt) < 0.1f)
        {
            currentTilt = targetTilt;
        }
        else
        {
            tiltDirection = (targetTilt - currentTilt) / Mathf.Abs(targetTilt - currentTilt);
            currentTilt += tiltSpeed * tiltDirection * Time.deltaTime;
        }

        transform.rotation = _direction.transform.rotation * Quaternion.Euler(0f, 0f, currentTilt);
    }
}
