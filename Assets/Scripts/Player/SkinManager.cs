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

    [SerializeField] private SkinDataSO SkinData;
    private PlayerController player;
    private const string playerSkinsFile = "SkinFile";

    public void Salva()
    {
        try
        {
            string json = JsonUtility.ToJson(SkinData);
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

            if (!SkinData)
            {
                Debug.LogError("SkinData è NULL prima della sovrascrittura! Assicurati di assegnarlo.");
                return;
            }

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);

                JsonUtility.FromJsonOverwrite(json, SkinData);

                if (player != null)
                {
                    player.SetActiveSkin();
                }
                else
                {
                    Debug.LogWarning("player è null, impossibile aggiornare la skin.");
                }
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
        Salva();
        Carica();
    }
}
