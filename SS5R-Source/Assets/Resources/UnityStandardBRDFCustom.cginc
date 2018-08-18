// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

#include "UnityCG.cginc"
#include "UnityStandardConfig.cginc"
#include "UnityLightingCommon.cginc"

#include "UnityStandardBRDF.cginc"

sampler2D_float unity_NHxRoughnessCustom;
half3 BRDF3_DirectCustom(half3 diffColor, half3 specColor, half rlPow4, half smoothness)
{
    half LUT_RANGE = 16.0; // must match range in NHxRoughness() function in GeneratedTextures.cpp
    // Lookup texture to save instructions
    half specular = tex2D(unity_NHxRoughness, half2(rlPow4, SmoothnessToPerceptualRoughness(smoothness))).r * LUT_RANGE;
#if defined(_SPECULARHIGHLIGHTS_OFF)
    specular = 0.0;
#endif
    return diffColor + specular * specColor;
}

half3 BRDF3_IndirectCustom(half3 diffColor, half3 specColor, UnityIndirect indirect, half grazingTerm, half fresnelTerm)
{
    half3 c = indirect.diffuse * diffColor;
    c += indirect.specular * lerp (specColor, grazingTerm, fresnelTerm);
    return c;
}

// Old school, not microfacet based Modified Normalized Blinn-Phong BRDF
// Implementation uses Lookup texture for performance
//
// * Normalized BlinnPhong in RDF form
// * Implicit Visibility term
// * No Fresnel term
//
// TODO: specular is too weak in Linear rendering mode
half4 BRDF3_Unity_PBS_Custom (half3 diffColor, half3 specColor, half oneMinusReflectivity, half smoothness,
    float3 normal, float3 viewDir,
    UnityLight light, UnityIndirect gi)
{
    float3 reflDir = reflect (viewDir, normal);

    half nl = saturate(dot(normal, light.dir));
    half nv = saturate(dot(normal, viewDir));

    // Vectorize Pow4 to save instructions
    half2 rlPow4AndFresnelTerm = Pow4 (float2(dot(reflDir, light.dir), 1-nv));  // use R.L instead of N.H to save couple of instructions
    half rlPow4 = rlPow4AndFresnelTerm.x; // power exponent must match kHorizontalWarpExp in NHxRoughness() function in GeneratedTextures.cpp
    half fresnelTerm = rlPow4AndFresnelTerm.y;

    half grazingTerm = saturate(smoothness + (1-oneMinusReflectivity));

    half3 color = BRDF3_DirectCustom(diffColor, specColor, rlPow4, smoothness);
    color *= light.color;
    color *= min(pow(light.color, 1.5) * 20, 2) * (nl * .4 + .6);
    // color *= (light.color) * (nl * .1 + .9);
    // color *= light.color * nl;
    color += BRDF3_IndirectCustom(diffColor, specColor, gi, grazingTerm, fresnelTerm);

    return half4(color, 1);
}

