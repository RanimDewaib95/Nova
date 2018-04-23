using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;


public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 1000f;//Change in inspector to adjust move speed
    public int flag = 0;
    Vector3 forward, right; // Keeps track of our relative forward and right vectors

    string blockName;
    public int score = 0;

    //blockName = DragHandler.chosenBlocks[i];

    List<string> runCommands = new List<string>(); //All Slot Panel Blocks Translated
    string TBCC; //String Carries Names of all blocks in panel Slot
    int chosenBlocks = 0; // Number of Blocks in panel slot, # of commands to be executed
    //Vector3 playerInitialPosition, playerInitialForward; // Initial Player Setting


    void Start()
    {
        //forward = Camera.main.transform.forward; // Set forward to equal the camera's forward vector
        //playerInitialPosition = transform.position;
        //playerInitialForward = transform.forward;
        forward = transform.forward;
        forward.y = 0; // make sure y is 0
        forward = Vector3.Normalize(forward); // make sure the length of vector is set to a max of 1.0
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; // set the right-facing vector to be facing right relative to the camera's forward vector

        Button ResetButton = GameObject.Find("ResetButton").GetComponent<Button>();
        ResetButton.interactable = false;
    }

    public void RunButtonClicker()
    {
        //Debug.Log(Inventory.TBCC);
        Button RunButton = GameObject.Find("RunButton").GetComponent<Button>();
        Button ResetButton = GameObject.Find("ResetButton").GetComponent<Button>();
        RunButton.interactable = false;
        ResetButton.interactable = true;
        //Split TBCC into a List
        TBCC = Inventory.TBCC;
        runCommands = TBCC.Split(',').ToList<string>();
        runCommands.RemoveAt(0);
        chosenBlocks = runCommands.Count;
        //Debug.Log("# of blocks" + chosenBlocks);

        StartCoroutine(updateMovement());
    }

    public void ResetButtonClicker()
    {
        chosenBlocks = runCommands.Count;
        StartCoroutine(reverseMovement());
        //Enable Run Button
        Button RunButton = GameObject.Find("RunButton").GetComponent<Button>();
        Button ResetButton = GameObject.Find("ResetButton").GetComponent<Button>();
        RunButton.interactable = true;
        ResetButton.interactable = false;
        //Clear Slots Panel

    }

    public IEnumerator Move(float moveSpeed)
    {
        //float moveSpeed = 8000f;
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * -1);//set main character to move forward
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

            for (int i = 0; i < runCommands.Count; i++)
            {
                //Debug.Log(i + runCommands[i]);
                if (runCommands[i] == "rotateRightBlock")
                {
                    StartCoroutine(RotateAround(Vector3.up, 90.0f, 1.0f));
                }
                else if (runCommands[i] == "rotateLeftBlock")
                {
                    StartCoroutine(RotateAround(Vector3.up, -90.0f, 1.0f));

                }
                else if (runCommands[i] == "moveBlock")
                {
                    StartCoroutine(Move(8000f));
                }else
                {

                }
                //Debug.Log("B"+chosenBlocks);
                chosenBlocks--;
                //Debug.Log("A"+chosenBlocks);
                yield return new WaitForSeconds(2.0f);
            }
            flag = 0;
        }

    }


    public IEnumerator reverseMovement()
    {
         while (chosenBlocks > 0 && flag == 0) {
            flag = 1;

        for (int i = runCommands.Count - 1; i >= 0; i--)
        {
            //Debug.Log(i + runCommands[i]);
            if (runCommands[i] == "rotateLeftBlock")
            {
                StartCoroutine(RotateAround(Vector3.up, 90.0f, 1.0f));
            }
            else if (runCommands[i] == "rotateRightBlock")
            {
                StartCoroutine(RotateAround(Vector3.up, -90.0f, 1.0f));

            }
            else if (runCommands[i] == "moveBlock")
            {
                StartCoroutine(Move(-8000f));
            }
            else
            {

            }
            chosenBlocks--;
            yield return new WaitForSeconds(2.0f);
        }
        flag = 0;
         }

    }

    void OnTriggerEnter(Collider other)
    {
        //Destroy(other.gameObject);
        if (other.gameObject.CompareTag("collectible"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
