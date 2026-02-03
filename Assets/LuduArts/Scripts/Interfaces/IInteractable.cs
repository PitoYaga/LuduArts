using UnityEngine;

public interface IInteractable
{
    bool IsInteractable { get; set; }
    bool HoldInteract { get; }
    float HoldDuration { get; }
    bool MultipleUse { get; }
    string InteractionName { get; }
    string ItemName { get; }

    void OnInteraction(GameObject interactor);
}
