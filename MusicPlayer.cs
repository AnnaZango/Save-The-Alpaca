﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{        
    private void Awake()
    {       
        SetUpSingleton();       
    }    

    private void SetUpSingleton()
    {
        if (FindObjectsOfType<MusicPlayer>().Length > 1) 
        {
            Destroy(gameObject); 
        } else 
        {
            DontDestroyOnLoad(gameObject); 
        }        
    }

    public void DestroyMusicPlayer()
    {
        Destroy(gameObject);
    }

}
