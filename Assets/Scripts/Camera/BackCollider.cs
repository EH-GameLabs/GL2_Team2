using UnityEngine;

public class BackCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            if (GameManager.instance.IsGameActive())
            {
                Animator animator = other.GetComponent<Animator>();
                animator.SetBool("AfkDeath", true);
                if (UIManager.Instance.GetCurrentActiveUI() == UIManager.GameUI.InGame)
                {
                    FindAnyObjectByType<CameraMovement>().SetFocusOnPlayer();
                }
            }

        }
    }
}
