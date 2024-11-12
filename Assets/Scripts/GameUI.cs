using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _timerText;


    public void UpdateScore(int score)
    {
        _scoreText.text = $"Score: {score}";
    }

    public void UpdateHealth(int health)
    {
        _healthText.text = $"Health: {health}";
    }

    public void UpdateTimer(int timeLeft)
    {
        _timerText.text = $"Time Left: {timeLeft}";
    }
}
