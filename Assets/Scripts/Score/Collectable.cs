using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private int scoreToAdd = 1;
    [SerializeField] private ParticleSystem collectableEffect;

    SpriteRenderer spriteRenderer;
    Collider2D col;
    bool collected = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collected)
        {
            collected = true;
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
            col = GetComponent<Collider2D>();
            col.enabled = false;

            SoundManager.Instance.PlaySoundEffect(SFX.Collectable);
            if (collectableEffect)
                collectableEffect.Play();
            Invoke(nameof(Collected), 1f);
            if (ScoreManager.Instance)
            {
                ScoreManager.Instance.AddScore(scoreToAdd);
            }
        }
    }

    void Collected()
    {
        gameObject.SetActive(false);
    }
}
