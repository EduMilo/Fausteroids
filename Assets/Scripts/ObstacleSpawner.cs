using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class ObstacleSpawner : Singleton<ObstacleSpawner>
{
    [Header("Requirements")]
    [SerializeField] private Transform[] _spawners;
    [SerializeField] private Obstacle _obstaclePrefab;
    [SerializeField] private Obstacle _rareObstaclePrefab;
    [SerializeField] private int _rareObstacleRarity;


    [Header("Properties")]
    [SerializeField] private int _timeBetweenWavesLength;
    [SerializeField] private int _timeBetweenSpawnsLength;

    private int _obstacleCount = 1; //we start this at one to avoid it modulusing to zero.

    private void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
        Transform nextSpawner = _spawners[Random.Range(0, _spawners.Length - 1)];
            
        if (_obstaclePrefab != null && (_obstacleCount % _rareObstacleRarity == 0))
        {
            Obstacle newObstacle = Instantiate(_rareObstaclePrefab, nextSpawner.position, nextSpawner.rotation);
            newObstacle.Fly();
        } else
        {
            Obstacle newObstacle = Instantiate(_obstaclePrefab, nextSpawner.position, nextSpawner.rotation);
            newObstacle.Fly();
        }
        _obstacleCount++;
        yield return new WaitForSeconds(_timeBetweenSpawnsLength);

        if (GameManager.Instance.IsBetweenWaves())
        {
            StartCoroutine(NextWave());
        }
        else
        {
            StartCoroutine(SpawnObstacles());
        }
    }


    IEnumerator NextWave()
    {
        yield return new WaitForSeconds(_timeBetweenWavesLength);
        GameManager.Instance.StartNewWave();
        //otherwise, queue up the amount of upcoming enemies!
        _obstacleCount = 1;
        StartCoroutine(SpawnObstacles());
    }

}
