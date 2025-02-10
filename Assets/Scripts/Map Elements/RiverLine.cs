using System.Collections.Generic;
using UnityEngine;

public class RiverLine : MonoBehaviour
{
    [SerializeField] private GameObject logPrefab;
    public List<Transform> obstacles = new List<Transform>();
    public float obstacleSpeed = 1.0f;

    private void Start()
    {
        obstacles.Clear();
        GameObject g = Instantiate(logPrefab, transform.position + new Vector3(-4.5f, -0.5f, 0), Quaternion.identity, transform);
        obstacles.Add(g.transform);
    }

    private void Update()
    {
        foreach (Transform t in obstacles)
        {
            t.Translate(Vector3.right * Time.deltaTime * obstacleSpeed);
        }
    }

}
