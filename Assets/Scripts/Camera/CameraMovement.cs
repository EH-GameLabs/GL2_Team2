using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private int stepsToIncreaseSpeed;
    private float addVelocity = 1;
    private float currentSpeed;
    private GameObject playerRef;

    private void Start()
    {
        currentSpeed = GameManager.instance.GetCurrentSpeed();
        playerRef = FindAnyObjectByType<PlayerController>().gameObject;
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.IsGameActive()) return;
        transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed * addVelocity);

        UpdateCameraSpeed();
    }

    private void UpdateCameraSpeed()
    {
        if (playerRef.transform.position.z < 0) return;

        if ((playerRef.transform.position.z - 0.5f) % stepsToIncreaseSpeed == 0)
        {
            currentSpeed = 1 + (playerRef.transform.position.z - 0.5f) / stepsToIncreaseSpeed / 100;
        }
    }

    // starUML

    public void IncreaseSpeed(float value) { addVelocity = value; }
    public void DecreaseSpeed() { addVelocity = 1; }
}
