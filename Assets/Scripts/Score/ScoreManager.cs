using System;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{

    private int _score = 0;
    public Action<int> OnScoreChanged;

    public int Score => _score;


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    public void AddScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        OnScoreChanged?.Invoke(_score);
    }

    public void ResetScore()
    {
        _score = 0;
    }

    public void PlayCollectableEffect(ParticleSystem collectableEffect, Vector3 position)
    {
        ParticleSystem effect = Instantiate(collectableEffect, position, Quaternion.identity);
        effect.Play();
    }
}
