using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _mouseMovement;

    private Transform _parent;
    private float camXRotation;
    Slider slider;

    // Get parent and lock cursor
    void Start()
    {
        _parent = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Get mouse coords then convert that into a clamp radius that the player can move the camera around
    void Update()
    {
        float camX = Input.GetAxis("Mouse X") * _mouseMovement * Time.deltaTime;
        float camY = Input.GetAxis("Mouse Y") * _mouseMovement * Time.deltaTime;

        camXRotation -= camY;
        camXRotation = Mathf.Clamp(camXRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(camXRotation, 0f, 0f);
        _parent.Rotate(Vector3.up * camX);

        //_mouseMovement = slider.value;
    }
}
