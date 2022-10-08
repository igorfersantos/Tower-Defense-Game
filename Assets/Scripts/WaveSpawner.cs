using System;
using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;

    // both should be longer, but we're leaving it as it is 
    // for development porpuses
    public float timeBetweenWaves = 10f;
    private float _countdown;
    public int Countdown { get { return (int)Math.Floor(_countdown); } }

    // wave number could be a polynomial function
    private int waveIndex = 0;
    public float timeBetweenSpawns = 0.5f;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
      _countdown = timeBetweenWaves;  
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if (_countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            _countdown = timeBetweenWaves;
        }

        _countdown -= Time.deltaTime;

        _countdown = Mathf.Clamp(_countdown, 0f, Mathf.Infinity);
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        PlayerStats.Rounds++;

        Debug.Log($"Wave is going to be spawned!\nWave size:{waveIndex}");
        
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}

