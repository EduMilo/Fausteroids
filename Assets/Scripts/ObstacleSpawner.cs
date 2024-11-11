using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class ObstacleSpawner : Singleton<ObstacleSpawner>
{
    [Header("Requirements")]
    [SerializeField] private Transform[] _spawners;
    [SerializeField] private Obstacle _obstaclePrefab;

    [Header("Wave Properties")]
    [SerializeField] private int _waveCount; // the amount of waves in the level.

    [Header("Properties")]
    [SerializeField] private int _startingObstacleAmount;
    [SerializeField] private int _waveMultiplier;
    [SerializeField] private int _onScreenLimit;
    [SerializeField] private int _waveIntervalLength;
    [SerializeField] private int _timeBetweenSpawnsLength;



    private int _obstaclesLeft; // how many obstacles left this wave.
    private int _currWave = 1;

    private void Awake()
    {
        //first make sure that we know how many obstacles we have to spawn
        _obstaclesLeft = _startingObstacleAmount;
    }

    private void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
        int obstaclesToSpawn;
        if(_obstaclesLeft > _onScreenLimit)
        {
            obstaclesToSpawn = _onScreenLimit;
        } else
        {
            obstaclesToSpawn = _obstaclesLeft;
        }


        for (int i = 0; i < obstaclesToSpawn; i++)
        {
            Transform nextSpawner = _spawners[Random.Range(0, _spawners.Length - 1)];
            Obstacle newObstacle = Instantiate(_obstaclePrefab, nextSpawner.position, nextSpawner.rotation);
            newObstacle.Fly();
            yield return new WaitForSeconds(_timeBetweenSpawnsLength);
        }
        yield return new WaitForSeconds(_waveIntervalLength / 2); //wait half of the waveinterval before spawning another set of enemies.
        if(_obstaclesLeft < 0)
        {
            Debug.Log("Wave " + _currWave + " defeated!");
            StartCoroutine(NextWave());
            
        } else
        {
            StartCoroutine(SpawnObstacles());
        }
    }

    IEnumerator NextWave()
    {
        _currWave++;
        if(_currWave > _waveCount)
        {
            //You win! Win stuff here!
            Debug.Log("all waves defeated! ya won!");
        }

        yield return new WaitForSeconds(_waveIntervalLength);
        //otherwise, queue up the amount of upcoming enemies!
        _startingObstacleAmount *= _waveMultiplier;
        _obstaclesLeft = _startingObstacleAmount;
        StartCoroutine(SpawnObstacles());
    }

    public void ObstacleDestroyed()
    {
        _obstaclesLeft--;
    }
}
