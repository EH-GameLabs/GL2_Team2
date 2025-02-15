using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverLine : MonoBehaviour
{
    [Header("River Info")]
    [SerializeField] private int numObstacles = 3;
    [SerializeField] private float obstacleSpeed = 1.0f;
    [SerializeField] private bool leftToRight;
    private Vector3 horizontalDir;

    [Header("Obstacles")]
    public List<Transform> obstacles = new List<Transform>();
    [SerializeField] private GameObject logPrefab;

    [Header("Spawn/Despawn")]
    [SerializeField] private GameObject point1;
    [SerializeField] private GameObject point2;

    private void Start()
    {
        obstacles.Clear();
        for (int i = 0; i < numObstacles; i++)
        {
            float size = logPrefab.GetComponent<BoxCollider>().size.x;
            float timer = Random.Range(1 + i, numObstacles * 2 * i) + size * i;
            StartCoroutine(SpawnAtRandomInterval(timer));
        }
        leftToRight = Random.Range(0, 2) == 0 ? false : true;
    }

    private void Update()
    {
        horizontalDir = leftToRight ? Vector3.right : Vector3.left;
        foreach (Transform t in obstacles)
        {
            t.Translate(obstacleSpeed * Time.deltaTime * horizontalDir);
            CheckIfOutsideMap(t);
        }
    }

    private IEnumerator SpawnAtRandomInterval(float timer)
    {
        yield return new WaitForSeconds(timer);
        GameObject g = Instantiate(logPrefab, leftToRight ? point1.transform.position : point2.transform.position, Quaternion.identity, transform);
        obstacles.Add(g.transform);
    }

    private void CheckIfOutsideMap(Transform t)
    {
        if (leftToRight)
        {
            if (Vector3.Distance(t.position, point2.transform.position) < 1f)
            {
                t.position = point1.transform.position;
            }
        }
        else
        {
            if (Vector3.Distance(t.position, point1.transform.position) < 1f)
            {
                t.position = point2.transform.position;
            }
        }
    }

}
