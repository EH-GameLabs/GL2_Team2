using UnityEngine;

public class SkinSelectionUI : MonoBehaviour, IGameUI
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

    [SerializeField] private GameObject currentSkin;

    private void OnEnable()
    {

    }

    public void ShowMainMenu()
    {
        UIManager.Instance.ShowUI(UIManager.GameUI.MainMenu);
    }

    //public void SetSkinActive()
    //{
    //    PlayerController.instance.SelectSkin(currentSkin);
    //}
}
