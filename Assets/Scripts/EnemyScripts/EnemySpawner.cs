using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    WaveConfigSO currentWave;
    public List<GameObject> spawnedEnemies = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }
    IEnumerator SpawnEnemyWaves()
    {
        foreach (WaveConfigSO wave in waveConfigs)
        {
            currentWave = wave;
            for (int i = 0; i < currentWave.GetEmemyCount(); i++)
            {
                //make the enemy
                GameObject enemy = Instantiate(currentWave.GetEnemyPrefab(i),
                currentWave.GetStartingWaypoint().position + new Vector3(0, 1, 0),
                Quaternion.Euler(0, 0, 180), transform);

                //add it to a list to keep track of them
                spawnedEnemies.Add(enemy);
                //this is for in between enemies, not in between waves
                yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
            }
            //this waits for in between waves
            yield return new WaitForSeconds(timeBetweenWaves);
        }
        if (currentWave == waveConfigs[waveConfigs.Count - 1])//if on the last wave
        {
            while (spawnedEnemies.Count > 0)//wait till the enemys are gone
            {
                yield return new WaitForEndOfFrame();
                spawnedEnemies.RemoveAll(enemy => enemy == null);
            }
            //last wave, and enemies dead
            //Do something like level over
        }
    }
    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }
}