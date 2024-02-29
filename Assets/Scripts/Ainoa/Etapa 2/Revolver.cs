using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Ainoa.Shoot
{
    public class Revolver : GrabItem
    {
        [SerializeField] private Transform _firePoint;

        public override void Interact(BaseInteractionEventArgs hover)
        {
            if (_attached) return;

            base.Interact(hover);

            var v = hover.interactorObject.transform.GetComponentInChildren<ShootManager>();

            if (v != null)
            {
                v.Attach(_firePoint);

                //set up well in hand.
                _attached = true;
            }
        }
    }
}