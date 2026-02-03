using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, I_PlayerInventory
{
    [HideInInspector] public InventoryUI m_inventoryUI;

    #region Inventory Data

    public List<E_Keys> keys = new List<E_Keys>();

    #endregion 

    #region Interface Values

    public bool HasKey(E_Keys key)
    {
        return keys.Contains(key);
    }

    #endregion

    #region Interface Functions

    public void AddKey(E_Keys key)
    {
        if (!keys.Contains(key))
        {
            keys.Add(key);
            string keyName = key.ToString();
            m_inventoryUI.AddItemToInventory(keyName);
        }
    }

    #endregion

    private void Start()
    {
        m_inventoryUI = GameObject.FindGameObjectWithTag("HUD").GetComponent<InventoryUI>();
    }

}
