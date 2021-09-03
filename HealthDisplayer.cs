using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplayer : MonoBehaviour
{
    TextMeshProUGUI myHealth;
    
    Player player;
    
    void Start()
    {
        myHealth = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        myHealth.text = player.GetHealth().ToString();
    }
}
