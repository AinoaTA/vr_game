using UnityEngine;
using Ainoa.Items;
using UnityEngine.SceneManagement;
using System.Collections;

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

        public void NextScene()
        {
            StartCoroutine(NextSceneRoutine());
        }

        IEnumerator NextSceneRoutine()
        {
            UI.Fade.instance.TransitionFade(1, 0.3f);
            yield return new WaitForSeconds(0.3f);

            int index = SceneManager.GetActiveScene().buildIndex;
            index++;

            var s = SceneManager.GetSceneByBuildIndex(index); //just in case if index doesn't exist.
            if (s == null) yield break;

            SceneManager.LoadScene(index);
        }
    }
}