using UnityEngine;

public class GameManager : MonoBehaviour//Singleton<GameManager>
{
    private static GameManager Instance;
    public static GameManager instance
    {
        get { return Instance; }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }

    [SerializeField] private float currentSpeed;

    private bool isGameActive;
    private UIManager.GameUI currentGameUI;

    private int beers;
    private int score;

    private void Start()
    {
        beers = 0;
        currentGameUI = UIManager.Instance.GetCurrentActiveUI();
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
        PlayerDataManager.instance.SaveData();
        isGameActive = false;
        FindAnyObjectByType<InGameUI>().GoToGameOver();
    }

    // GETTERS & SETTERS
    public bool IsGameActive() { return isGameActive; }
    public void SetIsGameActive(bool active) { isGameActive = active; }
    public float GetCurrentSpeed() { return currentSpeed; }
    public void SetBeersAmount(int amount) { beers = amount; }
    public int GetCurrentBeersAmount() { return beers; }
    public void SetScore(int score) { this.score = score; }
    public int GetScore() { return score; }
}
