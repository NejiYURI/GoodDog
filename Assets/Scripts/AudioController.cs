using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AudioData
{
    public AudioClip clip;
    public float volume = 1.0f;
}
[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    [SerializeField]
    private AudioSource sfxSource;

    [SerializeField]
    private AudioSource bgmSource;


    private void Awake()
    {
        if (AudioController.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //sfxSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audioClip, float Volume = 1, bool overlapping = true)
    {
        if (audioClip == null | sfxSource == null) return;
        if (!this.sfxSource.isPlaying | overlapping)
        {
            this.sfxSource.PlayOneShot(audioClip, Volume);
        }
    }

    public bool IsBGMPlaying()
    {
        return bgmSource && bgmSource.isPlaying;
    }

    public void PlayBgm()
    {
        if (!bgmSource) return;
        bgmSource.Play();
    }

    public void StopBgm()
    {
        if (!bgmSource) return;
        bgmSource.Stop();
    }
}
