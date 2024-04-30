using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class AsteroidFlotingimpulse : MonoBehaviour
{
    public int MaxImpulsePower;
    public int MinImpulsePower;
    public float MaxRotationSpeed;
    public float MinRotationSpeed;
    private Rigidbody _rb;
    private Vector3 _randomRotation;
    private float _rotationSpeed;
    private void Start()
    {
        _rb = gameObject.transform.GetComponent<Rigidbody>();
        _randomRotation = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        _rotationSpeed = Random.Range(MinRotationSpeed, MaxRotationSpeed);
    }

    private void Update()
    {
        AddRotation();
        if (_rb.velocity == Vector3.zero)
        {
            AddImpulse();
        }
    }

    public void AddImpulse()
    {
        Vector3 RandomDirection = new Vector3(Random.Range(0, 1), Random.Range(0, 1), Random.Range(0, 1));
        _rb.AddForce(Vector3.forward * Random.Range(MinImpulsePower,MaxImpulsePower),ForceMode.Impulse);
    }

    public void AddRotation()
    {
       gameObject.transform.Rotate(_randomRotation*_rotationSpeed);
        
    }
}
