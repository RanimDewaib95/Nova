using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableHandler : MonoBehaviour {

    public int score = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("in collectible handler script");
	}

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("in collision function");
        if (col.gameObject.tag == "collectible")
        {
            score += 1;
            Debug.Log(score);
            Destroy(col.gameObject);
        }
    }
}
