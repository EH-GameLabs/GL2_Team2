using UnityEngine;

public class GameManager : MonoBehaviour//Singleton<GameManager>
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    [SerializeField] private float currentSpeed;

    private bool isGameActive;
    private UIManager.GameUI currentGameUI;
    private GameObject playerObj;

    private int beers;

    private void Start()
    {
        beers = 0;
        currentGameUI = UIManager.Instance.GetCurrentActiveUI();
        playerObj = FindAnyObjectByType<PlayerController>().gameObject;
    }

    private void Update()
    {
        if (currentGameUI != UIManager.Instance.GetCurrentActiveUI())
        {
            currentGameUI = UIManager.Instance.GetCurrentActiveUI();
        }

        if (isGameActive && currentGameUI == UIManager.GameUI.InGame && Input.GetKeyDown(KeyCode.P))
        {
            isGameActive = false;
            UIManager.Instance.ShowUI(UIManager.GameUI.Pause);
        }
    }

    public void GameOver()
    {
        isGameActive = false;
        FindAnyObjectByType<InGameUI>().GoToGameOver();
    }

    internal void ResetAll()
    {
        // reset map & positions
        print("transform");
        playerObj.transform.position = new Vector3(0.5f, 0f, -3.5f);
        playerObj.transform.rotation = Quaternion.Euler(0, 180, 0);
        //playerObj.SetActive(true);
    }

    // GETTERS & SETTERS
    public bool IsGameActive() { return isGameActive; }
    public void SetIsGameActive(bool active) { isGameActive = active; }
    public float GetCurrentSpeed() { return currentSpeed; }
    public void SetBeersAmount(int amount) { beers = amount; }
    public int GetCurrentBeersAmount() { return beers; }
}
