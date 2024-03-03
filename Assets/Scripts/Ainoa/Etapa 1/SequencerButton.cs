using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Script copied and modified from Valem Tutorials (https://www.youtube.com/watch?v=bts8VkDP_vU&t=850s)
/// It was a start point to know how create these scripts for VR unity events.
/// </summary>
namespace Ainoa.Scene1
{
    public class SequencerButton : MonoBehaviour
    {
        public SequencerManager.Sequences Type => _typeSequence;
        [SerializeField] private SequencerManager.Sequences _typeSequence;
        [SerializeField] private Material _default;
        [SerializeField] private Material _hint;
        [SerializeField] private Transform _visual; 
        [SerializeField] private Vector3 _localAxis;
        [SerializeField] private AudioClip _press;

        [SerializeField] private float _resetSpeed = 5f;
        private Transform _pokeAttachTransform;
        private XRBaseInteractable _interact;
        private Vector3 _initLocalPos;
        private bool _freeze;
        private Vector3 _offset; 
        private bool _isFollowing; 
        private bool _pressedCompleted;

        public delegate void DelegateSequence(SequencerManager.Sequences seq);
        public static DelegateSequence OnSequence;
         
        private void Awake()
        {
            TryGetComponent(out _interact);
            _interact.hoverEntered.AddListener(Interact);
            _interact.hoverExited.AddListener(ResetAction);
            _interact.selectEntered.AddListener(Freeze);
        }

        private void Start()
        {
            _initLocalPos = _visual.localPosition;
        }
        public void Interact(BaseInteractionEventArgs hover)
        {
            if (hover.interactorObject is XRPokeInteractor poke)
            {
                _isFollowing = true;
                _freeze = false;

                _pokeAttachTransform = poke.attachTransform;
                _offset = _visual.position - _pokeAttachTransform.position;
            }
        }

        public void Freeze(BaseInteractionEventArgs hover)
        {
            if (hover.interactorObject is XRPokeInteractor)
            {
                _freeze = true;
            }
        }
        public void ResetAction(BaseInteractionEventArgs hover)
        {
            if (hover.interactorObject is XRPokeInteractor)
            {
                _isFollowing = false;
                _pressedCompleted = false;
            }
        }

        private void Update()
        {
            if (_freeze) return;

            if (_isFollowing)
            {
                Vector3 localTarget = _visual.InverseTransformPoint(_pokeAttachTransform.position + _offset);
                Vector3 constrainedLocalTargetPosition = Vector3.Project(localTarget, _localAxis);


                _visual.position = _visual.TransformPoint(constrainedLocalTargetPosition);
                Vector3 clampY = _visual.localPosition;

                clampY.y = Mathf.Clamp(_visual.localPosition.y, -0.0221f, 0);
                _visual.localPosition = clampY;
            }
            else
            {
                _visual.localPosition = Vector3.Lerp(_visual.localPosition, _initLocalPos, Time.deltaTime * _resetSpeed);
            }

            //clamp Y pos
            if (_visual.localPosition.y <= -0.02f && !_pressedCompleted)
            {
                _pressedCompleted = true;
                OnSequence?.Invoke(_typeSequence);
                ManagerSound.Instance.PlaySound(_press);
                Show();
            }
        }

        /// <summary>
        /// Take a hint for player.
        /// </summary>
        public void Show()
        {
            StartCoroutine(Showroutine());
        }

        /// <summary>
        /// Just change between material for hint.
        /// </summary>
        /// <returns></returns>
        private IEnumerator Showroutine()
        {
            _visual.TryGetComponent(out MeshRenderer r);

            r.material = _hint;
            yield return new WaitForSeconds(0.5f);

            r.material = _default;
        }
    }
}