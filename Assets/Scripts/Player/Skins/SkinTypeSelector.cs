using System;
using UnityEngine;

public class SkinTypeSelector : MonoBehaviour
{
    public SkinType SkinType;
    public GameObject skinActive;
    public GameObject skinInactive;

    private void OnEnable()
    {
        CheckIfThisSKinIsActive();
    }

    private void CheckIfThisSKinIsActive()
    {
        foreach (var skin in PlayerDataManager.instance.SkinData.Skins)
        {
            if (skin.skin.GetComponent<SkinTypeSelector>().SkinType != this.SkinType) continue;
            ActiveSkin(!skin.isLocked);
        }
    }

    public void ActiveSkin(bool active)
    {
        skinActive.SetActive(active);
        skinInactive.SetActive(!active);
    }

}

public enum SkinType
{
    Nano,
    Cavaliere,
    Mago,
    Bardo,
    Unicorno,
}