using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static src_Models;

public class src_CharacterController : MonoBehaviour
{

    private CharacterController _characterController;
    private DefaultInput _defaultInput;
    public Vector2 Input_Movement;
    public Vector2 Input_View;

    private Vector3 _newCameraRotation;
    private Vector3 _newCharacterRotation;

    [Header("References")] public Transform cameraHolder;
    
    [Header("Settings")] 
    public PlayerSettingsModel playerSettings;
    public float ViewClampYMin = -70;
    public float ViewClampYMax = 80;
    
    
    private void Awake()
    {
        _defaultInput = new DefaultInput();

        _defaultInput.Character.Movement.performed += e => Input_Movement = e.ReadValue<Vector2>();
        _defaultInput.Character.View.performed += e => Input_View = e.ReadValue<Vector2>();
        
        _defaultInput.Enable();

        _newCameraRotation = cameraHolder.localRotation.eulerAngles;
        _newCharacterRotation = transform.localRotation.eulerAngles;

        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        CalculateView();
        CalculateMovement();
    }

    private void CalculateView()
    {
        _newCameraRotation.y += playerSettings.ViewXSensitivity * (playerSettings.ViewXInverted ? -Input_View.x : Input_View.x) * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(_newCharacterRotation);
        
        _newCameraRotation.x += playerSettings.ViewYSensitivity * (playerSettings.ViewYInverted ? Input_View.y : -Input_View.y) * Time.deltaTime;
        _newCameraRotation.x = Mathf.Clamp(_newCameraRotation.x,ViewClampYMin,ViewClampYMax);
        
        cameraHolder.localRotation = Quaternion.Euler(_newCameraRotation);
    }
    
    private void CalculateMovement()
    {
        var verticalSpeed = playerSettings.WalkingForwardSpeed*Input_Movement.y*Time.deltaTime;
        var horizontalSpeed = playerSettings.WalkingStrafeSpeed*Input_Movement.x*Time.deltaTime;

        var newMovementSpeed = new Vector3(horizontalSpeed, 0, verticalSpeed);

        newMovementSpeed = cameraHolder.TransformDirection(newMovementSpeed);
        
        _characterController.Move(newMovementSpeed);
    }
}
