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

    public void GoToPause()
    {
        UIManager.instance.ShowUI(UIManager.GameUI.Pause);
    }
}
