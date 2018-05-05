using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

public class PickUp
{
    private int scoreCount;
    private Text scoreText;

    public void setInitialScore()
    {
        scoreCount = 0;
    }

    public void updateScore()
    {
        scoreCount += 1;
    }

    public void updateScoreText()
    {
        scoreText.text = "Score: " + scoreCount.ToString();
    }
}
