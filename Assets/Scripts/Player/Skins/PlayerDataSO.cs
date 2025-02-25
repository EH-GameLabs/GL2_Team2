using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObject/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    public int bestScore;
    public int coins;
    public List<SkinData> Skins;
}


[System.Serializable]
public class SkinData
{
    public GameObject skin;
    public bool isActive;
    public bool isLocked;
    //[Range(0f, 1f)] public float weight;
}