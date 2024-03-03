using System.Collections; 
using UnityEngine;

public class MinigameFeedback : MonoBehaviour
{
    [SerializeField] private MeshRenderer _visual;

    [SerializeField] private Material _base;
    [SerializeField] private Material _correct;
    [SerializeField] private Material _wrong;
    [SerializeField] private AudioClip _wrongClip;
    [SerializeField] private AudioClip _wellClip;
    private IEnumerator _routine;


    /// <summary>
    /// Show to player bad feedback
    /// </summary>
    public void Wrong()
    {
        if (_routine != null) StopCoroutine(_routine);
        StartCoroutine(_routine = Showroutine(_wrong));
        ManagerSound.Instance.PlaySound(_wrongClip);
    }

    /// <summary>
    /// Show to player good feedback
    /// </summary>
    public void Correct()
    {
        if (_routine != null) StopCoroutine(_routine);
        StartCoroutine(_routine = Showroutine(_correct));
        ManagerSound.Instance.PlaySound(_wellClip);
    }
     
    private IEnumerator Showroutine(Material m)
    {
        _visual.material = m;
        yield return new WaitForSeconds(0.5f);

        _visual.material = _base;
    }
}
