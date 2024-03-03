using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Ainoa.Scene3
{
    public class ColorObject : GrabItem
    {
        public Basket.Color ColorReference => _color;

        [SerializeField] private Basket.Color _color;
        [SerializeField] private AudioClip _grab;
        private Collider _col;
        private Rigidbody _rb;

        protected override void Awake()
        { 
            TryGetComponent(out _rb);
            TryGetComponent(out _col);

            _initPos = transform.position;
        }

        public override void Interact(BaseInteractionEventArgs hover)
        { 
            base.Interact(hover);

            if (_attached) return;

            ManagerSound.Instance.PlaySound(_grab);
            _attached = true;
            _col.enabled = false; 
        }

        public override void ResetAction(BaseInteractionEventArgs hover)
        { 
            _col.enabled = true; 
            _attached = false; 
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
                    _col.enabled = true;
                    _rb.isKinematic = false;
                }
            }
        }
    }
}