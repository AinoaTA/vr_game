using UnityEngine;

namespace Ainoa.Shoot
{
    public class Bullet : MonoBehaviour
    {
        private float _speed;
        private Vector3 _dir;

        private float _maxTimeAlive = 10;
        private float _currTimeAlive = 0;
        public void Init(Vector3 dir, float speed)
        {
            _speed = speed;
            _dir = dir;

            transform.rotation = Quaternion.LookRotation(_dir); 
        }

        private void Update()
        {
            transform.position += _speed * Time.deltaTime * _dir;

            _currTimeAlive += Time.deltaTime;

            if (_currTimeAlive >= _maxTimeAlive)
            {
                Disabled();
            }
        }
        
        private void Disabled()
        {
            gameObject.SetActive(false);

            _speed = 0;
            _dir = Vector3.zero;
            _currTimeAlive = 0;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Interactable"))
            {
                other.TryGetComponent(out Bottle.Bottle b);

                if (b != null)
                {
                    b.Interact();

                    Disabled();
                }
            }
        }
    }
}