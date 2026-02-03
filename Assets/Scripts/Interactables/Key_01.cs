using UnityEngine;

public class Key_01 : MonoBehaviour, IInteractable
{
    [Header("Interaction Data")]
    public InstantInteractionSO m_InteractionData;


    #region Interface Values

    bool IInteractable.IsInteractable { get => m_InteractionData.IsInteracaable; set => m_InteractionData.IsInteracaable = value; }
    public bool HoldInteract => false;
    public float HoldDuration => 0;
    public bool MultipleUse => false;
    public string InteractionName => m_InteractionData.InteractionName;
    public string ItemName => m_InteractionData.ItemName;

    #endregion

    public E_Keys m_KeyType;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void OnInteraction(GameObject interactor)
    {
        var playerInventory = interactor.GetComponentInParent<I_PlayerInventory>();

        if (playerInventory != null) 
        {
            playerInventory.AddKey(m_KeyType);
        }
        Destroy(gameObject);
    }

}
