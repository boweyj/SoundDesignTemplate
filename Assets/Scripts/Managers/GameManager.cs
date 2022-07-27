using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector] public SoundManager sound_manager;

    private void Awake()
    {
        if (GameManager.instance != null && GameManager.instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            GameManager.instance = this;
            DontDestroyOnLoad(gameObject);

            // initialization
            sound_manager = GetComponent<SoundManager>();
        }
    }
}
