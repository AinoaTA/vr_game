using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
/// <summary>
/// Script copied and modified from Valem Tutorials (https://www.youtube.com/watch?v=bts8VkDP_vU&t=850s)
/// </summary>
namespace Ainoa.Scene1
{
    public class SequencerButton : MonoBehaviour
    { 
        public SequencerManager.Sequences Type => _typeSequence;
        [SerializeField] private SequencerManager.Sequences _typeSequence;
        [SerializeField] private Transform _visual;

        private Vector3 _initLocalPos;

        private Animator _anim;
        private Transform _pokeAttachTransform;
        [SerializeField] private Vector3 _localAxis;
        [SerializeField] private float _resetSpeed = 5f;

        private bool _freeze;
        private Vector3 _offset;

       [SerializeField] private XRBaseInteractable _interact;
        private bool _isFollowing;

        public delegate void DelegateSequence(SequencerManager.Sequences seq);
        public static DelegateSequence OnSequence;

        private bool _pressedCompleted;
        private void Awake()
        {
            _anim = GetComponentInChildren<Animator>();
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
            Debug.Log(hover.interactorObject);
            bool b = hover.interactorObject is XRPokeInteractor;
            Debug.Log(hover.interactorObject.GetType());
            Debug.Log(b);
            if (b)
            {
                var poke = hover.interactorObject as XRPokeInteractor;

                _isFollowing = true;
                _freeze = false;

                _pokeAttachTransform = poke.attachTransform;
                _offset = _visual.position - _pokeAttachTransform.position;

                Debug.Log("interaction");
            }

            //OnSequence?.Invoke(_typeSequence);
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
                _visual.localPosition = Vector3.Lerp(_visual.localPosition,  _initLocalPos, Time.deltaTime*_resetSpeed);
            }

            if (_visual.localPosition.y <= -0.02f && !_pressedCompleted) 
            {
                _pressedCompleted = true;
                OnSequence?.Invoke(_typeSequence);
                Debug.Log("seeuqnce");
            }
        }

        public void Show()
        {
            //_anim.Play("button_press");
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    Debug.Log(other.name);
        //}
    }
}