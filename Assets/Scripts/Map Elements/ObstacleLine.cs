using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ObstacleLine : MonoBehaviour
{
    [Header("River Info")]
    [SerializeField] private int numObstacles = 3;
    [SerializeField] private float obstacleMinSpeed = 1;
    [SerializeField] private float obstacleMaxSpeed;
    [SerializeField] private bool leftToRight;
    private Vector3 horizontalDir;
    private float obstacleSpeed = 1.0f;

    [Header("Obstacles")]
    [SerializeField] private List<GameObject> obstaclePrefabs;
    private List<Transform> obstacles = new List<Transform>();

    [Header("Spawn/Despawn")]
    [SerializeField] private GameObject point1;
    [SerializeField] private GameObject point2;

    private float spawnTimer;
    private float timer;

    private void Start()
    {
        obstacles.Clear();
        //for (int i = 0; i < numObstacles; i++)
        //{
        //    GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
        float size = obstaclePrefabs[0].GetComponent<BoxCollider>().size.x;
        //spawnTimer = Random.Range(1, numObstacles * 2) + size; //DELETED
        spawnTimer = Random.Range(1f, numObstacles) + size; //ADDED
        //    StartCoroutine(SpawnAtRandomInterval(obstaclePrefab, timer));
        //}
        leftToRight = Random.Range(0, 2) == 0 ? false : true;
        horizontalDir = leftToRight ? Vector3.right : Vector3.right;

        obstacleSpeed = Random.Range(obstacleMinSpeed, obstacleMaxSpeed);
        timer = spawnTimer;
    }

    private void Update()
    {
        if (UIManager.Instance.GetCurrentActiveUI() != UIManager.GameUI.InGame) return;

        if (obstacles.Count < numObstacles)
        {
            timer += Time.deltaTime;
            if (timer > spawnTimer)
            {
                SpawnObstacle(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)]); //ADDED
                float size = obstaclePrefabs[0].GetComponent<BoxCollider>().size.x; //ADDED
                spawnTimer = Random.Range(1f, numObstacles) + size;
                timer = 0;
            }
        }

        foreach (Transform t in obstacles)
        {
            t.Translate(obstacleSpeed * Time.deltaTime * horizontalDir);
            CheckIfOutsideMap(t);
        }
    }

    private void SpawnObstacle(GameObject obstaclePrefab)
    {
        GameObject g = Instantiate(obstaclePrefab,
            leftToRight ? point1.transform.position : point2.transform.position,
            leftToRight ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0),
            transform);
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
