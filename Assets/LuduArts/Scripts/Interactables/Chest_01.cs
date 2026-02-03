using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Chest_01 : MonoBehaviour, IInteractable
{
    [Header("Interaction Data")]
    public HoldInteractionSO m_InteractionData;


    #region Interface Values

    bool IInteractable.IsInteractable { get => m_InteractionData.IsInteractable; set => m_InteractionData.IsInteractable = value; }
    public bool HoldInteract => true;
    public float HoldDuration => m_InteractionData.HoldDuration;
    public bool MultipleUse => false;
    public string InteractionName => m_InteractionData.InteractionName;
    public string ItemName => m_InteractionData.ItemName;

    #endregion

    [Header("Chest Animation Settings")]
    public Transform m_TopPivot;
    public float m_OpenAngle;
    public float m_OpenTime;
    Quaternion m_OpenRotation;


    [Header("Inventory Data")]
    public List<E_Keys> m_Keys = new List<E_Keys>();

    AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Setup target open rotation
    /// </summary>
    private void Start()
    {
        m_InteractionData.IsInteractable = true;
        m_OpenRotation = m_TopPivot.localRotation * Quaternion.Euler(-m_TopPivot.forward * m_OpenAngle);
    }



    public void OnInteraction(GameObject interactor)
    {
        if (m_InteractionData.IsInteractable)
        {
            var playerInventory = interactor.GetComponentInParent<I_PlayerInventory>();

            if (playerInventory != null)
            {
                StartCoroutine(OpenChest());
                for (int i = 0; i < m_Keys.Count; i++)
                {
                    Debug.Log(m_Keys[i]);
                    playerInventory.AddKey(m_Keys[i]);
                }

                m_InteractionData.IsInteractable = false;

                if (audioSource)
                {
                    audioSource.PlayOneShot(m_InteractionData.InteractionSound);
                }
                if (m_InteractionData.InteractionParticle)
                {
                    Instantiate(m_InteractionData.InteractionParticle, transform.position, Quaternion.identity);
                }
            }
            
        }
        
    }


    /// <summary>
    /// Chest open animation
    /// </summary>
    /// <returns></returns>
    private IEnumerator OpenChest()
    {
        while (Quaternion.Angle(m_TopPivot.localRotation, m_OpenRotation) > 0.1f)
        {
            m_TopPivot.localRotation = Quaternion.RotateTowards(m_TopPivot.localRotation, m_OpenRotation, Time.deltaTime * (m_OpenAngle / m_OpenTime));
            yield return null;
        }
        m_TopPivot.localRotation = m_OpenRotation;
    }
}
