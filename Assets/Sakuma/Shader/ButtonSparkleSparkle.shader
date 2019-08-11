Shader "Custom/ButtonSparkleSparkle"
{
    Properties {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
		_Compression ("compression", Float)=0

		_Radius("Radius", Range(0,0.5)) = 0
		_Height("_Height", Range(0.001,1)) = 0
		_Width("_Width", Range(0.001,1)) = 0

		_S("S", Int) = 0
		_P("P", Int) = 0
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
			float _Compression;
			
			float _Radius;
			float _Height;
			float _Width;

			int _S;
			int _P;

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

				float2 pos[4];

				pos[0]=float2(0.5f-_Width/2,0.5f+_Height/2);
				pos[1]=float2(0.5f+_Width/2,0.5f+_Height/2);
				pos[2]=float2(0.5f+_Width/2,0.5f-_Height/2);
				pos[3]=float2(0.5f-_Width/2,0.5f-_Height/2);

				float data=_Radius;

				float wavePas=0;

				for(int i=0;i<4;i++){
					float dis=sqrt((output.uv.x-pos[i].x)*(output.uv.x-pos[i].x)+(output.uv.y-pos[i].y)*(output.uv.y-pos[i].y));
					if(dis<_Radius){
						//c.r=1;
					}
					float2 pos1;
					float2 pos2;

					switch(i){
						case 0:
						pos1=pos[0];
						pos2=pos[1]; 
						break;
						case 1:
						pos1=pos[1];
						pos2=pos[2]; 
						break;
						case 2:
						pos1=pos[2];
						pos2=pos[3]; 
						break;
						case 3:
						pos1=pos[3];
						pos2=pos[0]; 
						break;
					}

					float dis1=1;
					
					float wavePasBf;

					float a = pos2.x - pos1.x;
					float b = pos2.y - pos1.y;
					float a2 = a * a;
					float b2 = b * b;
					float r2 = a2 + b2;
					float tt = -(a*(pos1.x-output.uv.x)+b*(pos1.y-output.uv.y));
					if( tt < 0 ) {
						//dis1= (pos1.x-output.uv.x)*(pos1.x-output.uv.x) + (pos1.y-output.uv.y)*(pos1.y-output.uv.y);
					}
					else if( tt > r2 ) {
						dis1= (pos2.x-output.uv.x)*(pos2.x-output.uv.x) + (pos2.y-output.uv.y)*(pos2.y-output.uv.y);
						wavePasBf=(i*2)+2;
					}else{
						float f1 = a*(pos1.y-output.uv.y)-b*(pos1.x-output.uv.x);
						dis1= (f1*f1)/r2;
						wavePasBf=(i*2)+1;
					}
					dis1=sqrt(dis1);

					if(dis1<data){
						data=dis1;
						wavePas=wavePasBf;
					}


				}

				if(data<_Radius){
					//c.b=1;
					float pi=4*atan(1);

					float plas=0;
					switch(wavePas){
						case 1:
						
						plas=abs(pos[0].x-output.uv.x)/(2*_Width+2*_Height);
						break;
						case 2:
						plas=(_Width)/(2*_Width+2*_Height);
						break;
						case 3:
						plas=(_Width)/(2*_Width+2*_Height);
						plas+=abs(pos[1].y-output.uv.y)/(2*_Width+2*_Height);
						break;
						case 4:
						plas=(_Width+_Height)/(2*_Width+2*_Height);
						break;
						case 5:
						plas=(_Width+_Height)/(2*_Width+2*_Height);
						plas+=abs(pos[2].x-output.uv.x)/(2*_Width+2*_Height);
						break;
						case 6:
						plas=(2*_Width+_Height)/(2*_Width+2*_Height);
						break;
						case 7:
						plas=(2*_Width+_Height)/(2*_Width+2*_Height);
						plas+=abs(pos[3].y-output.uv.y)/(2*_Width+2*_Height);
						break;
						case 8:
						plas=(2*_Width+2*_Height)/(2*_Width+2*_Height);
						break;

					}
					float Spead=_S;
					float pop=_P;
					float Wave1=((sin(_Time.y*Spead+plas*2*pi*pop)+1)/8)+0.75;
					float Wave2=((sin(-_Time.y*2+plas*2*pi*7)+1)/8)+0.75;

					c.a=1-((data/(Wave1+Wave2)*2)/(_Radius));
					if(c.a<0){
						c.a=0;
					}
					//c.a*=Wave1;
				}


				if(output.uv.x>pos[0].x&&output.uv.x<pos[1].x&&output.uv.y<pos[0].y&&output.uv.y>pos[3].y){
					c.a=0;
				}


				c.rgb *= c.a;

                return c;
            }
        ENDCG
        }
    }
}
