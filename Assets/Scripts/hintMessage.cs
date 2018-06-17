using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class hintMessage : MonoBehaviour {

    public static GameObject hintPopUp;
    List<string> hintMessages = new List<string>();
    GameObject image;
    Sprite mySprite;

    int hintCounter = 0;

    void Start () {
        hintPopUp = GameObject.Find("Hint Message").gameObject;
        hintPopUp.SetActive(false);

        image = GameObject.Find("hint");
        mySprite = new Sprite();

        if (LevelsMenuController.level == 1)
        {
            //add hints
            Debug.Log("added all hints");
        }
        if (LevelsMenuController.level == 2)
        {
            //add hints
            Debug.Log("added all hints");
        }
        if (LevelsMenuController.level == 3)
        {
            //add hints
            Debug.Log("added all hints");
        }
        if (LevelsMenuController.level == 4)
        {
            //add hints
            Debug.Log("added all hints");
        }
    }

    public IEnumerator displayHint()
    {
        Debug.Log("in hint display");
        //mySprite = Resources.Load<Sprite>(hintMessages[0]);
        //image.GetComponent<Image>().sprite = mySprite;
        hintPopUp.SetActive(true);
       
        yield return new WaitForSeconds(5.0f);
        hintPopUp.SetActive(false);

        hintCounter++;
    }

}
