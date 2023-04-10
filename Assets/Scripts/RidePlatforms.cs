using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidePlatforms : MonoBehaviour
{
    private CharacterController _controller;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.tag == "Platform")
            {
                Rigidbody rb = hit.transform.GetComponent<Rigidbody>();

                _controller.transform.position += rb.velocity * Time.deltaTime;
            }
        }
    }
}
