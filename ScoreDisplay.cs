using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    TextMeshProUGUI myText;
    GameSession myGameSession;
    int score;

    void Start()
    {
        myText = GetComponent<TextMeshProUGUI>();
        myGameSession = FindObjectOfType<GameSession>();
    }

    void Update()
    {
        score = myGameSession.GetScore();
        if (score < 0)
        {
            score = 0;
        }

        myText.text = score.ToString();
    }

}
