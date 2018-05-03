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
            moveBlocks.Add("move1");
            moveBlocks.Add("move2");
            moveBlocks.Add("move3");
            moveBlocks.Add("move4");
            moveBlocks.Add("move5");
        }
        if (SceneManager.GetActiveScene().name == "Planet2 - Level Dummy")
        {
            procedureBlocks.Add("p11");
            procedureBlocks.Add("p12");
            procedureBlocks.Add("p13");
            procedureBlocks.Add("p14");
            procedureBlocks.Add("p15");
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
    }
    #endregion

    public void OnPointerClick(PointerEventData eventData)
    {
        if (SceneManager.GetActiveScene().name != "Spacestation-Level1")
        {
            string nameOfClickedBlock;
            Sprite nextSprite;

            if (transform.childCount > 0)
            {
                nameOfClickedBlock = transform.GetChild(0).name;

                switch (nameOfClickedBlock)
                {
                    case "moveBlock(Clone)":
                        if (clicksCount < 5)
                        {
                            nextSprite = Resources.Load<Sprite>(moveBlocks[clicksCount]);
                            transform.GetChild(0).GetComponent<Image>().sprite = nextSprite;
                            clicksCount++;
                            Debug.Log(clicksCount);
                        }
                        else
                        {
                            clicksCount = 1;
                            nextSprite = Resources.Load<Sprite>(moveBlocks[clicksCount]);
                            transform.GetChild(0).GetComponent<Image>().sprite = nextSprite;
                        }
                        break;

                    case "procedureBlock(Clone)":
                        if (clicksCount < 5)
                        {
                            nextSprite = Resources.Load<Sprite>(procedureBlocks[clicksCount]);
                            transform.GetChild(0).GetComponent<Image>().sprite = nextSprite;
                            clicksCount++;
                            Debug.Log(clicksCount);
                        }
                        else
                        {
                            clicksCount = 1;
                            nextSprite = Resources.Load<Sprite>(procedureBlocks[clicksCount]);
                            transform.GetChild(0).GetComponent<Image>().sprite = nextSprite;
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
}
