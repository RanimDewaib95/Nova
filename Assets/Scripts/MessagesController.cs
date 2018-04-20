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

    int nextButtonFlag = 0;
    


    Text message1;
    string message2;
    string message3, message4;

    // Use this for initialization
    void Start () {
        messages.Add("Level1Message1");
        messages.Add("Level1Message2");

        image = GameObject.Find("Image");

        //message2 = GameObject.Find("text2").GetComponentInChildren<Text>();
        //message3 = GameObject.Find("text3").GetComponentInChildren<Text>();

        //message2.SetActive(false);
        //message3.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        //updateButton();
        nextButton = image.transform.Find("Next Button").gameObject;

        nextButton.GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(updateButton()); });

        //image.GetComponent<Button>().onClick.AddListener(delegate { updateButton(); });
	}
   
    private IEnumerator updateButton()
    {
        for(int i = 0; i < messages.Count; i++)
        {
            if(nextButtonFlag == 0 && NextButton.clicked == true)
            {
                nextButtonFlag = 1;
                Sprite mySprite = Resources.Load<Sprite>(messages[i]);
                image.GetComponent<Image>().sprite = mySprite;
                nextButtonFlag = 0;
            }
 
            yield return new WaitForSeconds(2.0f);
            
        }
    }
    //NextButton.clicked == false;
}
