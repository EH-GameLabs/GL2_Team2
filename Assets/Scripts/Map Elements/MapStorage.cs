using System.Collections.Generic;
using UnityEngine;

public class MapStorage : Singleton<MapStorage>
{
    public List<GameObject> TypeBlocks = new List<GameObject>();


    public GameObject GetNewBlock()
    {
        return TypeBlocks[Random.Range(0, TypeBlocks.Count - 1)];
    }
}
