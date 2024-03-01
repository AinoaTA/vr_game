using UnityEngine;

namespace Ainoa.Data
{
    public class ManagerData
    {
        private static SaveDataGame _data = new();
        static string _timerVar = "Timer";

        /// <summary>
        /// Will save the time if its smaller than previous saved.
        /// </summary>
        /// <param name="t"></param>
        public static void SaveData(int t)
        {
            if (_data.GetBestTimePlayed() > t)
            {
                PlayerPrefs.SetInt(_timerVar, t);
                Debug.Log("Saving!");
            }
        }

        public static void LoadData()
        {
            var v = PlayerPrefs.GetInt(_timerVar);

            if (v != 0)
            {
                _data.SetBestTimePlayed(v);

#if UNITY_EDITOR
                int minutes = Mathf.FloorToInt(v / 60f);
                int seconds = Mathf.FloorToInt(v - minutes * 60);

                Debug.Log("Previous time was " + string.Format("{0:0}:{1:00}", minutes, seconds));
#endif
            }
        }
    }
}
