Shader "CustomRenderTexture/SnowHeightMap"
{
    Properties
    {
        _DrawPosition ("DrawPosition", Vector) = (-1,-1,0,0)
        _BrushSize ("BrushSize",float) = 1
    }

     SubShader
     {
        Lighting Off
        Blend One Zero

        Pass
        {
            CGPROGRAM
            #include "UnityCustomRenderTexture.cginc"
            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment frag
            #pragma target 3.0

            float4 _DrawPosition;
            float _BrushSize;
            
            float4 frag(v2f_customrendertexture IN) : COLOR
            {
                float4 previousPixColor = tex2D(_SelfTexture2D,IN.localTexcoord);
                
                float pixDrawPosDistance = distance(IN.localTexcoord.xy,_DrawPosition);
                float4 drawColor = smoothstep(0,_BrushSize,pixDrawPosDistance);

                if(pixDrawPosDistance <= _BrushSize)
                    return previousPixColor * drawColor;
                else
                    return min(drawColor,previousPixColor);
            }
            
            ENDCG
        }
    }
}