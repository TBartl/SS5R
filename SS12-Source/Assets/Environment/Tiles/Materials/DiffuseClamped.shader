Shader "Custom/DeferredDiffuse"
{
Properties 
{
	_MainTex ("Base (RGB)", 2D) = "white" {}
}
SubShader 
{
	Tags { "RenderType"="Opaque" }
	LOD 200
	
	CGPROGRAM
	#pragma surface surf MyDiffuse

	sampler2D _MainTex;

	struct Input {
		float2 uv_MainTex;
	};

	void surf (Input IN, inout SurfaceOutput o) {
		half4 c = tex2D (_MainTex, IN.uv_MainTex);
		o.Albedo = c.rgb;
		o.Alpha = c.a;
	}
	
	float4 LightingMyDiffuse_PrePass(SurfaceOutput i, float4 light)
	{
		return float4(i.Albedo * min(light.rgb, 1), 1.0);
	}
	ENDCG
} 
}