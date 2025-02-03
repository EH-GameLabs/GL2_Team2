using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isMoving = false;
    private Vector3 originPos, targetPos;
    private Rigidbody playerRb;

    [Header("Player Stats")]
    [SerializeField] private float timeToMove = 0.2f;
    [SerializeField] private float maxHeight = 0.5f;

    [Header("Animator")]
    [SerializeField] private Animator animator;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        originPos = transform.position;
    }

    void Update()
    {
        if (!isMoving) // Previene l'accumulo di coroutines
        {
            Move();
            RotateWithAnimation();
        }
    }

    private void RotateWithAnimation()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("LoadingJump", true);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("LoadingJump", true);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetBool("LoadingJump", true);
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetBool("LoadingJump", true);
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    private void Move()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            StartCoroutine(MovePlayer(Vector3.forward));
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            StartCoroutine(MovePlayer(Vector3.back));
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            StartCoroutine(MovePlayer(Vector3.left));
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            StartCoroutine(MovePlayer(Vector3.right));
        }
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        animator.SetBool("LoadingJump", false);
        if (direction == Vector3.forward)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction == Vector3.right)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (direction == Vector3.back)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        isMoving = true;

        float time = 0;
        Vector3 startPos = new Vector3(transform.position.x, originPos.y, transform.position.z);
        targetPos = startPos + direction;
        originPos = targetPos;

        while (time < timeToMove)
        {
            float t = time / timeToMove;
            float height = Mathf.Sin(t * Mathf.PI) * maxHeight; // Crea il picco a metà del movimento
            transform.position = Vector3.Lerp(startPos, targetPos, t) + Vector3.up * height;

            time += Time.deltaTime;
            yield return null;
        }

        transform.position = new Vector3(targetPos.x, originPos.y, targetPos.z);
        isMoving = false;
    }
}

