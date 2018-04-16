using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runButton : MonoBehaviour{

    public static bool clicked = false;

    public void runButtonClicked()
    {
        clicked = true;
        /*MovePlayer script = FindObjectOfType<MovePlayer>();
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
                    //moveForward();
                    script.Move();
                    Debug.Log("in move");
                    break;
            }
        }*/
    }
}
