using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDetectPlayer : MonoBehaviour
{
    private GameObject gameOverCanva;

    private void Start()
    {
        gameOverCanva = GameObject.FindGameObjectWithTag("GameOverCanva");
        
        // Ensure the game over canvas is initially disabled
        if (gameOverCanva != null)
        {
            gameOverCanva.SetActive(false);
        }
        else
        {
            Debug.LogError("Game Over Canvas not found.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider that triggered this method is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("GAME OVER");
            ShowGameOverScreen();
        }
    }

    private void ShowGameOverScreen() 
    {
        // Activate the game over canvas
        if (gameOverCanva != null)
        {
            gameOverCanva.SetActive(true);
        }
        else
        {
            Debug.LogError("Game Over Canvas not assigned.");
        }
    }
}
