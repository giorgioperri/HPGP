using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GameStatus
{
    Playing,
    Lose,
    Win
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public int foodLeft = 99;
    public TextMeshProUGUI timerText;
    public int timer = 45;
    public TextMeshProUGUI foodLeftText;
    public GameStatus gameStatus = GameStatus.Playing;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    private void Start()
    {
        StartCoroutine(reduceTimer());
    }

    private void Update()
    {
        foodLeftText.text = foodLeft.ToString();

        if (foodLeft == 0)
        {
            gameStatus = GameStatus.Win;
        }
        
        if (timer == 0)
        {
            gameStatus = GameStatus.Lose;
        }
        
        if (gameStatus == GameStatus.Lose)
        {
            Debug.Log("You lose");
        }
        
        if (gameStatus == GameStatus.Win)
        {
            Debug.Log("You win");
        }
    }
    
    private IEnumerator reduceTimer()
    {
        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            timer--;
            timerText.text = timer.ToString();
        }
    }
}
