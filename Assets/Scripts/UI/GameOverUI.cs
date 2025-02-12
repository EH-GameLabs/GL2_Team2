using UnityEngine;

public class GameOverUI : MonoBehaviour, IGameUI
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

    [SerializeField] private GameObject coins;

    public void GoToMainMenu()
    {
        GameManager.instance.ResetAll();
        UIManager.instance.ShowUI(UIManager.GameUI.MainMenu);
        coins.SetActive(true);
    }
}
