using System.Collections;
using UnityEngine;

public class CarObstacle : MovingObstacle
{
    [SerializeField] private GameObject[] meshes;
    [SerializeField] private bool needAnimation;
    [SerializeField] private float animationTime;
    [SerializeField] private Animator animator;

    private void Start()
    {
        meshes[1].SetActive(false);
        meshes[0].SetActive(true);

        if (needAnimation)
            StartCoroutine(AnimationRoutine());
    }

    public IEnumerator AnimationRoutine()
    {
        while (true)
        {
            meshes[0].SetActive(!meshes[0].activeInHierarchy);
            meshes[1].SetActive(!meshes[1].activeInHierarchy);
            yield return new WaitForSeconds(animationTime);
        }
    }
}
