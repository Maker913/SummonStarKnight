Shader "Custom/Sparkle"
{
    Properties {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
		_Compression ("compression", Float)=0

		_Radius("Radius", Range(0,0.5)) = 0

		_Pawa("Pawa", Range(0.001,1)) = 0
    }

    SubShader{
        Tags { 
            "Queue"="Transparent"
        }
       
	ZWrite Off
        //Blend One OneMinusSrcAlpha //乗算済みアルファ
			Blend One OneMinusSrcColor
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
			float _Compression;
			
			float _Radius;
			float _Pawa;

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

#if false
				//float data2=(sin((_Time.y*4)+(output.uv.x*5*(_Height/_Width)))+1)/2;
				//float data=1+((-1*abs( output.uv.y-0.5f))*2)-(data2)/4;


				float data =sqrt((0.5f- output.uv.x)*(0.5f- output.uv.x)+(0.5f- output.uv.y)*(0.5f- output.uv.y));

				if ((0.5f*_Rate) - data < 0) {
					c.a = 0;
				}
				else {
					c.a = data/(_Rate/2);
				}


				c.a *= 1-_Rate;
#endif
				
				float dis = sqrt((0.5f - output.uv.x)*(0.5f - output.uv.x) + (0.5f - output.uv.y)*(0.5f - output.uv.y));

				if (dis > _Radius) {


					float wave1 = ((sin(_Time.w + (atan2(output.uv.x - 0.5, output.uv.y - 0.5) * 15)) + 1) / 2);
					float wave2 = ((sin(-_Time.z + (atan2(output.uv.x - 0.5, output.uv.y - 0.5) * 9)) + 1) / 2);

					float data=c.rgba *= 1-(dis - _Radius)/((0.5f- _Radius)*(_Pawa)* (((wave1+wave2)/2/2)+0.5f) );
					if (data > 0) {
						c.a *= data;
					}
					else {
						c.a = 0;
					}
				}
				else {
					c.rgba = 0;
				}



				//c.rgb *= c.a;

                return c;
            }
        ENDCG
        }
    }
}
