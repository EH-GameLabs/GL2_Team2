using UnityEngine;

public class SpawnNextCollider : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Line"))
        {
            MapManager.Instance.SpawnLine(MapManager.Instance.ChooseLine());
        }
    }
}
