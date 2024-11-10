using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Attributes")]
    [SerializeField] float _thrusterSpeed = 1.0f;
    [SerializeField] float _turnSpeed = 1.0f;
    [SerializeField] int _hitPoints = 3;

    [Header("Weapon")]
    [SerializeField] Transform _blaster;
    [SerializeField] Bullet _bulletPrefab;


    private Rigidbody2D _rb;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            _rb.AddForce(_thrusterSpeed * Input.GetAxis("Vertical") * transform.up);
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            _rb.AddTorque(Input.GetAxis("Horizontal") * _turnSpeed * -1);
        }

        //shooting
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Bullet bullet = Instantiate(_bulletPrefab, _blaster.position, _blaster.rotation);
        bullet.Fire(_blaster.up);
    }

    public void Hurt(int damage)
    {
        _hitPoints -= damage;
        if (_hitPoints < 0)
        {
            Destroy(gameObject);
        }
        Debug.Log("YEOUCH!! player got hurt!");
    }
}
