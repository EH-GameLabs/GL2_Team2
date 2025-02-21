using UnityEngine;

public class SkinTurning : MonoBehaviour
{
    [SerializeField] private float speed;

    private GameObject skin;

    private void Start()
    {
        //SetSkin();
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
    }

    //public void SetSkin() { skin = gameObject.GetComponentInChildren<SkinTypeSelector>().gameObject; print("skin"); }
}
