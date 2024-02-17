using UnityEngine;

namespace Ainoa
{
    public class GameManager : MonoBehaviour
    {
        private Data.SaveDataGame _data;

        private void Awake()
        {
            Data.ManagerData.LoadData();
        }
    }
}