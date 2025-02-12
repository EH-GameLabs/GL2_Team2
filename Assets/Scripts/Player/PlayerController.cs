using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private float timeToMove = 0.2f;
    [SerializeField] private float maxHeight = 0.5f;

    [Header("Animator")]
    [SerializeField] private Animator animator;

    [Header("Layers")]
    [SerializeField] private LayerMask mapLayer;
    [SerializeField] private LayerMask logLayer;
    [SerializeField] private LayerMask waterLayer;

    private bool isMoving = false;
    private KeyCode currentKey;
    private Vector3 currentDirection;
    private MovementType currentMovType;
    private Vector3 targetPos;
    private Vector3 rayOffset = new Vector3(0, 0.5f, 0);
    private RaycastHit hit;
    public enum MovementType
    {
        Normal,
        Log,
    }

    public delegate void MyDelegate(KeyCode key, Vector3 direction, MovementType movementType);
    MyDelegate myDelegate;

    void Update()
    {
        if (!GameManager.instance.IsGameActive()) return;

        if (!isGrounded(mapLayer) && !isGrounded(logLayer) && isGrounded(waterLayer))
        {
            GameManager.instance.GameOver();
            //gameObject.SetActive(false);
        }

        Debug.DrawRay(transform.position + rayOffset, Vector3.down * 5, Color.yellow);
        if (!isMoving && isGrounded(mapLayer))
        {
            currentMovType = MovementType.Normal;
            RotateWithAnimation(MovementType.Normal);
        }
        if (isGrounded(logLayer))
        {
            currentMovType = MovementType.Log;
            RotateWithAnimation(MovementType.Log);
        }
        myDelegate?.Invoke(currentKey, currentDirection, currentMovType);
    }

    public bool isGrounded(LayerMask layer) => Physics.Raycast(transform.position + rayOffset, Vector3.down, out hit, 0.6f, layer);

    private void RotateWithAnimation(MovementType movementType)
    {
        if (movementType == MovementType.Log)
        {
            transform.position = hit.transform.position + rayOffset;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            UIManager.instance.ShowUI(UIManager.GameUI.InGame);
            PrepareToMove(KeyCode.W, Vector3.forward);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            UIManager.instance.ShowUI(UIManager.GameUI.InGame);
            PrepareToMove(KeyCode.S, Vector3.back);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            UIManager.instance.ShowUI(UIManager.GameUI.InGame);
            PrepareToMove(KeyCode.A, Vector3.left);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            UIManager.instance.ShowUI(UIManager.GameUI.InGame);
            PrepareToMove(KeyCode.D, Vector3.right);
        }

    }

    private void PrepareToMove(KeyCode key, Vector3 direction)
    {
        animator.SetBool("LoadingJump", true);
        if (!CanPlayerMove(direction))
        {
            animator.SetBool("LoadingJump", false);
            return;
        }

        transform.rotation = Quaternion.LookRotation(direction);
        currentKey = key;
        currentDirection = direction;
        isMoving = true;
        myDelegate = Move;
    }

    private void Move(KeyCode key, Vector3 direction, MovementType movementType)
    {
        if (Input.GetKeyUp(key))
        {
            StartCoroutine(MovePlayer(direction, movementType));
            myDelegate = null;
        }
    }

    private IEnumerator MovePlayer(Vector3 direction, MovementType movementType)
    {
        currentKey = 0;
        animator.SetBool("LoadingJump", false);
        transform.rotation = Quaternion.LookRotation(direction);

        float time = 0;
        Vector3 startPos = new Vector3(transform.position.x, 0, transform.position.z);
        targetPos = startPos;
        if (movementType == MovementType.Log && (direction == Vector3.forward || direction == Vector3.back))
        {
            targetPos -= new Vector3(targetPos.x % 1f - 0.5f, 0, -direction.z);
        }
        else
        {
            targetPos += direction;
        }

        while (time < timeToMove)
        {
            float t = time / timeToMove;
            float height = Mathf.Sin(t * Mathf.PI) * maxHeight; // Crea il picco a metà del movimento
            transform.position = Vector3.Lerp(startPos, targetPos, t) + Vector3.up * height;

            time += Time.deltaTime;
            yield return null;
        }

        if (movementType == MovementType.Normal || movementType == MovementType.Log && (direction == Vector3.forward || direction == Vector3.back))
        {
            transform.position = new Vector3(targetPos.x, 0, targetPos.z);
        }
        isMoving = false;
    }

    private bool CanPlayerMove(Vector3 direction)
    {
        if (Physics.Raycast(transform.position + rayOffset, direction, 1, mapLayer))
            return false;

        return true;
    }
}

