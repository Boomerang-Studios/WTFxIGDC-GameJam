using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrap : MonoBehaviour
{
    [SerializeField] private float _fuseTime = 3f;
    [SerializeField] private float _explosionRadius = 5f;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // add Fuse Effect here
            StartCoroutine(ExplodeAfterDelay());
        }
    }
    
    private IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(_fuseTime);
        Explode();
    }
    
    private void Explode()
    {
        // add Explosion Effect here
    }
}
