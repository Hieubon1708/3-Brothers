Shader "Custom/Splatmap"
{
    Properties
    {
        _Control ("Control Map (R = Grass, G = Dirt)", 2D) = "white" {}
        _GrassTex ("Grass Texture", 2D) = "white" {}
        _DirtTex ("Dirt Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _Control;
        sampler2D _GrassTex;
        sampler2D _DirtTex;

        struct Input
        {
            float2 uv_Control;   
            float2 uv_GrassTex;
            float2 uv_DirtTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            float4 control = tex2D(_Control, IN.uv_Control);
            float4 grassCol = tex2D(_GrassTex, IN.uv_GrassTex);
            float4 dirtCol = tex2D(_DirtTex, IN.uv_DirtTex);

            float blendGrass = control.r;
            float blendDirt = control.g;

            float total = blendGrass + blendDirt + 0.0001;
            blendGrass /= total;
            blendDirt /= total;

            fixed4 finalCol = grassCol * blendGrass + dirtCol * blendDirt;

            o.Albedo = finalCol.rgb;
            o.Alpha = 1;
        }
        ENDCG
    }

    Fallback "Diffuse"
}
