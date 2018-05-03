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
    int numberOfClicksProcedure = 1;
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
                if (item.name == "procedureBlock(Clone)") //get blocks from procedure panel
                {
                    slots = GameObject.Find("codePanelProcedure").transform;
                    numberOfClicksProcedure = slotTransform.GetComponent<Slot>().clicksCount;
                    Debug.Log("clicks of procedure:" + numberOfClicksProcedure);

                    for (int r = 0; r < numberOfClicksProcedure+1; r++)
                    {
                        Debug.Log("repeating");
                        foreach (Transform slotTransformProcedure in slots) //slots of the procedure panel
                        {
                            item = slotTransformProcedure.GetComponent<Slot>().item;

                            if (item)
                            {
                                numberOfClicks = slotTransformProcedure.GetComponent<Slot>().clicksCount;
                                loopThroughClicks(item.name, numberOfClicks);
                            }
                        }
                    }
                }
                else
                {
                    numberOfClicks = slotTransform.GetComponent<Slot>().clicksCount;
                    loopThroughClicks(item.name, numberOfClicks);
                }
            }
        }
        finished = true;
    }

    public StringBuilder loopThroughClicks(string nameOfBlock, int numberOfClicks)
    {
        if(numberOfClicks == 0) //append the block once
        {
            builder.Append(",");
            builder.Append(nameOfBlock);
        }
        else //append the block numberOfClicks times
        {
            for (int r = 0; r < numberOfClicks+1; r++)
            {
                builder.Append(",");
                builder.Append(nameOfBlock);
                //Debug.Log("loop");
                //Debug.Log(builder.ToString());
            }
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
