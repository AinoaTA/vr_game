using UnityEngine;

namespace Ainoa.Scene3
{
    public class Donut : MonoBehaviour
    {
        public enum Sizes { LITTLE, LITTLE_MEDIUM, MEDIUM, MEDIUM_BIG, BIG }
        [SerializeField] private Sizes _size;
        private bool _attached;

        
    }
}