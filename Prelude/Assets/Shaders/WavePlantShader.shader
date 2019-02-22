Shader "Custom/WavePlantShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_WorldSize("World Size", vector) = (1, 1, 1, 1)
		_WindSpeed("Wind Speed", vector) = (1, 1, 1, 1)
		_WindTex ("Wind Texture", 2D) = "white" {}
		_WaveSpeed("Wave Speed", float) = 1.0
		_WaveAmp("Wave Amp", float) = 1.0
		_HeightCutoff("Height Cutoff", float) = 1.0
		_HeightFactor("Height Factor", float) = 1.0

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
			float4 _MainTex_ST;
			vector _WorldSize;
			vector _WindSpeed;
			sampler2D _WindTex;
			float _WaveSpeed;
			float _WaveAmp;
			float _HeightCutoff;
			float _HeightFactor;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				
				//Movimentação de acordo com o "vento"
				float heightFactor = v.vertex.y > _HeightCutoff;
				
				heightFactor = heightFactor + (v.vertex.y * _HeightFactor);

				float4 worldPos = mul(v.vertex, unity_ObjectToWorld);
				
				float2 samplePos = worldPos.xy/_WorldSize.xy;
				//samplePos = v.uv;
				samplePos += _Time.y * _WindSpeed.xy;

				float windSample = tex2Dlod(_WindTex, float4(samplePos, 0, 0));
				

				
				//o.uv = samplePos;

				
				o.vertex.y += cos(_WaveSpeed*windSample)*_WaveAmp * heightFactor;
				o.vertex.x += sin(_WaveSpeed*windSample)*_WaveAmp * heightFactor;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				
				fixed4 col = tex2D(_MainTex, i.uv);
				
				//col = fixed4(frac(i.uv.x), 0, 0, 1);
				return col;
			}
			ENDCG
		}
	}
}
