Shader "Custom-Unlit/DissolveGlow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MaskTex ("Mask Texture", 2D) = "white" {}
        _Appear ("Appear", Range(0,1)) = 1
        _Color ("Color", Color) = (1,1,1,1)
        [Enum(UnityEngine.Rendering.BlendMode)]
        _SrcFactor ("Src Factor", Float) = 5
        [Enum(UnityEngine.Rendering.BlendMode)]
        _DstFactor ("Dst Factor", Float) = 10
        [Enum(UnityEngine.Rendering.BlendOp)]
        _Opp ("Operation", Float) = 0
    }
    
    SubShader
    {
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
            sampler2D _MaskTex;
            float4 _MaskTex_ST;
            float _Appear;
            float4 _Color;

            struct appdata // mesh data
            {
                float4 vertex : POSITION; // object space position
                float2 uv : TEXCOORD0;
            };

            struct v2f // vert to frag
            {
                float2 uv : TEXCOORD0;
                float2 mask_uv : TEXCOOR1;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.mask_uv = TRANSFORM_TEX(v.uv, _MaskTex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);
                float4 maskCol = tex2D(_MaskTex, i.mask_uv);

                float maskInner = maskCol.r >= _Appear + .05;
                maskInner = lerp(0, maskInner, col.a);

                float maskOuter = maskCol.r >= _Appear;
                maskOuter = lerp(0, maskOuter, col.a);

                float maskDiff = maskOuter - maskInner;
                
                return maskInner * col + maskDiff * _Color;
            }
            
            ENDCG
        }
    }
}
