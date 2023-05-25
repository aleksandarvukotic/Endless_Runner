using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private float score;
    PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        score = 0;
    }

    private void Update()
    {
        int intScore = (int)Mathf.RoundToInt(score);
        if (!playerController.gameOver)
        {
            score += Time.time / 10;
            scoreText.text = "Score: " + intScore;
        }
        else
        {
            scoreText.text = "Score: " + intScore;
        }
        
    }
}