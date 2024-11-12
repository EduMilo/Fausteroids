using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Obstacle
{
    //If bombs are shot, they explode in a radius around itself. 
    [Header("Bomb Properties")]
    [SerializeField] private float _radius;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        //if the actual bomb collides with the player
        if (collision.gameObject.GetComponent<PlayerController>() || collision.gameObject.GetComponent<Bullet>())
        {
            Explode();
        }
    }

    private void Explode()
    {
        //TODO: explosion animation here?


        var collisionsInRadius = Physics2D.OverlapCircleAll(transform.position, _radius);
        foreach (var collision in collisionsInRadius)
        {
            //if player is in range, make em lose damage
            if (collision.gameObject.GetComponent<PlayerController>())
            {
                GameManager.Instance.LoseHealth(_damage);
            }
            //if obstacles are also in range, make em get hit
            //NOTE - this part also conveniently destroys itself :)
            if (collision.gameObject.GetComponent<Obstacle>())
            {
                collision.gameObject.GetComponent<Obstacle>().Hit(_damage);
            }

        }

    }
}
