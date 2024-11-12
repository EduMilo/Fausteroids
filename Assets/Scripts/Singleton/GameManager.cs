using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Requirements")]
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _obstacleTarget;

    [Header("Properties")]
    [SerializeField] private int _health;
    [SerializeField] private int _respawnLength;

    [Header("Round Attributes")]
    [SerializeField] private int[] _waveLengths; //the length of each wave.
    [SerializeField] private int[] _waveScoreRequirements; //how many points each wave needs to survive.

    private int _currWave;
    private bool _betweenWaves;
    private Coroutine _waveCountdown;
    private bool _isWon;

    private int _score = 0;

    private void Awake()
    {
        _gameUI.UpdateScore(_score);
        _gameUI.UpdateHealth(_health);
        _currWave = 0;
        StartNewWave();
    }

    public void AddScore(int score)
    {
        _score += score;
        _gameUI.UpdateScore(_score);
        if(_score >= _waveScoreRequirements[_currWave])
        {
            EndCurrentWave();
        }
    }

    public void LoseHealth(int health)
    {
        _health -= health;
        _gameUI.UpdateHealth(_health);
        if (_health <= 0)
        {
            Destroy(_player);
            GameOver();
            return;
        }

        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        //destroy all missles and sentrys before respawning
        foreach(Sentry sentry in FindObjectsOfType<Sentry>()) { sentry.PermaHit(); }
        foreach (Missle missle in FindObjectsOfType<Missle>()) { missle.PermaHit(); }

        _player.SetActive(false);
        yield return new WaitForSeconds(_respawnLength);
        _player.transform.position = Vector2.zero;
        _player.SetActive(true);
    }

    public void EndCurrentWave()
    {
        if ((_currWave + 1) == _waveLengths.Length)
        {
            Win();
            return;
        }
        _betweenWaves = true;
        _currWave++;
        StopCoroutine(_waveCountdown);
        //from here, the ObstacleSpawner will be the one to start the new wave UNLESS the game is won.
        Debug.Log("You beat wave " + (_currWave - 1));

        _gameUI.UpdateTimer(_waveLengths[_currWave]);
    }
    public void StartNewWave()
    {
        _betweenWaves = false;
        _waveCountdown = StartCoroutine(WaveCountdown(_waveLengths[_currWave]));
    }

    IEnumerator WaveCountdown(int totalSeconds)
    {
        int secondsLeft = totalSeconds;
        while (secondsLeft > 0)
        {
            _gameUI.UpdateTimer(secondsLeft);
            yield return new WaitForSeconds(1.0f);
            secondsLeft--;
        }
        //if this ever runs, its automatically game over cus it never got stopped before the time ran out.
        GameOver();
    }
    public Vector3 GetPlayerPosition()
    {
        return _player.transform.position;
    }
    public bool IsBetweenWaves()
    {
        return _betweenWaves;
    }
    public bool IsWon()
    {
        return _isWon;
    }
    public int GetWave()
    {
        return _currWave;
    }
    public Transform GetObstacleTargetTransform()
    {
        return _obstacleTarget.transform;
    }
    private void GameOver()
    {
        Debug.Log("Game Over!!");
        Destroy(_player);
        //TODO: send player back to main menu or reload scene!
    }

    private void Win()
    {
        _isWon = true;
        Debug.Log("ya won the game!");
    }

}
