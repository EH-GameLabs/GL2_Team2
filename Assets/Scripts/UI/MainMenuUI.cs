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

    [SerializeField] private UIManager.GameUI playGameUI;
    public void PlayGameUI()
    {
        UIManager.instance.ShowUI(playGameUI);
    }
}
