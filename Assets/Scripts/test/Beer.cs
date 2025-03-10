using UnityEngine;

public class Beer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            SoundManager.instance.PlaySFX(SoundManager.instance.skin);
            PlayerDataManager.instance.SkinData.coins += 1;
            FindAnyObjectByType<InGameUI>()?.SetBeersAmount(PlayerDataManager.instance.SkinData.coins);
            Destroy(gameObject);
        }
    }

}
