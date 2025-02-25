using UnityEngine;

public class RiverDeathPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            print("fiume morte");
            // Animazione
            //animator.SetBool("Dead", true);
            if (UIManager.Instance.GetCurrentActiveUI() == UIManager.GameUI.InGame)
                GameManager.instance.GameOver();
        }
    }
}
