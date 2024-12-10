using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terresquall; // Virtual Joystick namespace

public class Control : MonoBehaviour
{
    private Vector3 _inputDirection; // Hareket y�n� i�in de�i�ken
    public VirtualJoystick joystick; // Joystick referans�
    private PlayerInput _playerinput; // PlayerInput referans�
    private float _rotationSpeed = 80f; // D�n�� h�z�

    private void Awake()
    {
        _playerinput = new PlayerInput();

        // Input sistemini kullanarak d�nmeyi tetikleyen eventler
        _playerinput.CylinderControl.Rotation.performed += x => RotateCylinder(x.ReadValue<Vector2>());
        _playerinput.CylinderControl.Rotation.canceled += x => RotateCylinder(x.ReadValue<Vector2>());
    }

    void RotateCylinder(Vector2 val)
    {
        // Silindirin d�n�� y�n�n� ayarl�yoruz
        _inputDirection = new Vector3(0, val.x, 0);
    }

    private void Update()
    {
        // Joystick girdisinden yatay eksende hareket al
        float horizontalInput = joystick.GetAxis("Horizontal");

        // Yatay eksen girdisiyle silindiri d�nd�r
        _inputDirection = new Vector3(0, horizontalInput, 0);

        // Silindirin d�n���n� uygulayal�m
        transform.Rotate(_inputDirection * Time.deltaTime * _rotationSpeed);
    }

    private void OnEnable()
    {
        // PlayerInput'i aktif hale getir
        _playerinput.CylinderControl.Enable();
    }

    private void OnDisable()
    {
        // PlayerInput'i devre d��� b�rak
        _playerinput.CylinderControl.Disable();
    }
}
