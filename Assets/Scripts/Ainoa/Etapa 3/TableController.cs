using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ainoa.Scene3
{
    public class TableController : Minigame
    {
        [SerializeField] private Basket[] _basket;

        private int _val;
        private void OnEnable()
        {
            _basket.ToList().ForEach(n => n.OnComplete += Check);
        }

        private void OnDisable()
        {
            _basket.ToList().ForEach(n => n.OnComplete -= Check);
        }

        private void Check() 
        {
            _val++;

            if (_val >= _basket.Length)
            {
                EndMinigame();
            }
        }
    }
}