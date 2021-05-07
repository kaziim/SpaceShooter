using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Wave
{
    public string waveName;
    public int noOfEnemises;
    public float spawnInterval;
    public GameObject[] typeOfEnemies;
    
}

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Wave[] waves;
    public Transform[] spawnPoints;
    public Animator animator;
    public Text WaveName;

    private Wave currentWave;
    private int currentWaveNumber;
    private float nextSpawnTime;

    private bool canSpawn = true;
    private bool canAnimate = false;

    void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
        var totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (totalEnemies.Length == 0)
        {
            if (currentWaveNumber+1 != waves.Length)
            {
                if (canAnimate)
                {
                    WaveName.text = waves[currentWaveNumber + 1].waveName;
                    animator.SetTrigger("WaveComplete");
                    canAnimate = false; 
                }
            }else
            {
                Debug.Log("GameFinish");
                SceneManager.LoadScene(0);

            }
            
        }
        
    }

    void SpawnNextWave()
    {
        currentWaveNumber++;
        canSpawn = true;
    }

    void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            currentWave.noOfEnemises--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if (currentWave.noOfEnemises == 0)
            {
                canSpawn = false;
                canAnimate = true;
            }
        }
        
    }
}
