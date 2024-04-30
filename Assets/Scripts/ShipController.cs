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
    public float AxisAuthorityThreshold = 0.1f;
    public float MaximumPitch = 80f;
    public float SensitivityYaw = 40;
    public float SensitivityPitch = 25;

    private Rigidbody _rb;
    private InputAction Aim => Actions.FindAction("Aim", true);

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
        Handles.Label(positionHandle, $"\ud83d\ude80 ({_velocity})");
    }

    #endregion

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _motion = Vector3.zero;
    }

    private void Update()
    {
        UpdateVelocity();
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        Vector2 aimDelta = Aim.ReadValue<Vector2>();
        
        float incrementYaw = aimDelta.x * SensitivityYaw * Time.deltaTime;
        float incrementPitch = aimDelta.y * SensitivityPitch * Time.deltaTime;

        Debug.Log($"AimDelta: Yaw[{aimDelta.x}], Pitch[{aimDelta.y}]");

        if ((incrementPitch > 0 && incrementPitch < AxisAuthorityThreshold) || (incrementPitch < 0 && incrementPitch > -AxisAuthorityThreshold))
            incrementPitch = 0;
        if ((incrementYaw > 0 && incrementYaw < AxisAuthorityThreshold) || (incrementPitch < 0 && incrementPitch > -AxisAuthorityThreshold))
            incrementYaw = 0;

        _yaw += incrementYaw;
        _pitch -= incrementPitch;
        _pitch = Mathf.Clamp(_pitch, -MaximumPitch, MaximumPitch);

        //Quaternion targetRotation = Quaternion.AngleAxis(_pitch, Vector3.right) * Quaternion.AngleAxis(_yaw, Vector3.up);
        Vector3 targetRotation = (Vector3.right * _pitch) + (Vector3.up * _yaw);
        // targetRotation = Vector3.Lerp(transform.rotation.eulerAngles, targetRotation, Time.deltaTime * TorqueSpeed);
        transform.eulerAngles = targetRotation;
        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * TorqueSpeed);
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
