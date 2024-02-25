using System.Collections;
using System;
using UnityEngine;

public class ManagerSound : MonoBehaviour
{
    private static ManagerSound _instance;
    public static bool MusicMuted = false;

    // Volumes
    [Range(0, 1)] [SerializeField] float _volumeEffects = 1;
    [Range(0, 1)] [SerializeField] float _volumeMusic = 1;

    [SerializeField] AudioSource _effectsSource;
    [SerializeField] AudioSource _musicSource;

    public struct SoundParams
    {
        internal float InitialValue;
        internal float FinalValue;
        internal float Time;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Effects

        _effectsSource.transform.parent = transform;
        _effectsSource.volume = _volumeEffects;


        _musicSource.transform.parent = transform;
        _musicSource.loop = true;
        _musicSource.volume = _volumeMusic;
    }

    public void MuteMusic()
    {
        MusicMuted = true;
        _instance._musicSource.volume = _instance._volumeMusic * (MusicMuted ? 0 : 1);
    }

    public void UnmuteMusic()
    {
        MusicMuted = false;
        _instance._musicSource.volume = _instance._volumeMusic * (MusicMuted ? 0 : 1);
    }

    public void PlaySound(AudioClip clip, bool randomize = false)
    {
        if (randomize)
        {
            AudioSource g = new GameObject().AddComponent<AudioSource>();
            g.transform.parent = _instance.transform;
            g.volume = _instance._volumeEffects;
            g.pitch = UnityEngine.Random.Range(.9f, 1.1f);
            g.PlayOneShot(clip);
            Destroy(g, clip.length);
        }
        else
            _instance._effectsSource.PlayOneShot(clip);
    }

    public void StopAllSounds()
    {
        if (_instance._effectsSource) _instance._effectsSource.Stop();
    }

    public void PlayMusic(AudioClip clip, bool crossfade = false)
    {
        if (clip == _instance._musicSource.clip) return;

        if (crossfade)
        {
            _instance.StartCoroutine(_instance.CrossfadeMusic(clip));
        }
        else
        {
            if (_instance._musicSource.clip != clip)
                _instance._musicSource.clip = clip;

            _instance._musicSource.Play();
        }
    }

    public void PauseMusic()
    {
        if (_instance._musicSource == null) return;
        _instance._musicSource.Pause();
    }

    public void ResumeMusic()
    {
        if (_instance._musicSource == null) return;
        if (_instance._musicSource.isPlaying == false) _instance._musicSource.Play();
    }


    public AudioSource PlaySoundCustom(AudioClip clip, bool loop = false, SoundParams? fadeIn = null, SoundParams? pitchIn = null, Transform setParent = null)
    {
        AudioSource g = new GameObject().AddComponent<AudioSource>();
        g.transform.SetParent(setParent);

        if (fadeIn != null) _instance.StartCoroutine(_instance.ModifySoundParamInTime(result => g.volume = result, fadeIn));
        else g.volume = _instance._volumeEffects;

        if (pitchIn != null) _instance.StartCoroutine(_instance.ModifySoundParamInTime(result => g.pitch = result, pitchIn));
        else g.pitch = 1f;

        g.clip = clip;
        g.loop = loop;

        g.Play();

        return g;
    }

    public void StopMusic(bool fadeOut = false)
    {
        if (fadeOut)
        {
            _instance.StartCoroutine(_instance.FadeOutMusic());
        }
        else
        {
            _instance._musicSource.Stop();
        }
    }

    IEnumerator CrossfadeMusic(AudioClip clip)
    {
        AudioSource oldMusicSource = _musicSource;
        oldMusicSource.gameObject.name = "SourceMusic_old";

        // Create new music source
        _musicSource = new GameObject().AddComponent<AudioSource>();
        _musicSource.transform.parent = transform;
        _musicSource.volume = 0;
        _musicSource.clip = clip;
        _musicSource.loop = true;
        _musicSource.Play();

        float duration = 1.42f;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            oldMusicSource.volume = Mathf.Lerp(_volumeMusic, 0, t);
            _musicSource.volume = Mathf.Lerp(0, _volumeMusic, t);
            yield return null;
        }

        // Destroy old music source
        Destroy(oldMusicSource.gameObject);
    }

    IEnumerator FadeOutMusic()
    {
        float duration = 1.42f;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            _musicSource.volume = Mathf.Lerp(_volumeMusic, 0, t);
            yield return null;
        }
        _musicSource.Stop();
        _musicSource.volume = _volumeMusic;
    }

    public void ChangePitch(AudioSource audioSource, float time = 1f, float initValue = 0.5f, float finalValue = 1f)
    {
        SoundParams sp = new();
        sp.Time = time;
        sp.InitialValue = initValue;
        sp.FinalValue = finalValue;

        _instance.StartCoroutine(_instance.ModifySoundParamInTime(result => audioSource.pitch = result, sp));
    }

    public void FadeSound(AudioSource audioSource, float time = 1f, float initValue = 0.5f, float finalValue = 1f)
    {
        SoundParams sp = new();
        sp.Time = time;
        sp.InitialValue = initValue;
        sp.FinalValue = finalValue;

        _instance.StartCoroutine(_instance.ModifySoundParamInTime(result => audioSource.volume = result, sp));
    }

    IEnumerator ModifySoundParamInTime(Action<float> variable, SoundParams? soundParams)
    {
        SoundParams sp = new();
        sp.Time = 1f;
        sp.InitialValue = 0f;
        sp.FinalValue = 1f;

        sp = soundParams ?? sp;

        float duration = sp.Time;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            variable(Mathf.Lerp(sp.InitialValue, sp.FinalValue, t));
            yield return null;
        }
        variable(sp.FinalValue);
        yield return null;
    }
}