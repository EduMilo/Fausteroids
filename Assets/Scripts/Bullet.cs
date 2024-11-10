using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] float _duration = 10f;
    [SerializeField] float _speed = 20.0f;
    [SerializeField] int _damage = 1;

    private Rigidbody2D _rb;


    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController pc))
        {
            pc.Hurt(_damage);

        }

        Destroy(gameObject);
    }

    public void Fire(Vector2 dir)
    {
        _rb.AddForce(dir * _speed);
        Destroy(gameObject, _duration);
    }

}
