using UnityEngine;

public class Beer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.SetBeersAmount(GameManager.instance.GetCurrentBeersAmount() + 1);
            FindAnyObjectByType<InGameUI>()?.SetBeersAmount(GameManager.instance.GetCurrentBeersAmount());
            Destroy(gameObject);
        }
    }

}
