Shader "Custom/DissolveUnlit"
{
    Properties
    {
        _BaseColor ("Color", Color) = (1,1,1,1)
        _Dissolve ("Dissolve", Range(0,1)) = 0
    }

    SubShader
    {
        Tags 
        { 
            "RenderPipeline"="UniversalPipeline"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            float4 _BaseColor;
            float _Dissolve;

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.worldPos = TransformObjectToWorld(IN.positionOS.xyz);
                return OUT;
            }

            half4 frag (Varyings IN) : SV_Target
            {
                // Simple noise using world position
                float noise = frac(
                    sin(dot(IN.worldPos.xy, float2(12.9898,78.233))) * 43758.5453
                );

                // Burst dissolve
                clip(noise - _Dissolve);

                return _BaseColor;
            }
            ENDHLSL
        }
    }
}
