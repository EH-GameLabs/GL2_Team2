using System.Collections.Generic;
using UnityEngine;

public class ObstacleMapBlock : MonoBehaviour
{
    public List<GameObject> movingObstacles = new List<GameObject>();

    private void Update()
    {
        foreach (var obstacle in movingObstacles)
        {
            MoveObstacle(obstacle);
        }
    }

    private void MoveObstacle(GameObject obstacle)
    {
        // 
    }
}
