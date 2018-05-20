using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class endingMessage : MonoBehaviour {

    public void displayLastMessage()
    {
        MessagesController.endMessage.SetActive(true);
    }

    public void goToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene("Planet1-Level1");
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
