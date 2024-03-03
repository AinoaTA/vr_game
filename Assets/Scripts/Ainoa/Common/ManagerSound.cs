using System.Collections;
using System;
using UnityEngine;

public class ManagerSound : MonoBehaviour
{
    public static ManagerSound Instance;
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
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        } 

        _effectsSource.transform.parent = transform;
        _effectsSource.volume = _volumeEffects;


        _musicSource.transform.parent = transform;
        _musicSource.loop = true;
        _musicSource.volume = _volumeMusic;
    }

    public void MuteMusic()
    {
        MusicMuted = true;
        Instance._musicSource.volume = Instance._volumeMusic * (MusicMuted ? 0 : 1);
    }

    public void UnmuteMusic()
    {
        MusicMuted = false;
        Instance._musicSource.volume = Instance._volumeMusic * (MusicMuted ? 0 : 1);
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
            Instance._effectsSource.PlayOneShot(clip);
    }

    public void StopAllSounds()
    {
        if (Instance._effectsSource) Instance._effectsSource.Stop();
    }

    public void PlayMusic(AudioClip clip, bool crossfade = false)
    {
        if (clip == Instance._musicSource.clip) return;

        if (crossfade)
        {
            Instance.StartCoroutine(Instance.CrossfadeMusic(clip));
        }
        else
        {
            if (Instance._musicSource.clip != clip)
                Instance._musicSource.clip = clip;

            Instance._musicSource.Play();
        }
    }

    public void PauseMusic()
    {
        if (Instance._musicSource == null) return;
        Instance._musicSource.Pause();
    }

    public void ResumeMusic()
    {
        if (Instance._musicSource == null) return;
        if (Instance._musicSource.isPlaying == false) Instance._musicSource.Play();
    }


    public AudioSource PlaySoundCustom(AudioClip clip, bool loop = false, SoundParams? fadeIn = null, SoundParams? pitchIn = null, Transform setParent = null)
    {
        AudioSource g = new GameObject().AddComponent<AudioSource>();
        g.transform.SetParent(setParent);

        if (fadeIn != null) Instance.StartCoroutine(Instance.ModifySoundParamInTime(result => g.volume = result, fadeIn));
        else g.volume = Instance._volumeEffects;

        if (pitchIn != null) Instance.StartCoroutine(Instance.ModifySoundParamInTime(result => g.pitch = result, pitchIn));
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
            Instance.StartCoroutine(Instance.FadeOutMusic());
        }
        else
        {
            Instance._musicSource.Stop();
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

        Instance.StartCoroutine(Instance.ModifySoundParamInTime(result => audioSource.pitch = result, sp));
    }

    public void FadeSound(AudioSource audioSource, float time = 1f, float initValue = 0.5f, float finalValue = 1f)
    {
        SoundParams sp = new();
        sp.Time = time;
        sp.InitialValue = initValue;
        sp.FinalValue = finalValue;

        Instance.StartCoroutine(Instance.ModifySoundParamInTime(result => audioSource.volume = result, sp));
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