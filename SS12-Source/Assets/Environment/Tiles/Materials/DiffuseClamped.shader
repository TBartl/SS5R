// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Simplified Diffuse shader. Differences from regular Diffuse one:
// - no Main Color
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.

Shader "Mobile/Diffuse" {
Properties {
    _MainTex ("Base (RGB)", 2D) = "white" {}
}
SubShader {
    Tags { "RenderType"="Opaque" }
    LOD 150

CGPROGRAM
#pragma surface surf LambertClamped 

half4 LightingLambertClamped( SurfaceOutput s, half3 lightDir, half atten )
{
   half NdotL = dot( s.Normal, lightDir );
   half4 c;
   c.rgb = s.Albedo * min(_LightColor0.rgb * NdotL * atten, 1);
   c.a = s.Alpha;
   return c;
}

sampler2D _MainTex;

struct Input {
    float2 uv_MainTex;
};

void surf (Input IN, inout SurfaceOutput o) {
    fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
    o.Albedo = c.rgb;
    o.Alpha = c.a;
}
ENDCG
}

Fallback "Mobile/VertexLit"
}
