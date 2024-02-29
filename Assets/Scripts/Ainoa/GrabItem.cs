using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabItem : MonoBehaviour
{
    private Vector3 _initPos;

    protected XRBaseInteractable _interact;
    protected bool _attached;
    private void Awake()
    {
        TryGetComponent(out _interact);

        _interact.hoverEntered.AddListener(Interact);
    }
    private void Start()
    {
        _initPos = transform.position;
    }

    public virtual void Interact(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRDirectInteractor /*dr*/)
        {
            transform.SetParent(hover.interactorObject.transform);
        }
    }
    protected virtual void SetUpInHand() { }

    public void ResetAction(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRDirectInteractor)
        {
            transform.position = _initPos;
            _attached = false;
        }
    }

    //private void Update()
    //{
    //    //if (_freeze) return;

    //    //if (_isFollowing)
    //    //{
    //    //    Vector3 localTarget = _visual.InverseTransformPoint(_pokeAttachTransform.position + _offset);
    //    //    Vector3 constrainedLocalTargetPosition = Vector3.Project(localTarget, _localAxis);


    //    //    _visual.position = _visual.TransformPoint(constrainedLocalTargetPosition);
    //    //    Vector3 clampY = _visual.localPosition;

    //    //    clampY.y = Mathf.Clamp(_visual.localPosition.y, -0.0221f, 0);
    //    //    _visual.localPosition = clampY;
    //    //}
    //    //else
    //    //{
    //    //    _visual.localPosition = Vector3.Lerp(_visual.localPosition, _initLocalPos, Time.deltaTime * _resetSpeed);
    //    //}

    //    ////clamp Y pos
    //    //if (_visual.localPosition.y <= -0.02f && !_pressedCompleted)
    //    //{
    //    //    _pressedCompleted = true;
    //    //    OnSequence?.Invoke(_typeSequence);
    //    //    StartCoroutine(Showroutine());
    //    //}
    //}
}
