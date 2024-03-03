using TMPro;
using UnityEngine;

namespace Ainoa.Rounds
{
    public class RoundCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private int _current = 0;
        private int _max;

        private void OnEnable()
        {
            Ainoa.Scene1.SequencerManager.OnCounterAdd += AddRound;
            Ainoa.Scene1.SequencerManager.OnCounterSetUp += SetupMax; 
        }
        private void OnDisable()
        {
            Ainoa.Scene1.SequencerManager.OnCounterAdd -= AddRound;
            Ainoa.Scene1.SequencerManager.OnCounterSetUp -= SetupMax; 
        }
        public void SetupMax(int val)
        {
            _max = val;
            _text.text = _current.ToString() + " / " + _max.ToString();
        }

        public void AddRound()
        {
            _current++;
            _text.text = _current.ToString() + " / " + _max.ToString();
        }
    }
}