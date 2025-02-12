using UnityEngine;

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

    public void PlayGameUI()
    {
        UIManager.instance.ShowUI(UIManager.GameUI.InGame);
        GameManager.instance.SetIsGameActive(true);
    }
}
