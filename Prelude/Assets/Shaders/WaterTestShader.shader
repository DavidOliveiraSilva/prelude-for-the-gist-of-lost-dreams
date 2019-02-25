Shader "Custom/WaterTestShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_HeightCutoff ("Height Cutoff", float) = 1.0
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

			#define OCTAVES 6
			float fbm (float2 st) {
				// Initial values
				float value = 0.0;
				float amplitude = .5;
				float frequency = 0.;
				//
				// Loop of octaves
				for (int i = 0; i < OCTAVES; i++) {
					value += amplitude * noise(st);
					st *= 4.;
					amplitude *= .25;
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
				float4 screenPos : TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float _HeightCutoff;

			v2f vert (appdata v)
			{
				v2f o;
				
				float heightFactor = v.vertex.x > _HeightCutoff;
				

				//v.vertex.y += (sin(_Time.y * 2 * v.vertex.x)  ) * heightFactor;

				v.vertex.y += sin(fbm(float2(v.vertex.x+sin(_Time.y), v.vertex.y+cos(_Time.y))))*heightFactor;

				float y = v.vertex.y;
				float x = v.vertex.x;

				

				//v.vertex.y += y;
				

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.screenPos = ComputeScreenPos(v.vertex);

				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				float2 fuv = i.uv;
				fuv.y += fuv.x % 2 == 0 ? 0.1 : -0.1; 

				fixed4 col = tex2D(_MainTex, i.uv);

				//col.rgb -= noise(i.uv + _Time.y);

				return col;
			}
			ENDCG
		}
	}
}
