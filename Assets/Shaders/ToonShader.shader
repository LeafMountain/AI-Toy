// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "TreeBranch/ToonShader" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		// _RampTex ("Light Ramp", 2D) = "white" {}
		_Color("Main Color", Color) = (0, 0, 0, 0)

		// Emission
		_EmissionMap("Emission Map", 2D) = "black" {}
		_Emission("Emission Color", Color) = (1, 1, 1, 1)

		// Infection
		_NoiseMap("Noise", 2D) = "white" {}
		_InfectionTex("Infection Texture", 2D) = "white" {}
		_InfectionColor("Infection Color", Color) = (1, 1, 1, 1)
		// _InfectionNormal("Infection Normal", 2D) = "white" {}		
		_InfectionAmount("Infection Amount", Range(0, .5)) = 0

		// Outline
		_OutlineColor("Outline Color", Color) = (0, 0, 0, 0)
		_OutlineWidth("Outline Width", Range(0.0, 0.07)) = 0
		_OutlineZ("Outline Z", Range(-.002, 0)) = -.001
	}

	CGINCLUDE
	#include "UnityCG.cginc"

		struct appdata {
			float4 vertex : POSITION;
			float3 normal :  NORMAL;
			float4 texcoord : TEXCOORD0;
		};

		struct v2f {
			float4 pos : SV_POSITION;
			UNITY_FOG_COORDS(0)
			fixed4 color : COLOR;
		};

		float _OutlineWidth;
		float4 _OutlineColor;
		float _OutlineZ;

		v2f vert (appdata v){
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);

			//Creates an offest to expand from center of mesh
			float3 norm = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, v.normal));
        	float2 offset = TransformViewToProjection(norm.xy);

			//Offests vertex by outline width (adjusted by offset)
			o.pos.xy += offset * _OutlineWidth;

			o.pos.z += _OutlineZ;
        	UNITY_TRANSFER_FOG(o, o.pos);
		
			return o;
		}

	ENDCG

	SubShader {

		Tags{ 
			"RenderType" = "Opaque"
			"Queue" = "Transparent"
		}
		Cull off

        // UsePass "Toon/Lit/FORWARD"

		// Outline
		Pass {
			Name "OUTLINE"
			Tags { "LightMode" = "Always" }
			Cull Off
			ZWrite On
			ColorMask RGB

			CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag

				half4 frag(v2f i) : SV_TARGET{
					return _OutlineColor;
				}

			ENDCG
		}

		// Toon shading
		CGPROGRAM
		#pragma surface surf CelShadingForward fullforwardshadows vertex:avert
		#pragma target 3.0

		half4 LightingCelShadingForward(SurfaceOutput s, half3 lightDir, half atten) {
			half NdotL = dot(s.Normal, lightDir);
			if (NdotL <= 0.0) NdotL = 0;
			else NdotL = 1;
			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten);
			c.a = s.Alpha;
			return c;
		}

		sampler2D _MainTex;
		fixed4 _Color;

		sampler2D _InfectionTex;
		sampler2D _NoiseMap;
		sampler2D _InfectionNormal;
		float _InfectionAmount;
		fixed4 _InfectionColor;

		sampler2D _EmissionMap;
		fixed4 _Emission;

		struct Input {
			float2 uv_MainTex;
			float2 uv_NoiseMap;
			float2 uv_InfectionTex;
		};

		void avert( inout appdata_full v) {
			fixed4 noise = tex2Dlod (_NoiseMap, float4(v.texcoord.xy,0,0));

			v.vertex.xyz += lerp(0, -((v.normal * noise) * 0.1), step(noise, _InfectionAmount));
		}

		void surf(Input IN, inout SurfaceOutput o) {

			fixed4 color = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			fixed4 infectionColor = tex2D(_InfectionTex, IN.uv_InfectionTex) * _InfectionColor;
			fixed4 noise = tex2D(_NoiseMap, IN.uv_NoiseMap);
			fixed4 emission = tex2D(_EmissionMap, IN.uv_MainTex) * _Emission;			

			// Lerp between maintex and infectiontex depending on noisemap and infectionamount
			o.Albedo = lerp(color.rgb, infectionColor.rgb, step(noise.r, _InfectionAmount));

			o.Alpha = color.a;

			o.Emission = emission;
			// o.Emission = lerp(0, emission.rgb, step(noise.rgb, _InfectionAmount));
			// o.Normal = tex2D(_InfectionNormal, IN.uv_MainTex);
		}
		ENDCG
	}


	Fallback "Toon/Basic"
}
