using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {
    GameObject goalMessage;

    // Use this for initialization
    void Start ()
    {
        goalMessage = GameObject.FindGameObjectWithTag("Finish");
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("found collider");
        goalMessage.GetComponent<MessagesController>().displayLastMessage();
        //goalMessage.SetActive(true);
    }
}
