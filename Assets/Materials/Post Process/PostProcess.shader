// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:1,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:False,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:1,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:6,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:True,qofs:1,qpre:4,rntp:5,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:33399,y:33255,varname:node_2865,prsc:2|emission-2880-OUT;n:type:ShaderForge.SFN_TexCoord,id:4219,x:31938,y:33237,cmnt:Default coordinates,varname:node_4219,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Relay,id:8397,x:32163,y:33237,cmnt:Refract here,varname:node_8397,prsc:2|IN-4219-UVOUT;n:type:ShaderForge.SFN_Relay,id:4676,x:32530,y:33354,cmnt:Modify color here,varname:node_4676,prsc:2|IN-7542-RGB;n:type:ShaderForge.SFN_Tex2dAsset,id:4430,x:31938,y:33437,ptovrint:False,ptlb:MainTex,ptin:_MainTex,cmnt:MainTex contains the color of the scene,varname:node_9933,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:7542,x:32339,y:33354,varname:node_1672,prsc:2,ntxv:0,isnm:False|UVIN-8397-OUT,TEX-4430-TEX;n:type:ShaderForge.SFN_SceneDepth,id:7039,x:32416,y:33749,varname:node_7039,prsc:2|UV-1195-OUT;n:type:ShaderForge.SFN_Add,id:1195,x:32212,y:33749,varname:node_1195,prsc:2|A-4219-UVOUT,B-7374-OUT;n:type:ShaderForge.SFN_Vector2,id:7374,x:32055,y:33806,varname:node_7374,prsc:2,v1:0.001,v2:0.001;n:type:ShaderForge.SFN_SceneDepth,id:3031,x:32416,y:33889,varname:node_3031,prsc:2|UV-4990-OUT;n:type:ShaderForge.SFN_Add,id:4990,x:32212,y:33889,varname:node_4990,prsc:2|A-4219-UVOUT,B-4181-OUT;n:type:ShaderForge.SFN_Vector2,id:4181,x:32055,y:33946,varname:node_4181,prsc:2,v1:-0.001,v2:0.001;n:type:ShaderForge.SFN_SceneDepth,id:6366,x:32416,y:34032,varname:node_6366,prsc:2|UV-3689-OUT;n:type:ShaderForge.SFN_Add,id:3689,x:32212,y:34032,varname:node_3689,prsc:2|A-4219-UVOUT,B-5322-OUT;n:type:ShaderForge.SFN_Vector2,id:5322,x:32055,y:34089,varname:node_5322,prsc:2,v1:-0.001,v2:-0.001;n:type:ShaderForge.SFN_SceneDepth,id:4862,x:32416,y:34184,varname:node_4862,prsc:2|UV-1994-OUT;n:type:ShaderForge.SFN_Add,id:1994,x:32212,y:34184,varname:node_1994,prsc:2|A-4219-UVOUT,B-2402-OUT;n:type:ShaderForge.SFN_Vector2,id:2402,x:32055,y:34241,varname:node_2402,prsc:2,v1:0.001,v2:-0.001;n:type:ShaderForge.SFN_Add,id:4585,x:32640,y:33975,varname:node_4585,prsc:2|A-7039-OUT,B-3031-OUT,C-6366-OUT,D-4862-OUT;n:type:ShaderForge.SFN_SceneDepth,id:5293,x:32416,y:33590,varname:node_5293,prsc:2|UV-4219-UVOUT;n:type:ShaderForge.SFN_Subtract,id:1790,x:32860,y:33607,varname:node_1790,prsc:2|A-3373-OUT,B-5293-OUT;n:type:ShaderForge.SFN_Divide,id:3373,x:32967,y:34007,varname:node_3373,prsc:2|A-4585-OUT,B-6291-OUT;n:type:ShaderForge.SFN_Vector1,id:6291,x:32746,y:34132,varname:node_6291,prsc:2,v1:4;n:type:ShaderForge.SFN_Multiply,id:2880,x:32900,y:33354,varname:node_2880,prsc:2|A-4676-OUT,B-4736-OUT;n:type:ShaderForge.SFN_SceneDepth,id:8000,x:35301,y:32879,varname:node_8000,prsc:2;n:type:ShaderForge.SFN_SceneDepth,id:9112,x:35365,y:32943,varname:node_9112,prsc:2;n:type:ShaderForge.SFN_SceneDepth,id:2,x:35621,y:33199,varname:node_2,prsc:2;n:type:ShaderForge.SFN_SceneDepth,id:8071,x:35365,y:32943,varname:node_8071,prsc:2;n:type:ShaderForge.SFN_SceneDepth,id:9299,x:35429,y:33007,varname:node_9299,prsc:2;n:type:ShaderForge.SFN_SceneDepth,id:2074,x:35685,y:33263,varname:node_2074,prsc:2;n:type:ShaderForge.SFN_SceneDepth,id:8062,x:35365,y:32943,varname:node_8062,prsc:2;n:type:ShaderForge.SFN_SceneDepth,id:770,x:35429,y:33007,varname:node_770,prsc:2;n:type:ShaderForge.SFN_SceneDepth,id:2065,x:35685,y:33263,varname:node_2065,prsc:2;n:type:ShaderForge.SFN_SceneDepth,id:9652,x:35429,y:33007,varname:node_9652,prsc:2;n:type:ShaderForge.SFN_SceneDepth,id:2344,x:35493,y:33071,varname:node_2344,prsc:2;n:type:ShaderForge.SFN_SceneDepth,id:6648,x:35749,y:33327,varname:node_6648,prsc:2;n:type:ShaderForge.SFN_OneMinus,id:4736,x:33039,y:33613,varname:node_4736,prsc:2|IN-1790-OUT;proporder:4430;pass:END;sub:END;*/

Shader "Shader Forge/PostProcess" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Overlay+1"
            "RenderType"="Overlay"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            ZTest Always
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _CameraDepthTexture;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float2 node_8397 = i.uv0; // Refract here
                float4 node_1672 = tex2D(_MainTex,TRANSFORM_TEX(node_8397, _MainTex));
                float3 node_4676 = node_1672.rgb; // Modify color here
                float node_7039 = max(0, LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, (i.uv0+float2(0.001,0.001)))) - _ProjectionParams.g);
                float node_4585 = (node_7039+max(0, LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, (i.uv0+float2(-0.001,0.001)))) - _ProjectionParams.g)+max(0, LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, (i.uv0+float2(-0.001,-0.001)))) - _ProjectionParams.g)+max(0, LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, (i.uv0+float2(0.001,-0.001)))) - _ProjectionParams.g));
                float node_5293 = max(0, LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv0)) - _ProjectionParams.g);
                float node_1790 = ((node_4585/4.0)-node_5293);
                float3 emissive = (node_4676*(1.0 - node_1790));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
