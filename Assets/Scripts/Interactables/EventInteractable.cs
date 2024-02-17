using UnityEngine;
using UnityEngine.Events;

namespace Interactables
{
    public class EventInteractable : InteractableBase
    {
        [field: SerializeField] public UnityEvent OnInteraction { get; private set; }
        public override void Interact()
        {
            OnInteraction.Invoke();
        }
    }
}