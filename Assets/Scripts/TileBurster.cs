using UnityEngine;

public class TileBurster : Singleton<TileBurster>
{
    private void Awake()
    {
        Instance = this;
    }
    public ParticleSystem burstParticleMain;
    public float speed;

    bool isBursting;

    public void StartBursting()
    {
        if (!isBursting)
        {
            isBursting = true;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector3(speed, 0, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<TileBurst>())
        {
            collision.GetComponent<TileBurst>().Burst(burstParticleMain);
        }
    }
}