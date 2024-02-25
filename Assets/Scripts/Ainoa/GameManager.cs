using UnityEngine;
using Ainoa.Items;

namespace Ainoa
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public Inventory inventory { get => _inventory; private set => _inventory = value; }

        [SerializeField]
        private Inventory _inventory = new();

        private void OnEnable()
        { 
            Minigame.OnReward += UpdateInventory; 
        }

        private void OnDisable()
        {
            Minigame.OnReward -= UpdateInventory;
        }

        private void UpdateInventory(Item i) 
        {
            _inventory.AddItem(i);
        }

        private void Awake()
        {
            Data.ManagerData.LoadData();
        }
    }
}