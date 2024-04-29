using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class ShipController : MonoBehaviour
{
    [Header("Velocity")]
    public float BaseSpeed;
    public float VelocityIncrement;
    public float MaximumVelocity;
    
    [Header("Rotations")]
    public float MaximumRoll;
    public float MaximumPitch;
    public float MaximumYaw;

    private Rigidbody _rb;

    private Vector3 _motion;
    private Vector3 _angle;

    private bool _acc;
    private bool _dec;

    #region Debug

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
            
        Vector3 positionHandle = transform.position + Vector3.up * 0.6f;
        Handles.Label(positionHandle, $"\ud83d\ude80 ({_motion.x}, {_motion.y}, {_motion.z})");

        Vector3 rotationHandle = transform.position + Vector3.up * -0.1f;
        Handles.Label(rotationHandle, $"\ud83d\udd04 ({_angle.x}, {_angle.y}, {_angle.z})");
    }

    #endregion

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _motion = Vector3.zero;
        _angle = Vector3.zero;
    }

    private void Update()
    {
        _rb.AddForce(_motion, ForceMode.Acceleration);
        UpdateVelocity();
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        switch (_angle) // Roll
        {
            case Vector3 { x: < 0 }:
                
            case Vector3 { x: > 0 }:

            case Vector3 { y: < 0 }:
                break;
            case Vector3 { y: > 0 }:
                break;
        }
    }

    private void UpdateVelocity()
    {
        if (_acc) _motion.z += VelocityIncrement * Time.deltaTime;
        else if (_dec) _motion.z -= VelocityIncrement * Time.deltaTime;

        _motion.z = Mathf.Clamp(_motion.z, 0, MaximumVelocity);
    }

    public void OnMove(InputValue input)
    {
        Vector2 value = input.Get<Vector2>();

        _motion.x = value.x * BaseSpeed * Time.deltaTime;
        _motion.y = value.y * BaseSpeed * Time.deltaTime;
    }

    public void OnAim(InputValue input)
    {
        
    }

    public void OnAccelerate() {
        _acc = !_acc;
        if (_acc && _dec) _dec = false;
    }

    public void OnBrake() {
        _dec = !_dec;
        if (_dec && _acc) _acc = false;
    }
}
