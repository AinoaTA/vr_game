using UnityEngine;

namespace Ainoa.Shoot
{
    public class ShootManager : MonoBehaviour
    {
        [SerializeField] private float _bulletSpeedDefault = 5;

        [Header("References")]
        [SerializeField] private Bullet _prefabBullet;
        [SerializeField] private Transform _bulletStore;
        [SerializeField] private Transform _firePoint;

        private BulletPooling _pooling;

        private void Awake()
        {
            _pooling = new(_prefabBullet, 30, _bulletStore, true);
        }

        public void Shoot()
        {
            if (_firePoint == null) return;

            var b = _pooling.GetObjectPooling();

            b.Init(_firePoint.forward, _bulletSpeedDefault);
            b.transform.position = _firePoint.position;
            b.gameObject.SetActive(true);
        }

        public void Attach(Transform firePoint) 
        {
            _firePoint = firePoint;
        }
    }
}