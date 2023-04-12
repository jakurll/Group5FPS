using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAcidCheck : MonoBehaviour
{
    private CharacterController _controller;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_controller.isGrounded)
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                var texture = GetComponent<Material>().GetTexture("_Mask");
                var texture2D = (Texture2D)texture;
                Color floorColor = texture2D.GetPixel((int)hit.point.x, (int)hit.point.y);

                
            }
        }
    }
}
