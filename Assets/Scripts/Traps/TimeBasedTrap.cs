using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBasedTrap : MonoBehaviour
{
    [SerializeField] private float _triggerTime = 3f;

    [Header("Effect")]
    [SerializeField] private ParticleSystem _warningEffect;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // add Fuse Effect here
            StartCoroutine(TriggerAfterSec());
            _warningEffect.Play();
        }
    }
    
    private IEnumerator TriggerAfterSec()
    {
        yield return new WaitForSeconds(_triggerTime);
        Explode();
    }
    
    private void Explode()
    {
        TileBurster.Instance.StartBursting();
    }
}
