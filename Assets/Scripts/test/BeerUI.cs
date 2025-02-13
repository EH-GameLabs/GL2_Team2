using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BeerUI : MonoBehaviour
{
    private void Start()
    {
        SetUp();
    }
    private void SetUp()
    {
        int a = GameManager.Instance.GetCurrentBeersAmount();
        GetComponentInChildren<TextMeshProUGUI>().text = a.ToString();
    }
}
