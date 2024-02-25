using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Ainoa.UI
{
    public class Fade : MonoBehaviour
    {
        [SerializeField] private Image _fadeImage;

        public Fade instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(instance);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        public void TransitionFade(float dur = 0.5f, float fadeTime = 0.3f)
        {
            StartCoroutine(FadeRoutine(dur, fadeTime));
        }

        /// <summary>
        /// In - Out Fade
        /// </summary>
        /// <param name="dur"></param>
        /// <returns></returns>
        private IEnumerator FadeRoutine(float dur, float fadeTime)
        {
            float t = 0;
            Color c = _fadeImage.color;
            Color futureColor = c;
            futureColor.a = 1;
            c.a = 0;
            _fadeImage.color = c;

            while (t < fadeTime)
            {
                t += Time.deltaTime;
                _fadeImage.color = Color.Lerp(c, futureColor, t / dur);
                yield return null;
            }

            yield return new WaitForSeconds(dur);
            t = 0;
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                _fadeImage.color = Color.Lerp(futureColor, c, t / dur);
                yield return null;
            }
        }
    }
}