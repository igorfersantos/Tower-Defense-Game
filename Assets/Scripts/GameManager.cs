using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool debugEnabled = false;
    public static bool GameIsOver;

    public GameObject gameOverUI;
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("GameManager is null!");
            }

            return _instance;
        }
    }


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        _instance = this;
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        Debug.unityLogger.logEnabled = debugEnabled;
        GameIsOver = false;
    }

    private void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
        Debug.Log("Game Over!");
    }

    public void ReducePlayerLives()
    {
        PlayerStats.Lives--;

        if (PlayerStats.Lives <= 0)
        {
            Debug.Log("Player lives reached 0. Ending Game!");
            EndGame();
        }
    }

    public void addMoneyToPlayer(int amount)
    {
        int playerNewMoneyAmount = PlayerStats.Money + amount;

        StartCoroutine(addMoneyToPlayerCoroutine(playerNewMoneyAmount, 1));
    }

    public void removeMoneyFromPlayer(int amount)
    {
        PlayerStats.Money -= amount;
    }

    IEnumerator addMoneyToPlayerCoroutine(int newAmount, int increaseStep)
    {
        if (PlayerStats.Money < newAmount)
        {
            for (int i = PlayerStats.Money; i < newAmount; i++)
            {
                PlayerStats.Money += increaseStep;
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
    }
}
