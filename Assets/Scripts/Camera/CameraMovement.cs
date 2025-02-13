using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float addVelocity = 1;
    private float currentSpeed;

    private void Start()
    {
        currentSpeed = GameManager.Instance.GetCurrentSpeed();
    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.IsGameActive()) return;
        transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed * addVelocity);
    }

    public void IncreaseSpeed(float value) { addVelocity = value; }
    public void DecreaseSpeed() { addVelocity = 1; }
}
