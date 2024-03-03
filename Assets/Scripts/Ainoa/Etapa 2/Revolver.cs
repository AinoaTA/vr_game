using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Ainoa.Shoot
{
    public class Revolver : GrabItem
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private Vector3 _localPos;

        protected override void Awake() { }
        public override void Interact(BaseInteractionEventArgs hover)
        {
            if (_attached) return;


            //base.Interact(hover);
            Debug.Log("selecting");
            var v = hover.interactorObject.transform.parent.GetComponentInChildren<ShootManager>();

            if (v != null)
            {
                v.Attach(_firePoint);
                _attached = true;
            }
        }

        public override void ResetAction(BaseInteractionEventArgs hover)
        {
            base.ResetAction(hover);

            _attached = false;
        }
    }
}