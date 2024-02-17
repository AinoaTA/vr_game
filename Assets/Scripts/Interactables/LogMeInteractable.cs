using UnityEngine;

namespace Interactables
{
    public class LogMeInteractable : InteractableBase
    {
        public override void Interact()
        {
            Debug.Log($"Log this interaction!");
        }
    }
}