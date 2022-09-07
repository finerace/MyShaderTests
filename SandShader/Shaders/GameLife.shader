Shader "Unlit/GameLife"
{
    
    Properties
    {
        
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
            float4 _DrawColor;
            
            float4 get(v2f_customrendertexture IN,int x,int y) : COLOR
            {
                fixed2 uv = IN.localTexcoord.xy +
                    fixed2(x/_CustomRenderTextureWidth,y/_CustomRenderTextureHeight); 

                if(uv.y < 0)
                    return float4(1,1,1,1);
                else if(uv.y > 1)
                    return float4(0,0,0,0);
                
                return tex2D(_SelfTexture2D,uv);
            }
            
            float4 frag(v2f_customrendertexture IN) : COLOR
            {
                float4 pixColor = get(IN,0,0);
                float somethingValue = 0.1;
                bool isPixEmpty = pixColor.a < somethingValue;
                float4 resultColor;
                const float4 nothing = float4(0,0,0,0);

                if(distance(_DrawPosition,IN.localTexcoord.xy) < 0.01)
                    resultColor = _DrawColor;
                else if(isPixEmpty)
                {
                    float4 upPixColor = get(IN,0,1);
                    bool isUpPixEmpty = upPixColor.a < somethingValue;
                    
                    if(!isUpPixEmpty)
                    {
                        resultColor = upPixColor;                           
                    }
                }
                else
                {
                    bool isDownPixEmpty = get(IN,0,-1).a < somethingValue;
                    
                    if(isDownPixEmpty)
                    {
                        resultColor = nothing;                           
                    }
                    else
                    {
                        resultColor = pixColor;
                    }
                }
                
                return resultColor;
            }
            
            ENDCG
        }
        
    }
    
    
}
