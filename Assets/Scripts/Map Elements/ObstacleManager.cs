using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : Singleton<ObstacleManager>
{
    public List<GameObject> obstacles = new List<GameObject>();
    Obstacle tmpObstacle;

    private void Update()
    {
        foreach (var obstacle in obstacles)
        {
            tmpObstacle = obstacle.GetComponent<Obstacle>();
            Move(tmpObstacle);
            // check if outside the map
        }
    }

    private void Move(Obstacle tmpObstacle)
    {
        if (tmpObstacle.rightToLeft)
        {

        }
        else
        {

        }
    }

    // bring the obstacle to the spawnPoint
    private void RespawnObstacle()
    {

    }
}
