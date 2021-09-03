using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> allWaveConfigs; 
    [SerializeField] int indexNumOfStartingWave = 0; 
    [SerializeField] bool looping = false; 

    IEnumerator Start() 
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);         
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveCount = indexNumOfStartingWave; waveCount < allWaveConfigs.Count; waveCount++)
        {
            var currentWave = allWaveConfigs[waveCount]; 
            yield return StartCoroutine(SpawnAllEnemiesInWave(waveConfigToUse: currentWave)); 
        }        
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfigToUse)
    {            
        for (int enemyCount = 0; enemyCount < waveConfigToUse.GetNumberOfEnemies(); enemyCount++)
        {            
            var newEnemy = Instantiate(waveConfigToUse.GetEnemyPrefab(), waveConfigToUse.GetWayPointsFromPathPrefab()[0].transform.position, Quaternion.identity); 
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfigToUse);                        

            yield return new WaitForSeconds(waveConfigToUse.GetTimeBetweenSpawns()); 
        }    
    }    
    
}
