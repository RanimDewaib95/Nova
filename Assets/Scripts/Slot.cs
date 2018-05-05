using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Slot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    public int clicksCount = 0;
    List<string> moveBlocks = new List<string>();
    List<string> procedureBlocks = new List<string>();

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Planet1-Level 1" || SceneManager.GetActiveScene().name == "Planet2 - Level Dummy")
        {
            moveBlocks.Add("move2");
            moveBlocks.Add("move3");
            moveBlocks.Add("move4");
            moveBlocks.Add("move5");
            moveBlocks.Add("move1");
        }
        if (SceneManager.GetActiveScene().name == "Planet2 - Level Dummy")
        {
            procedureBlocks.Add("p12");
            procedureBlocks.Add("p13");
            procedureBlocks.Add("p14");
            procedureBlocks.Add("p15");
            procedureBlocks.Add("p11");
        }
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

        if (DragHandler.draggedBlock.transform.parent.name == "DeleteSlot")
        {
            Destroy(DragHandler.draggedBlock);
        }
    }
    #endregion

    public void OnPointerClick(PointerEventData eventData)
    {
        if (SceneManager.GetActiveScene().name != "Spacestation-Level1")
        {
            string nameOfClickedBlock;
           
            if (transform.childCount > 0)
            {
                nameOfClickedBlock = transform.GetChild(0).name;

                switch (nameOfClickedBlock)
                {
                    case "moveBlock(Clone)":
                        if (clicksCount < 5)
                        {
                            displaySprite(nameOfClickedBlock);
                            clicksCount++;

                            Debug.Log(clicksCount);
                        }
                        else
                        {
                            clicksCount = 0;
                            displaySprite(nameOfClickedBlock);
                        }
                        break;

                    case "procedureBlock(Clone)":
                        if (clicksCount < 5)
                        {
                            displaySprite(nameOfClickedBlock);
                            clicksCount++;
                            Debug.Log(clicksCount);
                        }
                        else
                        {
                            clicksCount = 1;
                            displaySprite(nameOfClickedBlock);
                        }
                        break;
                }
            }
        }
        else
        {
            Debug.Log("In level 1");
        }
    }

    public void displaySprite(string nameOfClickedBlock)
    {
        Sprite nextSprite;
        List<string> sprites = new List<string>();

        switch (nameOfClickedBlock)
        {
            case "moveBlock(Clone)":
                sprites = moveBlocks;
                break;

            case "procedureBlock(Clone)":
                sprites = procedureBlocks;
                break;
        }

        nextSprite = Resources.Load<Sprite>(sprites[clicksCount]);
        transform.GetChild(0).GetComponent<Image>().sprite = nextSprite;
    }
}
