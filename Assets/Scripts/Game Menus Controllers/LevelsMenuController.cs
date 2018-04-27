using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenuController : MonoBehaviour {

    public void spaceStationButtonClicked()
    {
        SceneManager.LoadScene("Levels Menu Intro");
    }

    public void backButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

<<<<<<< HEAD
    public void level1ButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
=======

>>>>>>> master
}
