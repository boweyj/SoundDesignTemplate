using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource sfx_source, music_source;
    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioMixerGroup sfx_group, music_group;

    // Loading player volume levels from settings page
    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float value = PlayerPrefs.GetFloat("MusicVolume");
            float volume = Mathf.Log10(value) * 20;
            mixer.SetFloat("MusicVolume", volume);
        }
        else
        {
            float volume = Mathf.Log10(1) * 20;
            PlayerPrefs.SetFloat("MusicVolume", 1);
            mixer.SetFloat("MusicVolume", volume);
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            float value = PlayerPrefs.GetFloat("SFXVolume");
            float volume = Mathf.Log10(value) * 20;
            mixer.SetFloat("SFXVolume", volume);
        }
        else
        {
            float volume = Mathf.Log10(1) * 10;
            PlayerPrefs.SetFloat("SFXVolume", 1);
            mixer.SetFloat("SFXVolulme", volume);
        }
    }

    // Plays a 2D sound clip through the SFX mixer channel
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null)
            return;
        sfx_source.PlayOneShot(clip);

    }

    // add play sfx at position
    public void PlaySFXAtPosition(AudioClip clip, Vector3 position)
    {
        GameObject sfx = new GameObject("SFX_" + clip.name);
        AudioSource src = sfx.AddComponent<AudioSource>();
        src.spatialBlend = 1; // 3D sound
        src.clip = clip;
        src.Play();
        src.outputAudioMixerGroup = sfx_group;
        StartCoroutine(CoRemoveGameObjectWhenClipPlayed(src));
    }

    // add support for moving audiosources??
    public void PlaySFXOnGameObject(AudioClip clip, GameObject obj)
    {
        AudioSource src = obj.AddComponent<AudioSource>();
        src.clip = clip;
        src.spatialBlend = 1;
        src.Play();
        src.outputAudioMixerGroup = sfx_group;
        //StartCoroutine(co)
    }


    // Plays a sound clip through the Music mixer channel
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) // if supplied clip is empty, do nothing
            return;
        if(music_source.clip == null)
        {
            music_source.clip = clip;
            music_source.Play();
            return;
        }
        if (clip.name.Equals(music_source.clip.name)) // if the song you are trying to play is already playing, do nothing
            return;
        StartCoroutine(CoFadeMusic(0.5f, 0.5f, clip));
    }

    private IEnumerator CoFadeMusic(float time_to_fade_out, float time_to_fade_in, AudioClip new_music)
    {
        float music_step = 1 / time_to_fade_out * Time.deltaTime; // amount of volume to fade out each frame

        while(music_source.volume > 0)
        {
            music_source.volume -= music_step;
            yield return new WaitForEndOfFrame();
        }

        music_step = 1 / time_to_fade_in * Time.deltaTime;
        music_source.clip = new_music;
        music_source.Play();

        while(music_source.volume < 1)
        {
            music_source.volume += music_step;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator CoRemoveGameObjectWhenClipPlayed(AudioSource src)
    {
        while(src.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        Destroy(src.gameObject);
    }

    private IEnumerator CoRemoveAudioSourceWhenClipPlayed(AudioSource src)
    {
        while(src.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
    }
}
