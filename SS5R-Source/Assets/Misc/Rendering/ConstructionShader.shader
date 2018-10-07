Shader "Custom/ConstructionShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _MainTex2 ("Albedo2 (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _ConstructY ("ConstructY", Range(0,1)) = 0.0
        _ConstructGap ("ConstructGap", Range(0,1)) = 0.0
        _ConstructColor ("ConstructColor", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        Cull Off

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input {
            float2 uv_MainTex;
            float2 uv2_MainTex2;
            float3 worldPos;
            float3 viewDir;
            float3 worldNormal;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _ConstructY;
        float _ConstructGap;
        fixed4 _ConstructColor;        

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)
        
        void surf (Input IN, inout SurfaceOutputStandard o) {
            if (IN.uv2_MainTex2.y > _ConstructY + _ConstructGap) {
                discard;
            }
            
            if (IN.uv2_MainTex2.y > _ConstructY || dot(IN.worldNormal, IN.viewDir) < 0)
            {
                o.Albedo = _ConstructColor.rgb;
                o.Alpha  = _ConstructColor.a;
                o.Emission = _ConstructColor.rgb;
            }
            else
            {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb;
                o.Alpha  = c.a;
            }

            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
