using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAcidCheck : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField] Color floorColor;
    [SerializeField] Texture2D texture2D;
    [SerializeField] Texture texture;
    [SerializeField] Material material;
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
                if (hit.transform.gameObject.layer == 10)
                {
                    material = hit.transform.gameObject.GetComponent<MeshRenderer>().sharedMaterial;
                    texture = material.GetTexture("_Mask");
                    texture2D = new Texture2D(texture.width, texture.height);
                    StartCoroutine(CheckPixels());
                    texture2D.Apply();
                    floorColor = texture2D.GetPixel((int)hit.point.x, (int)hit.point.y);

                    Debug.Log(floorColor);
                }
            }
        }
    }

    IEnumerator CheckPixels()
    {
        yield return new WaitForEndOfFrame();
        texture2D.ReadPixels(new Rect(0f, 0f, texture.width, texture.height), 0, 0);
    }
}
