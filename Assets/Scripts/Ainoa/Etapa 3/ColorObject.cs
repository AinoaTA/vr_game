using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Ainoa.Scene3
{
    public class ColorObject : GrabItem
    {
        public Basket.Color ColorReference => _color;

        [SerializeField] private Basket.Color _color;

        private Collider _col;
        private Rigidbody _rb;

        protected override void Awake()
        {
            //base.Awake();
            TryGetComponent(out _rb);
            TryGetComponent(out _col);

            _initPos = transform.position;
        }

        public override void Interact(BaseInteractionEventArgs hover)
        {
            //_pointToFollow = hover.
            base.Interact(hover);

            if (_attached) return;
            _attached = true;
            _col.enabled = false;
            //_rb.isKinematic = true;
        }

        public override void ResetAction(BaseInteractionEventArgs hover)
        {
            //if (hover.interactorObject is XRDirectInteractor)
            //{
            _col.enabled = true;
            //_rb.isKinematic = false;
            _attached = false;
            //}
        }

        private void OnCollisionStay(Collision collision)
        {
            if (_attached) return;

            if (collision.collider.CompareTag("Interactable"))
            {
                var b = collision.gameObject.GetComponent<Basket>();
                if (b != null && b.ColorReference == ColorReference)
                {
                    b.Add(gameObject);
                    _rb.isKinematic = true;
                    _col.enabled = false;
                }
                else
                {
                    transform.position = _initPos;
                }
            }
        }
    }
}