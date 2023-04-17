using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BossAcidCheck : MonoBehaviour
{
    private CharacterController _controller;
    private GameObject _curGround;
    private Texture2D _texture2D;
    private Material _material;
    private float _distance;

    [SerializeField] Color targetColor;
    [SerializeField] float colorRange;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        UpdateCurrentGround();
    }

    private void FixedUpdate()
    {
        UpdateCurrentGround();
    }

    public void UpdateCurrentGround()
    {
        if (_controller.isGrounded)
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hit;     

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.layer == 10)
                {
                    // Get material from ground once per gameobject
                    if (hit.collider.gameObject != _curGround)
                    {
                        _curGround = hit.collider.gameObject;
                        _material = hit.transform.gameObject.GetComponent<MeshRenderer>().sharedMaterial;
                    }

                    if (_texture2D != null)
                    {
                        GetFloorColor(hit);
                        HurtPlayer();
                    }
                }
            }
        }
    }

    // Material -> Texture -> RenderTexture -> Texture2D
    public void GetTexture()
    {
        Texture _texture = _material.GetTexture("_Mask");
        RenderTexture rt = RenderTexture.GetTemporary(_texture.width, _texture.height, 32);
        Graphics.Blit(_texture, rt);
        RenderTexture.active = rt;
        _texture2D = new Texture2D(rt.width, rt.height, TextureFormat.RGBA32, false);
        _texture2D.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        _texture2D.Apply();
        RenderTexture.ReleaseTemporary(rt);
    }

    /// <summary>
    /// Get coordinates of texture from where the player is standing
    /// then use that to get the color from pixel at that coordinates 
    /// </summary>
    public void GetFloorColor(RaycastHit hit)
    {
        Vector2 _textureCoords = hit.textureCoord;

        Color floorColor = _texture2D.GetPixel((int)(_textureCoords.x * _texture2D.width), (int)(_textureCoords.y * _texture2D.height));

        _distance = Vector4.Distance(targetColor, floorColor);
        Debug.Log(_distance);
    }

    public void HurtPlayer()
    {
        if (_distance <= colorRange)
        {
            Debug.Log("Hurt PLayer");
        }
        else
        {
            return;
        }
    }
}
