using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy wave config")] 

public class WaveConfig : ScriptableObject 
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float movementSpeed = 2f;

    public GameObject GetEnemyPrefab() 
    {
        return enemyPrefab;
    }

    public List<Transform> GetWayPointsFromPathPrefab()
    {
        var waveWayPoints = new List<Transform>();

        foreach(Transform wayPoint in pathPrefab.transform) 
        {
            waveWayPoints.Add(wayPoint); 
        }
        return waveWayPoints; 
    }

    public float GetTimeBetweenSpawns()  { return timeBetweenSpawns; }
    
    public float GetSpawnRandomFactor() { return spawnRandomFactor; }

    public int GetNumberOfEnemies() { return numberOfEnemies; }

    public float GetMovementSpeed() { return movementSpeed; }
}