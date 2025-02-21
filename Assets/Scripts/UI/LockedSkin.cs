using UnityEngine;

public class LockedSkin : MonoBehaviour
{
    [SerializeField] private GameObject LeftSkinLocked;
    [SerializeField] private GameObject MainSkinLocked;
    [SerializeField] private GameObject RightSkinLocked;

    public void ActiveLeft(bool active) { LeftSkinLocked.SetActive(active); }
    public void ActiveMain(bool active) { MainSkinLocked.SetActive(active); }
    public void ActiveRight(bool active) { RightSkinLocked.SetActive(active); }
}
