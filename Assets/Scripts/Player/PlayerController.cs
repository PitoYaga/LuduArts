using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Input Data

    PlayerInput m_PlayerInput;
    InputAction m_MoveAction;
    InputAction m_CameraAction;
    public InputAction m_InteractionAction;
    public InputActionReference m_InventoryAction;

    #endregion


    [Header("Movement Settings")]
    public float m_WalkSpeed = 5;

    [Header("Camera Settings")]
    public Camera m_Camera;
    public float m_LookSensivity = 5;
    public float m_CameraClamp = 70;
    float verticalRotation;

    [Header("Interaction Settings")]
    public float m_InteractionDistance = 150;
    bool m_HoldingKey;
    float m_InputHoldTime;

    InteractionUI interactionUI;


    #region Setup Inputs
    /// <summary>
    /// Enable Inputs
    /// </summary>
    private void OnEnable()
    {
        m_InventoryAction.action.Enable();
        m_InventoryAction.action.started += ToggleInventory;
    }


    /// <summary>
    /// Disable Inputs
    /// </summary>
    private void OnDisable()
    {
        m_InventoryAction.action.started -= ToggleInventory;
        m_InventoryAction.action.Disable();
    }
    #endregion


    #region BeginPlay Setup

    private void Awake()
    {
        m_PlayerInput = GetComponent<PlayerInput>();
        m_MoveAction = m_PlayerInput.actions.FindAction("Movement");
        m_CameraAction = m_PlayerInput.actions.FindAction("Camera");
        m_InteractionAction = m_PlayerInput.actions.FindAction("Interaction");

        m_InteractionAction.started += OnInteractStarted;
        m_InteractionAction.canceled += OnInteractCanceled;
    }



    void Start()
    {
        interactionUI = GameObject.FindGameObjectWithTag("HUD").GetComponent<InteractionUI>();
    }
    #endregion


    private void Update()
    {
        MovePlayer();
        CameraMovement();
        InteractionRay();
    }


    #region Player Movement Controls

    /// <summary>
    /// Move the player with WASD
    /// </summary>
    void MovePlayer()
    {
        Vector2 direction = m_MoveAction.ReadValue<Vector2>();
        Vector3 move = transform.forward * direction.y + transform.right * direction.x;

        transform.position += move * Time.deltaTime * m_WalkSpeed;
    } 

    /// <summary>
    /// Player can look arond with mouse
    /// </summary>
    void CameraMovement()
    {
        Vector2 lookDirection = m_CameraAction.ReadValue<Vector2>();

        verticalRotation -= lookDirection.y * m_LookSensivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -m_CameraClamp, m_CameraClamp);
        m_Camera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);


        transform.Rotate(0, lookDirection.x * m_LookSensivity, 0);
        m_Camera.transform.Rotate(lookDirection.y * m_LookSensivity, 0, 0);
    }

    #endregion


    #region Interaction Key

  
    private void OnInteractStarted(InputAction.CallbackContext context) // Interaction key pressed
    {
        m_InputHoldTime = 0;
        m_HoldingKey = true;
        
    }

    private void OnInteractCanceled(InputAction.CallbackContext context) // Interaction key released
    {
        m_InputHoldTime = 0;
        m_HoldingKey = false;
        interactionUI.UpdateHoldingBar(0);
    }

    #endregion


    #region Inventory Key

    /// <summary>
    /// Open and close inventory
    /// </summary>
    private void ToggleInventory(InputAction.CallbackContext context)
    {
        InventoryUI inventoryUI = gameObject.GetComponent<PlayerInventory>().m_inventoryUI;
        inventoryUI.ToggleInventory();
    }


    #endregion


    #region Interaction Ray Settings

    void InteractionRay()
    {
        //Debug.Log("Interaction Ray");
        Ray ray = new Ray(m_Camera.transform.position, m_Camera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, m_InteractionDistance))
        {
            var interactable = hit.collider.GetComponentInParent<IInteractable>();

            if (interactable != null)
            {
                if(interactable.IsInteractable)
                {
                    interactionUI.UpdateInteractionText(interactable.InteractionName, interactable.ItemName);

                    if (m_HoldingKey)
                    {
                        if (interactable.HoldInteract)
                        {
                            m_InputHoldTime += Time.deltaTime;
                            interactionUI.UpdateHoldingBar(m_InputHoldTime / interactable.HoldDuration);

                            if (m_InputHoldTime >= interactable.HoldDuration)
                            {
                                m_InputHoldTime = 0;
                                m_HoldingKey = false;
                                interactionUI.UpdateHoldingBar(0);
                                interactable.OnInteraction(gameObject);
                            }
                        }
                        else
                        {
                            m_HoldingKey = false;
                            interactable.OnInteraction(gameObject);
                        }
                    }
                }
                else
                {
                    m_InputHoldTime = 0;
                    interactionUI.ResetInteractionWindow();
                }
            }
            else // Close interaction widget if hit item has no interface.
            {
                m_InputHoldTime = 0;
                interactionUI.ResetInteractionWindow();
            }
        }
        else // Close interaction window if there is no hit item.
        {
            m_InputHoldTime = 0;
            interactionUI.ResetInteractionWindow();
        }

        //Debug.DrawRay(ray.origin, ray.direction * m_InteractionDistance, Color.green);


    }

    #endregion
}
