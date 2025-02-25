using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour, IGameUI
{
    #region IGameUI
    public UIManager.GameUI gameUI;

    public UIManager.GameUI GetUIType()
    {
        return gameUI;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
    #endregion

    [Header("Pull")]
    [SerializeField] private TextMeshProUGUI coinsRequested;
    public TextMeshProUGUI coins;

    [Header("Dropdown")]
    [SerializeField] private Image dropdownButton;
    [SerializeField] private Sprite dropdown1;
    [SerializeField] private Sprite dropdown2;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private GameObject soundButton;
    [SerializeField] private GameObject exitButton;

    [Header("Settings")]
    [SerializeField] private GameObject imageSettings;

    [Header("Sound")]
    [SerializeField] private GameObject soundSettings;

    private bool isActive;

    private void Start()
    {
        dropdownButton.sprite = dropdown1;
        isActive = false;
        settingsButton.SetActive(false);
        soundButton.SetActive(false);
        exitButton.SetActive(false);
    }

    public void PlayGameUI()
    {
        UIManager.Instance.ShowUI(UIManager.GameUI.InGame);
        GameManager.instance.SetIsGameActive(true);
    }

    public void SelectionSkin()
    {
        SceneManager.LoadScene("SelectSkinScene", LoadSceneMode.Single);
    }

    public void TryPullASkin()
    {
        int playerCoins = int.Parse(coins.text);
        int coinsToPull = int.Parse(coinsRequested.text);

        if (playerCoins >= coinsToPull)
        {
            PlayerController.instance.PullRandomSkin(coinsToPull);
        }
        else
        {
            // animazione oste arrabbiato
            print("Animazione");
        }
    }

    public void SetCoins(string value)
    {
        coins.text = value;
    }

    // Options
    public void ShowDropdown()
    {
        isActive = !isActive;
        dropdownButton.sprite = isActive ? dropdown2 : dropdown1;
        settingsButton.SetActive(!settingsButton.activeInHierarchy);
        soundButton.SetActive(!soundButton.activeInHierarchy);
        exitButton.SetActive(!exitButton.activeInHierarchy);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
    }

    public void ShowCommands()
    {
        imageSettings.SetActive(!imageSettings.activeInHierarchy);

        if (imageSettings.activeInHierarchy)
        {
            soundSettings.SetActive(false);
        }
    }

    public void ShowSound()
    {
        soundSettings.SetActive(!soundSettings.activeInHierarchy);

        if (soundSettings.activeInHierarchy)
        {
            imageSettings.SetActive(false);
        }
    }
}
