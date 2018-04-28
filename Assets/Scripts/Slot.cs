using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    public int clicksCount = 0;
    List<string> moveBlocks = new List<string>();

    private void Start()
    {
        moveBlocks.Add("move1");
        moveBlocks.Add("move2");
        moveBlocks.Add("move3");
        moveBlocks.Add("move4");
        moveBlocks.Add("move5");
    }

    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                //Debug.Log (transform.childCount);
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    Transform CodePanelTransform;

    #region IDropHandler implementation
    public void OnDrop(PointerEventData eventData)
    {
        CodePanelTransform = transform.parent;

        if (!item)
        {
            DragHandler.draggedBlock.transform.SetParent(transform);
            ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
        }
    }
    #endregion

    public void OnPointerClick(PointerEventData eventData)
    {
        string nameOfClickedBlock;
        Sprite nextSprite;

        if (transform.childCount > 0)
        {
            nameOfClickedBlock = transform.GetChild(0).name;
 
            switch (nameOfClickedBlock)
            {
                case "moveBlock(Clone)":
                    if (clicksCount < 5 )
                    {
                        nextSprite = Resources.Load<Sprite>(moveBlocks[clicksCount]);
                        transform.GetChild(0).GetComponent<Image>().sprite = nextSprite;
                        clicksCount++;
                        Debug.Log(clicksCount);
                    }
                    else
                    {
                        clicksCount = 0;
                        nextSprite = Resources.Load<Sprite>(moveBlocks[clicksCount]);
                        transform.GetChild(0).GetComponent<Image>().sprite = nextSprite;
                    }
                    break;
            }       
        }
    }
}
