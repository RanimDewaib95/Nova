using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour {

    public static bool clicked = false;

    public void nextButtonClicked()
    {
        clicked = true;
    }
}
