using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.SetBeersAmount(GameManager.Instance.GetCurrentBeersAmount()+1);
        FindAnyObjectByType<InGameUI>().SetBeersAmount(GameManager.Instance.GetCurrentBeersAmount());
    }
}
