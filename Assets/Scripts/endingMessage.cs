using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class endingMessage : MonoBehaviour {

    public static GameObject endMessage;
 
    private void Start()
    {
        endMessage = GameObject.Find("CanvasPop (2)").gameObject;
        endMessage.SetActive(false);
    }

    public void displayLastMessage()
    {
        endMessage.SetActive(true);
    }

    public void goToNextLevel()
    {
        if (LevelsMenuController.level == 1)
        {
            LevelsMenuController.level = 2;
            SceneManager.LoadScene("Pop up messages");
        }

        if (LevelsMenuController.level == 2)
        {
            LevelsMenuController.level = 3;
            SceneManager.LoadScene("Pop up messages");
        }

        if (LevelsMenuController.level == 3)
        {
            LevelsMenuController.level = 4;
            SceneManager.LoadScene("Pop up messages");
        }

    }

    public void goToLevelsMenu()
    {
        SceneManager.LoadScene("Levels Menu");
    }

    public void replayButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
