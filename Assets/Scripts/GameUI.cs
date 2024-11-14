using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _waveText;


    public void UpdateHealth(int health)
    {
        _healthText.text = $"Health: {health}";
    }

    public void UpdateTimer(int timeLeft)
    {
        _timerText.text = $"{timeLeft}";
    }

    public void UpdateScore()
    {
        _waveText.text = $"Wave: {GameManager.Instance.GetWave() + 1}\nScore Needed: {GameManager.Instance.GetScoreLeft()}";
    }

    public void WaveComplete()
    {
        _waveText.text = "Wave Complete!";
    }

    
}
