using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : Enemy
{
    [SerializeField] private float _moveSpeed;

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, GameManager.Instance.GetPlayerPosition(), _moveSpeed * Time.deltaTime);
        transform.up = GameManager.Instance.GetPlayerPosition() - transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            GameManager.Instance.LoseHealth(_damage);
        }

        if(collision.gameObject.GetComponent<Enemy>())
        {
            collision.gameObject.GetComponent<Enemy>().Hit(_damage);
        }

        if (collision.gameObject.GetComponent<Obstacle>())
        {
            collision.gameObject.GetComponent<Obstacle>().Hit(_damage);
        }

        if (collision.gameObject.GetComponent<Bullet>())
        {
            if (collision.gameObject.tag != "EnemyBullets")
            {
                Hit(collision.gameObject.GetComponent<Bullet>().GetDamage());
            }
        }
    }
}
