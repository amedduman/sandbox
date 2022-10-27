Shader "Custom-Unlit/Bar"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Frequency ("Frequency", float) = 50
        _Speed ("Speed", float) = 1
        _LineWidth ("Line Width", Range(0,1)) = .2
        [Enum(UnityEngine.Rendering.BlendMode)]
        _SrcFactor ("Src Factor", Float) = 5
        [Enum(UnityEngine.Rendering.BlendMode)]
        _DstFactor ("Dst Factor", Float) = 10
        [Enum(UnityEngine.Rendering.BlendOp)]
        _Opp ("Operation", Float) = 0
    }
    
    SubShader
    {
//        Tags { "RenderType"="Opaque" }
        Tags { "RenderType"="Transparent" }
        Blend [_SrcFactor] [_DstFactor]
        BlendOp [_Opp]
            
        Pass    
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _Frequency;
            float _Speed;
            float _LineWidth;

            struct appdata // mesh data
            {
                float4 vertex : POSITION; // object space position
                float2 uv : TEXCOORD0;
            };

            struct v2f // vert to frag
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);
                float mask = sin((i.uv.y * _Frequency) + _Time.y * _Speed) * .5 + .5;
                // float mask = saturate(sin((i.uv.y * _Frequency) + _Time.y * _Speed));
                mask = step(mask, _LineWidth);
                return lerp(float4(0,0,0,0), col, mask);
            }
            
            ENDCG
        }
    }
}
