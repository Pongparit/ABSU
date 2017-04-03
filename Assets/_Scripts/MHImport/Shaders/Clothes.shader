Shader "MHImport/Clothes" {
	Properties {

        _MainTex ("Base (RGB)", 2D) = "white" { }
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
				o.Albedo = albedo.rgb;
				o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
			}
		ENDCG

	} 
	FallBack "Diffuse"
}
