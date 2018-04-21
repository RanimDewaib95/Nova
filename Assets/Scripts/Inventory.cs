using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IHasChanged {
	[SerializeField] Transform slots;
	[SerializeField] Text inventoryText; 

	public static string TBCC;
	public GameObject textBlock;

    int numberOfClicks = 0;

	// Use this for initialization
	void Start () {
		HasChanged ();
	}

	#region IHasChanged implementation

	public void HasChanged ()
	{
		System.Text.StringBuilder builder = new System.Text.StringBuilder ();
        
        foreach (Transform slotTransform in slots) {
			GameObject item = slotTransform.GetComponent<Slot>().item;
            /*if (item.name.Contains("Counter"))
            {
                numberOfClicks = item.GetComponent<DragHandler>().clicksCount;
            }*/
			else if (item) {
                builder.Append(",");
                if (item.name.Contains("rotateLeftBlock")){
                    builder.Append("rotateLeftBlock");
                }else if (item.name.Contains("rotateRightBlock")){
                    builder.Append("rotateRightBlock");
                }else if (item.name.Contains("moveBlock")){
                    builder.Append("moveBlock");
				}else if (item.name.Contains ("jump")){
					builder.Append ("jump");
				} else if (item.name.Contains ("collect")) {
                    builder.Append("collect");
				} else {
                    //Debug.Log("Error");
				}
			}
		}
		TBCC = builder.ToString ();
        //Debug.Log(TBCC);
		//inventoryText.text = builder.ToString ();
	}

	#endregion
}
	
	
namespace UnityEngine.EventSystems {
	public interface IHasChanged : IEventSystemHandler {
		void HasChanged ();	
	}
}
