Shader "Custom-Unlit/Circle"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Rotation ("Rotation In Radians", float) = 0
        _Percentage("Percentage", float) = 0

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
            float _Percentage;

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

            float2 toPolar(float2 cartesian){
                float distance = length(cartesian);
                float angle = atan2(cartesian.y, cartesian.x);
                return float2(angle / UNITY_TWO_PI, distance);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            

            float4 frag (v2f i) : SV_Target
            {
                //make input uvs centered and scaled to -1 to 1
                i.uv -= 0.5;
                i.uv *= 2;
                float2 uv = toPolar(i.uv);
                // float mask =  step(uv.x, _Percentage);  
                // return float4(frac(uv.x), frac(uv.y), 0, 1); //test outpu
                float x = frac(uv.x);
                return step(x, _Percentage);
                // return mask;

                // float4 col = tex2D(_MainTex, i.uv);
                return float4(i.uv.xy, 0, 1);
                // float mask = step(i.uv.y, _Percentage);
                // return mask; 
                // float d = distance(i.uv, float2(0,0));
                // float sdf = d - _Percentage;
                // if(sdf < 0 || sdf > .5)
                // {
                    //     discard;
                // }
                // sdf = step(sdf, .1);
                // return sdf;
                // return float4(d,d,d,1);

                // return col; 
            }
            
            ENDCG
        }
    }
}
