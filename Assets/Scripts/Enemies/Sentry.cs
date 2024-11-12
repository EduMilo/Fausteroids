using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Sentry : Enemy
{
    private Transform[] _positions;
    //sentry has to aim at the player and shoot at em, then after shooting move to another corner of the map.
    private int _currPosition;
    private bool _isMoving;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private int _timeBetweenShots;
    [SerializeField] private float _moveSpeed;

    private void Awake()
    {
        _isMoving = false;
        _currPosition = 0;
    }

    public void SetPositions(Transform[] spawnerPositions)
    {
        _positions = spawnerPositions;
    }

    private void Start()
    {
        Move();
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(_timeBetweenShots);
        Vector2 force = _player.gameObject.transform.position - transform.position;
        Bullet enemyBullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
        enemyBullet.Fire(force);
        Move();
        StartCoroutine(Shoot());
    }

    private void Move()
    {
        _currPosition++;
        if(_currPosition >= _positions.Length)
        {
            _currPosition = 0;
        }

        _isMoving = true;
    }


    private void Update()
    {
        if (_isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, _positions[_currPosition].position, _moveSpeed);
            if (transform.position == _positions[_currPosition].position)
            {
                _isMoving = false;
            }
        }
        
    }

    
    public override void Hit(int incomingDamage)
    {
        //TODO: cool sentry effects on death;
        base.Hit(incomingDamage);
    }

    //used to clear the screen.
    public override void PermaHit()
    {
        //TODO: cool sentry effect here too!
        base.PermaHit();
    }
}
