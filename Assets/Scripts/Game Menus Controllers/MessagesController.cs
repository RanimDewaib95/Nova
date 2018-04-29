using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagesController : MonoBehaviour {
    List<string> messagesLevel1 = new List<string>();

    GameObject image;
    GameObject nextButton;
    GameObject previousButton;
    GameObject doneButton;

    int nextButtonFlag = 0;
    int previousButtonFlag = 0;

    public static bool clickedNext = false;
    public static bool clickedPrevious = false;

    int i = 0; //counter to display messages when next is pressed

    // Use this for initialization
    void Start () {
        messagesLevel1.Add("Level1Message1");
        messagesLevel1.Add("Level1Message2");
        messagesLevel1.Add("MessagesLevel1-03");
        messagesLevel1.Add("MessagesLevel1-04");
        messagesLevel1.Add("MessagesLevel1-05");
        messagesLevel1.Add("MessagesLevel1-06");
        messagesLevel1.Add("MessagesLevel1-07");
        messagesLevel1.Add("MessagesLevel1-08");
        messagesLevel1.Add("MessagesLevel1-09");

        image = GameObject.Find("Image");
        nextButton = image.transform.Find("Next Button").gameObject;
        previousButton = image.transform.Find("Previous Button").gameObject;
        doneButton = image.transform.Find("Done Button").gameObject;

        doneButton.SetActive(false);
        previousButton.SetActive(false);
    }

    public void Update () {
        
        nextButton.GetComponent<Button>().onClick.AddListener(delegate { updateNextButton(); }); 
        previousButton.GetComponent<Button>().onClick.AddListener(delegate { updatePreviousButton(); });

        Sprite mySprite = Resources.Load<Sprite>(messagesLevel1[i]);
        image.GetComponent<Image>().sprite = mySprite;
    }
    
    public void updateNextButton ()
    {
        if (i < messagesLevel1.Count && clickedNext == true && nextButtonFlag == 0)
        {
            previousButton.SetActive(true);

            Debug.Log(i);
            nextButtonFlag = 1;
            nextButtonFlag = 0;
            clickedNext = false;
            i++;

            //begining of the messages
            if (i == 0)
            {
                previousButton.SetActive(false);
            }

            //reached the end of the messages
            if (i == messagesLevel1.Count - 1)
            {
                nextButton.SetActive(false);
                doneButton.SetActive(true);
            }
        }    
   }

    private void updatePreviousButton ()
    {
        if (i > 0 && clickedPrevious == true && previousButtonFlag == 0)
        {
            Debug.Log("in previous");
            previousButtonFlag = 1;
            previousButtonFlag = 0;
            clickedPrevious = false;
            i--;
        }

        //begining of the messages
        if (i == 0)
        {
            previousButton.SetActive(false);
        }

        //pressed previous when player reached the end of the messages
        if (i == messagesLevel1.Count - 2)
        {
            nextButton.SetActive(true);
            doneButton.SetActive(false);
        }
    }

    public void nextButtonClicked()
    {
        clickedNext = true;
        updateNextButton();
        clickedNext = false;
    }

    public void previousButtonClicked()
    {
        clickedPrevious = true;
        updatePreviousButton();
        clickedPrevious = false;
    }

    public void doneButtonClicked()
    {
        GameObject.Find("CanvasPop (1)").SetActive(false);
    }

}
