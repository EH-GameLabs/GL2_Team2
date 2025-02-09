using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float currentSpeed;


    public void GameOver()
    {
        Debug.Log("Hai perso!");
    }

    // GETTERS & SETTERS
    public float GetCurrentSpeed() { return currentSpeed; }
}
