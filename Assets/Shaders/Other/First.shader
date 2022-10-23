Shader "Custom-Unlit/First"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _LineWidth ("Line Width", Range(0,1)) = 1
        _LineCol ("Line Color", Color) = (1,1,1,1) 
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

            float4 _Color;
            float4 _MainTex_ST;
            float _LineWidth;
            float4 _LineCol;
            float _Offset;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float l = i.uv.y;
                float mask = step(l, _LineWidth);
                float4 lineCol = _LineCol * mask;

                float4 col = lerp(_Color, lineCol, mask);
                return col;
            }
            
            ENDCG
        }
    }
}
