using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private int stepsToIncreaseSpeed;
    private float addVelocity = 1;
    private float currentSpeed;
    private float targetVelocity = 1; // Nuovo valore target per la velocità
    private float smoothSpeed = 2f; // Velocità di interpolazione
    private GameObject playerRef;
    private Vector3 offset;

    private void Start()
    {
        currentSpeed = GameManager.instance.GetCurrentSpeed();
        playerRef = FindAnyObjectByType<PlayerController>().gameObject;
        offset = transform.position - playerRef.transform.position;
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.IsGameActive()) return;
        if (UIManager.Instance.GetCurrentActiveUI() != UIManager.GameUI.InGame) return;

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

    public void SetFocusOnPlayer()
    {
        StartCoroutine(FocusOnPlayerCoroutine());
    }

    private IEnumerator FocusOnPlayerCoroutine()
    {
        GameManager.instance.SetIsGameActive(false);
        Vector3 startPos = transform.position;
        Vector3 targetPos = playerRef.transform.position + offset;

        float elapsedTime = 0;

        while (elapsedTime < 1.5f)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / 1.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        GameManager.instance.GameOver();
    }
}
