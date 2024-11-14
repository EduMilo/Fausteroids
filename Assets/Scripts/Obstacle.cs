using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] protected int _health;
    [SerializeField] protected int _damage;
    [SerializeField] protected int _score;
    [SerializeField] protected int _minMagnitude;
    [SerializeField] protected int _maxMagnitude;
    [SerializeField] protected float _duration;

    [Header("Sub-Obstacles")]
    [SerializeField] protected Obstacle _subObstacle;
    [SerializeField] protected int _subObstacleCount;

    [Header("Collectible")]
    [SerializeField] protected Collectible _collectiblePrefab;

    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Fly()
    {
        //obstacle will fly towards the ObstacleTarget contained by GameManager
        Vector2 force = GameManager.Instance.GetObstacleTargetTransform().position - transform.position;
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

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //if collides with player
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            GameManager.Instance.LoseHealth(_damage);
        }

        //if hit by a bullet, add score, update spawner, check for subObstacles, check for collectibles, then destroy.
        if (collision.gameObject.GetComponent<Bullet>())
        {
            Hit(collision.gameObject.GetComponent<Bullet>().GetDamage());
        }
    }

    public void Hit(int incomingDamage)
    {
        AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position);

        _health -= incomingDamage;
        if(_health > 0)
        {
            return;
        }
        
        GameManager.Instance.AddScore(_score);

        if (_subObstacle != null && incomingDamage != 999)
        {
            for (int i = 0; i < _subObstacleCount; i++)
            {
                Obstacle newObstacle = Instantiate(_subObstacle, transform.position, transform.rotation);
                newObstacle.SubFly();
            }
        }

        if (_collectiblePrefab != null)
        {
            Instantiate(_collectiblePrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }

    //used to destroy the object without caring for health. used for clearing the screen.
    public void PermaHit()
    {
        //TODO: destruction effect
        Destroy(gameObject);
    }
}
