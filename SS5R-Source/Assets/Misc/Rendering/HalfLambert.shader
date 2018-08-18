// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/DiffuseClamped"
{
Properties {
	_MainTex ("Base (RGB)", 2D) = "white"{}
}
SubShader{
	Pass{
	Tags {"LightMode" = "PrePassBase"}
	CGPROGRAM
	#pragma vertex vert
	#pragma fragment frag		

	struct vIN
	{
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};
	struct vOUT
	{
		float4 pos : SV_POSITION;
		float3 wNorm : TEXCOORD0;
	};
	vOUT vert(vIN v)
	{
		vOUT o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.wNorm = mul((float3x3)unity_ObjectToWorld, v.normal);
		return o;
	}

	float4 frag(vOUT i) : COLOR
	{
		float3 norm = (i.wNorm * 0.5) + 0.5;
		return float4(norm, 0);
	}
	ENDCG
}
Pass{
	Tags{"LightMode" = "PrePassFinal"}
	ZWrite off
	CGPROGRAM
	#pragma vertex vert
	#pragma fragment frag

	sampler2D _MainTex;
	uniform sampler2D _LightBuffer;

	struct vIN
	{
		float4 vertex : POSITION;
		float2 texcoord : TEXCOORD0;
	};

	struct vOUT
	{
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
		float4 uvProj : TEXCOORD1;
	};
    vOUT vert(vIN v)
    {
        vOUT o;
        o.pos = UnityObjectToClipPos(v.vertex);
        
        float4 posHalf = o.pos * 0.5;
        posHalf.y *= _ProjectionParams.x;
        
        o.uvProj.xy = posHalf.xy + float2(posHalf.w, posHalf.w);
        o.uvProj.zw = o.pos.zw;
        o.uv = v.texcoord;
        
        return o;
    }
    float4 frag(vOUT i) : COLOR
    {
        float4 light = min(tex2Dproj(_LightBuffer, i.uvProj), 1);
        float4 texCol = tex2D(_MainTex, i.uv);
        return float4((texCol.xyz * light.xyz) * light.w, texCol.w);
    }
    ENDCG
}
Pass {
	Name "ShadowCaster"
	Tags { "LightMode" = "ShadowCaster" }
	
	Fog {Mode Off}
	Offset 1, 1

	CGPROGRAM
	#pragma vertex vert
	#pragma fragment frag
	#pragma multi_compile_shadowcaster
	#include "UnityCG.cginc"

	struct v2f { 
		V2F_SHADOW_CASTER;
	};

	v2f vert( appdata_base v )
	{
		v2f o;
		TRANSFER_SHADOW_CASTER(o)
		return o;
	}

	float4 frag( v2f i ) : SV_Target
	{
		SHADOW_CASTER_FRAGMENT(i)
	}
	ENDCG
} 
}
}