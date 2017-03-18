Shader "Hidden/PaletteShader" {
	Properties {
		_MainTex ("Sprite Texture", 2D) = "white" {}
		_Palette ("Palette Texture", 2D) = "white" {}
		_PaletteNormWidth ("Palette Normalized Width", Float) = 0.125
		_PaletteNormHeight ("Palette Normalized Height", Float) = 0.125
		_Color ("Indexed Color", Int) = 6
	}
	SubShader {
		Tags {
			"RenderType" = "Opaque"
			"Queue" = "Transparent+1"
		}

		Pass {
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
			// Enable rendering of polygon's back face (occurs when scale.y == -1)
			Cull Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile DUMMY PIXELSNAP_ON

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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			sampler2D _Palette;
			float _PaletteNormWidth;
			float _PaletteNormHeight;
			unsigned int _Color;

			fixed4 frag (v2f i) : SV_Target {
				// Get the alpha and red component of the current color
				fixed4 col = tex2D(_MainTex, i.uv);
				// Retrieve the shade from the last 3 bits (0b101 = lightest, 0b000 = darkest)
				unsigned int shade = (unsigned int)(col.r * 255);
				shade = (shade & 7) - 1;
				// Calculate the position of the color within the palette
				float2 index = float2(shade * _PaletteNormWidth, (_Color + 1) * _PaletteNormHeight);
				// Multiply by alpha to hide all transparent pixel
				return col.a * tex2D(_Palette, index);
			}
			ENDCG
		}
	}
}
