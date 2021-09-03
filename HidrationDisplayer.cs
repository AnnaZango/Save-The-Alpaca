using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HidrationDisplayer : MonoBehaviour
{
    TextMeshProUGUI myText;
    Player myPlayer;
    int hidration;

    void Start()
    {
        myText = GetComponent<TextMeshProUGUI>();
        myPlayer = FindObjectOfType<Player>();
    }

    void Update()
    {
        hidration = myPlayer.GetHidration();
        myText.text = hidration.ToString();
    }
}
