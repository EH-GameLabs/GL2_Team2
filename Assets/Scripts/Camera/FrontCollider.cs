using UnityEngine;

public class FrontCollider : MonoBehaviour
{
    [SerializeField] private CameraMovement CameraMovement;
    [SerializeField] private float addSpeed = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            CameraMovement.IncreaseSpeed(addSpeed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            CameraMovement.DecreaseSpeed();
        }
    }
}
