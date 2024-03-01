using TMPro;
using UnityEngine;

namespace Ainoa.Bottle
{
    public class BottleCounter : Minigame
    {
        [SerializeField] private TMP_Text _counterBottle;

        private int _max;
        private int _current = -1;

        public delegate void DelegateEndMinigame();
        public static DelegateEndMinigame OnEndMinigame;

        private void OnEnable()
        {
            Bottle.OnHit += UpdateText;
        }

        private void OnDisable()
        {
            Bottle.OnHit -= UpdateText;
        }

        private void Start()
        {
            _max = FindObjectsOfType<Bottle>().Length;

            UpdateText();
        }

        private void UpdateText()
        {
            _current++;

            _current = Mathf.Clamp(_current, 0, _max);

            _counterBottle.text = $"{_current} / {_max} Bottles";

            if (_current >= _max) 
            {
                OnEndMinigame?.Invoke();
            }
        }
    }
}