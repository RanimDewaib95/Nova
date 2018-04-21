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
		
	#region IDropHandler implementation
	public void OnDrop (PointerEventData eventData)
	{
		CodePanelTransform = transform.parent;

		if (!item) {
			DragHandler.draggedBlock.transform.SetParent (transform);
			ExecuteEvents.ExecuteHierarchy<IHasChanged> (gameObject, null, (x, y) => x.HasChanged ());
		}
	}
    #endregion

}
