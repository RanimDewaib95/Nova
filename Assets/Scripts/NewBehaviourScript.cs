using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {
    endingMessage goalMessage;

    // Use this for initialization
    void Start ()
    {
        goalMessage = new endingMessage();
	}

    void OnTriggerEnter(Collider other)
    {
        goalMessage.displayLastMessage();
        Debug.Log("found collider");
        //goalMessage.SetActive(true);
    }
}
