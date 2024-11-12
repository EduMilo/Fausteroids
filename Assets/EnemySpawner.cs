using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Positions")]
    [SerializeField] Transform[] _spawnPoints;
    [SerializeField] Transform[] _sentryPositions;
    private int _currSpawnPoint;

    [Header("Enemy Types")]
    [SerializeField] Sentry _sentryPrefab;
    [SerializeField] Missle _misslePrefab;

    [Header("Spawn Rates")]
    [SerializeField] int _sentrySpawnRate;
    [SerializeField] int _missleSpawnRate;

    private void Start()
    {
        StartCoroutine(SpawnSentry());
        StartCoroutine(SpawnMissle());
    }

    IEnumerator SpawnSentry()
    {
        yield return new WaitForSeconds(_sentrySpawnRate);
        Sentry newSentry = Instantiate(_sentryPrefab, _spawnPoints[_currSpawnPoint].position, transform.rotation);
        newSentry.SetPositions(_sentryPositions);
        NextSpawnpoint();
        StartCoroutine(SpawnSentry());
    }

    IEnumerator SpawnMissle()
    {
        yield return new WaitForSeconds(_missleSpawnRate);
        Missle newMissle = Instantiate(_misslePrefab, _spawnPoints[_currSpawnPoint].position, transform.rotation);
        NextSpawnpoint();
        StartCoroutine(SpawnMissle());
    }

    private void NextSpawnpoint()
    {
        _currSpawnPoint++;
        if(_currSpawnPoint >= _spawnPoints.Length)
        {
            _currSpawnPoint = 0;
        }

    }
}
