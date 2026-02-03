using UnityEngine;

public interface I_PlayerInventory 
{
    bool HasKey(E_Keys key);
    void AddKey(E_Keys newKey);
}
