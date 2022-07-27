using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{

    [SerializeField] private bool play_on_start;
    [SerializeField] private AudioClip music;

    private void Start()
    {
        if(play_on_start)
            GameManager.instance.sound_manager.PlayMusic(music);
    }
}
