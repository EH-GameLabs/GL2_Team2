using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [Header("Skins Data")]
    public List<SkinData> skinData;

    public delegate void MyDelegate(KeyCode key, Vector3 direction, MovementType movementType);
    MyDelegate myDelegate;

    private void Start()
    {
        SkinManager.instance.SetPlayerRef(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            PullRandomSkin();
        }

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
            CheckPosition();
            RotateWithAnimation(MovementType.Normal);
        }
        if (isGrounded(logLayer))
        {
            currentMovType = MovementType.Log;
            RotateWithAnimation(MovementType.Log);
        }
        myDelegate?.Invoke(currentKey, currentDirection, currentMovType);
    }

    public bool isGrounded(LayerMask layer) => Physics.Raycast(transform.position + rayOffset, Vector3.down, out hit, 1f, layer);

    #region MOVE
    private void RotateWithAnimation(MovementType movementType)
    {
        if (movementType == MovementType.Log)
        {
            transform.position = hit.transform.position + rayOffset;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            UIManager.Instance.ShowUI(UIManager.GameUI.InGame);
            PrepareToMove(KeyCode.W, Vector3.forward);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            UIManager.Instance.ShowUI(UIManager.GameUI.InGame);
            PrepareToMove(KeyCode.S, Vector3.back);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            UIManager.Instance.ShowUI(UIManager.GameUI.InGame);
            PrepareToMove(KeyCode.A, Vector3.left);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            UIManager.Instance.ShowUI(UIManager.GameUI.InGame);
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
            targetPos -= new Vector3(0, 0, -direction.z);
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
    #endregion

    private void CheckPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.FloorToInt(pos.x) + 0.5f;
        transform.position = pos;
    }

    #region SKINS
    public void SetActiveSkin()
    {
        bool firstActive = false;
        foreach (SkinData skin in skinData)
        {
            skin.skin.SetActive(skin.isActive && !skin.isLocked && !firstActive);
            if (skin.isActive) firstActive = true;
        }
    }

    public void UnlockSkin(GameObject unlockedSkin)
    {
        foreach (SkinData skin in skinData)
        {
            if (skin.skin == unlockedSkin)
            {
                skin.isLocked = false;
            }
        }
    }

    public void SelectSkin(GameObject selectedSkin)
    {
        foreach (SkinData skin in skinData)
        {
            skin.skin.SetActive(skin.skin == selectedSkin);
        }
    }

    public void PullRandomSkin()
    {
        GameObject skinToUnlock = null;
        List<SkinData> lockedSkin = new();

        // initialize lockedSkin
        foreach (SkinData skin in skinData)
        {
            if (skin.isLocked) lockedSkin.Add(skin);
        }

        // normalize weight
        print("prima: " + lockedSkin);
        NormalizeWeights(lockedSkin);
        print("dopo: " + lockedSkin);



        // unlock selected skin
        // UnlockSkin(skinToUnlock);
    }

    private void OnValidate()
    {
        NormalizeWeights(skinData);
    }

    private void NormalizeWeights(List<SkinData> skinData)
    {
        float total = skinData.Sum(e => e.weight);
        if (total == 0) return;

        for (int i = 0; i < skinData.Count; i++)
        {
            skinData[i].weight = Mathf.Round((skinData[i].weight / total) * 100f) / 100f; // Arrotonda a due cifre decimali
        }

        // Assicura che la somma sia esattamente 1 (aggiustando l'ultimo valore)
        float adjustedTotal = skinData.Sum(e => e.weight);
        if (adjustedTotal != 1f && skinData.Count > 0)
        {
            skinData[^1].weight += (1f - adjustedTotal);
        }
    }
    #endregion

}

[System.Serializable]
public class SkinData
{
    public GameObject skin;
    public bool isActive;
    public bool isLocked;
    [Range(0f, 1f)] public float weight;
}