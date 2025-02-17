using System.Collections;
using UnityEngine;

public class CarObstacle : MovingObstacle
{
    [SerializeField] private GameObject[] meshes;
    [SerializeField] private float animationTime;
    [SerializeField] private Animator animator;

    private void Start()
    {
        meshes[0].SetActive(true);
        meshes[1].SetActive(false);
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

    public void CallGameOver()
    {
        animator.SetBool("Dead", false);
        GameManager.Instance.GameOver();
    }

    // crystal disk info
}
