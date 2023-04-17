using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  [SerializeField] private float _mouseMovement;

  private Transform parent;

  // Start is called before the first frame update
  void Start()
  {
    parent = transform.parent;
    Cursor.lockState = CursorLockMode.Locked;
  }

  // Update is called once per frame
  void Update()
  {
    float movement = Input.GetAxis("Mouse X") * _mouseMovement * Time.deltaTime;

    parent.Rotate(Vector3.up, movement);
  }
}
