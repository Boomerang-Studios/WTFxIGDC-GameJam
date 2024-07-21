using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileBurster : Singleton<TileBurster>
{
    public ParticleSystem burstParticleMain;
    public float speed;
    [SerializeField] CameraShake m_CameraShake;

    private List<Transform> tiles = new List<Transform>();
    
    public Transform player;
    private Vector3 defaulPlayerPos;

    bool isBursting;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
        defaulPlayerPos = player.position;
    }

    public void StartBursting()
    {
        if (!isBursting)
        {
            isBursting = true;
            rb.velocity = new Vector3(speed, 0, 0);
        }
    }
    
    IEnumerator IncreaseSpeed()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 0.5f)
        {
            speed += 0.2f;
            if(isBursting)
                rb.velocity = new Vector3(speed, 0, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TileBurst tileBurst = collision.GetComponent<TileBurst>();
        if (tileBurst)
        {
            tiles.Add(collision.transform);
            tileBurst.Burst(burstParticleMain);
            m_CameraShake.Shake();
        }
        if (collision.GetComponent<SpikeTrap>())
        {
            collision.GetComponent<SpikeTrap>().Burst(burstParticleMain);
            m_CameraShake.Shake();
        }
    }

    public void Reset()
    {
        isBursting = false;
        int i = 0;
        float speedMultiplier = 1;
        if (tiles.Count > 50)
            speedMultiplier = 0.5f;
        for(int j = tiles.Count - 1; j >= 0; j--)
        {
            i++;
            StartCoroutine(MoveFromAToB(tiles[j], tiles[j].position, 0.1f + i*0.02f *speedMultiplier));
            tiles.RemoveAt(j);
        }
        
        rb.velocity = new Vector3(-(speed*6/speedMultiplier), 0, 0);
        StartCoroutine(playerMove(0.2f + i*0.02f*speedMultiplier));
        StartCoroutine(SceneReload(0.2f/speedMultiplier + i*0.03f));
    }

    IEnumerator playerMove(float duration)
    {
        Vector3 startPos = player.position;
        defaulPlayerPos = new Vector3(defaulPlayerPos.x, defaulPlayerPos.y - 20, defaulPlayerPos.z);
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            player.position = Vector3.Lerp(startPos, defaulPlayerPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        player.position = defaulPlayerPos;
    }
    
    IEnumerator SceneReload(float delay)
    {
        if(delay<1)
            delay = 1;
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    IEnumerator MoveFromAToB(Transform obj, Vector3 defaultPos, float duration)
    {
        obj.position -= new Vector3(0, 20, 0);
        Vector3 startPos = obj.position;
        obj.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            obj.position = Vector3.Lerp(startPos, defaultPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        obj.position = defaultPos;
    }
    
}