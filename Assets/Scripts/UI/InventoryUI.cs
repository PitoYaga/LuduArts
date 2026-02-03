using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject m_inventoryWindow;
    public TextMeshProUGUI m_InventorySlot;
    public Transform m_InventoryGrid;

    bool m_InventoryOpen;


    private void Start()
    {
        m_InventoryOpen = false;
        m_inventoryWindow.SetActive(false);
    }


    /// <summary>
    /// Add item to inventory. It is an text based inventory so item name is enough for now.
    /// </summary>
    /// <param name="itemName"></param>
    public void AddItemToInventory(string itemName)
    {
        TextMeshProUGUI spawnedSlot = Instantiate(m_InventorySlot, m_InventoryGrid);
        spawnedSlot.SetText(itemName);
        LayoutRebuilder.ForceRebuildLayoutImmediate(m_InventoryGrid as RectTransform);
    }


    /// <summary>
    /// Open/Close Inventory Window
    /// </summary>
    public void ToggleInventory()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(m_InventoryGrid as RectTransform);
        m_InventoryOpen = !m_InventoryOpen;
        m_inventoryWindow.SetActive(m_InventoryOpen);
    }
}
