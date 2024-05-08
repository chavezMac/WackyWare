Shader "Trail2D/LiquidStream"{

	Properties
	{	
		_MaskTex("Mask Texture", 2D) = "white" {}
		_Color1 ("Color 1", Color) = (1,0,0,1)
		_Color2 ("Color 2", Color) = (1,1,0,1)
		_TextureYScale ("Texture Y Scale", float) = 1.0
		_ScrollSpeed1 ("Scroll Speed", float) = -1.0
		_ScrollSpeed2 ("Scroll Speed", float) = -0.8
	}

	SubShader{
		Tags{ "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		Blend SrcAlpha OneMinusSrcAlpha
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
				};

				sampler2D _MaskTex;
				half4 _MaskTex_ST;
				sampler2D _RampTex;
				half4 _RampTex_ST;
				half3 _Color1;
				half3 _Color2;
				half _TextureYScale;
				half _ScrollSpeed1;
				half _ScrollSpeed2;

				vertOutput vert(vertInput input)
				{
					vertOutput output;
					output.vertex = UnityObjectToClipPos(input.vertex);
					output.texcoord = TRANSFORM_TEX(fixed2(input.texcoord.x, input.texcoord.y * _TextureYScale + _Time.y * _ScrollSpeed1), _MaskTex);
					output.texcoord2 = TRANSFORM_TEX(fixed2(input.texcoord.x, input.texcoord.y * _TextureYScale + _Time.y * _ScrollSpeed2), _MaskTex);
					output.color = input.color;
					return output;
				}

				fixed4 frag(vertOutput output) : COLOR
				{
					half4 yScroll1 = tex2D(_MaskTex, output.texcoord);
					half4 yScroll2 = tex2D(_MaskTex, output.texcoord2);

					return fixed4(
						lerp(_Color1.rgb,_Color2.rgb,(yScroll1.b+yScroll2.b)*0.5).rgb, // colors
						(max(yScroll1.g-0.5,0) + max(yScroll2.g-0.5,0)) * output.color.r * output.color.g // alpha
					);
				}
			ENDCG
		}
	}
}