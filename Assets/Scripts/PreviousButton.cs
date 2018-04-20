using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousButton : MonoBehaviour {

    public static bool clicked = false;

    public void previousButtonClicked()
    {
        clicked = true;
    }
}
