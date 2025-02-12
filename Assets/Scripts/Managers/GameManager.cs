using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float currentSpeed;

    private bool isGameActive;
    private UIManager.GameUI currentGameUI;
    private GameObject playerObj;

    private void Start()
    {
        currentGameUI = UIManager.instance.GetCurrentActiveUI();
        playerObj = FindAnyObjectByType<PlayerController>().gameObject;
    }

    private void Update()
    {
        if (currentGameUI != UIManager.instance.GetCurrentActiveUI())
        {
            currentGameUI = UIManager.instance.GetCurrentActiveUI();
        }

        if (isGameActive && currentGameUI == UIManager.GameUI.InGame && Input.GetKeyDown(KeyCode.Escape))
        {
            isGameActive = false;
            UIManager.instance.ShowUI(UIManager.GameUI.Pause);
        }
    }

    public void GameOver()
    {
        Debug.Log("Hai perso!");
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

}
