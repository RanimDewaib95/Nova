using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler {

	public GameObject item {
		get {
			if (transform.childCount > 0) {
				Debug.Log (transform.childCount);
			return transform.GetChild (0).gameObject;
			}
			return null;
		}
	}

	Transform CodePanelTransform;
	string textcolNum;
	string textslotName;

		
	#region IDropHandler implementation
	public void OnDrop (PointerEventData eventData)
	{
		CodePanelTransform = transform.parent;

		if (!item) {
			DragHandler.draggedBlock.transform.SetParent (transform);

			//---------------------FOR CASE----------------------------------------------//
			if (transform.GetChild (0).gameObject.name.Contains ("forBlock")) {
				string subSlotName = transform.name.Substring(0,6); //Slot01

				textcolNum = (int.Parse (transform.name.Substring (6, 2)) + 1).ToString(); 
				textslotName = subSlotName + "0" + textcolNum;
				GameObject leftBracket = Instantiate (Resources.Load ("leftBracket"), CodePanelTransform.Find(textslotName)) as GameObject;

				textcolNum = (int.Parse (transform.name.Substring (6, 2)) + 2).ToString(); 
				textslotName = subSlotName + "0" + textcolNum;
				GameObject textSlot = Instantiate (Resources.Load ("Ini"), CodePanelTransform.Find(textslotName)) as GameObject;


				textcolNum = (int.Parse (transform.name.Substring (6, 2)) + 3).ToString(); 
				textslotName = subSlotName + "0" + textcolNum;
				textSlot = Instantiate (Resources.Load ("semicolon"), CodePanelTransform.Find(textslotName)) as GameObject;

				textcolNum = (int.Parse (transform.name.Substring (6, 2)) + 4).ToString(); 
				textslotName = subSlotName + "0" + textcolNum;
				textSlot = Instantiate (Resources.Load ("Cond"), CodePanelTransform.Find(textslotName)) as GameObject;

				textcolNum = (int.Parse (transform.name.Substring (6, 2)) + 5).ToString(); 
				textslotName = subSlotName + "0" + textcolNum;
				textSlot = Instantiate (Resources.Load ("semicolon"), CodePanelTransform.Find(textslotName)) as GameObject;

				textcolNum = (int.Parse (transform.name.Substring (6, 2)) + 6).ToString(); 
				textslotName = subSlotName + "0" + textcolNum;
				textSlot = Instantiate (Resources.Load ("Step"), CodePanelTransform.Find(textslotName)) as GameObject;

				string rightcolNum = (int.Parse (transform.name.Substring (6, 2)) + 7).ToString(); 
				string rightslotName = subSlotName + "0" + rightcolNum;
				GameObject rightBracket = Instantiate (Resources.Load ("CloseBracket"), CodePanelTransform.Find(rightslotName)) as GameObject;


//				subSlotName = transform.name.Substring(0,4); //Slot
//				//string textcolname = transform.name.Substring(4,2);
//
//				string textRowNum = (int.Parse (transform.name.Substring (4, 2)) + 1).ToString(); 
//				textslotName = subSlotName + "0" + textRowNum + "01";
//				GameObject leftPara = Instantiate (Resources.Load ("leftPara"), CodePanelTransform.Find(textslotName)) as GameObject;
//
//				textRowNum = (int.Parse (transform.name.Substring (4, 2)) + 3).ToString(); 
//				textslotName = subSlotName + "0" + textRowNum + "01";
//				GameObject rightPara = Instantiate (Resources.Load ("rightPara"), CodePanelTransform.Find(textslotName)) as GameObject;
			}

			//-----------------------PRINT CASE----------------------//
			if (transform.GetChild (0).gameObject.name.Contains ("printBlock")) {
				string subSlotName = transform.name.Substring(0,6); //Slot01

				string leftcolNum = (int.Parse (transform.name.Substring (6, 2)) + 1).ToString(); 
				string leftslotName = subSlotName + "0" + leftcolNum;
				GameObject leftBracket = Instantiate (Resources.Load ("leftBracket"), CodePanelTransform.Find(leftslotName)) as GameObject;

				string rightcolNum = (int.Parse (transform.name.Substring (6, 2)) + 5).ToString(); 
				string rightslotName = subSlotName + "0" + rightcolNum;
				GameObject rightBracket = Instantiate (Resources.Load ("rightBracket"), CodePanelTransform.Find(rightslotName)) as GameObject;


			}

			//---------------------TEXT CASE------------------------------------//
			if (transform.GetChild (0).gameObject.name.Contains ("textBlock")) {
				string subSlotName = transform.name.Substring(0,6); //Slot01

				string textcolNum = (int.Parse (transform.name.Substring (6, 2)) + 1).ToString(); 
				string textslotName = subSlotName + "0" + textcolNum;
				//GameObject textSlot = Instantiate (Resources.Load ("RanimSays"), CodePanelTransform.Find(textslotName)) as GameObject;
				GameObject textSlot = Instantiate (Resources.Load ("Says"), CodePanelTransform.Find(textslotName)) as GameObject;


				string rightcolNum = (int.Parse (transform.name.Substring (6, 2)) + 2).ToString(); 
				string rightslotName = subSlotName + "0" + rightcolNum;
				GameObject rightBracket = Instantiate (Resources.Load ("textBlock"), CodePanelTransform.Find(rightslotName)) as GameObject;
			}


			ExecuteEvents.ExecuteHierarchy<IHasChanged> (gameObject, null, (x, y) => x.HasChanged ());
		}
	}
	#endregion

}

//string colNum = transform.name.Substring (6, 2);

//GameObject rightBracket = Instantiate (Resources.Load ("rightBracket"), CodePanelTransform.Find("Slot" + +)) as GameObject;
//GameObject leftBracket = Instantiate (Resources.Load ("leftBracket"), transform) as GameObject;