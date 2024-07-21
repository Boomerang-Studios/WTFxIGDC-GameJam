using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManger : Singleton<ScoreManger>
{
    
    private int _score = 0;

    public TextMeshProUGUI textTmp;
    
    public void AddScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        UpdateScoreText();
    }
    
    public void ResetScore()
    {
        _score = 0;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        textTmp.text = _score.ToString();
    }

    public int GetScore()
    {
        return _score;
    }
    
    public void PlayCollectableEffect(ParticleSystem collectableEffect,Vector3 position)
    {
        ParticleSystem effect = Instantiate(collectableEffect, position, Quaternion.identity);
        effect.Play();
    }
}
