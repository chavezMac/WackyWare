Shader "Trail2D/Smoke"{

	Properties
	{	
		_MaskTex("Mask Texture", 2D) = "white" {}
		_Color1 ("Color 1", Color) = (1,0,0,1)
		_TextureYScale ("Texture Y Scale", float) = 1.0
		_ScrollSpeed ("Scroll Speed", float) = 1.0
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
					fixed2 texcoord3 : TEXCOORD2;
				};

				sampler2D _MaskTex;
				half4 _MaskTex_ST;
				sampler2D _RampTex;
				half4 _RampTex_ST;
				half3 _Color1;
				half _TextureYScale;
				half _ScrollSpeed;

				vertOutput vert(vertInput input)
				{
					vertOutput output;
					output.vertex = UnityObjectToClipPos(input.vertex);
					output.color = input.color;
					output.texcoord = TRANSFORM_TEX(fixed2(input.texcoord.x, input.texcoord.y-_Time.z*_ScrollSpeed), _MaskTex);
					output.texcoord2 = TRANSFORM_TEX(fixed2(input.texcoord.x*1.1-_Time.y*_ScrollSpeed, input.texcoord.y-_Time.y*_ScrollSpeed), _MaskTex);
					output.texcoord3 = TRANSFORM_TEX(fixed2(input.texcoord.x*(output.color.a),_TextureYScale*input.texcoord.y-_Time.x), _MaskTex);
					
					return output;
				}

				fixed4 frag(vertOutput output) : COLOR
				{
					half4 dist1 = tex2D(_MaskTex, output.texcoord);
					half4 dist2 = tex2D(_MaskTex, output.texcoord2);
					half4 mask = tex2D(_MaskTex, output.texcoord3 - .1 +float2(dist1.r+dist2.r,dist1.r+dist2.r)*.2);
					
					half4 color = half4(_Color1.rgb,lerp(mask.r,mask.g,output.color.a+0.5)*output.color.a*dist1.b*output.color.r);
					
					return fixed4(color);
				}
			ENDCG
		}
	}
}