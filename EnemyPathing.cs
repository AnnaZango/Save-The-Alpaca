using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig; 
    List<Transform> waypoints=default; 

    int waypointIndex = 0; 

    void Start()
    {        
        waypoints = waveConfig.GetWayPointsFromPathPrefab(); 
        transform.position = waypoints[waypointIndex].transform.position;
    }
        
    void Update()
    {
        MoveEnemy();
    }

    public void SetWaveConfig(WaveConfig waveConfigToSet) 
    {
        this.waveConfig = waveConfigToSet;
    }

    private void MoveEnemy()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementEnemyPerFrame = waveConfig.GetMovementSpeed() * Time.deltaTime; 
            transform.position = Vector2.MoveTowards 
                (transform.position, targetPosition, movementEnemyPerFrame);

            if (transform.position == targetPosition) 
            {
                waypointIndex++; 
            }
        }
        else 
        {            
            Destroy(gameObject); 
            waypointIndex = 0;
        }
    }
   
}
