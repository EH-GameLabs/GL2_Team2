using UnityEngine;

public class Beer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetBeersAmount(GameManager.Instance.GetCurrentBeersAmount() + 1);
            FindAnyObjectByType<InGameUI>()?.SetBeersAmount(GameManager.Instance.GetCurrentBeersAmount());
            Destroy(gameObject);
        }
    }

}
