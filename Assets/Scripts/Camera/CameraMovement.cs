using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float addVelocity = 1;
    private void LateUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * GameManager.instance.GetCurrentSpeed() * addVelocity);
    }


    public void IncreaseSpeed(float value) { addVelocity = value; }
    public void DecreaseSpeed() { addVelocity = 1; }
}
