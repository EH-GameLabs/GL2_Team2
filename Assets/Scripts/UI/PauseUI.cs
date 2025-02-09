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

    public void GoToInGame()
    {
        StartCoroutine(ResumeTimerRoutine());
        UIManager.instance.ShowUI(UIManager.GameUI.Pause);
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

        UIManager.instance.ShowUI(UIManager.GameUI.Pause);
    }

}
