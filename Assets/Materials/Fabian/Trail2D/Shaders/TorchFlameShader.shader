Shader "Trail2D/TorchFlame"{

	Properties
	{	
		_ColorTex("Color Texture", 2D) = "white" {}
		_DistTex("Distortion Texture", 2D) = "white" {}
		_DistortionSpeeds ("Distortion speeds", Vector) = (1,1,-1,-1)
		_DistortionUVScales ("Distortion UV scales", Vector) = (1,1,1,1)
	}

	SubShader{
		Tags{ "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		Blend SrcAlpha One
		Cull Off
		Lighting Off
		ZWrite Off
		Pass{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				struct vertInput {
					float4 vertex : POSITION;
					fixed4 color: COLOR;
					fixed3 texcoord : TEXCOORD0;
				};

				struct vertOutput {
					float4 vertex : POSITION;
					fixed4 color: COLOR;
					fixed2 texcoord : TEXCOORD0;
					fixed2 texcoord2 : TEXCOORD1;
					fixed2 texcoord3 : TEXCOORD2;
				};

				sampler2D _ColorTex;
				half4 _ColorTex_ST;
				sampler2D _DistTex;
				half4 _DistTex_ST;
				sampler2D _RampTex;
				half4 _RampTex_ST;
				half3 _Color1;
				half3 _Color2;
				half _TextureYScale;
				float4 _DistortionSpeeds;
				float4 _DistortionUVScales;

				vertOutput vert(vertInput input)
				{
					vertOutput output;
					output.vertex = UnityObjectToClipPos(input.vertex);
					output.texcoord = TRANSFORM_TEX(input.texcoord*_DistortionUVScales.xy+_DistortionSpeeds.xy*_Time.xx, _DistTex);
					output.texcoord2 = TRANSFORM_TEX(input.texcoord*_DistortionUVScales.zw+_DistortionSpeeds.zw*_Time.xx, _DistTex);
					output.texcoord3 = TRANSFORM_TEX(float2(input.texcoord.x, input.texcoord.y*1), _ColorTex);
					output.color = input.color;
					return output;
				}

				fixed4 frag(vertOutput output) : COLOR
				{
					fixed4 dist1 = tex2D(_DistTex, output.texcoord);
					fixed4 dist2 = tex2D(_DistTex, output.texcoord2);
					fixed4 mask = tex2D(_ColorTex, output.texcoord3+(dist1+dist2)*0.25-0.25);
					fixed dist = (dist1+dist2) * mask.a + mask.a * output.color.a;
					dist *= output.color.a;
					
					fixed3 color3 = tex2D(_ColorTex, float2(0.5, 1.0-dist.r*dist.r));
					
					return fixed4(color3.rgb,smoothstep(0.0,0.05,dist.r-output.color.a));
				}
			ENDCG
		}
	}
}