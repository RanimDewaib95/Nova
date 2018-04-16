using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 700f;//Change in inspector to adjust move speed
    public int flag = 0;
    Vector3 forward, right; // Keeps track of our relative forward and right vectors
    string blockName;

    //blockName = DragHandler.chosenBlocks[i];

    void Start()
    {
        //forward = Camera.main.transform.forward; // Set forward to equal the camera's forward vector
        forward = transform.forward;
        forward.y = 0; // make sure y is 0
        forward = Vector3.Normalize(forward); // make sure the length of vector is set to a max of 1.0
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; // set the right-facing vector to be facing right relative to the camera's forward vector

    }

    void Update()
    {
        if (runButton.clicked == true)
        {
            Debug.Log("in runButton");

            StartCoroutine(updateMovement());

            //for (int i = 0; i < DragHandler.chosenBlocks.Count; i++)
            //{
            //    Debug.Log(i);
            //    //blockName = DragHandler.chosenBlocks[i];
            //    switch (blockName)
            //    {
            //        case "rotateRightBlock(Clone)":
            //            StartCoroutine(RotateAround(Vector3.up, 90.0f, 1.0f));
            //            Debug.Log("in right");
            //            break;

            //        case "rotateLeftBlock(Clone)":
            //            StartCoroutine(RotateAround(Vector3.up, -90.0f, 1.0f));
            //            Debug.Log("in left");
            //            break;

            //        case "moveBlock(Clone)":
            //            //moveForward();
            //            StartCoroutine(Move());
            //            Debug.Log("in move");
            //            break;
            //    }
            //}
        }
        
        /*
        if (Input.GetKey(KeyCode.UpArrow) && flag == 0)
        {
            flag = 1;
            Move();
        }

        if (Input.GetKey(KeyCode.DownArrow) && flag == 0)
        {
            flag = 1;
            Move();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(RotateAround(Vector3.up, 90.0f, 1.0f));
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(RotateAround(Vector3.up, -90.0f, 1.0f));
        }*/
    }

    public IEnumerator Move()
    {
        GameObject player = GameObject.Find("Cube");//locating main character/player
        float moveSpeed = 300f;
        player.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);//set main character to move forward
        yield return null;

        /*
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Vector3 HorStep = new Vector3(100f, 0f, 0f);
        Vector3 rightMovement = (HorStep + right) * Input.GetAxis("Horizontal");

        Vector3 VerStep = new Vector3(0f, 0f, 100f);
        Vector3 upMovement = (VerStep + forward) * Input.GetAxis("Vertical");

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        transform.forward = heading;
        transform.position += rightMovement;
        transform.position += upMovement;
        */
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
        Debug.Log("Coroutine started");

        while (DragHandler.chosenBlocks.Count != 0 && flag == 0) {
            flag = 1;
            blockName = DragHandler.chosenBlocks.Dequeue();
            Debug.Log(blockName);

            //blockName = DragHandler.chosenBlocks[i];
            switch (blockName)
            {
                case "rotateRightBlock(Clone)":
                    StartCoroutine(RotateAround(Vector3.up, 90.0f, 1.0f));
                    Debug.Log("in right");
                    break;

                case "rotateLeftBlock(Clone)":
                    StartCoroutine(RotateAround(Vector3.up, -90.0f, 1.0f));
                    Debug.Log("in left");
                    break;

                case "moveBlock(Clone)":
                    //moveForward();
                    StartCoroutine(Move());
                    Debug.Log("in move");
                    break;
            }
            yield return new WaitForSeconds(1.5f);
            flag = 0;
        }
        runButton.clicked = false;
    }
}
