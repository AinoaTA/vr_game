using UnityEngine;

namespace Ainoa.Locomotion
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 3;
        private Rigidbody _rb;
        private Vector3 _dir;
        private void OnEnable()
        {
            PlayerInputs.OnMoveDelegate += Movement;
        }

        private void OnDisable()
        {
            PlayerInputs.OnMoveDelegate -= Movement;
        }

        private void Awake()
        {
            TryGetComponent(out _rb);
        }

        public void Movement(Vector3 dir)
        {
            _dir = new(dir.x, 0, dir.y);
        }

        private void FixedUpdate()
        {
            if (_dir != Vector3.zero)
            _rb.velocity = _speed * Time.fixedDeltaTime * _dir;
        }
    }
}