using System;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Rigidbody))]
public class ShipController : MonoBehaviour
{
    [Header("Inputs")]
    public InputActionAsset Actions;
    
    [Header("Velocity")]
    public float BaseSpeedMultiplier;
    public float VelocityIncrement;
    public float MaximumVelocity;

    [Header("Torque")]
    public float TorqueSpeed = 10f;
    public float Sensitivity;

    private Rigidbody _rb;
    private InputAction _aim;

    private Vector3 _motion;
    private float _yaw;
    private float _pitch;
    private float _roll;
    private float _velocity;

    private bool _acc;
    private bool _dec;

    #region Debug

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
            
        Vector3 positionHandle = transform.position + Vector3.up * 0.6f;
        Handles.Label(positionHandle, $"\ud83d\ude80 ({_motion.x}, {_motion.y}, {_motion.z})");

        Vector3 rotationHandle = transform.position + Vector3.up * -0.1f;
        Handles.Label(rotationHandle, $"\ud83d\udd04 ({_pitch}, {_yaw}, {_roll})");
    }

    #endregion

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _motion = Vector3.zero;
        _aim = Actions.FindAction("Aim", true);

        transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        UpdateVelocity();
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        _pitch = Mathf.Clamp(_pitch, -80, 80);

        Quaternion targetRotation = Quaternion.AngleAxis(_pitch, Vector3.right) * Quaternion.AngleAxis(_yaw, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * TorqueSpeed);
    }

    private void UpdateVelocity()
    {
        if (_acc) _velocity += VelocityIncrement * Time.deltaTime;
        else if (_dec) _velocity -= VelocityIncrement * Time.deltaTime;

        _motion = transform.forward * Mathf.Clamp(_velocity, 0, MaximumVelocity);
        
        _rb.AddForce(_motion, ForceMode.Acceleration);
    }

    #region Actions
    
    public void OnAim(InputValue input)
    {
        Vector2 mouseDelta = input.Get<Vector2>();
        
        _yaw += mouseDelta.x * Sensitivity * Time.deltaTime;
        _pitch -= mouseDelta.y * Sensitivity * Time.deltaTime;
    }

    public void OnAccelerate() {
        _acc = !_acc;
        if (_acc && _dec) _dec = false;
    }

    public void OnBrake() {
        _dec = !_dec;
        if (_dec && _acc) _acc = false;
    }

    #endregion
}
