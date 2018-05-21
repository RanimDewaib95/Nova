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

        scoreText.text = "";
        scoreCount = 0;
        SetScoreText ();

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
    }

    public IEnumerator Move(int direction)
    {
        float s = (15 / Time.deltaTime)*10*direction;
        Debug.Log("speed is" + s);
        transform.Translate(Vector3.forward * s * Time.deltaTime * -1);//set main character to move forward
        yield return null;
    }

    public IEnumerator Jump(int direction)
    {
        if (direction == 1)
        {
            stepUpFlag = 0;
        }
        else if (direction == -1)
        {
            stepDownFlag = 0;
        }

        float s = (15 / Time.deltaTime) * 10 * direction;
        Debug.Log("speed is" + s);

        transform.Translate(Vector3.up * s * Time.deltaTime * 0.5f);//set main character to move upwards then
        transform.Translate(Vector3.forward * s * Time.deltaTime * direction * -1);//move forward to add the "jump" effect
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
                    StartCoroutine(Jump(1));
                }
                else if (runCommands[i] == "jumpBlock(Clone)" && stepDownFlag == 1)
                {
                    StartCoroutine(Jump(-1));
                }
                else
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
            else
            {

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

            col.gameObject.SetActive (false);
            scoreCount = scoreCount + 1;
            SetScoreText ();
        }
        else if (col.gameObject.CompareTag("Step Up"))
        {
            Debug.Log("IN STEP-UP CODE !");
            stepUpFlag = 1;
        }
        else if (col.gameObject.CompareTag("Step Down"))
        {
            Debug.Log("IN STEP-DOWN CODE !");
            stepDownFlag = 1;
        }
        else if (col.gameObject.CompareTag("Midway Step"))
        {
            Debug.Log("IN MIDWAY-STEP CODE !");
            stepUpFlag = 1;
            stepDownFlag = 1;
        }
    }

    void SetScoreText ()
    {
        scoreText.text = "Score: " + scoreCount.ToString ();
    }
}
