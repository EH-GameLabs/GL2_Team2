using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isMoving = false;
    private KeyCode currentKey;
    private Vector3 currentDirection;
    private Vector3 originPos, targetPos;
    private Rigidbody playerRb;

    [Header("Player Stats")]
    [SerializeField] private float timeToMove = 0.2f;
    [SerializeField] private float maxHeight = 0.5f;

    [Header("Animator")]
    [SerializeField] private Animator animator;

    public delegate void MyDelegate(KeyCode key, Vector3 direction);
    MyDelegate myDelegate;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        originPos = transform.position;
    }

    void Update()
    {
        if (!isMoving) RotateWithAnimation();
        myDelegate?.Invoke(currentKey, currentDirection);
    }

    private void RotateWithAnimation()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            PrepareToMove(KeyCode.W, Vector3.forward);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            PrepareToMove(KeyCode.S, Vector3.back);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            PrepareToMove(KeyCode.A, Vector3.left);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            PrepareToMove(KeyCode.D, Vector3.right);
        }
    }

    private void PrepareToMove(KeyCode key, Vector3 direction) 
    {
        animator.SetBool("LoadingJump", true);
        transform.rotation = Quaternion.LookRotation(direction);
        currentKey = key;
        currentDirection = direction;
        isMoving = true;
        myDelegate = Move;
    }

    private void Move(KeyCode key, Vector3 direction)
    {
        if (Input.GetKeyUp(key)) 
        {
            StartCoroutine(MovePlayer(direction));
            myDelegate = null;
        }
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        currentKey = 0;
        animator.SetBool("LoadingJump", false);
        transform.rotation = Quaternion.LookRotation(direction);

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

