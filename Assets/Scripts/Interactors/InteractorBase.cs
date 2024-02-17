using System;
using UnityEngine;
using UnityEngine.Events;
using Interactables;
using ComponentHolderProtocol = Unity.VisualScripting.ComponentHolderProtocol;

namespace Interactors
{
    public abstract class InteractorBase : MonoBehaviour
    {
        [SerializeField] private InteractableBase _selectedInteractable;
        [field: SerializeField] public UnityEvent onSelectionChanged;
        private NullInteractable _nullInteractable;

        private void Awake()
        {
            _nullInteractable = new GameObject("NullInteractable").AddComponent<NullInteractable>();
            _selectedInteractable = _nullInteractable;
        }

        protected void UpdateSelection(GameObject newSelection)
        {
            if (_selectedInteractable.gameObject == newSelection) return;
            if (newSelection.TryGetComponent<InteractableBase>(out var newInteractable))
            {
                _selectedInteractable = newInteractable;
                Debug.Log($"Selected: {_selectedInteractable.name}");
                onSelectionChanged.Invoke();
            }
        }

        protected void CancelSelection()
        {
            Debug.Log("Selection cancelled");
            _selectedInteractable = _nullInteractable;
            onSelectionChanged.Invoke();
        }

        public void Interact()
        {
            _selectedInteractable.Interact();
        }
    }
}