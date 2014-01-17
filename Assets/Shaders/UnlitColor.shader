Shader "Unlit/Color" {
    Properties {
        _Color ("Color", Color) = (1, 1, 1, 1)
    }
    SubShader {
        Tags { "Queue" = "Transparent" }
        Pass {
            Cull Off
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                fixed4 _Color;

                // vertex input: position, color
                struct appdata {
                    float4 vertex : POSITION;
                };

                struct v2f {
                    float4 pos : SV_POSITION;
                };

                v2f vert (appdata v) {
                    v2f o;
                    o.pos = mul( UNITY_MATRIX_MVP, v.vertex );
                    return o;
                }

                fixed4 frag (v2f i) : COLOR0 {
                    return _Color;
                }
            ENDCG
        }
    }
}
