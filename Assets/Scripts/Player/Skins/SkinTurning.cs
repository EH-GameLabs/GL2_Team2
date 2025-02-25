using UnityEngine;

public class SkinTurning : MonoBehaviour
{
    [SerializeField] private float speed;

    void Update()
    {
        transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
    }
}
