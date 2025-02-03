using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    public List<GameObject> mapObjects;
    public List<GameObject> mapObstacles;


    public void SpawnObstacle()
    {
        GameObject obstacle = Instantiate(mapObstacles[0]); // position della mappa
        obstacle.GetComponent<Obstacle>().SpawnObstacle();
    }
}
