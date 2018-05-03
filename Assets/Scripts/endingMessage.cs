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
        SceneManager.LoadScene("Planet1-Level 1");
    }
}
