using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider music_slider, sfx_slider;

    [SerializeField] private AudioClip music;

    private void Start()
    {
        GameManager.instance.sound_manager.PlayMusic(music);

        if(PlayerPrefs.HasKey("MusicVolume"))
        {
            float value = PlayerPrefs.GetFloat("MusicVolume");
            music_slider.value = value;
            float volume = Mathf.Log10(value) * 20;
            mixer.SetFloat("MusicVolume", volume);
        }
        else
        {
            float volume = Mathf.Log10(music_slider.value) * 20;
            mixer.SetFloat("MusicVolume", volume);
        }

        if(PlayerPrefs.HasKey("SFXVolume"))
        {
            float value = PlayerPrefs.GetFloat("SFXVolume");
            sfx_slider.value = value;
            float volume = Mathf.Log10(value) * 20;
            mixer.SetFloat("SFXVolume", volume);
        }
        else
        {
            float volume = Mathf.Log10(sfx_slider.value) * 10;
            mixer.SetFloat("SFXVolulme", volume);
        }
    }

    public void OnMusicSliderChanged(float f)
    {
        float volume = Mathf.Log10(f) * 20;
        PlayerPrefs.SetFloat("MusicVolume", f);
        mixer.SetFloat("MusicVolume", volume);
    }

    public void OnSFXSliderChanged(float f)
    {
        float volume = Mathf.Log10(f) * 20;
        PlayerPrefs.SetFloat("SFXVolume", f);
        mixer.SetFloat("SFXVolume", volume);
    }

    public void LoadScene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }
}
