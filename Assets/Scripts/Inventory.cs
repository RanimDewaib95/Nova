using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    Transform slots;
    [SerializeField]
    Text inventoryText;

    StringBuilder builder = new StringBuilder();

    public static string TBCC;
    public static bool finished = false;
    public static bool start = false;

    public GameObject textBlock;
    public GameObject player; 

    int numberOfClicks = 0;
    GameObject item;

    public void Start()
    {
        player = GameObject.Find("astronaut");
    }
    public void Changed()
    {
        getStringOfBlocks();
        if (finished == true)
        {
            Debug.Log("I am printing TBCC");
            TBCC = builder.ToString();
            //Debug.Log(TBCC);
            start = true;
            player.GetComponent<MovePlayer>().RunButtonClicker();
        }
    }

    public void getStringOfBlocks()
    {
        foreach (Transform slotTransform in slots)
        {
            item = slotTransform.GetComponent<Slot>().item;
            if (item)
            {
                //Debug.Log("in");
                numberOfClicks = slotTransform.GetComponent<Slot>().clicksCount;
                //Debug.Log(item.name);
                //Debug.Log("Number of clicks");
                //Debug.Log(numberOfClicks);
                loopThroughClicks(item.name, numberOfClicks);
            }
        }
        finished = true;
    }

    public StringBuilder loopThroughClicks(string nameOfBlock, int numberOfClicks)
    {
        for (int r = 0; r < numberOfClicks+1; r++)
        {
            builder.Append(",");
            builder.Append(nameOfBlock);
            //Debug.Log("loop");
            //Debug.Log(builder.ToString());
        }
        return builder;
    }
}


namespace UnityEngine.EventSystems
{
    public interface IHasChanged : IEventSystemHandler
    {
        void HasChanged();
    }
}
