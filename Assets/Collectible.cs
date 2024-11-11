using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private int _value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController p))
        {
            GameManager.Instance.AddScore(_value);
            Destroy(gameObject);
        }
    }
}
