using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MessagesController : MonoBehaviour {
    List<string> messagesLevel1 = new List<string>();

    GameObject image;
    GameObject nextButton;
    GameObject previousButton;
    GameObject doneButton;

    Sprite mySprite;

    int nextButtonFlag = 0;
    int previousButtonFlag = 0;

    public static bool clickedNext = false;
    public static bool clickedPrevious = false;

    int i = 0; //counter to display messages when next is pressed

    // Use this for initialization
    void Start () {

        if(LevelsMenuController.level == 1)
        {
            messagesLevel1.Add("L1M1");
            messagesLevel1.Add("L1M2");
            messagesLevel1.Add("L1M3");
            messagesLevel1.Add("L1M4");
            messagesLevel1.Add("L1M5");
            messagesLevel1.Add("L1M6");
            messagesLevel1.Add("L1M7");
        }

        if (LevelsMenuController.level == 2)
        {
            messagesLevel1.Add("L2M1");
            messagesLevel1.Add("L2M2");
            messagesLevel1.Add("L2M3");
            messagesLevel1.Add("L2M4");
            messagesLevel1.Add("L2M5");
            messagesLevel1.Add("L2M6");
        }

        if (LevelsMenuController.level == 3)
        {
            messagesLevel1.Add("L3M1");
            messagesLevel1.Add("L3M2");
            messagesLevel1.Add("L3M3");
            messagesLevel1.Add("L3M4");
        }

        if (LevelsMenuController.level == 4)
        {
            messagesLevel1.Add("L4M1");
            messagesLevel1.Add("L4M2");
            messagesLevel1.Add("L4M3");
            messagesLevel1.Add("L4M4");
            messagesLevel1.Add("L4M5");
        }

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

        mySprite = Resources.Load<Sprite>(messagesLevel1[i]);
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
        GameObject.Find("CanvasPop").SetActive(false);

        if (LevelsMenuController.level == 1)
        {
            SceneManager.LoadScene("Spacestation-Level1");
        }
        if (LevelsMenuController.level == 2)
        {
            SceneManager.LoadScene("Planet1-Level1");
        }
        if (LevelsMenuController.level == 3)
        {
            SceneManager.LoadScene("Planet2-Level1");
        }
        if (LevelsMenuController.level == 4)
        {
            SceneManager.LoadScene("Planet3-Level1");
        }
    }
}
