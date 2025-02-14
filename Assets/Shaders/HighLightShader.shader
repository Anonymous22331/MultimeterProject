Shader "Custom/HighLightShader"
{
     Properties
    {
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth ("Outline Width", Range(0.001, 0.03)) = 0.01
        _Color ("Main Color (with Alpha)", Color) = (1,1,1,1)
        _MainTex ("Base Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        
        Pass
        {
            Name "OUTLINE"
            Tags { "LightMode"="Always" }
            Cull Front  
            ZWrite On
            ZTest LEqual

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            float _OutlineWidth;
            float4 _OutlineColor;

            v2f vert (appdata_t v)
            {
                v2f o;
                float3 norm = normalize(v.normal);
                v.vertex.xyz += norm * _OutlineWidth; 
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }
        
        CGPROGRAM
        #pragma surface surf Standard alpha:blend

        sampler2D _MainTex;
        fixed4 _Color;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 texColor = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = texColor.rgb;
            o.Alpha = texColor.a;
        }
        ENDCG
    }
    FallBack "Transparent/Diffuse"
}
