using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endingMessage : MonoBehaviour {

    public void displayLastMessage()
    {
        MessagesController.endMessage.SetActive(true);
    }
}
