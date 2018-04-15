using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IHasChanged {
	[SerializeField] Transform slots;
	[SerializeField] Text inventoryText; 

	public static string TBCC;
	//public InputField textBlock ;
	public GameObject textBlock ;

	// Use this for initialization
	void Start () {
		HasChanged ();
	}

	#region IHasChanged implementation

	public void HasChanged ()
	{
		System.Text.StringBuilder builder = new System.Text.StringBuilder ();
		//builder.Append (" - ");
		foreach (Transform slotTransform in slots) {
			GameObject item = slotTransform.GetComponent<Slot> ().item;
			if (item) {
				if (item.name.Contains ("for")) {
					builder.Append ("for");
				} else if (item.name.Contains ("print")) {
					builder.Append ("Debug.Log");
				} else if (item.name.Contains ("leftBracket")) {
					builder.Append ("(");
				} else if (item.name.Contains ("rightBracket")) {
					builder.Append (");");
				} else if (item.name.Contains ("CloseBracket")) {
					builder.Append (")");
				} else if (item.name.Contains ("textBlock")) {
					//builder.Append ("\\");
					builder.Append ("\"");
				} else if (item.name.Contains ("Says")) {
					//InputField TextBlock = item.GetComponent<InputField> ();
					InputField TextBlock = GameObject.Find ("Says(Clone)").GetComponent<InputField> ();
					builder.Append (TextBlock.textComponent.text);
					//builder.Append ("\'HelloWorld\'");
				} else if (item.name.Contains ("Ini")) {
					InputField TextBlock = GameObject.Find ("Ini(Clone)").GetComponent<InputField> ();
					builder.Append (TextBlock.textComponent.text);

				} else if (item.name.Contains ("Step")) {
					InputField TextBlock = GameObject.Find ("Step(Clone)").GetComponent<InputField> ();
					builder.Append (TextBlock.textComponent.text);

				}else if (item.name.Contains ("Cond")) {
					InputField TextBlock = GameObject.Find ("Cond(Clone)").GetComponent<InputField> ();
					builder.Append (TextBlock.textComponent.text);

				}else if (item.name.Contains ("semicolon")) {
					builder.Append (";");
				} else {
				}

			}
		}
		TBCC = builder.ToString ();
		inventoryText.text = builder.ToString ();
	}

	#endregion
}
	
	
namespace UnityEngine.EventSystems {
	public interface IHasChanged : IEventSystemHandler {
		void HasChanged ();	
	}
}