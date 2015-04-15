Shader "Click/Click_Sonar" {
	Properties {
	
		_BaseColor ("Base Color", Color)       = (0.1,0.1,0.1,0.0)
		_WaveColor ("Wave Color", Color)       = (1.0,0.1,0.1,0.0)
		_WaveParams("Wave Parameters", Vector) = (1.0,20.0,20.0,10.0)
		_WaveVector("Wave Vector", Vector)     = (1.0,1.0,1.0,0.0)
		_AddColor  ("Add Color", Color)        = (0.0,0.0,0.0,0.0)
		
	}
	SubShader {
		Tags { "RenderType" = "Opaque" }
		CGPROGRAM
		#pragma surface surf Lambert
		#pragma multi_compile SONAR_SPHERICAL

		float3 _BaseColor;
		float3 _WaveColor;
		float4 _WaveParams;
		float3 _WaveVector;
		float3 _AddColor;

		struct Input {
		
			float3 worldPos;
			
		};
		
		void surf (Input IN, inout SurfaceOutput o) {
			
			//float w = dot(IN.worldPos, _WaveVector);
			float w = length(IN.worldPos - _WaveVector);

			
			w -= _Time.y * _WaveParams.w;

			w /= _WaveParams.z;
			w = w - floor(w);
			
			//Make the Gradient Steeper
			float p = _WaveParams.y;
			w = ( pow( w, p) + pow( 1 - w, p * 4)) * 0.5;
			
			//Amplify
			w *= _WaveParams.x;
			
			//Apply to surface
			o.Albedo = _BaseColor;
			o.Emission = _WaveColor * w + _AddColor;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
