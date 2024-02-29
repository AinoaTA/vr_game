using UnityEngine;

namespace Ainoa.Locomotion
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 3;
        //private Rigidbody _rb;
        private Vector3 _dir;
        private CharacterController _controller;
        private Camera _main;
        private Vector3 _futurePos;
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
            _main = Camera.main;
            TryGetComponent(out _controller);
        }

        public void Movement(Vector2 dir)
        {
            _dir = new(dir.x, 0, dir.y);
            _futurePos = _main.transform.forward;
            _futurePos.y = 0;
        }

        private void FixedUpdate()
        {
            if (_dir != Vector3.zero)
                _controller.transform.position += _speed * Time.fixedDeltaTime * _futurePos;
        }
    }
}