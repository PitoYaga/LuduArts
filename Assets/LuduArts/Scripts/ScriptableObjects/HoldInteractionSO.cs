using UnityEngine;

public abstract class HoldInteractionSO : ScriptableObject
{
    public bool IsInteractable;
    public string ItemName;
    public string InteractionName;
    public float HoldDuration;
    public AudioClip InteractionSound;
    public ParticleSystem InteractionParticle;
}
