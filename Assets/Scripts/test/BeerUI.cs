using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeerUI : MonoBehaviour
{
    private void Start()
    {
        SetUp();
    }
    private void SetUp()
    {
        int a = GameManager.instance.GetCurrentBeersAmount();
        GetComponentInChildren<TextMeshProUGUI>().text = a.ToString();
    }
}

