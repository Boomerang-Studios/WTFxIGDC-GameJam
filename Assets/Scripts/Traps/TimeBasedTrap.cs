using System;
using UnityEngine;

public class TimeBasedTrap : MonoBehaviour
{
    [SerializeField] GameObject trap;
    [SerializeField] private float _triggerTime = 3f;
    Animator _animator;
    bool triggered = false;

    public static Action SensorTriggered;
    public static Action SwitchToSpike;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        TimeBasedTrap.SwitchToSpike += SwitchTrap;
        TimeBasedTrap.SensorTriggered += Triggered;
    }
    private void OnDisable()
    {
        TimeBasedTrap.SwitchToSpike -= SwitchTrap;
        TimeBasedTrap.SensorTriggered += Triggered;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !triggered)
        {
            triggered = true;
            _animator.SetTrigger("Start");
            SensorTriggered?.Invoke();
            Invoke(nameof(SwitchTraps), 1f);
        }
    }
    private void SwitchTraps()
    {
        SwitchToSpike?.Invoke();
    }
    private void Triggered()
    {
        triggered = true;
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
