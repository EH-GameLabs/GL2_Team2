using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private int stepsToIncreaseSpeed;
    private float addVelocity = 1;
    private float currentSpeed;
    private float targetVelocity = 1; // Nuovo valore target per la velocità
    private float smoothSpeed = 2f; // Velocità di interpolazione
    private GameObject playerRef;

    private void Start()
    {
        currentSpeed = GameManager.instance.GetCurrentSpeed();
        playerRef = FindAnyObjectByType<PlayerController>().gameObject;
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.IsGameActive()) return;

        // Interpolazione per rendere il cambio di velocità graduale
        addVelocity = Mathf.Lerp(addVelocity, targetVelocity, Time.deltaTime * smoothSpeed);

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

    public void IncreaseSpeed(float value)
    {
        targetVelocity = value; // Imposta il nuovo valore target
    }

    public void DecreaseSpeed()
    {
        targetVelocity = 1; // Torna gradualmente alla velocità base
    }
}
