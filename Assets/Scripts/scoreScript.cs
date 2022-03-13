using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreScript : MonoBehaviour
{
    public Text scoreText;

    public void updateScore(int score)
    {
        scoreText.text = score.ToString();
    }
  
}
