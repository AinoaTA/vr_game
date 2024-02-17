using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ainoa.Scene1
{
    public class SequencerManager : MonoBehaviour
    {
        public enum Sequences { RED, BLUE, YELLOW, GREEN, END }

        [Header("Settings")]
        [SerializeField] private Sequences[] _sequences;
        [SerializeField] private int _maxRounds = 3;

        [Header("References")]
        [SerializeField] private GameObject[] _sequencerButtons;

        private List<Sequences> _currentPlayerSeq;
        private float _maxSequencesLength = 4;
        private int _currentRound;
        private bool _blockInteraction;
        private bool _completedSequence;
        private bool _endMinigame = false;
        private void OnEnable()
        {
            SequencerButton.OnSequence += AddPressSequence;
        }

        private void OnDisable()
        {
            SequencerButton.OnSequence -= AddPressSequence;
        }

        private void AddPressSequence(Sequences s)
        {
            if (_blockInteraction || _endMinigame) return;

            _currentPlayerSeq.Add(s);

            _completedSequence = _currentPlayerSeq.Count == _sequences.Length;

            for (int i = 0; i < _currentPlayerSeq.Count; i++)
            {
                if (_currentPlayerSeq[i] != _sequences[i])
                {
                    _currentPlayerSeq.Clear();
                    ShowSequence();
                    _completedSequence = false;
                    break;
                }
            }

            if (_completedSequence)
                Completed();
        }

        private void Completed()
        {
            _completedSequence = false;
            _currentRound++;

            if (_currentRound < _maxRounds)
            {
                _sequences = GetNewSequence();
                ShowSequence();
            }
            else
            {
                _endMinigame = true;
                EndMinigame();
            }
        }

        private void Awake()
        {
            StartGame();
        }

        public void StartGame()
        {
            _sequences = GetNewSequence();
        }

        private Sequences[] GetNewSequence()
        {
            List<Sequences> seq = new();
            for (int i = 0; i < _maxSequencesLength; i++)
            {
                seq.Add((Sequences)Random.Range(0, (int)Sequences.END));
            }

            return seq.ToArray();
        }

        private void ShowSequence()
        {
            if (_endMinigame) return;

            _blockInteraction = true;
            StartCoroutine(ShowSequenceRoutine());
        }

        private IEnumerator ShowSequenceRoutine()
        {

            yield return null;
            _blockInteraction = false;
        }

        private void EndMinigame() 
        {
        //do something
        }
    }
}