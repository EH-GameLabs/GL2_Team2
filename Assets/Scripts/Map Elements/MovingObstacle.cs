using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            // Animazione
            //animator.SetBool("Dead", true);
            GameManager.instance.GameOver();
            other.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
