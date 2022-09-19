using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveUI : MonoBehaviour
{
    private MoveInventory inventory;
    [SerializeField] private Transform uiMove;
    [SerializeField] private Transform moveTemplate;
    [SerializeField] private Image image;

    public void SetMoveInventory(MoveInventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryMove();
    }

    private void Inventory_OnItemListChanged(object sander, System.EventArgs e)
    {
        RefreshInventoryMove();
    }

    // MoveTemplate(Clone) , name of gameobject clone
    private void RefreshInventoryMove()
    {
        foreach (Transform child in uiMove)
        {
            if (child == moveTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float moveSlotCellSize = 0f; //90

        foreach (Move move in inventory.GetMoveList())
        {
            image.sprite = move.GetSprite();

            RectTransform moveSlotRectTransform = Instantiate(moveTemplate, uiMove).GetComponent<RectTransform>();
            moveSlotRectTransform.gameObject.SetActive(true);

            moveSlotRectTransform.anchoredPosition = new Vector2(x * moveSlotCellSize, y * moveSlotCellSize);
            // Image image = moveSlotRectTransform.Find("image").GetComponent<Image>();

            x++;
        }
    }

    public void RemoveMove()
    {
        foreach (Transform child in uiMove)
        {
            if (child.name == moveTemplate.name) continue;
            Destroy(child.gameObject);
            inventory.RemoveMove();
        }
    }
}
