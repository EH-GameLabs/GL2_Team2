using UnityEngine;

public class FrontCollider : MonoBehaviour
{
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private float addSpeed = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            cameraMovement.IncreaseSpeed(addSpeed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            cameraMovement.DecreaseSpeed();
        }
    }
}
