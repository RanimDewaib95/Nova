using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagesController : MonoBehaviour {
    //Dictionary<int, string> messages = new Dictionary<int, string>();
    List<string> messages = new List<string>();

    GameObject image;
    GameObject nextButton;
    GameObject previousButton;
    


    Text message1;
    string message2;
    string message3, message4;

    // Use this for initialization
    void Start () {
        messages.Add("Level1Message1");

        image = GameObject.Find("Image");

        message2 = "ranim is 2";
        message3 = "heba is 3";
        message4 = "heba is 4";
        //message2 = GameObject.Find("text2").GetComponentInChildren<Text>();
        //message3 = GameObject.Find("text3").GetComponentInChildren<Text>();

        //message2.SetActive(false);
        //message3.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        updateButton();
        image.transform.FindChild("Next Button");

        //image.GetComponent<Button>().onClick.AddListener(delegate { updateButton(); });
	}
   
    void updateButton()
    {
        for(int i = 0; i < messages.Count; i++)
        {
            Sprite mySprite = Resources.Load<Sprite>(messages[i]);
            image.GetComponent<Image>().sprite = mySprite;

            //button.GetComponentInChildren<Text>().text = messages[i].ToString();
            
            Debug.Log("changed");
        }
    }
}
