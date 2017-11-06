Shader "Custom/Water" {
	Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _DispTex ("Disp Texture", 2D) = "gray" {}
        _NormalMap ("Normalmap", 2D) = "bump" {}

        _Tess ("Tessellation", Range(1,100)) = 4
        _Displacement ("Displacement", Range(0, 1.0)) = 0.3
        _Color ("Color", color) = (1,1,1,0)
        _SpecColor ("Spec color", color) = (0.5,0.5,0.5,0.5)
        
        _Alpha ("Alpha Intensidade", Range(0,1)) = 0.5
        
       	_Speed      ("Wave Speed", Range(0.1, 80)) = 2
	    _Length     ("Wave Length", Range(0,3)) = 2.6
	    _Amplitude  ("Wave Amplitude", Range(0,1)) = 0.3
        
       	_ScrollXSpeed ("X Scroll Speed", Range(-10, 10)) = 0
		_ScrollYSpeed ("Y Scroll Speed", Range(-10, 10)) = 0
		
		_Cubemap ("Cubemap", CUBE) = "" {}
		_EmissionAmount("Emission Amount", Range(0,1)) = 1
		_FresnelFalloff ("Fresnel Falloff", Range(0.1, 8.0)) = 2
		_SpecNS ("Specular Concentration", Range(0.01,1)) = 0.5
		_Gloss ("Gloss", Range(0,2)) = 1
	}
	CGINCLUDE
			#define UNITY_SETUP_BRDF_INPUT SpecularSetup
	ENDCG
	SubShader {
		Tags {"Queue"="AlphaTest" "RenderType"="TransparentCutout"}
		LOD 200
		ZWrite Off 
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
 		#pragma surface surf BlinnPhong vertex:disp tessellate:tessFixed nolightmap alpha
 		#pragma target 5.0
 		
		sampler2D _MainTex;
		sampler2D _DispTex;
		sampler2D _NormalMap;
	    fixed _TintAmount,_Amplitude,_ScrollXSpeed,_ScrollYSpeed,_Alpha,_EmissionAmount,_SpecNS,_Gloss;
		float _Tess,_Displacement,_ColisionX,ColisionY;
	    half _Speed,_Length,_FresnelFalloff;
        fixed4 _Color;
        samplerCUBE _Cubemap;


		struct appdata{
            float4 vertex : POSITION;
            float4 tangent : TANGENT;
            float3 normal : NORMAL;
            float2 texcoord : TEXCOORD0;
		};
		

        float4 tessFixed()
        {
            return _Tess;
        }
		
		struct Input {
			float2 uv_MainTex;
			float2 uv_NormalMap;
			float3 viewDir;
			float3 worldRefl;			
			INTERNAL_DATA // habilita World Reflection
		};
		
		void disp (inout appdata v)
        {
        	float waveHeight = v.vertex.y;
			
			waveHeight = sin( (_Time.y * _Speed) + v.vertex.x * _Length ) * _Amplitude;			  
			v.vertex.xyz = float3( v.vertex.x, v.vertex.y + waveHeight, v.vertex.z + waveHeight );
			
			//   Faz com que a textura do Disp mexa os vertices   //
			fixed2 scrolledDisp = v.texcoord;
			fixed xScrollValue = _ScrollXSpeed * _Time;
			fixed yScrollValue = _ScrollYSpeed * _Time;
			scrolledDisp += fixed2(xScrollValue, yScrollValue + _Time.x);
			//====================================================//
			
            float d = tex2Dlod(_DispTex, float4(v.texcoord.xy+scrolledDisp,0,0)).r * _Displacement;
            v.vertex.xyz += v.normal * d;
            v.normal = v.vertex;
        }

        void surf (Input IN, inout SurfaceOutput o) {
        
        	fixed2 scrolledUV = IN.uv_MainTex;
        	fixed2 scrolledNM = IN.uv_NormalMap;
			fixed xScrollValue = _ScrollXSpeed * _Time;
			fixed yScrollValue = _ScrollYSpeed * _Time;
			scrolledNM += fixed2(xScrollValue, yScrollValue);
			scrolledUV += fixed2(xScrollValue, yScrollValue);
			
			float InvNdotView = 1 - saturate(dot(o.Normal, normalize(IN.viewDir)));
			float rimEffect = pow( InvNdotView, _FresnelFalloff);		
			
            half4 c = tex2D (_MainTex, scrolledUV ) * _Color;
            
            
            o.Albedo = c.rgb;
			o.Emission = _EmissionAmount * (texCUBE(_Cubemap, -WorldReflectionVector(IN, o.Normal)).rgb * rimEffect);
			o.Specular = _SpecNS;
			o.Gloss = _Gloss;
            o.Alpha = _Alpha;
            o.Normal = UnpackNormal(tex2D(_NormalMap, scrolledNM));
        }
		ENDCG
	} 
	FallBack "Diffuse"
}
