using UnityEngine;

namespace Ainoa.Scene1
{
    public class SequencerButton : MonoBehaviour
    {
        [SerializeField] private SequencerManager.Sequences _typeSequence;
         
        public delegate void DelegateSequence(SequencerManager.Sequences seq);
        public static DelegateSequence OnSequence;

        public void Interact() 
        {
            Animations();
            OnSequence?.Invoke(_typeSequence);
        }

        private void Animations() 
        {
        }
    }
}