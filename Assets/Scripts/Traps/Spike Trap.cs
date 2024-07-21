using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    SpriteRenderer tile;
    Collider2D tileCollider;

    private void OnEnable()
    {
        tile = GetComponent<SpriteRenderer>();
        tileCollider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("plater ded");
            //collision.gameObject.GetComponent<Player>().KillPlayer();
        }
    }
    public void Burst(ParticleSystem burstParticleMain)
    {
        ParticleSystem burstParticle = Instantiate(burstParticleMain, transform);
        burstParticle.transform.localPosition = new Vector3(0, 0.25f, 0);
        burstParticle.gameObject.SetActive(true);
        if (burstParticle != null)
        {
            burstParticle.Play();
            SoundManager.Instance.PlaySoundEffect(SFX.BlockExplode);
            tile.enabled = false;
            tileCollider.enabled = false;
        }
    }
}