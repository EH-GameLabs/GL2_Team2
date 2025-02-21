using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkinSelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject mainSkinSlot;
    [SerializeField] private GameObject leftSkinSlot;
    [SerializeField] private GameObject rightSkinSlot;
    [SerializeField] private GameObject BackSkinSlot;
    [SerializeField] private List<GameObject> others = new List<GameObject>();

    [SerializeField] private SkinDataSO skins;
    [SerializeField] private float speedRotation;


    private bool isTurning;

    public static SkinSelectionManager instance { get; set; }

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        instance = this;
    }

    private void Start()
    {
        InitializeOthers();
    }

    private void InitializeOthers()
    {
        foreach (Transform child in BackSkinSlot.transform)
        {
            others.Add(child.gameObject);
        }
    }

    public void TurnLeftSkins()
    {
        if (isTurning) return;

        isTurning = true;

        // main -> left
        StartCoroutine(TranslateSkins(mainSkinSlot.GetComponentInChildren<SkinTypeSelector>().transform, leftSkinSlot.transform));
        // right -> main
        StartCoroutine(TranslateSkins(rightSkinSlot.GetComponentInChildren<SkinTypeSelector>().transform, mainSkinSlot.transform));
        // left -> others
        others.Add(leftSkinSlot.GetComponentInChildren<SkinTypeSelector>().gameObject);
        StartCoroutine(TranslateSkins(leftSkinSlot.GetComponentInChildren<SkinTypeSelector>().transform, BackSkinSlot.transform));
        // other.pop() -> right
        others[0].SetActive(true);
        StartCoroutine(TranslateSkins(others[0].transform, rightSkinSlot.transform));
        others.RemoveAt(0);
    }

    public void TurnRightSkins()
    {
        if (isTurning) return;

        isTurning = true;

        // other.pop() -> left
        others[others.Count - 1].SetActive(true);
        StartCoroutine(TranslateSkins(others[others.Count - 1].transform, leftSkinSlot.transform));
        others.RemoveAt(others.Count - 1);
        // main -> right
        StartCoroutine(TranslateSkins(mainSkinSlot.GetComponentInChildren<SkinTypeSelector>().transform, rightSkinSlot.transform));
        // right -> others
        others.Insert(0, rightSkinSlot.GetComponentInChildren<SkinTypeSelector>().gameObject);
        StartCoroutine(TranslateSkins(rightSkinSlot.GetComponentInChildren<SkinTypeSelector>().transform, BackSkinSlot.transform));
        // left -> main
        StartCoroutine(TranslateSkins(leftSkinSlot.GetComponentInChildren<SkinTypeSelector>().transform, mainSkinSlot.transform));
    }

    public IEnumerator TranslateSkins(Transform skin, Transform endPos)
    {
        DisableIfLocked(skin);

        Vector3 startPos = skin.position;
        float t = 0f;

        while (t < speedRotation)
        {
            t += Time.deltaTime;
            float lerpFactor = t / speedRotation;
            skin.position = Vector3.Lerp(startPos, endPos.position, lerpFactor);
            yield return null;
        }

        skin.SetParent(endPos);
        skin.position = endPos.position;
        if (endPos == BackSkinSlot.transform) skin.gameObject.SetActive(false);
        if (endPos == rightSkinSlot.transform || endPos == leftSkinSlot)
        {
            skin.transform.rotation = endPos.rotation;
        }

        isTurning = false;
    }

    private void DisableIfLocked(Transform skin)
    {
        foreach (var _skin in skins.Skins)
        {
            if (_skin.skin.GetComponent<SkinTypeSelector>().SkinType == skin.GetComponentInChildren<SkinTypeSelector>().SkinType)
            {
                if (_skin.isLocked)
                {
                    foreach (Transform child in skin.transform)
                        child.gameObject.SetActive(false);
                }
                else
                {
                    foreach (Transform child in skin.transform)
                        child.gameObject.SetActive(true);
                }
            }
        }
    }

    public void SelectActiveSkin()
    {
        foreach (var skin in skins.Skins)
        {
            if (skin.skin.GetComponent<SkinTypeSelector>().SkinType == mainSkinSlot.GetComponentInChildren<SkinTypeSelector>().SkinType)
            {
                skin.isActive = true;
            }
            else
            {
                skin.isActive = false;
            }
        }
    }

    public void ExitScreen()
    {
        SceneManager.LoadScene("MainScene");
    }
}
