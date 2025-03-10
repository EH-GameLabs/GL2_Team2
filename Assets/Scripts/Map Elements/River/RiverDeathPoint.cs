using UnityEngine;

public class RiverDeathPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            print("fiume morte");
            // Animazione
            if (UIManager.Instance.GetCurrentActiveUI() == UIManager.GameUI.InGame)
            {
                Animator animator = other.GetComponent<Animator>();
                animator.SetBool("RiverDeath", true);
                if (UIManager.Instance.GetCurrentActiveUI() == UIManager.GameUI.InGame)
                {
                    FindAnyObjectByType<CameraMovement>().SetFocusOnPlayer();
                }
            }
        }
    }
}
