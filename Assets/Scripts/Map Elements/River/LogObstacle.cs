using UnityEngine;

public class LogObstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            if (!GameManager.instance.IsGameActive()) return;
            other.transform.position = this.transform.position + new Vector3(0, 0.5f, 0);
        }
    }
}
