// (c) 2022 Zachery O Wagner

Shader "Portal/StencilMask"
{
	SubShader
	{
		Tags { "RenderType"="Opaque" }   // RenderType tag categorizes shaders into several predefined groups
		LOD 100                          // Level of Detail
		Zwrite Off                       // Do not write to the ZBuffer
		ColorMask 0                      // Do not draw colors
		Cull off                         // Do not cull polygons based on the direction that they face

		Pass
		{
			Stencil{
				Ref 1           // Set the reference vaue
				Comp always     // Default
				Pass replace    // Always Pass
			}

			/// Boiler Plate

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				return fixed4(0.0, 0.0, 0.0, 0.0);
			}
			ENDCG
		}
	}
}
