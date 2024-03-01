using Ainoa.Items;
using UnityEngine;

namespace Ainoa
{
    public class Minigame : MonoBehaviour
    {
        [SerializeField] private Item _itemReward;

        public delegate void DelegateReward(Item item);
        public static DelegateReward OnReward;
         
        protected virtual void EndMinigame()
        {
            OnReward?.Invoke(_itemReward);
        }
    }
}