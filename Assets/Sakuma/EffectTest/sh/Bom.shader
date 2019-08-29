Shader "Custom/Bom"
{
    Properties {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
		_Height ("height", Float)=0
		_Width ("width", Float)=0
		_Compression ("compression", Float)=0

		_Next("Time",Range(0,1))=0
		_Fade("Fade",Range(0,1))=1

		_pt1("pt1",Vector)=(0,0,0,0)
		_pt2("pt2",Vector)=(0,0,0,0)
		_pt3("pt3",Vector)=(0,0,0,0)
		_pt4("pt4",Vector)=(0,0,0,0)
		_pt5("pt5",Vector)=(0,0,0,0)
		_pt6("pt6",Vector)=(0,0,0,0)
		_pt7("pt7",Vector)=(0,0,0,0)
		_pt8("pt8",Vector)=(0,0,0,0)
		_pt9("pt9",Vector)=(0,0,0,0)
		_pt10("pt10",Vector)=(0,0,0,0)

		_AptFlg ("AptFlg", Float)=0

		_Apt1("Apt1",Vector)=(0,0,0,0)
		_Apt2("Apt2",Vector)=(0,0,0,0)
		_Apt3("Apt3",Vector)=(0,0,0,0)
		_Apt4("Apt4",Vector)=(0,0,0,0)
		_Apt5("Apt5",Vector)=(0,0,0,0)
		_Apt6("Apt6",Vector)=(0,0,0,0)

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
			
			float _Next;
			float _Fade;

			float4 _pt1;
			float4 _pt2;
			float4 _pt3;
			float4 _pt4;
			float4 _pt5;
			float4 _pt6;
			float4 _pt7;
			float4 _pt8;
			float4 _pt9;
			float4 _pt10;

			float _AptFlg;

			float4 _Apt1;
			float4 _Apt2;
			float4 _Apt3;
			float4 _Apt4;
			float4 _Apt5;
			float4 _Apt6;


            VertexOutput vert (VertexInput input) {
            	VertexOutput output;
            	output.v = UnityObjectToClipPos(input.pos);
            	output.uv = input.uv;

            	//もとの色(SpriteRendererのColor)と設定した色(TintColor)を掛け合わせる
            	output.color = input.color * _Color; 

            	return output;
            }

            float4 frag (VertexOutput output) : SV_Target {

				float4 c = tex2D(_MainTex, output.uv);
				float bump=15;

				if(_AptFlg==0){
					c.r=0;
					c.g=0;
					c.b=0;
					c.a=0;
				}
				if(c.r==1&&c.g==1&&c.b==1){
					c.a=0;
				}
				//辺の数、角度、p1のx、p1のy、p2のx...
				float data[6][14]={
				{5,50,_pt1.x,_pt1.y,_pt2.x,_pt2.y,_pt3.x,_pt3.y,_pt4.x,_pt4.y,_pt5.x,_pt5.y,_Apt1.x,_Apt1.y},
				{5,30,_pt1.x,_pt1.y,_pt2.x,_pt2.y,_pt7.x,_pt7.y,1     ,1     ,_pt6.x,_pt6.y,_Apt2.x,_Apt2.y},
				{5,200,_pt2.x,_pt2.y,_pt7.x,_pt7.y,1     ,0     ,_pt8.x,_pt8.y,_pt3.x,_pt3.y,_Apt3.x,_Apt3.y},
				{4,-120,_pt4.x,_pt4.y,_pt3.x,_pt3.y,_pt8.x,_pt8.y,_pt9.x,_pt9.y,0     ,0     ,_Apt4.x,_Apt4.y},
				{5,330,_pt5.x,_pt5.y,_pt4.x,_pt4.y,_pt9.x,_pt9.y,0     ,0     ,_pt10.x,_pt10.y,_Apt5.x,_Apt5.y},
				{5,-100,_pt1.x,_pt1.y,_pt5.x,_pt5.y,_pt10.x,_pt10.y,0   ,1     ,_pt6.x,_pt6.y,_Apt6.x,_Apt6.y}
				
				};
				
				float pushAngle[6];

				pushAngle[1]=atan2((_Apt1.y-_Apt2.y),(_Apt1.x-_Apt2.x));
				pushAngle[2]=atan2((_Apt1.y-_Apt3.y),(_Apt1.x-_Apt3.x));
				pushAngle[3]=atan2((_Apt1.y-_Apt4.y),(_Apt1.x-_Apt4.x));
				pushAngle[4]=atan2((_Apt1.y-_Apt5.y),(_Apt1.x-_Apt5.x));
				pushAngle[5]=atan2((_Apt1.y-_Apt6.y),(_Apt1.x-_Apt6.x));


				
				float aangle;
				float adis;
				float bangle;
				float bdis;

				bool peintFlg=false;

				for(int i=0;i<6;i++){
					int cont=0;

					float2 min;
					min.x=data[i][2];
					min.y=data[i][3];
					for(int ii=2;ii<=data[i][0]*2;ii+=2){
						if(min.x<data[i][ii]){min.x=data[i][ii];}
						if(min.y<data[i][ii+1]){min.y=data[i][ii+1];}
					}

					for(int j=0;j<data[i][0]*2;j+=2){

						float2 a;
						float2 b;
						float2 c;
						float2 d;

						if(j+2==data[i][0]*2){
							a.x=data[i][j+2];
							a.y=data[i][j+3];
							b.x=data[i][2];
							b.y=data[i][3];
						
						}else{
							a.x=data[i][j+2];
							a.y=data[i][j+3];
							b.x=data[i][j+4];
							b.y=data[i][j+5];
						}



						aangle=atan2((a.y-data[i][13]),(a.x-data[i][12]))+(data[i][1]*_Next)*3.1415f/180;
						adis=sqrt((a.y-data[i][13])*(a.y-data[i][13])+(a.x-data[i][12])*(a.x-data[i][12]));
						a.x=cos(aangle)*adis+data[i][12];
						a.y=sin(aangle)*adis+data[i][13];

						bangle=atan2((b.y-data[i][13]),(b.x-data[i][12]))+(data[i][1]*_Next)*3.1415f/180;
						bdis=sqrt((b.y-data[i][13])*(b.y-data[i][13])+(b.x-data[i][12])*(b.x-data[i][12]));
						b.x=cos(bangle)*bdis+data[i][12];
						b.y=sin(bangle)*bdis+data[i][13];


						if(i!=0){
							a.x-=cos(pushAngle[i])*_Next/bump;
							a.y-=sin(pushAngle[i])*_Next/bump;
							b.x-=cos(pushAngle[i])*_Next/bump;
							b.y-=sin(pushAngle[i])*_Next/bump;
						}







						c=output.uv;

						d.x=min.x-1;
						d.y=min.y-1;
						//ここから線分奴

						float ta=(c.x-d.x)*(a.y-c.y)+(c.y-d.y)*(c.x-a.x);
						float tb=(c.x-d.x)*(b.y-c.y)+(c.y-d.y)*(c.x-b.x);
						float tc=(a.x-b.x)*(c.y-a.y)+(a.y-b.y)*(a.x-c.x);
						float td=(a.x-b.x)*(d.y-a.y)+(a.y-b.y)*(a.x-d.x);


						if(tc*td<0&&ta*tb<0){
							cont++;
						}
					}

					if(cont%2==0){
						if(peintFlg==false){
							c.a=0;
							c.rgb*= c.a;
						}
					}else{
						if(_AptFlg==0){	
							c.r=(float)i/6;
						}else{
							float2 newpt;
							newpt.x=output.uv.x;
							newpt.y=output.uv.y;
							if(i!=0){
								newpt.x+=cos(pushAngle[i])*_Next/bump;
								newpt.y+=sin(pushAngle[i])*_Next/bump;

							}
							float uvdis=sqrt((newpt.y-data[i][13])*(newpt.y-data[i][13])+(newpt.x-data[i][12])*(newpt.x-data[i][12]));
							float uvangle=atan2((newpt.y-data[i][13]),(newpt.x-data[i][12]))-(data[i][1]*_Next)*3.1415f/180;
							newpt.x=cos(uvangle)*uvdis+data[i][12];
							newpt.y=sin(uvangle)*uvdis+data[i][13];

							if(peintFlg){
								if(c.a==0){
								c.rgb=tex2D(_MainTex, newpt);
								}
							}else{
								c.rgb=tex2D(_MainTex, newpt);
							}

						}
						c.a=1;
						if(c.r>=0.9f&&c.g>=0.9f&&c.b>=0.9f){
							c.a=0;
							c.rgb*= c.a;
						}
						peintFlg=true;
					}


				}


				if(c.a!=0){
					c.a=_Fade;
					c.rgb*= c.a;
				}





				if(_AptFlg==0){
					if(sqrt((_Apt1.x-output.uv.x)*(_Apt1.x-output.uv.x)+(_Apt1.y-output.uv.y)*(_Apt1.y-output.uv.y))<0.025f){
						c.b=1;
					}
					if(sqrt((_Apt2.x-output.uv.x)*(_Apt2.x-output.uv.x)+(_Apt2.y-output.uv.y)*(_Apt2.y-output.uv.y))<0.025f){
						c.b=1;
					}
					if(sqrt((_Apt3.x-output.uv.x)*(_Apt3.x-output.uv.x)+(_Apt3.y-output.uv.y)*(_Apt3.y-output.uv.y))<0.025f){
						c.b=1;
					}
					if(sqrt((_Apt4.x-output.uv.x)*(_Apt4.x-output.uv.x)+(_Apt4.y-output.uv.y)*(_Apt4.y-output.uv.y))<0.025f){
						c.b=1;
					}
					if(sqrt((_Apt5.x-output.uv.x)*(_Apt5.x-output.uv.x)+(_Apt5.y-output.uv.y)*(_Apt5.y-output.uv.y))<0.025f){
						c.b=1;
					}
					if(sqrt((_Apt6.x-output.uv.x)*(_Apt6.x-output.uv.x)+(_Apt6.y-output.uv.y)*(_Apt6.y-output.uv.y))<0.025f){
						c.b=1;
					}
					c.rgb*=tex2D(_MainTex, output.uv).rgb;
				}
                return c;
            }
        ENDCG
        }
    }
}
