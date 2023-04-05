using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wavescript : MonoBehaviour
{
    public int enemiesPerWave; 
    public float timeBetweenWaves; 
    public GameObject enemyPrefab;
    public GameObject bossPrefab;

    private int waveCounter; 
    private int lastwave = 3;
    private float timeSinceLastWave; 
    public Boolean waveStopper = true; 

    void Update()
    {
        timeSinceLastWave += Time.deltaTime;

        if (timeSinceLastWave >= timeBetweenWaves && waveCounter <=lastwave )
        {
            SpawnWave();
            timeSinceLastWave = 0;
        }

        else if (waveCounter >= lastwave && timeSinceLastWave >= timeBetweenWaves && waveStopper == true )
        {
            
            Debug.Log("BOSS COMING");
            Vector3 spawnPosition = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
            waveStopper = false;
        }
    }

    void SpawnWave()
    {
        waveCounter++;
        for (int i = 0; i < enemiesPerWave; i++)
        {
            // Spawn an enemy at a random position
            Vector3 spawnPosition = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
