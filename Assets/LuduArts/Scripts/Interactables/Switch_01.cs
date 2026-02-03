using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Switch_01 : MonoBehaviour, IInteractable
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

    [Header("Lever Settings")]
    [SerializeField] Transform m_LeverPivot;
    [SerializeField] float m_LeverAngle;
    [SerializeField] float m_MoveTime;

    [Header("Activate Events")]
    [SerializeField] private UnityEvent m_OnSwitchPressed;

    bool m_IsInteracted;
    private Quaternion m_InitialRotation;
    private Quaternion m_MinRotation;
    private Quaternion m_MaxRotation;

    AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        m_InitialRotation = m_LeverPivot.localRotation;


        m_MinRotation = Quaternion.Euler(-m_LeverAngle, 0f, 0f);
        m_MaxRotation = Quaternion.Euler(m_LeverAngle, 0f, 0f);
    }


    public void OnInteraction(GameObject interactor)
    {
        if (m_InteractionData.IsInteractable)
        {
            m_OnSwitchPressed.Invoke();

            StopAllCoroutines();
            StartCoroutine(RotateLever());

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


    private IEnumerator RotateLever()
    {
        float elapsedTime = 0f;
        Quaternion startRotation = m_LeverPivot.localRotation;
        Quaternion targetRotation = m_IsInteracted ? m_MinRotation : m_MaxRotation;
        m_IsInteracted = !m_IsInteracted;

        while (elapsedTime < m_MoveTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / m_MoveTime;

            m_LeverPivot.localRotation = Quaternion.Lerp(startRotation, targetRotation, t);

            yield return null;
        }

        m_LeverPivot.localRotation = targetRotation;
    }

}
