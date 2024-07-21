using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_SoundSettings : MonoBehaviour
{
    public bool setMusic;
    public bool setSFX;

    public float incrementValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (setMusic)
                SoundManager.Instance.ChangeMusicVolume(incrementValue);
            else if(setSFX)
                SoundManager.Instance.ChangeEffectsVolume(incrementValue);
        }
    }
}
