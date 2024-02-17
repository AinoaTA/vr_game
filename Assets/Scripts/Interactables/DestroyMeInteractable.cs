using UnityEngine;

namespace Interactables
{
    public class DestroyMeInteractable : InteractableBase
    {
        public override void Interact()
        {
            Destroy(gameObject);
        }
    }
}