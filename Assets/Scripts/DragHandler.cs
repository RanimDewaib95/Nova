﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public GameObject block; //the original block
	public static GameObject leftBracket;
	public static GameObject draggedBlock; //The block that is currently being dragged
    public static Queue<string> chosenBlocks = new Queue<string>(); //list that contains the blocks that the player has chosen

    Vector3 startPosition; //start position of the block
	Transform startParent; //start parent of  the parent
    Vector3 mousePos;
    Vector3 worldPos;

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		block = gameObject;
		startPosition = block.transform.position;
		startParent = block.transform.parent;

		if (block.name.Contains ("(Clone)")) {
			draggedBlock = block;
		} 
		else {
			draggedBlock = gameObject;
			draggedBlock = Instantiate (block, startPosition, Quaternion.identity, block.transform.parent );
			//draggedBlock.GetComponent<DragHandler>().
		}
			
		draggedBlock.GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
        mousePos = Input.mousePosition;
        mousePos.z = 2.0f;
        worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        draggedBlock.transform.position = worldPos;
        
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		//Debug.Log (draggedBlock.name);
		if (draggedBlock.name.Contains ("(Clone)")) {
			draggedBlock.transform.position = startPosition;
            //Debug.Log (draggedBlock.transform.position);
            chosenBlocks.Enqueue(draggedBlock.name); //add name of dragged block to the list of chosen blocks
        } 
		else {
			if (draggedBlock.transform.parent == startParent) {
				//Debug.Log ("destroy elmfrod"); ///////////////////////////////////ISSUE/////////////////////////
				Destroy (draggedBlock);
			}
		}
		//leftBracket = Resources.Load ("zBlock") as GameObject;
		//draggedBlock = null;
		draggedBlock.GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}
	#endregion

}
