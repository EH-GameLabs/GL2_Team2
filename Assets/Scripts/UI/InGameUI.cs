using TMPro;
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

    [SerializeField] private GameObject beersText;
    [SerializeField] private TextMeshProUGUI score;

    public void GoToPause()
    {
        UIManager.Instance.ShowUI(UIManager.GameUI.Pause);
    }

    public void GoToGameOver()
    {
        beersText.SetActive(false);
        UIManager.Instance.ShowUI(UIManager.GameUI.GameOver);
    }

    public void SetBeersAmount(int amount)
    {
        beersText.GetComponentInChildren<TextMeshProUGUI>().text = amount.ToString();
    }
}
