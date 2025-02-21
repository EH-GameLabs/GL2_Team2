using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinData", menuName = "ScriptableObject/SkinData")]
public class SkinDataSO : ScriptableObject
{
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