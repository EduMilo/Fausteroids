using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float _duration = 10f;
    [SerializeField] private float _speed = 20.0f;
    [SerializeField] private int _damage = 1;

    private Rigidbody2D _rb;


    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            GameManager.Instance.LoseHealth(_damage);
            Destroy(gameObject);
        }

        //destroy on impact with obstacles and enemies
        if (collision.gameObject.GetComponent<Obstacle>())
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.GetComponent<Enemy>())
        {
            if(gameObject.tag != "EnemyBullets")
            {
                Destroy(gameObject);
            }
        }
    }

    public void Fire(Vector2 dir)
    {
        _rb.AddForce(dir * _speed);
        GetComponent<AudioSource>().Play(); 
        Destroy(gameObject, _duration);
    }

    public int GetDamage()
    {
        return _damage;
    }

}
