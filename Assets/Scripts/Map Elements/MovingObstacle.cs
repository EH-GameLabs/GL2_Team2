using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            print("macchina morte");
            // Animazione
            //animator.SetBool("Dead", true);
        }
    }
}
