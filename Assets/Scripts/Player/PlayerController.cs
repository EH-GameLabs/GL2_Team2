using System.Collections;
using System.Collections.Generic;
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

    // score
    private int playerScore;
    private Score score;

    public enum MovementType
    {
        Normal,
        Log,
    }

    [Header("Skins Data")]
    public PlayerDataSO skinData;
    public List<GameObject> skins;

    public delegate void MyDelegate(KeyCode key, Vector3 direction, MovementType movementType);
    MyDelegate myDelegate;

    // Singleton
    public static PlayerController instance { get; set; }

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }

    private void Start()
    {
        PlayerDataManager.instance.SetPlayerRef(this);
        playerScore = -3;
    }

    void Update()
    {
        if (!GameManager.instance.IsGameActive()) return;
        score = FindAnyObjectByType<Score>();

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
            if (key == KeyCode.W)
            {
                playerScore++;
                if (score.GetScore() < playerScore)
                    score.SetScore(playerScore);
            }
            if (key == KeyCode.S)
            {
                playerScore--;
            }
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
    /// <summary>
    /// Set the current enable skin active and disable all the others
    /// </summary>
    public void SetActiveSkin()
    {
        foreach (var skin in skins)
        {
            if (skin != null)
                skin.SetActive(false);
        }

        for (int i = 0; i < skinData.Skins.Count; i++)
        {
            if (skinData.Skins[i].isActive && !skinData.Skins[i].isLocked)
            {
                if (i < skins.Count && skins[i] != null)
                {
                    skins[i].SetActive(true);
                }
                break;
            }
        }
    }

    /// <summary>
    /// Select the new active skin end enable it
    /// </summary>
    /// <param name="selectedSkin"></param>
    public void SelectSkin(GameObject selectedSkin)
    {
        foreach (SkinData skin in skinData.Skins)
        {
            skin.skin.SetActive(skin.skin == selectedSkin);
        }
        SetActiveSkin();
    }

    public void PullRandomSkin(int coinsToPull)
    {
        GameObject skinToUnlock = null;
        List<SkinData> lockedSkin = new();

        // initialize lockedSkin
        foreach (SkinData skin in skinData.Skins)
        {
            if (skin.isLocked) lockedSkin.Add(skin);
        }

        if (lockedSkin.Count > 0)
        {
            skinData.coins -= coinsToPull;
            FindAnyObjectByType<MainMenuUI>().SetCoins(skinData.coins.ToString());

            skinToUnlock = lockedSkin[Random.Range(0, lockedSkin.Count)].skin;
            print("skin unlocked: " + skinToUnlock);
            skinToUnlock.GetComponent<SkinTypeSelector>().ActiveSkin(true);

            // unlock selected skin
            UnlockSkin(skinToUnlock);
            PlayerDataManager.instance.SaveData();
        }
        else
        {
            Debug.Log("You have collected all the skins available!");
        }
    }

    public void UnlockSkin(GameObject unlockedSkin)
    {
        foreach (SkinData skin in skinData.Skins)
        {
            if (skin.skin == unlockedSkin)
            {
                skin.isLocked = false;
            }
        }
    }
    #endregion
}
