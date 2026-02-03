using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Audio;

public class Door_01 : MonoBehaviour, IInteractable
{
    [Header("Interaction Data")]
    public ToggleInteractionSO m_InteractionData;


    #region Interface Values

    bool IInteractable.IsInteractable { get => m_InteractionData.IsInteractable; set => m_InteractionData.IsInteractable = value; }
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

    AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

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
        if (audioSource)
        {
            audioSource.PlayOneShot(m_InteractionData.InteractionSound);
        }
        if (m_InteractionData.InteractionParticle)
        {
            Instantiate(m_InteractionData.InteractionParticle, transform.position, Quaternion.identity);
        }
    }


    /// <summary>
    /// Door open and close animation
    /// </summary>
    /// <returns></returns>
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
