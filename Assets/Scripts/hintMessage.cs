using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class hintMessage : MonoBehaviour {

    public static GameObject hintPopUp;
   
    int hintCounter = 0;
    float time = 3.0f;

    void Start () {
        hintPopUp = GameObject.Find("Hint Message").gameObject;
        hintPopUp.SetActive(false);
    }

    public IEnumerator displayHint()
    {
            Debug.Log("in hint display");
            Debug.Log(time);
            hintPopUp.SetActive(true);
            yield return new WaitForSeconds(time);
            hintPopUp.SetActive(false);
    }
}
