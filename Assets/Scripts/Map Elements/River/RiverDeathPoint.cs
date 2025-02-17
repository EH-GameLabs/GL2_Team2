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
            GameManager.instance.GameOver();
        }
    }
}
