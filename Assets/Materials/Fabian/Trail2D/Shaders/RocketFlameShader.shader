Shader "Trail2D/RocketFlame"{

	Properties
	{	
		_MaskTex("Mask Texture", 2D) = "white" {}
		_Color1 ("Color 1", Color) = (1,0,0,1)
		_Color2 ("Color 2", Color) = (1,1,0,1)
		_TextureYScale ("Texture Y Scale", float) = 1.0
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

				sampler2D _MaskTex;
				half4 _MaskTex_ST;
				sampler2D _RampTex;
				half4 _RampTex_ST;
				half3 _Color1;
				half3 _Color2;
				half _TextureYScale;

				vertOutput vert(vertInput input)
				{
					vertOutput output;
					output.vertex = UnityObjectToClipPos(input.vertex*(input.color.r+0.1));
					output.texcoord = TRANSFORM_TEX(fixed2(input.texcoord.x, input.texcoord.y * _TextureYScale), _MaskTex);
					output.texcoord2 = TRANSFORM_TEX(fixed2(input.texcoord.x+_Time.y,_TextureYScale*input.texcoord.y+_Time.y*-5), _MaskTex);
					output.texcoord3 = TRANSFORM_TEX(fixed2(input.texcoord.x,_TextureYScale*input.texcoord.y+_Time.y*-3), _MaskTex);
					output.color = input.color;
					return output;
				}

				fixed4 frag(vertOutput output) : COLOR
				{
					half mask = tex2D(_MaskTex, output.texcoord).g;
					half noise1 = tex2D(_MaskTex, output.texcoord2).b;
					half noise2 = tex2D(_MaskTex, output.texcoord3).b;
					noise1 = noise1 * noise2 * 2;
					half lifeLeft = output.color.a;
					half fadeoutMask = (noise1+lifeLeft)*4 - 2;
					fadeoutMask *= mask;
					
					half whiteness = max(0,(output.color.a)*10-9)*mask+fadeoutMask*0.15;
					half3 fire = lerp(_Color1,_Color2,mask*4-3-(1.0-lifeLeft)*5);
					return fixed4(fire+whiteness,saturate(fadeoutMask));
				}
			ENDCG
		}
	}
}