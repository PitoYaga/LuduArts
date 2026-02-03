using UnityEngine;

public interface IInteractable
{
    bool IsInteractable { get; }
    bool HoldInteract { get; }
    float HoldDuration { get; }
    bool MultipleUse { get; }
    string InteractionName { get; }
    string ItemName { get; }

    void OnInteraction(GameObject interactor);
}
