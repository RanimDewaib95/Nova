using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runButton : MonoBehaviour {

    public void runButtonClicked()
    {
        MovePlayer script = FindObjectOfType<MovePlayer>();
        string blockName;

        for(int i = 0; i < DragHandler.chosenBlocks.Count; i++)
        {
            Debug.Log(i);
            blockName = DragHandler.chosenBlocks[i];
            switch(blockName)
            {
                case "rotateRightBlock(Clone)":
                    StartCoroutine(script.RotateAround(Vector3.up, 90.0f, 1.0f));
                    Debug.Log("in right");
                    break;

                case "rotateLeftBlock(Clone)":
                    StartCoroutine(script.RotateAround(Vector3.up, -90.0f, 1.0f));
                    Debug.Log("in left");
                    break;

                case "moveBlock(Clone)":
                    moveForward();
                    //script.Move();
                    Debug.Log("in move");
                    break;
            }
        }
    }

    void moveForward()
    {
        GameObject player = GameObject.Find("Cube");//locating main character/player
        float moveSpeed = 100f;
        player.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);//set main character to move forward
    }
}
