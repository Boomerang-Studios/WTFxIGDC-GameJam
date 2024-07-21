using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBasedTrap : MonoBehaviour
{
    [SerializeField] GameObject trap;
    [SerializeField] private float _triggerTime = 3f;
    Animator _animator;
    bool triggered = false;

    public static Action SensorTriggered;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        TimeBasedTrap.SensorTriggered += SwitchTrap;
    }
    private void OnDisable()
    {
        TimeBasedTrap.SensorTriggered -= SwitchTrap;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !triggered)
        {
            triggered = true;
            _animator.SetTrigger("Start");
            SensorTriggered?.Invoke();
        }
    }
    private void CountDown()
    {
        SoundManager.Instance.PlaySoundEffect(SFX.CountDown);
    }
    private void Explode()
    {
        SoundManager.Instance.PlaySoundEffect(SFX.SensorBlast);
        TileBurster.Instance.StartBursting();
        Instantiate(trap, transform.position, Quaternion.identity);
        transform.parent.gameObject.SetActive(false);
    }
    private void SwitchTrap()
    {
        if (triggered) return;
        Instantiate(trap, transform.position, Quaternion.identity);
        transform.parent.gameObject.SetActive(false);
    }
}
