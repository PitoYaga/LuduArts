using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInput m_PlayerInput;
    InputAction m_MoveAction;
    InputAction m_CameraAction;
    InputAction m_InteractionAction;
    InputAction m_InventoryAction;

    [Header("Movement Settings")]
    public float m_WalkSpeed = 5;

    [Header("Camera Settings")]
    public Camera m_Camera;
    public float m_LookSensivity = 5;
    public float m_CameraClamp = 70;
    float verticalRotation;

    [Header("Interaction Settings")]
    public float m_InteractionDistance = 150;

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

        m_InventoryAction.started += ToggleInventory;
    }


    void Start()
    {
        interactionUI = GameObject.FindGameObjectWithTag("HUD").GetComponent<InteractionUI>();
    }


    private void Update()
    {
        MovePlayer();
        CameraMovement();
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
        InteractionRay();
    }

    void OnInteractionCanceled(InputAction.CallbackContext ctx) // Interaction input released
    {
        
    }

    #endregion


    #region Inventory Key


    void ToggleInventory(InputAction.CallbackContext ctx)
    {
        Debug.Log("Toggle Inventory");
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
                interactable.OnInteraction(gameObject);
            }

        }
    }

    #endregion
}
