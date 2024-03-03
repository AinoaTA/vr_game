using System.Linq;
using UnityEngine;

namespace Ainoa.Scene3
{
    public class Basket : MonoBehaviour
    { 
        public enum Color { RED, GREEN, ORANGE }
        public Color ColorReference => _color;
        [SerializeField] private Color _color;

        [SerializeField] private Transform _point;
        [SerializeField] private Ainoa.Rounds.RoundCounter _counter;

        private int _max;
        private int _curr;

        public delegate void DelegateCompleted();
        public DelegateCompleted OnComplete;
        public void Add(GameObject g)
        {
            _counter.AddRound();
            _curr++;
            g.transform.position = _point.position;

            if (_curr == _max)
                OnComplete?.Invoke();
        }

        private void Start()
        {
            var b = GameObject.FindObjectsOfType<ColorObject>();

            var list = b.ToList().FindAll(n => n.ColorReference == ColorReference);

            _max = list.Count;
            _curr = 0;
            _counter.SetupMax(_max);
        }
    }
}