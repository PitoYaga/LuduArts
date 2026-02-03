using TMPro;
using UnityEngine;

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

    public void AddItemToInventory(string itemName)
    {
        TextMeshProUGUI spawnedSlot = Instantiate(m_InventorySlot, m_InventoryGrid);
        spawnedSlot.SetText(itemName);
    }

    public void ToggleInventory()
    {
        m_InventoryOpen = !m_InventoryOpen;
        m_inventoryWindow.SetActive(m_InventoryOpen);
    }
}
