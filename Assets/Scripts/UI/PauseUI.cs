using System.Collections;
using TMPro;
using UnityEngine;

public class PauseUI : MonoBehaviour, IGameUI
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

    [SerializeField] private TextMeshProUGUI resumeText;
    [SerializeField] private GameObject resumeButton;

    public void GoToInGame()
    {
        resumeButton.SetActive(false);
        StartCoroutine(ResumeTimerRoutine());
        UIManager.Instance.ShowUI(UIManager.GameUI.Pause);
    }

    private IEnumerator ResumeTimerRoutine()
    {
        int t = 3;

        while (t > 0)
        {
            resumeText.text = t.ToString();
            yield return new WaitForSeconds(1f);
            t--;
        }

        resumeButton.SetActive(true);
        UIManager.Instance.ShowUI(UIManager.GameUI.InGame);
        GameManager.instance.SetIsGameActive(true);
    }

}
