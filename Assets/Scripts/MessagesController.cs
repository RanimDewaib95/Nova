using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagesController : MonoBehaviour {
    Queue<string> messages = new Queue<string>();

    GameObject button;

    Text message1;
    string message2;
    string message3, message4;

    // Use this for initialization
    void Start () {
        button = GameObject.Find("Message 1");

        message2 = "ranim is 2";
        message3 = "heba is 3";
        message4 = "heba is 4";
        //message2 = GameObject.Find("text2").GetComponentInChildren<Text>();
        //message3 = GameObject.Find("text3").GetComponentInChildren<Text>();

        //message2.SetActive(false);
        //message3.SetActive(false);

        messages.Enqueue(message2);
        messages.Enqueue(message3);
        messages.Enqueue(message4);
    }

    // Update is called once per frame
    void Update () {
        button.GetComponent<Button>().onClick.AddListener(delegate { updateButton(); });
	}

    void updateButton()
    {
        if (messages.Count != 0)
        {
            button.GetComponentInChildren<Text>().text = messages.Peek().ToString();
            messages.Dequeue();
            Debug.Log("changed");
        }
    }
}
