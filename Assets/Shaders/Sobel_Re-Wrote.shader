Shader "Shader_Demo/Sobel_Re-Write"{
	Properties{
	    _MainTex ("Base (RGB)", 2D) = "white" {}
    	_SobelIntensity("Sobel Intensity", Range(0.0,2500.0)) = 100
    	_Intensity("Intensity", Range(0.0,1.0)) = 1.0
	}
	
	SubShader{
		Tags {"LightMode" = "ForwardBase"}
		Pass {
        	ZTest Always Cull Off
        	Fog { Mode off }
        	CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest
            #include "UnityCG.cginc"
			
			//user defined varibles
			uniform sampler2D _MainTex;
            uniform float4 _MainTex_TexelSize;
            uniform float _SobelIntensity;
            uniform float _Intensity;
			//unity defined varibles 
			struct vertexInput{
				float3 vertex : POSITION;
				float4 texcoord : TEXCOORD;
			};
			struct vertexOutput{
				float4 pos : SV_POSITION;
				float4 tex : TEXCOORD0;
			};
			
			//vertex function
			vertexOutput vert(vertexInput v){
				vertexOutput o;
				o.pos = mul(UNITY_MATRIX_MVP,v.vertex);
				o.tex = v.texcoord;
				return o;
			}
			//fragment function
			float4 frag(vertexOutput input) : Color{
							
			    float p01 = tex2D(_MainTex, input.tex.xy + _MainTex_TexelSize * float2(-1, 0)).r;
                float p02 = tex2D(_MainTex, input.tex.xy +  _MainTex_TexelSize * float2( 0, 1)).r;
                float p03 = tex2D(_MainTex, input.tex.xy + _MainTex_TexelSize * float2( 1, 1)).r;
                									 		  
                float p04 = tex2D(_MainTex, input.tex.xy + _MainTex_TexelSize * float2(-1, 0)).r;
                float p05 = tex2D(_MainTex, input.tex.xy + _MainTex_TexelSize * float2( 0, 0)).r;
                float p06 = tex2D(_MainTex, input.tex.xy + _MainTex_TexelSize * float2( 1, 0)).r;
                									 		  
                float p07 = tex2D(_MainTex, input.tex.xy + _MainTex_TexelSize * float2(-1, -1)).r;
                float p08 = tex2D(_MainTex, input.tex.xy + _MainTex_TexelSize * float2( 0, -1)).r;
                float p09 = tex2D(_MainTex, input.tex.xy + _MainTex_TexelSize * float2( 1, -1)).r;
               
                float sobelX = saturate(( p01 + ( p02 + p02 ) + p03 - p07 - ( p08 + p08 ) - p09));
                float sobelY = saturate(( p03 + ( p06 + p06 ) + p09 - p01 - ( p04 + p04 ) - p07));

                float edgeSqr = (sobelX * sobelX + sobelY * sobelY);
                float edge = sqrt(edgeSqr);
               
                float4 result = _Intensity - edge * _SobelIntensity;
                return result;   
			}
			
			ENDCG
		}
		
	}
	Fallback "Diffuse"
}