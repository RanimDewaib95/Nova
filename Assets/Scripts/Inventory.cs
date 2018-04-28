using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text;

public class Inventory : MonoBehaviour {
	[SerializeField] Transform slots;
	[SerializeField] Text inventoryText;

    StringBuilder builder = new StringBuilder();

    public static string TBCC;
    public static bool finished = false;
    public static bool start = false;

    public GameObject textBlock;

    int numberOfClicks = 0;
    GameObject item;

    // Use this for initialization
    void Start () {

    }

	public void Changed ()
	{      
        getStringOfBlocks();
        if(finished == true)
        {
            Debug.Log("I am printing TBCC");
            TBCC = builder.ToString();
            start = true;
        }
    }

    public void getStringOfBlocks()
    {
        foreach (Transform slotTransform in slots)
        {
            item = slotTransform.GetComponent<Slot>().item;
            if (item)
            {
                numberOfClicks = slotTransform.GetComponent<Slot>().clicksCount;
                loopThroughClicks(item.name, numberOfClicks);
            }      
        }
        finished = true;
    }

    public StringBuilder loopThroughClicks( string nameOfBlock, int numberOfClicks)
    {
       for (int r = 0; r < numberOfClicks; r++)
       {
        builder.Append(",");
        builder.Append(nameOfBlock);
       }
        return builder;
    }
}
	
	
namespace UnityEngine.EventSystems {
	public interface IHasChanged : IEventSystemHandler {
		void HasChanged ();	
	}
}
