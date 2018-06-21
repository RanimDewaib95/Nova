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

    IEnumerator OnTriggerEnter(Collider other)
    {
       // if (other.CompareTag("Target"))
        //{
            yield return new WaitForSeconds(1.0f);
            goalMessage.displayLastMessage();
            Debug.Log("found collider");
       // }      
    }
}
