using Ainoa.Items;
using UnityEngine;

namespace Ainoa
{
    public class Minigame : MonoBehaviour
    {
        [SerializeField] private Item _itemReward;

        public delegate void DelegateReward(Item item);
        public static DelegateReward OnReward;

        public delegate void DelegateEndMinigame();
        public static DelegateEndMinigame OnEndMinigame;


        protected bool _enabledGame;
        protected virtual void EndMinigame()
        {
            OnReward?.Invoke(_itemReward);

            OnEndMinigame?.Invoke();
        }
    }
}