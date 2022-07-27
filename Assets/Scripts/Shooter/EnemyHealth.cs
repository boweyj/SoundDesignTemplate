using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int max_hp;
    private int hp;

    [SerializeField] private AudioClip death_sfx;

    private void Awake()
    {
        hp = max_hp;
    }

    public void Damage(int d)
    {
        hp = Mathf.Clamp(hp - d, 0, max_hp);
        if(hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.instance.sound_manager.PlaySFX(death_sfx);
        Destroy(gameObject);
    }


}
