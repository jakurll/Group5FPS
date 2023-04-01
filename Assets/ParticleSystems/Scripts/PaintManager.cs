using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PaintManager : MonoBehaviour
{
    [SerializeField] private Shader _painter;
    public Material _paintMaterial;

    //[SerializeField] private Shader _islands;
    //private Material _extendMaterial;

    // Initialize variables from shaders
    private int _prepareUVID = Shader.PropertyToID("_PrepareUV");
    private int _positionID = Shader.PropertyToID("_PaintPosition");
    private int _hardnessID = Shader.PropertyToID("_Hardness");
    private int _StrengthID = Shader.PropertyToID("_Strength");
    private int _radiusID = Shader.PropertyToID("_Radius");
    private int _colorID = Shader.PropertyToID("_PainterColor");
    private int _textureID = Shader.PropertyToID("_MainTex");
    //private int _uvOffsetID = Shader.PropertyToID("_OffsetUV");
    //private int _uvIslandsID = Shader.PropertyToID("_UVIslands");

    public CommandBuffer _command;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        _paintMaterial = new Material(_painter);
        //_extendMaterial = new Material(_islands);

        // CommandBuffer allows us to define when and what order to render certain things
        // Paint method is what will fill the command buffer with commands to execute
        _command = new CommandBuffer();
        _command.name = "CommandBuffer - " + gameObject.name;
    }

    // Sends all the textures to the command buffer
    public void InitTextures(Paintable paintable)
    {
        RenderTexture _mask = paintable.getMask();
        RenderTexture _support = paintable.getSupport();
        //RenderTexture uvIslands = paintable.getUVIslands();
        //RenderTexture extend = paintable.getExtend();
        Renderer _rend = paintable.getRenderer();

        _command.SetRenderTarget(_mask);
        //_command.SetRenderTarget(extend);
        _command.SetRenderTarget(_support);

        _paintMaterial.SetFloat(_prepareUVID, 1);
        //_command.SetRenderTarget(uvIslands);
        _command.DrawRenderer(_rend, _paintMaterial, 0);

        Graphics.ExecuteCommandBuffer(_command);
        _command.Clear();
    }

    // This method is to be called when wanting to paint something in world space. (Sends info to command buffer)
    public void Paint(Paintable paintable, Vector3 pos, float radius = 1f, float hardness = .5f, 
        float strength = .5f, Color? color = null)
    {
        RenderTexture _mask = paintable.getMask();
        RenderTexture _support = paintable.getSupport();
        //RenderTexture _uvIslands = paintable.getUVIslands();
        //RenderTexture _extend = paintable.getExtend();
        Renderer _rend = paintable.getRenderer();

        _paintMaterial.SetFloat(_prepareUVID, 0);
        _paintMaterial.SetVector(_positionID, pos);
        _paintMaterial.SetFloat(_hardnessID, hardness);
        _paintMaterial.SetFloat(_StrengthID, strength);
        _paintMaterial.SetFloat(_radiusID, radius);
        _paintMaterial.SetTexture(_textureID, _support);
        _paintMaterial.SetColor(_colorID, color ?? Color.yellow);
        //_extendMaterial.SetFloat(_uvOffsetID, paintable._extendsIslandsOffset);
        //_extendMaterial.SetTexture(_uvIslandsID, uvIslands);

        _command.SetRenderTarget(_mask);
        _command.DrawRenderer(_rend, _paintMaterial, 0);

        _command.SetRenderTarget(_support);
        _command.Blit(_mask, _support);

        //_command.SetRenderTarget(_extend);
        //_command.Blit(_mask, _extend, _extendMaterial);

        Graphics.ExecuteCommandBuffer(_command);
        _command.Clear();
    }
}
