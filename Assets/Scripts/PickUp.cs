using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

public class PickUp
{
    public void setInitial(int scoreCount)
    {
        scoreCount = 0;
    }

    public int updateScore(int scoreCount)
    {
        scoreCount += 1;
        return scoreCount;
    }

    public void updateScoreText(Text scoreText, int scoreCount)
    {
        scoreText.text = "Score: " + scoreCount.ToString();
    }
}
