using System.IO;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    private static SkinManager Instance;
    public static SkinManager instance
    {
        get { return Instance; }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }

    private PlayerController player;
    private const string playerSkinsFile = "SkinFile";

    public void Salva()
    {
        try
        {
            string json = JsonUtility.ToJson(player.skinData);
            File.WriteAllText(Application.persistentDataPath + "/" + playerSkinsFile + ".json", json);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Errore durante il salvataggio dei file: " + e.Message);
        }
    }

    public void Carica()
    {
        try
        {
            string path = Application.persistentDataPath + "/" + playerSkinsFile + ".json";

            if (File.Exists(path))
            {
                JsonUtility.FromJsonOverwrite(File.ReadAllText(path), player.skinData);
                player.SetActiveSkin();
            }
            else
            {
                Debug.LogWarning("File playerSkinsFile mancante, carico valori di default.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Errore durante il caricamento dei file: " + e.Message);
        }
    }

    public void SetPlayerRef(PlayerController playerRef)
    {
        this.player = playerRef;
        Carica();
    }
}
