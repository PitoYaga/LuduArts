using UnityEngine;

public class Key_01 : MonoBehaviour, IInteractable
{
    [Header("Interaction Data")]
    public InstantInteractionSO m_InteractionData;


    #region Interface Values

    public bool IsInteractable => true;
    public bool HoldInteract => false;
    public float HoldDuration => 0;
    public bool MultipleUse => false;
    public string InteractionName => m_InteractionData.InteractionName;
    public string ItemName => m_InteractionData.ItemName;

    #endregion

   

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void OnInteraction(GameObject interactor)
    {
        Debug.Log("Key picked up");
        Destroy(gameObject);
    }

}
