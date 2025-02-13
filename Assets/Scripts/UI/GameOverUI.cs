using UnityEngine;
using UnityEngine.SceneManagement;

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
        //GameManager.Instance.ResetAll();
        //UIManager.Instance.ShowUI(UIManager.GameUI.MainMenu);
        //coins.SetActive(true);
        SceneManager.LoadScene(0);
    }
}
