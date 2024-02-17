using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace Ainoa.UI
{

    public class UICounter : MonoBehaviour
    {
        private static UICounter _instance;
        private float _timer;
        private TMP_Text _text;

        private const string _format = "{0:0}:{1:00}"; //avoid string "lag"

        public UnityEvent onQuit;
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this);
                return;
            }
            else
            {
                _instance = this;
                _text = GetComponentInChildren<TMP_Text>();
                onQuit.AddListener(delegate { Data.ManagerData.SaveData((int)_timer); });
            }
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            int minutes = Mathf.FloorToInt(_timer / 60f);
            int seconds = Mathf.FloorToInt(_timer - minutes * 60);

            _text.text = string.Format(_format, minutes, seconds);
        }

        private void OnApplicationQuit()
        {
            onQuit?.Invoke();
        }
    }
}