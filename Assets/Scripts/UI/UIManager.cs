using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour//Singleton<UIManager>
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get { return instance; }
    }

    public enum GameUI
    {
        NONE,
        MainMenu,
        InGame,
        Pause,
        GameOver,
    }

    private Dictionary<GameUI, IGameUI> registeredUIs = new Dictionary<GameUI, IGameUI>();
    public Transform UIContainer;
    private GameUI currentActiveUI = GameUI.NONE;
    public GameUI startingGameUI;

    public void RegisterUI(GameUI uiType, IGameUI uiToRegister)
    {
        registeredUIs.Add(uiType, uiToRegister);
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        foreach (IGameUI enumeratedUI in UIContainer.GetComponentsInChildren<IGameUI>(true))
        {
            RegisterUI(enumeratedUI.GetUIType(), enumeratedUI);
        }

        

        ShowUI(startingGameUI); // TODO -> SHOW MAIN MENU FIRST
        
    }

    public void ShowUI(GameUI uiType)
    {
        foreach (KeyValuePair<GameUI, IGameUI> kvp in registeredUIs)
        {
            kvp.Value.SetActive(kvp.Key == uiType);
        }

        currentActiveUI = uiType;
    }

    public GameUI GetCurrentActiveUI()
    {
        return currentActiveUI;
    }

    public void SetAudio(Slider slider) 
    {
        SoundManager.instance.ApplyVolumeSettings(slider.value);
    }
}