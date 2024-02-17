using Unity.VisualScripting;
using UnityEngine;

namespace Interactors
{
    public class RaycastInteractor:InteractorBase
    {
        [SerializeField] private float maxDistance;
        private GameObject _lastSelection;

        private void Update()
        {
            SelectObjects();
        }
        
        private void SelectObjects()
        {
            var myTransform = transform;
            if (Physics.Raycast(transform.position, myTransform.forward, out RaycastHit hit, maxDistance)) 
            {
                    GameObject target = hit.transform.GameObject();
                    Debug.DrawRay(transform.position, myTransform.forward * hit.distance , Color.green);
                    if (target == _lastSelection) return;
                    _lastSelection = target;
                    UpdateSelection(target); 
            }
            else 
            {
                    Debug.DrawRay(transform.position, myTransform.forward * maxDistance , Color.red);
                    if (_lastSelection == null) return;
                    _lastSelection = null;
                    CancelSelection(); 
            }
        }
    }
}