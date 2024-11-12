using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTarget : MonoBehaviour
{
    [SerializeField] private Transform _worldOrigin;
    [SerializeField] private float _rotationSpeed;
    void Update()
    {
        this.transform.RotateAround(_worldOrigin.position, Vector3.forward, _rotationSpeed * Time.deltaTime);
    }
}
