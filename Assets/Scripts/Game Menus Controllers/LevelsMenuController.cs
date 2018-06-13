﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenuController : MonoBehaviour {

    public static int level;

    public void spaceStationButtonClicked()
    {
        SceneManager.LoadScene("Pop up messages");
        level = 1;
    }

    public void planet1ButtonClicked()
    {
        SceneManager.LoadScene("Pop up messages");
        level = 2;
    }

    public void planet3ButtonClicked()
    {
        SceneManager.LoadScene("Pop up messages");
        level = 3;
    }

    public void planet4ButtonClicked()
    {
        SceneManager.LoadScene("Pop up messages");
        level = 4;
    }

    public void backButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }


}
