using System.Collections; 
using UnityEngine;

namespace Ainoa
{
    public class ExitGame : MonoBehaviour
    {   // Start is called before the first frame update
        IEnumerator Start()
        {
            yield return new WaitForSeconds(3);
            Application.Quit();
        }
    }
}