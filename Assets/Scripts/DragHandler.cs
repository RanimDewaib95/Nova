using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public GameObject block; //the original block
	public static GameObject leftBracket;
	public static GameObject draggedBlock; //The block that is currently being dragged
    //public static Queue<string> chosenBlocks = new Queue<string>(); //list that contains the blocks that the player has chosen

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
            Debug.Log(Quaternion.identity);
			draggedBlock = Instantiate (block, startPosition, Quaternion.identity, block.transform.parent );
            draggedBlock.transform.Rotate(0f, 45f, 0f);
            
            //draggedBlock.transform.rotation = Quaternion.Euler(0, 0, 0);
            //Quaternion rot = new Quaternion();
            //rot.eulerAngles = new Vector3 (0, 0, 0);
            //draggedBlock.transform.rotation = rot;
            //draggedBlock.transform.Rotate(Vector3.right * Time.deltaTime);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 45, 0), Time.deltaTime);
			//draggedBlock.GetComponent<DragHandler>().
		}
			
		draggedBlock.GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{

		//draggedBlock.transform.position = Input.mousePosition; //drag the cloned object

        mousePos = Input.mousePosition;
        mousePos.z = 50.0f;
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
            //draggedBlock.transform.Rotate(20.705f, 49.107f, -22.208f);
            

            //chosenBlocks.Enqueue(draggedBlock.name); //add name of dragged block to the list of chosen blocks
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
