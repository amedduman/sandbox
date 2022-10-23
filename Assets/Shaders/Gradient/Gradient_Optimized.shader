Shader "Custom-Unlit/Gradient_Optimized"
{
    Properties
    {
        _ColorLeft ("Color Left", Color) = (1,1,1,1)
        _ColorRight ("Color Right", Color) = (1,1,1,1)
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            float4 _ColorLeft;
            float4 _ColorRight;

            struct appdata // mesh data
            {
                float4 vertex : POSITION; // object space position
                float4 uv : TEXCOORD0;
            };

            struct v2f // vert to frag
            {
                float4 color : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = lerp(_ColorLeft, _ColorRight, v.uv.x);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                return i.color;
            }
            
            ENDCG
        }
    }
}
