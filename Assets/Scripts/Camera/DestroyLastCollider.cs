using UnityEngine;

public class DestroyLastCollider : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Line"))
        {
            Destroy(other.gameObject);
        }
    }
}
