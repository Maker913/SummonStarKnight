Shader "Custom/Plain"
{
    Properties {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
		_Height ("height", Float)=0
		_Width ("width", Float)=0
		_Compression ("compression", Float)=1

		_Fade("Fade", Range(0,1))=1

    }

    SubShader{
        Tags { 
            "Queue"="Transparent"
        }
       
	ZWrite Off
        Blend One OneMinusSrcAlpha //乗算済みアルファ

        Pass {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct VertexInput {
                float4 pos	:	POSITION;    // 3D座標
                float4 color:	COLOR;
                float2 uv	:	TEXCOORD0;   // テクスチャ座標
            };

            struct VertexOutput {
                float4 v	:	SV_POSITION; // 2D座標
                float4 color:	COLOR;	
                float2 uv	:   TEXCOORD0;   // テクスチャ座標
            };

            //プロパティの内容を受け取る
            float4 _Color;
            sampler2D _MainTex;
			sampler2D _BomTex;
			float _Width;
			float _Height;
			float _Compression;
			float _Fade;


            VertexOutput vert (VertexInput input) {
            	VertexOutput output;
            	output.v = UnityObjectToClipPos(input.pos);
            	output.uv = input.uv;

            	//もとの色(SpriteRendererのColor)と設定した色(TintColor)を掛け合わせる
            	output.color = input.color * _Color; 

            	return output;
            }

            float4 frag (VertexOutput output) : SV_Target {

				float4 c = _Color;//tex2D(_MainTex, output.uv);
				float data2=(sin((_Time.y*4)+(output.uv.x*5*(_Height/_Width)))+1)/2;
				float data=1+((-1*abs( output.uv.y-0.5f))*2)-(data2)/4;

				c.a=data*_Fade;
				c.rgb*= c.a;
                return c;
            }
        ENDCG
        }
    }
}
