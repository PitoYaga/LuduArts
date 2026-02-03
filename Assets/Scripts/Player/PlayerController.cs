using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Input Data
    PlayerInput m_PlayerInput;
    InputAction m_MoveAction;
    InputAction m_CameraAction;
    InputAction m_InteractionAction;
    InputAction m_InventoryAction;
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


    private void Awake()
    {
        m_PlayerInput = GetComponent<PlayerInput>();
        m_MoveAction = m_PlayerInput.actions.FindAction("Movement");
        m_CameraAction = m_PlayerInput.actions.FindAction("Camera");
        m_InteractionAction = m_PlayerInput.actions.FindAction("Interaction");
        m_InventoryAction = m_PlayerInput.actions.FindAction("ToggleInventory");

        m_InteractionAction.started += OnInteractionStarted;
        m_InteractionAction.canceled += OnInteractionCanceled;
    }


    void Start()
    {
        interactionUI = GameObject.FindGameObjectWithTag("HUD").GetComponent<InteractionUI>();
    }


    private void Update()
    {
        MovePlayer();
        CameraMovement();
        InteractionRay();
        ToggleInventory();
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

    void OnInteractionStarted(InputAction.CallbackContext ctx) // Interaction input pressed
    {
        m_InputHoldTime = 0;
        m_HoldingKey = true;
    }

    void OnInteractionCanceled(InputAction.CallbackContext ctx) // Interaction input released
    {
        m_InputHoldTime = 0;
        m_HoldingKey = false;
        interactionUI.UpdateHoldingBar(0);
    }

    #endregion


    #region Inventory Key


    void ToggleInventory()
    {
        if (m_InventoryAction.triggered)
        {
            InventoryUI inventoryUI = gameObject.GetComponent<PlayerInventory>().m_inventoryUI;
            inventoryUI.ToggleInventory();
        }
        
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
                    else
                    {
                        interactionUI.UpdateInteractionText(interactable.InteractionName, interactable.ItemName);
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
    }

    #endregion
}
