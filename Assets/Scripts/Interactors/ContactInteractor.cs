using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Interactors
{
    public class ContactInteractor:InteractorBase
    {
        private void OnTriggerEnter(Collider other)
        {
            UpdateSelection(other.GameObject());
        }

        private void OnTriggerExit(Collider other)
        {
            CancelSelection();
        }
    }
}