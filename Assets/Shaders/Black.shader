Shader "Custom/Black"
{
    Properties 
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Color ("Color (RGBA)", Color) = (1, 1, 1, 0.34)
    }
    SubShader 
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        ZWrite Off
        ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Stencil
        {
            Ref 5
            Comp Always
            Pass Replace
        }

        Pass 
        {
            CGPROGRAM

            #pragma vertex vert alpha
            #pragma fragment frag alpha

            #include "UnityCG.cginc"

            struct v2f {
                float4  pos : SV_POSITION;
                float2  uv : TEXCOORD0;
            };

            float4 _Color;
            sampler2D _MainTex;
		    float4 _MainTex_ST;

            v2f vert (appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos (v.vertex);
                o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
                return o;
            }

            half4 frag (v2f i) : COLOR
            {
                half4 texcol = tex2D (_MainTex, i.uv);
		        return texcol * _Color;
            }

            ENDCG
        }
    }
}
