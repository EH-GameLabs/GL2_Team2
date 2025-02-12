using UnityEngine;

public class InGameUI : MonoBehaviour, IGameUI
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

    public void GoToPause()
    {
        UIManager.instance.ShowUI(UIManager.GameUI.Pause);
    }

    public void GoToGameOver()
    {
        coins.SetActive(false);
        UIManager.instance.ShowUI(UIManager.GameUI.GameOver);
    }
}
