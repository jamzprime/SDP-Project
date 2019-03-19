using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


/*
 * generate enemies
 * */
public class WaveSpawner : MonoBehaviour
{
	public static WaveSpawner instance;
	StatsManager statsManager;
	public Transform enemyType;
    public Transform basicEnemyPrefab;
	public Transform slowEnemyPrefab;
	public static bool toSpawn = false;

    public Transform spawnPoint;

//    public float timeBetweenWaves = 5f;
//    public float countdown = 2f;
	public float enemyDelay = 0.5f;
	public int multiplyer;

    //public Text waveCountdownText;

    public int waveIndex = 0;
	public int currentEnemyCount = 0;
	public int maxEnemyCount = 10;
	public int activeWavesCount = 0;

	void Start()
	{
		instance = this;
		statsManager = StatsManager.instance;
		multiplyer = 0;
		currentEnemyCount = 0;
	}

//    void FixedUpdate()
//    {
//        if (countdown <= 0f)
//        {
//            StartCoroutine(SpawnWave());
//
//            countdown = timeBetweenWaves;
//        }
//
//        countdown -= Time.deltaTime;

        //waveCountdownText.text = Mathf.Round(countdown).ToString();
//    }

	public void DestroyedEnemy()
	{
		currentEnemyCount -= 1;
		if (currentEnemyCount == 0)
			activeWavesCount = 0;
			
	}

	public IEnumerator SpawnWave()
    {
        waveIndex++;
		activeWavesCount++;
		//gold per unite kill increase every 5 waves
		if (waveIndex % 5 == 0)
			multiplyer++;
		//Update Wave Counter in UI
		statsManager.UpdateWaveCount(waveIndex);

		int currentWaveIndex = waveIndex;
		for (int i = 0; i < ((currentWaveIndex*2)+10); i++)
        {
			currentEnemyCount += 1;
			if (currentWaveIndex % 5 == 3) {
				SpawnEnemy (slowEnemyPrefab);
				yield return new WaitForSeconds(1.5f);
			} else {
				SpawnEnemy (basicEnemyPrefab);
				yield return new WaitForSeconds(0.5f);
			}

        }
    }

    /*
     * generate enemies
     * */
    void SpawnEnemy(Transform enemyType)
    {
		Instantiate(enemyType, MapGenerator.instance.waypoints[0], spawnPoint.rotation);

    }
}
