using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenuController : MonoBehaviour {

    public void spaceStationButtonClicked()
    {
        SceneManager.LoadScene("Levels Menu Intro");
    }

    public void planet1ButtonClicked()
    {
        SceneManager.LoadScene("Planet1-Level 1");
    }

    public void backButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }


}
