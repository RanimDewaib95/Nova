using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceStationMenu : MonoBehaviour {

    public void levelOneClicked()
    {
        SceneManager.LoadScene("Spacestation-Level1");
    }
}
