using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Ludus.XRIT.Interactables;

namespace Ainoa.Pose
{
    public class HandPose : BaseHand
    {
        public override void ApplyOffset(Vector3 position, Quaternion rotation)
        {
            //throw new System.NotImplementedException();
        }

        // Start is called before the first frame update
        void Start()
        {
            ApplyDefaultPose();
        } 
    }
}