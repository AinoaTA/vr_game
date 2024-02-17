using UnityEngine;

namespace Ainoa.Scene1
{
    public class SequencerButton : MonoBehaviour
    {
        public SequencerManager.Sequences Type => _typeSequence;
        [SerializeField] private SequencerManager.Sequences _typeSequence;

        private Animator _anim;

        public delegate void DelegateSequence(SequencerManager.Sequences seq);
        public static DelegateSequence OnSequence;

        private void Awake()
        {
            _anim = GetComponentInChildren<Animator>();
        }
        public void Interact()
        {
            Show();
            OnSequence?.Invoke(_typeSequence);
        }

        public void Show()
        {
            _anim.Play("button_press");
        }
    }
}