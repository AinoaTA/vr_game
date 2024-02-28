
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ainoa.Locomotion
{
    public class PlayerInputs : MonoBehaviour
    {
        public delegate void DelegateShot();
        public static DelegateShot OnShotDelegate;

        public delegate void DelegateMove(Vector3 vector);
        public static DelegateMove OnMoveDelegate;
        /// <summary>
        /// To get Axis values (x,y).
        /// </summary>
        /// <param name="ctx"></param>
        public void OnMove(InputAction.CallbackContext ctx)
        {
            Debug.Log(ctx.ReadValue<Vector3>());
            OnMoveDelegate?.Invoke(ctx.ReadValue<Vector3>());
        }
         
    }
}