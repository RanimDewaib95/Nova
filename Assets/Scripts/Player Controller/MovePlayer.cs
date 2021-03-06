﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Threading;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 1000f;//Change in inspector to adjust move speed
    public int flag = 0;
    Vector3 forward, right; // Keeps track of our relative forward and right vectors
    List<string> runCommands = new List<string>(); //All Slot Panel Blocks Translated
    string TBCC; //String Carries Names of all blocks in panel Slot
    int chosenBlocks = 0; // Number of Blocks in panel slot, # of commands to be executed
    //Vector3 playerInitialPosition, playerInitialForward; // Initial Player Setting

    private int scoreCount;
    public Text scoreText;

    public AudioClip pickupSound;
    public AudioSource pickupSource;
    public float volLowRange = .5f;
    public float volHighRange = 1.0f;
    float vol;

    public int stepUpFlag = 0;
    public int stepDownFlag = 0;
    Stack jumpReversePath = new Stack();

    endingMessage goalMessage;
    //public Vector3 bounceOffPosition;

    public static int  clicksCountResetButton = 0;
    hintMessage hint = new hintMessage();

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

        scoreCount = 0;
        SetScoreText();

        pickupSource = GetComponent<AudioSource>();
    }

    public void RunButtonClicker()
    {
        //Debug.Log(Inventory.TBCC);
        Button RunButton = GameObject.Find("RunButton").GetComponent<Button>();
        Button ResetButton = GameObject.Find("ResetButton").GetComponent<Button>();
        RunButton.interactable = false;
        ResetButton.interactable = true;

        //after inventory finished getting the chosen blocks
        if (Inventory.start == true)
        {
            TBCC = Inventory.TBCC;
            runCommands = TBCC.Split(',').ToList<string>();
            runCommands.RemoveAt(0);
            chosenBlocks = runCommands.Count;
            StartCoroutine(updateMovement());
            Inventory.start = false;
        }
    }

    public void ResetButtonClicker()
    {
        clicksCountResetButton++;
        Debug.Log("in reverse");
        chosenBlocks = runCommands.Count;
        Debug.Log("number of blocks to reverse is " + chosenBlocks);
        StartCoroutine(reverseMovement());
        //Enable Run Button
        Button RunButton = GameObject.Find("RunButton").GetComponent<Button>();
        Button ResetButton = GameObject.Find("ResetButton").GetComponent<Button>();
        RunButton.interactable = true;
        ResetButton.interactable = false;
        //Clear Slots Panel

        if(clicksCountResetButton == 3)
        {
            Debug.Log("will start displaying hints");
            StartCoroutine(hint.displayHint());
            clicksCountResetButton = 0;
        }
    }

    public IEnumerator Move(int direction)
    {
        float s = (15 / Time.deltaTime) * 10 * direction;
        Debug.Log("speed is" + s);
        transform.Translate(Vector3.forward * s * Time.deltaTime * -1);//set main character to move forward
        yield return null;
    }

    public IEnumerator Jump(int verticalDirection, int horizontalDirection)
    {
        if (verticalDirection == 1)//jump upwards
        {
            stepUpFlag = 0;
        }
        else if (verticalDirection == -1)//jump downwards
        {
            stepDownFlag = 0;
        }

        float s = (15 / Time.deltaTime) * 10;
        Debug.Log("speed is" + s);

        transform.Translate(Vector3.up * s * Time.deltaTime * verticalDirection * 0.5f);//set main character to move upwards then
        transform.Translate(Vector3.forward * s * Time.deltaTime * horizontalDirection * -1);//move forward to add the "jump" effect
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

        while (chosenBlocks > 0 && flag == 0)
        {
            flag = 1;
            for (int i = 0; i < runCommands.Count; i++)
            {
                //bounceOffPosition = transform.position;
                //Debug.Log(i + runCommands[i]);
                if (runCommands[i] == "rotateRightBlock(Clone)")
                {
                    StartCoroutine(RotateAround(Vector3.up, 90.0f, 1.0f));
                }
                else if (runCommands[i] == "rotateLeftBlock(Clone)")
                {
                    StartCoroutine(RotateAround(Vector3.up, -90.0f, 1.0f));

                }
                else if (runCommands[i] == "moveBlock(Clone)" && stepUpFlag == 0 && stepDownFlag == 0)
                {
                    StartCoroutine(Move(1));
                }
                else if (runCommands[i] == "jumpBlock(Clone)" && stepUpFlag == 1)
                {
                    StartCoroutine(Jump(1,1));
                    jumpReversePath.Push("jumpUp");
                    Debug.Log("IN STEP-UP CODE !");
                }
                else if (runCommands[i] == "jumpBlock(Clone)" && stepDownFlag == 1)
                {
                    StartCoroutine(Jump(-1,1));
                    jumpReversePath.Push("jumpDown");
                    Debug.Log("IN STEP-DOWN CODE !");
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
        while (chosenBlocks > 0 && flag == 0)
        {
            flag = 1;

            for (int i = runCommands.Count - 1; i >= 0; i--)
            {
                //Debug.Log(i + runCommands[i]);
                if (runCommands[i] == "rotateLeftBlock(Clone)")
                {
                    StartCoroutine(RotateAround(Vector3.up, 90.0f, 1.0f));
                }
                else if (runCommands[i] == "rotateRightBlock(Clone)")
                {
                    StartCoroutine(RotateAround(Vector3.up, -90.0f, 1.0f));

                }
                else if (runCommands[i] == "moveBlock(Clone)")
                {
                    StartCoroutine(Move(-1));
                }
                else if (runCommands[i] == "jumpBlock(Clone)" && jumpReversePath.Peek().Equals("jumpUp"))
                {
                    StartCoroutine(Jump(-1,-1));
                    jumpReversePath.Pop();
                    Debug.Log("IN REVERSE STEP-UP CODE !");
                }
                else if (runCommands[i] == "jumpBlock(Clone)" && jumpReversePath.Peek().Equals("jumpDown"))
                {
                    StartCoroutine(Jump(1,-1));
                    jumpReversePath.Pop();
                    Debug.Log("IN REVERSE STEP-DOWN CODE !");
                }
                chosenBlocks--;
                yield return new WaitForSeconds(2.0f);
            }
            flag = 0;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Pick Up"))
        {
            vol = Random.Range(volLowRange, volHighRange);
            pickupSource.PlayOneShot(pickupSound, vol);

            col.gameObject.SetActive(false);
            scoreCount = scoreCount + 1;
            SetScoreText();
        }
        else if (col.gameObject.CompareTag("Finish"))
        {
            transform.gameObject.SetActive(false);
            goalMessage = new endingMessage();
            Thread.Sleep(1000);
            goalMessage.displayLastMessage();
            Debug.Log("heba found collider");
        }
        else if (col.gameObject.CompareTag("Step Up"))
        {
            Debug.Log("DETECTED STEP-UP COLLIDER !");
            stepUpFlag = 1;
        }
        else if (col.gameObject.CompareTag("Step Down"))
        {
            Debug.Log("DETECTED STEP-DOWN COLLIDER !");
            stepDownFlag = 1;
        }
        else if (col.gameObject.CompareTag("Midway Step"))
        {
            Debug.Log("DETECTED MIDWAY-STEP COLLIDER !");
            stepUpFlag = 1;
            stepDownFlag = 1;
        }
        else if (col.gameObject.CompareTag("Wall"))
        {
            Debug.Log("BOUNCING WALL COLLIDER !");
            //transform.position = bounceOffPosition;
            transform.gameObject.SetActive(false);
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + scoreCount.ToString();
    }
}
