Shader "Custom/grow"
{
	Properties{
		_MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)


		_Ridius("Ridius",Range(0,0.05)) = 0
		_Rate("Rate",Int) = 1
	}

		SubShader{
			Tags {
				"Queue" = "Transparent"
			}

		ZWrite Off
			Blend One OneMinusSrcAlpha//乗算済みアルファ


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

				float _Rate;
				float _Ridius;

				VertexOutput vert(VertexInput input) {
					VertexOutput output;
					output.v = UnityObjectToClipPos(input.pos);
					output.uv = input.uv;

					//もとの色(SpriteRendererのColor)と設定した色(TintColor)を掛け合わせる
					output.color = input.color ;

					return output;
				}

				float4 frag(VertexOutput output) : SV_Target {

					float4 c = tex2D(_MainTex, output.uv) * output.color;
					
					sampler2D Tex = _MainTex;

					if (tex2D(Tex, output.uv).a < 0.5) {
						
						float2 pos = float2(output.uv.x- _Ridius, output.uv.y - _Ridius);
						float leng = (_Ridius * 2) / _Rate;


						float dis=1;

						for (int i = 0; i < 30; i++) {

							for (int j = 0; j <30; j++) {


								float2 pos2 = float2(pos.x + (leng*i), pos.y + (leng*j));



								if (tex2D(Tex, pos2).a > 0.5) {

									float data = sqrt((pos2.x - output.uv.x)*(pos2.x - output.uv.x) + (pos2.y - output.uv.y)*(pos2.y - output.uv.y));

									if (data < dis&&data< _Ridius) {
										dis = data;
									}

								}




							}


						}

						if (dis < 1) {
							c.rgb= _Color;

							c.a =( _Ridius-dis)/ _Ridius;
							c.a = 1;
						}


					}


					c.rgb *= c.a;

					return c;
				}
			ENDCG
			}
		}
}
