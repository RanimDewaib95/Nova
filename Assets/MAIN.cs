using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MAIN : MonoBehaviour {

	// Use this for initialization
	public void Start () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
