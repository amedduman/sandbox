Shader "Custom-Unlit/Rotation"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Rotation ("Rotation In Radians", float) = 0

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
            float4 _Color;
            float _Rotation;

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

            float2 Rotate(float degreeInRadians, float2 uv)
            {
                uv = uv * 2 - 1;

                float c = cos(_Rotation);
                float s = sin(_Rotation);
                float2x2 roataionMatrix = float2x2(c,-s,
                                                   s,c);
                uv =  mul(roataionMatrix, uv.xy); 

                return uv * .5 + .5;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv.xy = Rotate(_Rotation, o.uv);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            
            ENDCG
        }
    }
}
