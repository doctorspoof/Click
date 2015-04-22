Shader "Custom/SoundPulse" {
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		
		_SonarWorldPos1 ("SonarPos1", Vector) = (0, 0, 0, 0)
		_SonarWorldPos2 ("SonarPos2", Vector) = (0, 0, 0, 0)
		_SonarWorldPos3 ("SonarPos3", Vector) = (0, 0, 0, 0)
		_SonarWorldPos4 ("SonarPos4", Vector) = (0, 0, 0, 0)
		_SonarWorldPos5 ("SonarPos5", Vector) = (0, 0, 0, 0)
		_SonarWorldPos1 ("SonarPos6", Vector) = (0, 0, 0, 0)
		_SonarWorldPos2 ("SonarPos7", Vector) = (0, 0, 0, 0)
		_SonarWorldPos3 ("SonarPos8", Vector) = (0, 0, 0, 0)
		_SonarWorldPos4 ("SonarPos9", Vector) = (0, 0, 0, 0)
		_SonarWorldPos5 ("SonarPos10", Vector) = (0, 0, 0, 0)
		_SonarWorldPos1 ("SonarPos11", Vector) = (0, 0, 0, 0)
		_SonarWorldPos2 ("SonarPos12", Vector) = (0, 0, 0, 0)
		_SonarWorldPos3 ("SonarPos13", Vector) = (0, 0, 0, 0)
		_SonarWorldPos4 ("SonarPos14", Vector) = (0, 0, 0, 0)
		_SonarWorldPos5 ("SonarPos15", Vector) = (0, 0, 0, 0)
		_SonarWorldPos1 ("SonarPos16", Vector) = (0, 0, 0, 0)
		_SonarWorldPos2 ("SonarPos17", Vector) = (0, 0, 0, 0)
		_SonarWorldPos3 ("SonarPos18", Vector) = (0, 0, 0, 0)
		_SonarWorldPos4 ("SonarPos19", Vector) = (0, 0, 0, 0)
		_SonarWorldPos5 ("SonarPos20", Vector) = (0, 0, 0, 0)
		_SonarWorldPos1 ("SonarPos21", Vector) = (0, 0, 0, 0)
		_SonarWorldPos2 ("SonarPos22", Vector) = (0, 0, 0, 0)
		_SonarWorldPos3 ("SonarPos23", Vector) = (0, 0, 0, 0)
		_SonarWorldPos4 ("SonarPos24", Vector) = (0, 0, 0, 0)
		_SonarWorldPos5 ("SonarPos25", Vector) = (0, 0, 0, 0)
		_SonarWorldPos1 ("SonarPos26", Vector) = (0, 0, 0, 0)
		_SonarWorldPos2 ("SonarPos27", Vector) = (0, 0, 0, 0)
		_SonarWorldPos3 ("SonarPos28", Vector) = (0, 0, 0, 0)
		_SonarWorldPos4 ("SonarPos29", Vector) = (0, 0, 0, 0)
		_SonarWorldPos5 ("SonarPos30", Vector) = (0, 0, 0, 0)
		
		_SonarTime1 ("SonarTime1", Float) = 0
		_SonarTime2 ("SonarTime2", Float) = 0
		_SonarTime3 ("SonarTime3", Float) = 0
		_SonarTime4 ("SonarTime4", Float) = 0
		_SonarTime5 ("SonarTime5", Float) = 0
		_SonarTime1 ("SonarTime6", Float) = 0
		_SonarTime2 ("SonarTime7", Float) = 0
		_SonarTime3 ("SonarTime8", Float) = 0
		_SonarTime4 ("SonarTime9", Float) = 0
		_SonarTime5 ("SonarTime10", Float) = 0
		_SonarTime1 ("SonarTime11", Float) = 0
		_SonarTime2 ("SonarTime12", Float) = 0
		_SonarTime3 ("SonarTime13", Float) = 0
		_SonarTime4 ("SonarTime14", Float) = 0
		_SonarTime5 ("SonarTime15", Float) = 0
		_SonarTime1 ("SonarTime16", Float) = 0
		_SonarTime2 ("SonarTime17", Float) = 0
		_SonarTime3 ("SonarTime18", Float) = 0
		_SonarTime4 ("SonarTime19", Float) = 0
		_SonarTime5 ("SonarTime20", Float) = 0
		_SonarTime1 ("SonarTime21", Float) = 0
		_SonarTime2 ("SonarTime22", Float) = 0
		_SonarTime3 ("SonarTime23", Float) = 0
		_SonarTime4 ("SonarTime24", Float) = 0
		_SonarTime5 ("SonarTime25", Float) = 0
		_SonarTime1 ("SonarTime26", Float) = 0
		_SonarTime2 ("SonarTime27", Float) = 0
		_SonarTime3 ("SonarTime28", Float) = 0
		_SonarTime4 ("SonarTime29", Float) = 0
		_SonarTime5 ("SonarTime30", Float) = 0
		
		_SonarDistance1 ("SonarDistance1", Float) = 0
		_SonarDistance2 ("SonarDistance2", Float) = 0
		_SonarDistance3 ("SonarDistance3", Float) = 0
		_SonarDistance4 ("SonarDistance4", Float) = 0
		_SonarDistance5 ("SonarDistance5", Float) = 0
		_SonarDistance1 ("SonarDistance6", Float) = 0
		_SonarDistance2 ("SonarDistance7", Float) = 0
		_SonarDistance3 ("SonarDistance8", Float) = 0
		_SonarDistance4 ("SonarDistance9", Float) = 0
		_SonarDistance5 ("SonarDistance10", Float) = 0
		_SonarDistance1 ("SonarDistance11", Float) = 0
		_SonarDistance2 ("SonarDistance12", Float) = 0
		_SonarDistance3 ("SonarDistance13", Float) = 0
		_SonarDistance4 ("SonarDistance14", Float) = 0
		_SonarDistance5 ("SonarDistance15", Float) = 0
		_SonarDistance1 ("SonarDistance16", Float) = 0
		_SonarDistance2 ("SonarDistance17", Float) = 0
		_SonarDistance3 ("SonarDistance18", Float) = 0
		_SonarDistance4 ("SonarDistance19", Float) = 0
		_SonarDistance5 ("SonarDistance20", Float) = 0
		_SonarDistance1 ("SonarDistance21", Float) = 0
		_SonarDistance2 ("SonarDistance22", Float) = 0
		_SonarDistance3 ("SonarDistance23", Float) = 0
		_SonarDistance4 ("SonarDistance24", Float) = 0
		_SonarDistance5 ("SonarDistance25", Float) = 0
		_SonarDistance1 ("SonarDistance26", Float) = 0
		_SonarDistance2 ("SonarDistance27", Float) = 0
		_SonarDistance3 ("SonarDistance28", Float) = 0
		_SonarDistance4 ("SonarDistance29", Float) = 0
		_SonarDistance5 ("SonarDistance30", Float) = 0
		
		_PlayerPosition ("PlayerPosition", Vector) = (0,0,0,0)
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
			
			uniform float4 _SonarWorldPos1;
			uniform float4 _SonarWorldPos2;
			uniform float4 _SonarWorldPos3;
			uniform float4 _SonarWorldPos4;
			uniform float4 _SonarWorldPos5;
			uniform float4 _SonarWorldPos6;
			uniform float4 _SonarWorldPos7;
			uniform float4 _SonarWorldPos8;
			uniform float4 _SonarWorldPos9;
			uniform float4 _SonarWorldPos10;
			uniform float4 _SonarWorldPos11;
			uniform float4 _SonarWorldPos12;
			uniform float4 _SonarWorldPos13;
			uniform float4 _SonarWorldPos14;
			uniform float4 _SonarWorldPos15;
			uniform float4 _SonarWorldPos16;
			uniform float4 _SonarWorldPos17;
			uniform float4 _SonarWorldPos18;
			uniform float4 _SonarWorldPos19;
			uniform float4 _SonarWorldPos20;
			uniform float4 _SonarWorldPos21;
			uniform float4 _SonarWorldPos22;
			uniform float4 _SonarWorldPos23;
			uniform float4 _SonarWorldPos24;
			uniform float4 _SonarWorldPos25;
			uniform float4 _SonarWorldPos26;
			uniform float4 _SonarWorldPos27;
			uniform float4 _SonarWorldPos28;
			uniform float4 _SonarWorldPos29;
			uniform float4 _SonarWorldPos30;
			
			uniform float _SonarTime1;
			uniform float _SonarTime2;
			uniform float _SonarTime3;
			uniform float _SonarTime4;
			uniform float _SonarTime5;
			uniform float _SonarTime6;
			uniform float _SonarTime7;
			uniform float _SonarTime8;
			uniform float _SonarTime9;
			uniform float _SonarTime10;
			uniform float _SonarTime11;
			uniform float _SonarTime12;
			uniform float _SonarTime13;
			uniform float _SonarTime14;
			uniform float _SonarTime15;
			uniform float _SonarTime16;
			uniform float _SonarTime17;
			uniform float _SonarTime18;
			uniform float _SonarTime19;
			uniform float _SonarTime20;
			uniform float _SonarTime21;
			uniform float _SonarTime22;
			uniform float _SonarTime23;
			uniform float _SonarTime24;
			uniform float _SonarTime25;
			uniform float _SonarTime26;
			uniform float _SonarTime27;
			uniform float _SonarTime28;
			uniform float _SonarTime29;
			uniform float _SonarTime30;
			
			uniform float _SonarDistance1;
			uniform float _SonarDistance2;
			uniform float _SonarDistance3;
			uniform float _SonarDistance4;
			uniform float _SonarDistance5;
			uniform float _SonarDistance6;
			uniform float _SonarDistance7;
			uniform float _SonarDistance8;
			uniform float _SonarDistance9;
			uniform float _SonarDistance10;
			uniform float _SonarDistance11;
			uniform float _SonarDistance12;
			uniform float _SonarDistance13;
			uniform float _SonarDistance14;
			uniform float _SonarDistance15;
			uniform float _SonarDistance16;
			uniform float _SonarDistance17;
			uniform float _SonarDistance18;
			uniform float _SonarDistance19;
			uniform float _SonarDistance20;
			uniform float _SonarDistance21;
			uniform float _SonarDistance22;
			uniform float _SonarDistance23;
			uniform float _SonarDistance24;
			uniform float _SonarDistance25;
			uniform float _SonarDistance26;
			uniform float _SonarDistance27;
			uniform float _SonarDistance28;
			uniform float _SonarDistance29;
			uniform float _SonarDistance30;
			
			uniform float4 _PlayerPosition;

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
                float4 _SonarPoints[30] = {	_SonarWorldPos1, _SonarWorldPos2, _SonarWorldPos3, _SonarWorldPos4, _SonarWorldPos5, _SonarWorldPos6, _SonarWorldPos7, _SonarWorldPos8, _SonarWorldPos9, _SonarWorldPos10,
                							_SonarWorldPos11, _SonarWorldPos12, _SonarWorldPos13, _SonarWorldPos14, _SonarWorldPos15, _SonarWorldPos16, _SonarWorldPos17, _SonarWorldPos18, _SonarWorldPos19, _SonarWorldPos20,
                							_SonarWorldPos21, _SonarWorldPos22, _SonarWorldPos23, _SonarWorldPos24, _SonarWorldPos25, _SonarWorldPos26, _SonarWorldPos27, _SonarWorldPos28, _SonarWorldPos29, _SonarWorldPos30};
                float _SonarTimes[30] = {	_SonarTime1, _SonarTime2, _SonarTime3, _SonarTime4, _SonarTime5, _SonarTime6, _SonarTime7, _SonarTime8, _SonarTime9, _SonarTime10,
                							_SonarTime11, _SonarTime12, _SonarTime13, _SonarTime14, _SonarTime15, _SonarTime16, _SonarTime17, _SonarTime18, _SonarTime19, _SonarTime20,
                							_SonarTime21, _SonarTime22, _SonarTime23, _SonarTime24, _SonarTime25, _SonarTime26, _SonarTime27, _SonarTime28, _SonarTime29, _SonarTime30};
                float _SonarDistances[30] = {	_SonarDistance1, _SonarDistance2, _SonarDistance3, _SonarDistance4, _SonarDistance5, _SonarDistance6, _SonarDistance7, _SonarDistance8, _SonarDistance9, _SonarDistance10,
                								_SonarDistance11, _SonarDistance12, _SonarDistance13, _SonarDistance14, _SonarDistance15, _SonarDistance16, _SonarDistance17, _SonarDistance18, _SonarDistance19, _SonarDistance20,
                								_SonarDistance21, _SonarDistance22, _SonarDistance23, _SonarDistance24, _SonarDistance25, _SonarDistance26, _SonarDistance27, _SonarDistance28, _SonarDistance29, _SonarDistance30};
                
                float Effects[30] = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
                
                for(int j = 0; j < 30; j++)
                {
                	// Cache distance vars
	            	float DistanceToImpactPoint = distance(i.vertexPos.xyz, _SonarPoints[j].xyz);
	            
	            	// Cached pulse vars
	            	float CurrentPulseDistance = _SonarDistances[j] * _SonarTimes[j];
	            	float DistanceAttenuation = 1.0f - _SonarTimes[j];
	            	
	            	float4 OutputCol = float4(0.0, 0.0, 0.0, 0.0);
	            	if(_SonarTimes[j] < 1.0f)
	            	{
		            	float DistanceBetweenImpactAndPulse = CurrentPulseDistance - DistanceToImpactPoint;
		            	if(DistanceBetweenImpactAndPulse > 0.0f)
		            	{
			            	float SonarRingThickness = 5.0f;
			            	
			            	if(DistanceBetweenImpactAndPulse < SonarRingThickness)
			            	{
			            		float PulseColour = clamp(1.0 - DistanceBetweenImpactAndPulse, 0.0, 1.0) * DistanceAttenuation * (1.0f - (DistanceBetweenImpactAndPulse / SonarRingThickness));
			            		Effects[j] = PulseColour;
			            	}
		            	}
	            	}
                }
                
                float outEffect = 0.0;
                for(int j = 0; j < 30; j++)
                {
                	outEffect += Effects[j];
                }
                
                float DistanceToPlayerPos = distance(i.vertexPos.xyz, _PlayerPosition.xyz);
                float PlayerAmbientDistance = 0.75f;
                
                float DistanceCoeff = PlayerAmbientDistance - DistanceToPlayerPos;
                if(DistanceCoeff > 0.0f)
                {
                	float AmbientLightEffect = DistanceCoeff / PlayerAmbientDistance;
                	outEffect += AmbientLightEffect;
                }
                
                outEffect = clamp(outEffect, 0.0, 1.0);
                
            	return float4(outEffect, outEffect, outEffect, 1.0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
