using System.Collections; 
using UnityEngine;

namespace Ainoa
{
    public class ExitGame : MonoBehaviour
    {   // Start is called before the first frame update

        public delegate void DelegateEnd();
        public static DelegateEnd OnStop;
        IEnumerator Start()
        {
            OnStop?.Invoke();

            yield return new WaitForSeconds(3);
            Application.Quit();
        }
    }
}