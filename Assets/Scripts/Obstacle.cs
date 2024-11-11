using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] int _damage;
    [SerializeField] int _score;
    [SerializeField] int _minMagnitude;
    [SerializeField] int _maxMagnitude;
    [SerializeField] float _duration;

    [Header("Sub-Obstacles")]
    [SerializeField] Obstacle _subObstacle;
    [SerializeField] int _subObstacleCount;

    [Header("Collectible")]
    [SerializeField] Collectible _collectiblePrefab;

    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Fly()
    {
        //obstacle will fly towards the ObstacleTarget contained by GameManager
        Vector2 force = GameManager.Instance.GetObstacleTarget().up - transform.position;
        int magnitude = Random.Range(_minMagnitude, _maxMagnitude);
        _rb.AddForce(force * magnitude);
        Destroy(gameObject, _duration);
    }

    public void SubFly()
    {
        //obstacle will fly towards a random direction at minMagnitude.
        //This is meant for sub-obstacles that break off of a larger one.
        Vector2 force = Random.insideUnitCircle.normalized * 10;
        _rb.AddForce(force * _minMagnitude);
        Destroy(gameObject, _duration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if collides with player
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController pc))
        {
            GameManager.Instance.LoseHealth(_damage);
            Destroy(gameObject);
        }

        //if hit by a bullet, add score, update spawner, check for subObstacles, check for collectibles, then destroy.
        if (collision.gameObject.TryGetComponent<Bullet>(out Bullet b))
        {
            GameManager.Instance.AddScore(_score);
            ObstacleSpawner.Instance.ObstacleDestroyed();

            if (_subObstacle != null)
            {
                for (int i = 0; i < _subObstacleCount; i++)
                {
                    Obstacle newObstacle = Instantiate(_subObstacle, transform.position, transform.rotation);
                    newObstacle.SubFly();
                }
            }

            if(_collectiblePrefab != null)
            {
                Instantiate(_collectiblePrefab, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}
