using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            // Animazione
            if (UIManager.Instance.GetCurrentActiveUI() == UIManager.GameUI.InGame)
            {
                Animator animator = other.GetComponent<Animator>();
                animator.SetBool("CarDeath", true);
                GameManager.instance.GameOver();
            }
            other.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
