using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ainoa
{
    public class SwitchController : MonoBehaviour
    {
        public enum HandController { NONE = 0, POKE, DIRECT }
         
        [SerializeField] private PlayerInput _playerInput;
        [Space(5)]
        [SerializeField] private ControllerPose[] _data;

        public void Switch(int index)
        {
            //Debug.Log("press button", gameObject);

            HandController hc = (HandController)index;

            //Debug.Log(hc);
            _data.ToList().ForEach(n =>
            {
                n.Controller.SetActive(n.Hand == hc);
            });
        }

        [System.Serializable]
        public class ControllerPose
        {
            public GameObject Controller => _controller;
            public HandController Hand => _hand;

            [SerializeField] private GameObject _controller;
            [SerializeField] private HandController _hand;
        }
    }
}