using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ainoa.Bottle
{
    public class Bottle : MonoBehaviour
    {
        public delegate void DelegateHit();
        public static DelegateHit OnHit;

        private Collider _col;

        private void Awake()
        {
            TryGetComponent(out _col);
        }

        public void Interact() 
        {
            _col.enabled = false;
            OnHit?.Invoke();

            gameObject.SetActive(false);
        }
    }
}