void MainLight_float(half3 SpecColor, half Smoothness, float3 WorldPos, half3 WorldNormal, half3 WorldView, out half3 Diffuse, out half3 Specular)
{
   half3 diffuseColor = 0;
   half3 specularColor = 0;

#ifndef SHADERGRAPH_PREVIEW
   Smoothness = exp2(10 * Smoothness + 1);
   WorldNormal = normalize(WorldNormal);
   WorldView = SafeNormalize(WorldView);
   Light light = GetMainLight();
#if SHADOWS_SCREEN
   half4 clipPos = TransformWorldToHClip(WorldPos);
   half4 shadowCoord = ComputeScreenPos(clipPos);
#else
   half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
#endif
   half3 attenuatedLightColor = light.color * (light.distanceAttenuation * light.shadowAttenuation);
   diffuseColor += LightingLambert(attenuatedLightColor, light.direction, WorldNormal);
   specularColor += LightingSpecular(attenuatedLightColor, light.direction, WorldNormal, WorldView, half4(SpecColor, 0), Smoothness);
#endif

   Diffuse = diffuseColor;
   Specular = specularColor;
}