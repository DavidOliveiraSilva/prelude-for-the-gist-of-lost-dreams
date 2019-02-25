Shader "Custom/Fog2DShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_FogColor ("Fog Color", Color) = (1, 1, 1, 1)
		_UvRange ("UV Range", Range (1, 200)) = 20
		_FogStrenght ("Fog Strenght", Range(0, 1)) = 0.5
		_PlayerPos ("Player Position", vector) = (0, 0, 0)
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		LOD 100

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha


		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			float random (float2 st) {
				return frac(sin(dot(st.xy,
									float2(12.9898,78.233)))*
					43758.5453123);
			}

			float noise (float2 st) {
				float2 i = floor(st);
				float2 f = frac(st);

				// Four corners in 2D of a tile
				float a = random(i);
				float b = random(i + float2(1.0, 0.0));
				float c = random(i + float2(0.0, 1.0));
				float d = random(i + float2(1.0, 1.0));

				float2 u = f * f * (3.0 - 2.0 * f);

				return lerp(a, b, u.x) +
						(c - a)* u.y * (1.0 - u.x) +
						(d - b) * u.x * u.y;
			}

			#define OCTAVES 4
			float fbm (float2 st) {
				// Initial values
				float value = 0.0;
				float amplitude = .5;
				float frequency = 0.;
				//
				// Loop of octaves
				for (int i = 0; i < OCTAVES; i++) {
					value += amplitude * noise(st);
					st *= 6.;
					amplitude *= .5;
				}
				return value;
			}

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			fixed4 _FogColor;
			int _UvRange;
			float _FogStrenght;
			float3 _PlayerPos;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				//fixed4 col = tex2D(_MainTex, i.uv);
				float2 cuv = i.uv * _UvRange;
				float2 motion = float2(fbm(cuv + _Time.y/15), fbm(cuv + _Time.y/10));

				fixed4 col = _FogColor;

				float final = fbm((motion + cuv) * sin(_Time.y/200));

				//fixed4 col = tex2D(_MainTex, i.uv * final);
				col.a = final*_FogStrenght;
				return col;
			}
			ENDCG
		}
	}
}
