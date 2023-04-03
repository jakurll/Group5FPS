using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlatformRiding : MonoBehaviour
{
    private CharacterController _controller;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        var ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        float maxDistance = (_controller.height / 2f) + 0.1f;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            var platform = hit.collider.gameObject.GetComponent<MovingPlatform>();

            if (platform != null)
            {
                transform.position += platform._velocity * Time.deltaTime;
            }
        }
    }
}
