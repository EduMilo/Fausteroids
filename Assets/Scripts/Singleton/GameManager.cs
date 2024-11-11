using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Requirements")]
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _obstacleTarget;
    private int _score = 0;
    private int _health = 3; //starts at 3 i guess...
    //time?

    private void Awake()
    {
        _gameUI.UpdateScore(_score);
        _gameUI.UpdateHealth(_health);
    }

    public void AddScore(int score)
    {
        _score += score;
        _gameUI.UpdateScore(_score);
    }

    public void LoseHealth(int health)
    {
        _health -= health;
        if(_health <= 0)
        {
            Destroy(_player);
            GameOver();
        }
        _gameUI.UpdateHealth(_health);
    }

    public Transform GetObstacleTarget()
    {
        return _obstacleTarget;
    }

    private void GameOver()
    {
        Debug.Log("Game Over!!");
        //send player back to main menu or reload scene!
    }
}
