using Ainoa.Items;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ainoa.Scene1
{
    public class SequencerManager : Minigame
    {
        public enum Sequences { RED, BLUE, YELLOW, GREEN, END }

        [Header("Settings")]
        [SerializeField] private Sequences[] _sequences;
        [SerializeField] private int _maxRounds = 3;

        [Header("References")]
        [SerializeField] private SequencerButton[] _buttons;
        [SerializeField] private MinigameFeedback _feedback;

        private List<Sequences> _currentPlayerSeq = new();
        private float _maxSequencesLength = 4;
        private int _currentRound;
        private bool _blockInteraction;
        private bool _completedSequence;
        private bool _endMinigame = false; 

        public delegate void DelegateCounter();
        public static DelegateCounter OnCounterAdd;

        public delegate void DelegateCounterSet(int val);
        public static DelegateCounterSet OnCounterSetUp;


        private void OnEnable()
        {
            SequencerButton.OnSequence += AddPressSequence;
        }

        private void OnDisable()
        {
            SequencerButton.OnSequence -= AddPressSequence;
        }

        private IEnumerator Start()
        {
            OnCounterSetUp?.Invoke(_maxRounds);

            yield return new WaitForSeconds(1.5f);
            StartGame();
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
                    _feedback.Wrong();
                    _currentPlayerSeq.Clear();
                    ShowSequence();
                    _completedSequence = false;
                    break;
                }
            }

            _feedback.Correct();

            if (_completedSequence)
                Completed();
        }

        private void Completed()
        {
            _completedSequence = false;
            _currentRound++;

            if (_currentRound < _maxRounds)
            {
                OnCounterAdd?.Invoke();
                _sequences = GetNewSequence();
                ShowSequence();
            }
            else
            {
                _endMinigame = true;
                EndMinigame();
            }
        }

        public void StartGame()
        {
            _sequences = GetNewSequence();
            ShowSequence();
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
            WaitForSeconds waitFive = new(1);
            for (int i = 0; i < _sequences.Length; i++)
            {
                var b = _buttons.ToList().Single(n => n.Type == _sequences[i]);
                b.Show();
                yield return waitFive;
            }

            _blockInteraction = false;
            Debug.Log("blockeD: " + _blockInteraction);
        }

        protected override void EndMinigame()
        {
            base.EndMinigame();
            //do something
        }
    }
}