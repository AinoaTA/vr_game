using UnityEngine;

namespace Interactables
{
    public class NullInteractable : InteractableBase
    {
        public override void Interact()
        {
            Debug.Log("Null interactor, i do nothing");
        }
    }
}