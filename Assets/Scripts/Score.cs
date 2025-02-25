using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;

    private void Start()
    {
        score.text = "0";
    }

    public void SetScore(int score) { this.score.text = score.ToString(); }
    public int GetScore() { return int.Parse(score.text); }
}
