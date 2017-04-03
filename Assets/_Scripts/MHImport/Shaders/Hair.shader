Shader "MHImport/Hair" 
{
	Properties {

        _MainTex ("Base (RGB) Alpha (A)", 2D) = "white" { }
		_BumpMap("Bumpmap (RGB) ", 2D) = "" {}
    }

		SubShader{
		Cull Off
		CGPROGRAM
		#pragma surface surf BlinnPhong

			struct Input
			{
				float2 uv_MainTex;
			};

			sampler2D _MainTex, _BumpMap;

			void surf(Input IN, inout SurfaceOutput o)
			{
				fixed4 albedo = tex2D(_MainTex, IN.uv_MainTex);
				if (albedo.a < .95)
					discard;
				o.Albedo = albedo.rgb;
				o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
			}
		ENDCG
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
		
		Cull Front
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
			#pragma surface surf BlinnPhong alpha:fade

				struct Input
				{
					float2 uv_MainTex;
				};

				sampler2D _MainTex, _BumpMap;

				void surf(Input IN, inout SurfaceOutput o)
				{
					fixed4 albedo = tex2D(_MainTex, IN.uv_MainTex);
					if (albedo.a >= .95)
						discard;
					o.Albedo = albedo.rgb;
					o.Alpha = albedo.a / .95;
					o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
				}
		ENDCG

			Pass{
			AlphaTest Greater[_Cutoff]
			SetTexture[_MainTex]{
		}
		}
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
			#pragma surface surf BlinnPhong alpha:fade

				struct Input
				{
					float2 uv_MainTex;
				};

				sampler2D _MainTex, _BumpMap;

				void surf(Input IN, inout SurfaceOutput o)
				{
					fixed4 albedo = tex2D(_MainTex, IN.uv_MainTex);
					if (albedo.a >= .95)
						discard;
					o.Albedo = albedo.rgb;
					o.Alpha = albedo.a/.95;
					o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
				}
		ENDCG

	}
}
