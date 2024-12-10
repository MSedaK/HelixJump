using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terresquall; // Virtual Joystick namespace

public class Control : MonoBehaviour
{
    private Vector3 _inputDirection; // Hareket yönü için deðiþken
    public VirtualJoystick joystick; // Joystick referansý
    private PlayerInput _playerinput; // PlayerInput referansý
    private float _rotationSpeed = 80f; // Dönüþ hýzý

    private void Awake()
    {
        _playerinput = new PlayerInput();

        // Input sistemini kullanarak dönmeyi tetikleyen eventler
        _playerinput.CylinderControl.Rotation.performed += x => RotateCylinder(x.ReadValue<Vector2>());
        _playerinput.CylinderControl.Rotation.canceled += x => RotateCylinder(x.ReadValue<Vector2>());
    }

    void RotateCylinder(Vector2 val)
    {
        // Silindirin dönüþ yönünü ayarlýyoruz
        _inputDirection = new Vector3(0, val.x, 0);
    }

    private void Update()
    {
        // Joystick girdisinden yatay eksende hareket al
        float horizontalInput = joystick.GetAxis("Horizontal");

        // Yatay eksen girdisiyle silindiri döndür
        _inputDirection = new Vector3(0, horizontalInput, 0);

        // Silindirin dönüþünü uygulayalým
        transform.Rotate(_inputDirection * Time.deltaTime * _rotationSpeed);
    }

    private void OnEnable()
    {
        // PlayerInput'i aktif hale getir
        _playerinput.CylinderControl.Enable();
    }

    private void OnDisable()
    {
        // PlayerInput'i devre dýþý býrak
        _playerinput.CylinderControl.Disable();
    }
}
