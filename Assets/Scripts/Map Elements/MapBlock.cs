using UnityEngine;

public class MapBlock : MonoBehaviour
{
    public void SpawnNextMapBlock()
    {
        GameObject obj = MapStorage.instance.GetNewBlock();
        Instantiate(obj); // pos -> transform.position + offset (0, 0, 0.5f)
        // Add obj to MapManager List
    }

    public void DespawnBlock()
    {
        // remove from MapManager List
        Destroy(gameObject);
    }
}
