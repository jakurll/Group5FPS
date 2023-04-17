using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _mouseMovement;

    private Transform _parent;
    private float camXRotation;

    // Start is called before the first frame update
    void Start()
    {
        _parent = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float camX = Input.GetAxis("Mouse X") * _mouseMovement * Time.deltaTime;
        float camY = Input.GetAxis("Mouse Y") * _mouseMovement * Time.deltaTime;

        camXRotation -= camY;
        camXRotation = Mathf.Clamp(camXRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(camXRotation, 0f, 0f);
        _parent.Rotate(Vector3.up * camX);
    }
}
