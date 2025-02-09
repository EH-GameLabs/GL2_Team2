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

    public void GoToMainMenu()
    {
        UIManager.instance.ShowUI(UIManager.GameUI.MainMenu);
    }
}
