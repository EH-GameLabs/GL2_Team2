using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public List<Transform> obstacles = new List<Transform>();
    public float obstacleSpeed = 1.0f;

    private void Update()
    {
        print("obs: " + obstacles[0]);
        print("count: " + obstacles.Count);
        print("pos: " + obstacles[0].position);
        obstacles[0].Translate(Vector3.right * Time.deltaTime * obstacleSpeed);
    }

}
