Shader "Custom/SoundPulse" {
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_WorldPosition ("SonarPoint", Vector) = (0.0, 0.0, 0.0, 0.0)
		_SonarTime ("SonarTime", Float) = 0
		_SonarIntensity ("SonarIntensity", Float) = 0
		_StaticPulseDistance ("StaticPulseDistance", Float) = 10
	}
    SubShader 
    {
        Pass 
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"

			uniform sampler2D 	_MainTex;
			uniform float4		_WorldPosition;
			uniform float		_SonarTime;
			uniform float		_SonarIntensity;
			uniform float		_StaticPulseDistance;

            struct vertexInput 
            {
                float4 vertex : POSITION;
                float4 texcoord0 : TEXCOORD0;
            };

            struct fragmentInput
            {
                float4 position : SV_POSITION;
                float4 texcoord0 : TEXCOORD0;
                float4 vertexPos : TEXCOORD1;
            };

            fragmentInput vert(vertexInput i)
            {
                fragmentInput o;
                o.position = mul (UNITY_MATRIX_MVP, i.vertex);
                //o.vertexPos = i.vertex;
                o.vertexPos = mul(_Object2World, i.vertex);
                o.texcoord0 = i.texcoord0;
                return o;
            }
            
            float4 frag(fragmentInput i) : SV_Target 
            {
            	// Cache distance vars
            	float DistanceToImpactPoint = distance(i.vertexPos.xyz, _WorldPosition.xyz);
            
            	// Cached pulse vars
            	float CachedPulseMaxDistance = _SonarIntensity * _StaticPulseDistance;
            	float CurrentPulseDistance = CachedPulseMaxDistance * _SonarTime;
            	
            	float DistanceBetweenImpactAndPulse = abs(CurrentPulseDistance - DistanceToImpactPoint);
            	
            	float4 OutputCol = float4(0.0, 0.0, 0.0, 0.0);
            	if(DistanceBetweenImpactAndPulse < 1.0f)
            	{
            		float PulseColour = clamp(1.0 - DistanceBetweenImpactAndPulse, 0.0, 1.0);
            		
            		OutputCol = float4(PulseColour, PulseColour, PulseColour, 1.0);
            	}
            	
                //return tex2D(_MainTex, i.texcoord0.xy);
                return OutputCol;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
