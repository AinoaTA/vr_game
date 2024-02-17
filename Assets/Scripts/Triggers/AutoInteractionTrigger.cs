using System;
using Interactables;
using Interactors;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Triggers
{
    public class AutoInteractionTrigger : MonoBehaviour
    {
        [SerializeField] private InteractorBase interactor;
        private void OnEnable()
        {
            AutoExecuteInteractionOnNewSelection();
        }

        private void OnDisable()
        {
            CancelAutoExecution();
        }

        private void AutoExecuteInteractionOnNewSelection()
        {
            if (interactor != null || TryGetComponent(out interactor))
            {
                interactor.onSelectionChanged.AddListener(interactor.Interact);
            }
            Debug.LogError("Interactor not found");
        }

        private void CancelAutoExecution()
        {
            interactor.onSelectionChanged.RemoveListener(interactor.Interact);
        }
    }
}