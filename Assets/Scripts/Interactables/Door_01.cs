using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Door_01 : MonoBehaviour, IInteractable
{
    [Header("Interaction Data")]
    public ToggleInteractionSO m_InteractionData;


    #region Interface Values

    public bool IsInteractable => true;
    public bool HoldInteract => false;
    public float HoldDuration => 0;
    public bool MultipleUse => true;
    public string InteractionName => m_InteractionData.InteractionName;
    public string ItemName => m_InteractionData.ItemName;

    #endregion

    [Header("Door Settings")]
    public bool m_Locked;
    public Transform m_DoorPivot;
    public E_Keys m_CorrectKey;
    public float m_OpenAngle = 90;
    public float m_OpenTime = 2;

    bool m_IsOpen;
    private Quaternion m_ClosedRotation;
    private Quaternion m_OpenRotation;


    private void Awake()
    {
        if (m_DoorPivot == null)
        {
            m_DoorPivot = transform;
        }

        m_ClosedRotation = m_DoorPivot.localRotation;
        m_OpenRotation = Quaternion.Euler(m_DoorPivot.localEulerAngles + Vector3.up * m_OpenAngle);
    }



    void Start()
    {
        m_InteractionData.InteractionName = m_IsOpen ? "Close" : "Open";
    }


    void Update()
    {

    }

    public void OnInteraction(GameObject interactor)
    {
        var playerInventory = interactor.GetComponentInParent<I_PlayerInventory>();

        if (playerInventory != null)
        {
            if (m_Locked)
            {
                if (playerInventory.HasKey(m_CorrectKey))
                {
                    m_Locked = false;
                    ToggleDoor();
                }
                else
                {
                    playerInventory.ShowWarningText("Locked! Missing Key!");
                    return;
                }
            }
            else
            {
                ToggleDoor();
            }
           
        }
       
    }


    private void ToggleDoor()
    {
        m_IsOpen = !m_IsOpen;
        StopAllCoroutines();
        StartCoroutine(RotateDoor());
        m_InteractionData.InteractionName = m_IsOpen ? "Close" : "Open";
    }


    private IEnumerator RotateDoor()
    {
        Quaternion targetRotation = m_IsOpen ? m_OpenRotation : m_ClosedRotation;

        while (Quaternion.Angle(m_DoorPivot.localRotation, targetRotation) > 0.1f)
        {
            m_DoorPivot.localRotation = Quaternion.RotateTowards(m_DoorPivot.localRotation, targetRotation, Time.deltaTime * (m_OpenAngle / m_OpenTime));
            yield return null;
        }

        m_DoorPivot.localRotation = targetRotation;
    }
}
