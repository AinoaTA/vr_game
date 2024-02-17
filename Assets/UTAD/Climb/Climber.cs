using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;

namespace Utad.Interaction
{
	public class Climber : MonoBehaviour
	{
		public Transform _movingObject;
	
		private bool _trackingMovement = false;
		private Vector3 _controllerLastPos = Vector3.zero;
		private IXRSelectInteractor _mainInteractor = null;
		private IXRSelectInteractor _previousInteractor = null;
	
		private void Start()
		{
			_movingObject = FindObjectOfType<XROrigin>().transform;
		}
		
		private void Update()
		{
			if (!_trackingMovement) return;
			
			var displacement = -(_mainInteractor.transform.position - _controllerLastPos);
				
			//Convertimos el desplazamiento del mando en coordenadas globales a coordenadas locales del objeto que vamos a desplazar.
			displacement = _movingObject.transform.InverseTransformDirection(displacement); 
			displacement = Vector3.Scale(displacement, Vector3.up);
			_movingObject.transform.localPosition += displacement;
		}
	
		public void OnGripped(SelectEnterEventArgs eventArgs)
		{
			_previousInteractor = _mainInteractor;
			_mainInteractor = eventArgs.interactorObject;
	
			_controllerLastPos = _mainInteractor.transform.position;
			_trackingMovement = true;
		}
	
		public void OnReleased(SelectExitEventArgs eventArgs)
		{
			if (_mainInteractor == eventArgs.interactorObject)
			{
				_mainInteractor = _previousInteractor;
				if (_mainInteractor != null)
				{
					_controllerLastPos = _mainInteractor.transform.position;
				}
	
				_previousInteractor = null;
			}
	
			if (_previousInteractor == eventArgs.interactorObject)
			{
				_previousInteractor = null;
			}
	
			if (_mainInteractor == null && _previousInteractor == null)
			{
				_trackingMovement = false;
			}
		}
	
		
	}
}