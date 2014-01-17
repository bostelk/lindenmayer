Shader "Unlit/Vertex Color" {
    SubShader {
        Pass {
            Cull Off
            Fog { Mode Off }
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                // vertex input: position, color
                struct appdata {
                    float4 vertex : POSITION;
                    fixed4 color : COLOR;
                };

                struct v2f {
                    float4 pos : SV_POSITION;
                    fixed4 color : COLOR;
                };

                v2f vert (appdata IN) {
                    v2f OUT;
                    OUT.pos = mul( UNITY_MATRIX_MVP, IN.vertex );
                    OUT.color = IN.color;
                    return OUT;
                }

                fixed4 frag (v2f IN) : COLOR0 {
                    return IN.color;
                }
            ENDCG
        }
    }
}
