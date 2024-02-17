using System;
using Interactors;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Triggers
{
    public class InteractorActionTrigger : MonoBehaviour
    {
        [SerializeField] private InteractorBase interactor;
        [SerializeField] private InputActionReference inputAction;

        private void OnEnable()
        {
            inputAction.action.performed += RunInteraction;
        }

        private void OnDisable()
        {
            inputAction.action.performed -= RunInteraction;
        }

        private void RunInteraction(InputAction.CallbackContext callbackContext)
        {
            interactor.Interact();
        }
    }
}