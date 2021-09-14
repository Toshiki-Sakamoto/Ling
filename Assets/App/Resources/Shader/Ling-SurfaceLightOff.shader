Shader "Ling-SurfaceLightOff"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo(RGB)", 2D) = "white" {}
	}


    SubShader
    {
        Tags { "RenderType"="Opaque" }

		CGPROGRAM

		#pragma surface surf NoLighting noambient

		struct Input 
		{
			float2 uv_MainTex;
		};

		sampler2D _MainTex;
		fixed4 _Color;

		inline fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			return fixed4(s.Albedo, s.Alpha);
		}

		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb * _Color.rgb;
		}
		ENDCG
	}
}
