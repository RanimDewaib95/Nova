using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 700f;//Change in inspector to adjust move speed
    public int flag = 0;
    Vector3 forward, right; // Keeps track of our relative forward and right vectors
    string blockName;
    List<string> runCommands = new List<string>();
    string TBCC;
    int chosenBlocks = 0;

    //blockName = DragHandler.chosenBlocks[i];

    void Start()
    {
        //forward = Camera.main.transform.forward; // Set forward to equal the camera's forward vector
        forward = transform.forward;
        forward.y = 0; // make sure y is 0
        forward = Vector3.Normalize(forward); // make sure the length of vector is set to a max of 1.0
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; // set the right-facing vector to be facing right relative to the camera's forward vector

    }

    public void RunButtonClicker()
    {
        //Debug.Log(Inventory.TBCC);
        //Split TBCC into a List
        /*
        TBCC = Inventory.TBCC;
        runCommands = TBCC.Split(',').ToList<string>();
        runCommands.RemoveAt(0);
        chosenBlocks = runCommands.Count;
        Debug.Log("# of blocks" + chosenBlocks);

        StartCoroutine(updateMovement());*/
        if (Inventory.start == true)
        {
            //Debug.Log(Inventory.TBCC);
            //Split TBCC into a List
            TBCC = Inventory.TBCC;
            Debug.Log(TBCC);
            runCommands = TBCC.Split(',').ToList<string>();
            runCommands.RemoveAt(0);

            for (int j = 0; j < runCommands.Count; j++)
            {
                Debug.Log(runCommands[j]);
            }

            chosenBlocks = runCommands.Count;
            Debug.Log("# of blocks " + chosenBlocks);

            StartCoroutine(updateMovement());

            Inventory.start = false;
        }
    }

    public IEnumerator Move()
    {
        float moveSpeed = 8000f;
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);//set main character to move forward
        yield return null;
    }

    public IEnumerator RotateAround(Vector3 axis, float angle, float duration)
    {
        float elapsed = 0.0f;
        float rotated = 0.0f;
        while (elapsed < duration)
        {
            float step = angle / duration * Time.deltaTime;
            transform.RotateAround(transform.position, axis, step);
            elapsed += Time.deltaTime;
            rotated += step;
            yield return null;
        }
        transform.RotateAround(transform.position, axis, angle - rotated);
    }

    public IEnumerator updateMovement()
    {
        //Debug.Log("Coroutine started");

        while (chosenBlocks > 0 && flag == 0) {
            flag = 1;
            //blockName = DragHandler.chosenBlocks.Dequeue();//----------------------------------------
            //Debug.Log(blockName);

            for (int i = 0; i < runCommands.Count; i++)
            {
<<<<<<< HEAD
                Debug.Log(i + runCommands[i]);
                if (runCommands[i] == "rotateRightBlock")
=======
                //Debug.Log(i + runCommands[i]);
                if (runCommands[i] == "rotateRightBlock(Clone)")
>>>>>>> master
                {
                    StartCoroutine(RotateAround(Vector3.up, 90.0f, 1.0f));
                }
                else if (runCommands[i] == "rotateLeftBlock(Clone)")
                {
                    StartCoroutine(RotateAround(Vector3.up, -90.0f, 1.0f));

                }
                else if (runCommands[i] == "moveBlock(Clone)")
                {
                    StartCoroutine(Move());
                }
                else
                {

                }
                Debug.Log("B"+chosenBlocks);
                chosenBlocks--;
                Debug.Log("A"+chosenBlocks);
                yield return new WaitForSeconds(2.0f);
            }
            flag = 0;
        }
        ////////////////////////////////////
        //Create A Reset Function that will reset the following
        // the position of the cube on the right slot
        // the axis of the cube
        //Note: Disable the Run Button & Let player press the reset Button
    }
}
