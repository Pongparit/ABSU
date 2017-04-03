Shader "MHImport/Eye" 
{
	Properties {

        _MainTex ("Base (RGB) Alpha (A)", 2D) = "white" { }
		_BumpMap("Bumpmap (RGB) ", 2D) = "" {}
    }
		
		SubShader{
		
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
			#pragma surface surf StandardSpecular

				struct Input
				{
					float2 uv_MainTex;
				};

				sampler2D _MainTex, _BumpMap;

				void surf(Input IN, inout SurfaceOutputStandardSpecular o)
				{
					fixed4 albedo = tex2D(_MainTex, IN.uv_MainTex);
					if (albedo.a < .99)
						discard;
					o.Albedo = albedo.rgb*albedo.a;
					o.Smoothness = 0.5;
					o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
				}
		ENDCG
		
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
			#pragma surface surf StandardSpecular alpha:premul

				struct Input
				{
					float2 uv_MainTex;
				};

				sampler2D _MainTex, _BumpMap;

				void surf(Input IN, inout SurfaceOutputStandardSpecular o)
				{
					fixed4 albedo = tex2D(_MainTex, IN.uv_MainTex);
					if (albedo.a >= .99)
						discard;
					o.Smoothness = 1;
					o.Albedo = albedo.rgb;
					o.Alpha = albedo.a;
					o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
				}
		ENDCG

	}
}
