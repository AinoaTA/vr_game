using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ainoa.Scene3
{
    public class Zone : MonoBehaviour
    {
        private Donut.Sizes[] _correctOrder = { Donut.Sizes.LITTLE,
                                                Donut.Sizes.LITTLE_MEDIUM,
                                                Donut.Sizes.MEDIUM,
                                                Donut.Sizes.MEDIUM_BIG,
                                                Donut.Sizes.BIG };

        [SerializeField] private List<Donut.Sizes> _currentOrder = new();

        public void AddOrder(Donut.Sizes s)
        {
            if (!_currentOrder.Contains(s))
            {
                _currentOrder.Add(s);

                if (_currentOrder.SequenceEqual(_correctOrder)) 
                {
                
                }
            }
        }

        public void RemoveOrder(Donut.Sizes s)
        {
            if (_currentOrder.Contains(s))
            {
                _currentOrder.Remove(s);
            }
        }
    }
}