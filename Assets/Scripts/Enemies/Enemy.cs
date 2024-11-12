using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int _health;
    [SerializeField] protected int _damage;
    [SerializeField] protected int _scoreValue;
    [SerializeField] protected GameObject _player; //we give enemy access to the whole player so it can get updated locations if need be.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>())
        {
            GameManager.Instance.LoseHealth(_damage);
        }

        if (collision.gameObject.GetComponent<Bullet>())
        {
            if(collision.gameObject.tag != "EnemyBullets")
            {
                Hit(collision.gameObject.GetComponent<Bullet>().GetDamage());
            }
        }
    }

    public virtual void Hit(int incomingDamage)
    {
        _health -= incomingDamage;
        if (_health <= 0)
        {
            GameManager.Instance.AddScore(_scoreValue);
            Destroy(gameObject);
        }
    }

    public virtual void PermaHit()
    {
        Destroy(gameObject);
    }
}
