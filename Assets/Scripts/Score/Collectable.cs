using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private int scoreToAdd = 1;
    [SerializeField] private ParticleSystem collectableEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ScoreManger.Instance.AddScore(scoreToAdd);
            
            if(collectableEffect)
                ScoreManger.Instance.PlayCollectableEffect(collectableEffect,transform.position);
            
            gameObject.SetActive(false);
        }
    }
}
