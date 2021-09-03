using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] int score = 0;
   
    void Awake()
    {
        if (FindObjectsOfType<GameSession>().Length > 1)
        {
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject);
        }       
    }
    
    public int GetScore()
    {
        if (score >= 0)
        {
            return score;
        }
        else 
        {
            score = 0;
            return score;
        }        
    }
    
    public void AddToScore(int pointsPerKilling)
    {
        score = score + pointsPerKilling;
    }
       
    public void DestroyGameSession()
    {        
        Destroy(gameObject);
    }
    
}
