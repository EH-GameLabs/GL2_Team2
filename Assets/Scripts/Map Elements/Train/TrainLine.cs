using System.Collections;
using UnityEngine;

public class TrainLine : MonoBehaviour
{
    [Header("River Info")]
    [SerializeField] private float obstacleSpeed;
    [SerializeField] private float spawnTimer;
    [SerializeField] private bool leftToRight;
    private Vector3 horizontalDir;

    [Header("Obstacles")]
    [SerializeField] private GameObject obstaclePrefab;
    private GameObject currentTrain;

    [Header("Spawn/Despawn")]
    [SerializeField] private GameObject point1;
    [SerializeField] private GameObject point2;

    [Header("Animation")]
    [SerializeField] Animator animator;

    private void Start()
    {
        leftToRight = Random.Range(0, 2) == 0 ? false : true;
        //horizontalDir = leftToRight ? Vector3.right : Vector3.left;
        StartCoroutine(SpawnAtInterval(spawnTimer));
    }

    private void Update()
    {
        if (!GameManager.instance.IsGameActive()) return;
        if (currentTrain == null) return;

        currentTrain.transform.Translate(obstacleSpeed * Time.deltaTime * Vector3.right);
        CheckIfOutsideMap(currentTrain.transform);
    }

    private IEnumerator SpawnAtInterval(float timer)
    {
        animator.SetBool("ArrivingTrain", false);
        yield return new WaitForSeconds(timer);
        animator.SetBool("ArrivingTrain", true);
        currentTrain = Instantiate(obstaclePrefab,
            leftToRight ? point1.transform.position : point2.transform.position,
            leftToRight ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0),
            transform);
    }

    private void CheckIfOutsideMap(Transform t)
    {
        if (leftToRight)
        {
            if (Vector3.Distance(t.position, point2.transform.position) < 1f)
            {
                Destroy(currentTrain);
                StartCoroutine(SpawnAtInterval(spawnTimer));
            }
        }
        else
        {
            if (Vector3.Distance(t.position, point1.transform.position) < 1f)
            {
                Destroy(currentTrain);
                StartCoroutine(SpawnAtInterval(spawnTimer));
            }
        }
    }
}
