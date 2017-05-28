Shader "Hardsurface/Decal/Deferred Packed" 
{
	Properties 
	{
		_ColorMain ("Dielectric albedo/Metal specular", Color) = (1,1,1,1)
		_ColorEmission ("Emission color", Color) = (0,0,0,0)
		_ColorEmissionMultiplier ("Emission multiplier (HDR)", Range(1,16)) = 1.0

		_MainTex ("Emission mask (R)/Smoothness mask (G)/AO (B)/Alpha (A)", 2D) = "white" {}
		_BumpMap ("Normal map", 2D) = "bump" {}

		_SmoothnessLow ("Smoothness (Low)", Range(0,1)) = 0.5
		_SmoothnessHigh ("Smoothness (High)", Range(0,1)) = 0.5
		_Metalness ("Metalness", Range(0,1)) = 0.0

		_ContributionDiffuseSpecular ("Blend albedo/spec (RT0+1)", Range(0,1)) = 0.0
		_ContributionNormal ("Blend normals (RT2)", Range(0,1)) = 1.0
		_ContributionEmission ("Blend emission (RT3)", Range(0,1)) = 0.0

		_ContributionEmissionMask ("Output emission mask", Range(0,1)) = 0.0
		_ContributionOcclusion ("Output AO", Range(0,1)) = 1.0
		_ContributionCavity ("Output AO as cavity", Range(0,1)) = 1.0

		_MaskAlphaUsingAO ("Mask alpha with AO", Range(0,1)) = 0.0
		_MaskAlphaPower ("Mask alpha power", Range(1,4)) = 1.0
		_MaskAlphaShift ("Mask alpha shift", Range(0,1)) = 0.0
	}
	SubShader 
	{
		Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="Opaque" "ForceNoShadowCasting"="True"}
		LOD 300
		Offset -1, -1

		Blend SrcAlpha OneMinusSrcAlpha, Zero OneMinusSrcAlpha

		CGPROGRAM

		#pragma surface surf Standard finalgbuffer:DecalFinalGBuffer exclude_path:forward exclude_path:prepass noshadow noforwardadd keepalpha
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;

		struct Input 
		{
			float2 uv_MainTex;
		};

		fixed3 _ColorMain;
		fixed3 _ColorEmission;
		half _ColorEmissionMultiplier;
		half _Metalness;

		half _ContributionDiffuseSpecular;
		half _ContributionNormal;
		half _ContributionEmission;
		half _ContributionEmissionMask;
		half _ContributionOcclusion;
		half _ContributionCavity;

		half _MaskAlphaUsingAO;
		half _MaskAlphaPower;
		half _MaskAlphaShift;

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			fixed4 main = tex2D(_MainTex, IN.uv_MainTex);
			fixed3 normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));

			o.Alpha = main.w;
			o.Albedo = _ColorMain * lerp (1, main.z, _ContributionCavity);
			o.Normal = normal;
			o.Metallic = _Metalness;
			o.Smoothness = 1 - o.Alpha;
			o.Occlusion = lerp (1, main.z, _ContributionOcclusion);
			o.Emission = lerp (0, main.x * _ColorEmission * _ColorEmissionMultiplier, _ContributionEmissionMask);
		}

		void DecalFinalGBuffer (Input IN, SurfaceOutputStandard o, inout half4 diffuse, inout half4 specSmoothness, inout half4 normal, inout half4 emission)
		{
			float alphaMultiplier = lerp (1, 1 - pow (saturate (o.Occlusion + _MaskAlphaShift), _MaskAlphaPower), _MaskAlphaUsingAO);

			diffuse.a = o.Alpha * alphaMultiplier * _ContributionDiffuseSpecular;
			specSmoothness.a = o.Alpha * alphaMultiplier * _ContributionDiffuseSpecular;
			normal.a = o.Alpha * _ContributionNormal; 
			emission.a = o.Alpha * alphaMultiplier * _ContributionEmission;
		}

		ENDCG

		Blend One One
		ColorMask A

		CGPROGRAM

		#pragma surface surf Standard finalgbuffer:DecalFinalGBuffer exclude_path:forward exclude_path:prepass noshadow noforwardadd keepalpha
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input 
		{
			float2 uv_MainTex;
		};

		half _SmoothnessLow;
		half _SmoothnessHigh;
		half _ContributionDiffuseSpecular;
		half _ContributionOcclusion;
		
		half _MaskAlphaUsingAO;
		half _MaskAlphaPower;
		half _MaskAlphaShift;

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			fixed4 main = tex2D(_MainTex, IN.uv_MainTex);

			o.Alpha = main.w;
			o.Occlusion = lerp (1, main.z, _ContributionOcclusion);
			o.Smoothness = saturate (lerp (_SmoothnessLow, _SmoothnessHigh, main.y));
		}

		void DecalFinalGBuffer (Input IN, SurfaceOutputStandard o, inout half4 diffuse, inout half4 specSmoothness, inout half4 normal, inout half4 emission)
		{
			float alphaMultiplier = lerp (1, 1 - pow (saturate (o.Occlusion + _MaskAlphaShift), _MaskAlphaPower), _MaskAlphaUsingAO);

			specSmoothness.a *= o.Alpha * alphaMultiplier * _ContributionDiffuseSpecular;
		}

		ENDCG
	} 
	FallBack "Diffuse"
}



