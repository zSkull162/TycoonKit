Shader "zSkull162/Fresnel"
{
	// Shader made by hadashiA
	// Edited/fixed by StereoNezumi
	Properties
	{
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
		[HDR] _Color ("Tint", Color) = (1,1,1,1)
		[HDR] _FresnelColor ("Fresnel Color", Color) = (1,1,1,1)
		_FresnelBias ("Fresnel Bias", Range(-1,1)) = 0
		_FresnelScale ("Fresnel Scale", Range(0,1)) = 1
		_FresnelPower ("Fresnel Power", Range(0.0001,5)) = 1
	}

	SubShader
	{
		Tags
		{
			"Queue"="Geometry"
			"IgnoreProjector"="True"
			"RenderType"="Opaque"
		}

		Cull Back

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0

			#include "UnityCG.cginc"

			struct appdata_t
			{
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
				half3 normal : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 viewDir : TEXCOORD1;
				float3 normal : TEXCOORD2;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			float4 _Color, _FresnelColor;
			float _FresnelBias, _FresnelScale, _FresnelPower;

			v2f vert(appdata_t v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.pos);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				o.viewDir = _WorldSpaceCameraPos - mul(unity_ObjectToWorld, v.pos);

				#if UNITY_ASSUME_UNIFORM_SCALING
					o.normal = mul(unity_ObjectToWorld, v.normal);
				#else
					o.normal = mul(v.normal, unity_WorldToObject);
				#endif

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				i.viewDir = normalize(i.viewDir);
				i.normal = normalize(i.normal);

				fixed4 c = tex2D(_MainTex, i.uv) * _Color;

				float fresnel = _FresnelBias + _FresnelScale * pow(1 - saturate(dot(i.viewDir, i.normal)), _FresnelPower);

                return lerp(c, _FresnelColor, saturate(fresnel));
			}
			ENDCG
		}
	}
}