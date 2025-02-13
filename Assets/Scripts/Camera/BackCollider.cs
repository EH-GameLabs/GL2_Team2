using UnityEngine;

public class BackCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            GameManager.Instance.GameOver();
        }
    }
}
