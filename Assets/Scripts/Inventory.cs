using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text;

public class Inventory : MonoBehaviour, IHasChanged {
	[SerializeField] Transform slots;
	[SerializeField] Text inventoryText;

    StringBuilder builder = new StringBuilder();

    public static string TBCC;
	public GameObject textBlock;

    int numberOfClicks = 0;
    GameObject item;

    // Use this for initialization
    void Start () {
        HasChanged();
    }

	#region IHasChanged implementation

	public void HasChanged ()
	{
        while(true)
        {
            if (runButton.clicked == true)
            {
                getStringOfBlocks();
                TBCC = getStringOfBlocks().ToString();
                Debug.Log("I am printing TBCC");
                Debug.Log(TBCC);
                //inventoryText.text = builder.ToString ();
            }
        }

    }

    #endregion

    public StringBuilder getStringOfBlocks()
    {
        foreach (Transform slotTransform in slots)
        {
            item = slotTransform.GetComponent<Slot>().item;
            if (item)
            {
                numberOfClicks = item.GetComponent<DragHandler>().clicksCount;
                //Debug.Log(item.name);
                //Debug.Log("Number of clicks");
                //Debug.Log(numberOfClicks);
                loopThroughClicks(item.name, numberOfClicks);
            }      
        }
        return builder;
    }

    public StringBuilder loopThroughClicks( string nameOfBlock, int numberOfClicks)
    {
        //Debug.Log("in");
        //Debug.Log(numberOfClicks);
       for (int r = 0; r < numberOfClicks; r++)
       {
        builder.Append(",");
        builder.Append(nameOfBlock);
           // Debug.Log("loop");
           // Debug.Log(builder.ToString());
       }
        return builder;
    }
}
	
	
namespace UnityEngine.EventSystems {
	public interface IHasChanged : IEventSystemHandler {
		void HasChanged ();	
	}
}
