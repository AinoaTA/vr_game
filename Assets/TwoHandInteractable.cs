using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Utad.XRInteractionUtad.Scripts
{
	public class TwoHandInteractable : XRGrabInteractable
	{
		private IXRInteractor _primaryGrip;
		private IXRInteractor _secondaryGrip;

		public override bool IsSelectableBy(IXRSelectInteractor interactor)
		{
			var isAlreadyGrabbed = firstInteractorSelecting != null && !interactor.Equals(firstInteractorSelecting);
			return base.IsSelectableBy(interactor) && !isAlreadyGrabbed;
		}

		public void OnSecondHandGrab(SelectEnterEventArgs args)
		{
			Debug.Log("Secondary grip grabbed");
			_secondaryGrip = args.interactorObject;
		}

		public void OnSecondHandRelease(SelectExitEventArgs args)
		{
			Debug.Log("Secondary grip released");
			_secondaryGrip = null;
		}

		protected override void OnSelectEntered(SelectEnterEventArgs args)
		{
			Debug.Log("Primary grip grabbed");
			_primaryGrip = args.interactorObject;
			base.OnSelectEntered(args);
		}

		protected override void OnSelectExited(SelectExitEventArgs args)
		{
			Debug.Log("Primary grip released");
			base.OnSelectExited(args);
			_secondaryGrip = null;
			_primaryGrip = null;
		}

		public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
		{
			if (_primaryGrip != null && _secondaryGrip != null)
			{
				var primaryPosition = _primaryGrip.transform.position;
				var secondaryPosition = _secondaryGrip.transform.position;
				_primaryGrip.transform.rotation = Quaternion.LookRotation(secondaryPosition - primaryPosition);
			}

			base.ProcessInteractable(updatePhase);
		}
	}
}
