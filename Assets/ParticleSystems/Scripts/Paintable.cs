using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Paintable : MonoBehaviour
{
    // SOURCE: https://www.youtube.com/watch?v=YUWfHX_ZNCw, https://www.youtube.com/watch?v=FR618z5xEiM.

    // Paintable Class will initialize all information needed by
    // the buffer in the PaintManager class and will represent
    // what we can acutally paint on.

    const int TEXTURE_SIZE = 1024;

    //[SerializeField] private float _extendIslandOffset = 1;

    // Render textures are textures that can be rendered to
    private RenderTexture _maskRenderTexture;
    private RenderTexture _supportTexture;
    // private RenderTexture _uvIslandsRenderTexture;
    // private RenderTexture _extendIsalndsRenderTexture;

    private Renderer _rend;
    public PaintManager _paintManager;

    private int _maskTextureID = Shader.PropertyToID("_Mask");

    public RenderTexture getMask() => _maskRenderTexture;
    public RenderTexture getSupport() => _supportTexture;
    // public RenderTexture getUVIslands() => uvIslandsRenderTexture;
    // public RenderTexture getExtend() => extendIslandsRenderTexture;
    public Renderer getRenderer() => _rend;

    // Create all the textures and send them to the CommandBuffer
    // filtermode.bilinear is how it is sampled when scaled down, bilinear being the average setting
    void Start()
    {
        _maskRenderTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0);
        _maskRenderTexture.filterMode = FilterMode.Bilinear;
        
        _supportTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0);
        _supportTexture.filterMode = FilterMode.Bilinear;

        //_extendIslandsRenderTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0);
        //_extendIslandsRenderTexture.filterMode = FilterMode.Bilinear;

        //_uvIslandsRenderTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0);
        //_uvIslandsRenderTexture.filterMode = FilterMode.Bilinear;

        _rend = GetComponent<Renderer>();
        _rend.material.SetTexture(_maskTextureID, _maskRenderTexture);

        _paintManager.InitTextures(this);
    }

    // Release resources used by RenderTexture as to not collect garbage
    private void OnDisable()
    {
        _maskRenderTexture.Release();
        _supportTexture.Release();
        //uvIslandsRenderTexture.Release();
        //extendIslandsRenderTexture.Release();
    }
}
