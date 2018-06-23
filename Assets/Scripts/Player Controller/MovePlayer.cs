using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MovePlayer : MonoBehaviour
{
    Vector3 playerStart, playerCurrent;
    Vector3 forward, right; // Keeps track of our relative forward and right vectors

    List<string> runCommands = new List<string>(); //All Slot Panel Blocks Translated
    List<string> chosenColor = new List<string>();
    string TBCC; //String Carries Names of all blocks in panel Slot
    int chosenBlocks = 0; // Number of Blocks in panel slot, # of commands to be executed

    public float moveSpeed = 1000f;//Change in inspector to adjust move speed
    public int flag = 0;

    private int scoreCount;
    public Text scoreText;

    public AudioClip pickupSound;
    public AudioSource pickupSource;

    int i = 0;
    int ContinueFlag = 1; // Flag to know when player made a mistake
    
    int ifTrigger = 0;
	public GameObject[] ifPortal;
    List<GameObject> PickupsList = new List<GameObject>();
    public Renderer ren;
    public Material[] mat;

    int ColorCounter = 0;

    public int jumpUpFlag = 0;//to avoid jumping up again when player is on a high-level tile already
    public int jumpDownFlag = 0;//to make the player only jump down when going from a high-level tile to a ground-level tile

    public static int clicksCountResetButton = 0;
    hintMessage hint = new hintMessage();

    Button runButton;
    Button resetButton;

    void Start()
    {
        forward = transform.forward;
        forward.y = 0; // make sure y is 0
        forward = Vector3.Normalize(forward); // make sure the length of vector is set to a max of 1.0
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; // set the right-facing vector to be facing right relative to the camera's forward vector

        playerStart = transform.position;//save the initial position of the player for future use

        resetButton = GameObject.Find("ResetButton").GetComponent<Button>();
        resetButton.interactable = false;

        runButton = GameObject.Find("RunButton").GetComponent<Button>();

        scoreCount = 0;
        SetScoreText();

        pickupSource = GetComponent<AudioSource>();

    }

    public void RunButtonClicker()
    {
        runButton.interactable = false;
        resetButton.interactable = true;

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
        //Reset player to start position
        Debug.Log("RESET BUTTON CLICKED!");

        resetPlayer();
        //Clear Slots Panel:to be resolved later

        //Enable Run Button
        runButton.interactable = true;
        resetButton.interactable = false;

        //Check number of times the Reset Button is clicked to display hints
        clicksCountResetButton++;
        if (clicksCountResetButton == 3)
        {
            Debug.Log("will start displaying hints");
            StartCoroutine(hint.displayHint());
            clicksCountResetButton = 0;
        }

        //Resetting Pickups
        for (int i = 0; i < PickupsList.Count; i++)
        {
            PickupsList[i].SetActive(true);
        }
        scoreCount = 0;
        SetScoreText();

        //Resetting If Portals
        ColorCounter = 0;
        if (SceneManager.GetActiveScene().name == "Planet2-Level1")
        {
            ifPortal = GameObject.FindGameObjectsWithTag(chosenColor[1]);
            ren = ifPortal[0].GetComponent<Renderer>();//.material[2].color = Color.red;
            mat = ren.materials;
            mat[2].color = Color.green;
        }
        else if (SceneManager.GetActiveScene().name == "Planet3-Level1")
        {
            ifPortal = GameObject.FindGameObjectsWithTag(chosenColor[1]);
            ren = ifPortal[0].GetComponent<Renderer>();//.material[2].color = Color.red;
            mat = ren.materials;
            mat[2].color = Color.yellow;

            ren = ifPortal[1].GetComponent<Renderer>();//.material[2].color = Color.red;
            mat = ren.materials;
            mat[2].color = Color.cyan; //cyan is close enough xD
        }

    }


    void resetPlayer()
    {
        runButton.interactable = true;
        transform.position = playerStart;
        transform.forward = Vector3.Normalize(forward);

        jumpUpFlag = 0; jumpDownFlag = 0;

        StopAllCoroutines();
        runCommands.Clear();
        Inventory.start = true;

    }

    public IEnumerator Move(int direction)
    {
        float s = (15 / Time.deltaTime) * 10 * direction;
        transform.Translate(Vector3.forward * s * Time.deltaTime * -1);//set main character to move forward
        yield return null;
    }

    public IEnumerator Jump(int verticalDirection, int horizontalDirection)
    {
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
        while (chosenBlocks > 0 && flag == 0)
        {
            flag = 1;

            for (i = 0; i < runCommands.Count; i++)
            {
                if (ContinueFlag == 1)
                {
                    playerCurrent = transform.position;
                    if (runCommands[i] == "rotateRightBlock(Clone)")
                    {
                        StartCoroutine(RotateAround(Vector3.up, 90.0f, 1.0f));
                    }
                    else if (runCommands[i] == "rotateLeftBlock(Clone)")
                    {
                        StartCoroutine(RotateAround(Vector3.up, -90.0f, 1.0f));
                    }
                    else if (runCommands[i] == "moveBlock(Clone)")
                    {
                        StartCoroutine(Move(1));
                    }
                    else if (runCommands[i] == "jumpBlock(Clone)" && jumpDownFlag == 0 && jumpUpFlag == 0)
                    {
                        Debug.Log("PLAYER SHOULD JUMP UPWARDS!");
                        StartCoroutine(Jump(1, 1));

                        jumpUpFlag = 1;
                    }
                    else if (runCommands[i] == "jumpBlock(Clone)" && jumpDownFlag == 1)
                    {
                        Debug.Log("PLAYER SHOULD JUMP DOWNWARDS!");
                        StartCoroutine(Jump(-1, 1));

                        jumpDownFlag = 0;
                    }
                    else if (runCommands[i].Contains("ifBlock(Clone)"))
                    {
                        chosenColor = runCommands[i].Split('-').ToList<string>();
                        Debug.Log(chosenColor[1]);
						Debug.Log (Slot.LevelColors [ColorCounter]);

                        if (chosenColor[1] == Slot.LevelColors[ColorCounter] && ifTrigger == 1)
                        {
                            ContinueFlag = 1;
                            ColorCounter++;

							ifPortal = GameObject.FindGameObjectsWithTag (chosenColor [1]);
                            ren = ifPortal[0].GetComponent<Renderer>();//.material[2].color = Color.red;
                            mat = ren.materials;
                            mat[2].color = Color.clear;
                            //Destroy (ifPortal[0]);
                            ifTrigger = 0;
                        }
                        else
                        {
                            ContinueFlag = 0;
                        }
                    }

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
            pickupSource.PlayOneShot(pickupSound, 1);
            PickupsList.Add(col.gameObject);
            col.gameObject.SetActive(false);
            scoreCount = scoreCount + 1;
            SetScoreText();
        }
        else if (col.gameObject.CompareTag("Wall"))//if player hits a wall, reset player to start position
        {
            Debug.Log("DETECTED WALL BOUNDARY !");
            resetPlayer();
        }
        else if (col.gameObject.CompareTag("Step Up"))//if player hits a step-up tile while moving, return to previous position
        {
            Debug.Log("DETECTED JUMP-UP COLLIDER !");
            transform.position = playerCurrent;
        }
        else if (col.gameObject.CompareTag("Step Down"))//if player in on a high tile, set flag to allow downwards-jump
        {
            Debug.Log("DETECTED JUMP-DOWN COLLIDER !");
            jumpDownFlag = 1;
        }
        else if (col.gameObject.CompareTag("ifTrigger"))
        {
            ifTrigger = 1;
            Debug.Log("HIT if Trigger");
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + scoreCount.ToString();
    }

}



//public IEnumerator reverseMovement()
//{
//    while (chosenBlocks > 0 && flag == 0)
//    {
//        flag = 1;

//        for (int i = runCommands.Count - 1; i >= 0; i--)
//        {
//            //Debug.Log(i + runCommands[i]);
//            if (runCommands[i] == "rotateLeftBlock(Clone)")
//            {
//                StartCoroutine(RotateAround(Vector3.up, 90.0f, 1.0f));
//            }
//            else if (runCommands[i] == "rotateRightBlock(Clone)")
//            {
//                StartCoroutine(RotateAround(Vector3.up, -90.0f, 1.0f));

//            }
//            else if (runCommands[i] == "moveBlock(Clone)")
//            {
//                StartCoroutine(Move(-1));
//            }
//            else if (runCommands[i] == "jumpBlock(Clone)" && jumpReversePath.Peek().Equals("jumpUp"))
//            {
//                StartCoroutine(Jump(-1,-1));
//                jumpReversePath.Pop();
//                Debug.Log("IN REVERSE STEP-UP CODE !");
//            }
//            else if (runCommands[i] == "jumpBlock(Clone)" && jumpReversePath.Peek().Equals("jumpDown"))
//            {
//                StartCoroutine(Jump(1,-1));
//                jumpReversePath.Pop();
//                Debug.Log("IN REVERSE STEP-DOWN CODE !");
//            }
//            chosenBlocks--;
//            yield return new WaitForSeconds(2.0f);
//        }
//        flag = 0;
//    }
//}
