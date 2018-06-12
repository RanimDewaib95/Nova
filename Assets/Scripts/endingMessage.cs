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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
