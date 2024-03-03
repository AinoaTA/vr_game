using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabItem : MonoBehaviour
{
    protected Vector3 _initPos;

    protected XRBaseInteractable _interact;
    protected bool _attached;

    protected virtual void Awake()
    {
        TryGetComponent(out _interact);

        _interact.hoverEntered.AddListener(Interact);
    }
    private void Start()
    {
        _initPos = transform.position;
    }

    public virtual void Interact(BaseInteractionEventArgs hover) { }
    protected virtual void SetUpInHand() { }

    public virtual void ResetAction(BaseInteractionEventArgs hover)
    {
        //if (hover.interactorObject is XRDirectInteractor)
        //{
            transform.position = _initPos;
            _attached = false;
        //}
    } 
}
