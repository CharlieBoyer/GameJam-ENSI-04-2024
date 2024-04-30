using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public float Sensitivity;

    private Rigidbody _rb;

    private Vector3 _motion;
    private float _yaw;
    private float _pitch;
    private float _roll;

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
    }

    private void Update()
    {
        _rb.AddForce(_motion, ForceMode.Acceleration);
        UpdateVelocity();
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        _pitch = Mathf.Clamp(_pitch, -80, 80);

        transform.eulerAngles = new Vector3(_pitch, _yaw, 0f);
    }

    private void UpdateVelocity()
    {
        if (_acc) _motion.z += VelocityIncrement * Time.deltaTime;
        else if (_dec) _motion.z -= VelocityIncrement * Time.deltaTime;

        _motion.z = Mathf.Clamp(_motion.z, 0, MaximumVelocity);
    }

    #region Actions

    public void OnMove(InputValue input)
    {
        Vector2 value = input.Get<Vector2>();

        _motion.x = value.x * BaseSpeedMultiplier * Time.deltaTime;
        _motion.y = value.y * BaseSpeedMultiplier * Time.deltaTime;
    }

    public void OnAim(InputValue input)
    {
        Vector2 mouseDelta = input.Get<Vector2>();

        _yaw += mouseDelta.x * Sensitivity * Time.deltaTime;
        _pitch += mouseDelta.y * Sensitivity * Time.deltaTime;
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
