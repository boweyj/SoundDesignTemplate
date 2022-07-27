using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudio : MonoBehaviour
{
    [SerializeField] private List<AudioClip> ambience_sfx; // list of possible sounds to play
    [SerializeField] private Vector2 min_pos, max_pos; // how large of an area should be considered when randomly playing sounds
    [SerializeField] private Vector2 time_delay_range; // min and max time delays between each random sound

    private void Start()
    {
        StartCoroutine(CoPlayRandomAmbience());
    }

    private IEnumerator CoPlayRandomAmbience()
    {
        while(true)
        {
            float time_delay = Random.Range(time_delay_range.x, time_delay_range.y);
            AudioClip clip = ambience_sfx[Random.Range(0, ambience_sfx.Count)];
            Vector3 sound_location = new Vector3(Random.Range(min_pos.x, max_pos.x), 0, Random.Range(min_pos.y, max_pos.y));

            GameManager.instance.sound_manager.PlaySFXAtPosition(clip, sound_location);

            yield return new WaitForSeconds(time_delay);
        }
    }


}
