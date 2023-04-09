Shader "Unlit/Painter"
{
    // Source: https://www.youtube.com/watch?v=YUWfHX_ZNCw
    Properties
    {
        _PaintColor ("Paint Color", Color) = (0, 0, 0, 0)
    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float3 _PaintPosition;
            float _Radius;
            float _Hardness;
            float _Strength;
            float4 _PainterColor;
            float _PrepareUV;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 worldPos : TEXTCOORD1;
            };

            // Keeps track of where paint is being painted and the world position of each fragment
            float mask(float3 position, float3 center, float radius, float hardness)
            {
                float m = distance(center, position);
                // modulate using hardness and radius
                return 1 - smoothstep(radius * hardness, radius, m);
            }

            v2f vert (appdata v)
            {
                v2f o;
                // gives us world position by doing matrix multiplication of the current matrix and vertex of screen position
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.uv = v.uv;
                float4 uv = float4(0, 0, 0, 1);
                // Remap UV with a range between 0 - 1. This is because the rasterizer expects these coordinates
                // ProjectionParams: Built in Unity variable that lets us reflect rendering of a projection matrix
                
                // This was the line provided: uv.xy = (v.uv.xy * 2 - 1) * float2(1, _ProjectionParams.x);
                // And this is the line in the code! I'm sure they do the same things but still not sure
                uv.xy = float2(1, _ProjectionParams.x) * (v.uv.xy * float2(2, 2) - float2(1, 1));

                o.vertex = uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Not sure what this if statement does! Never explained.
                if (_PrepareUV > 0)
                {
                    return float4(0, 0, 0, 0);
                }

                // sample the texture
                float4 col = tex2D(_MainTex, i.uv);
                // Call our mask function and assign it to m
                float m = mask(i.worldPos, _PaintPosition, _Radius, _Hardness);
                // Add the strength to our mask as well
                float edge = m * _Strength;
                // Return lerp between texture color and desired color
                return lerp(col, _PainterColor, edge);
            }
            ENDCG
        }
    }
}
