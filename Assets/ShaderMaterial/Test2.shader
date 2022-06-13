// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33512,y:32519,varname:node_4013,prsc:2|diff-5149-OUT,spec-431-OUT,emission-1659-RGB,clip-2581-OUT,olwid-5840-OUT;n:type:ShaderForge.SFN_NormalVector,id:1849,x:32673,y:32564,prsc:2,pt:False;n:type:ShaderForge.SFN_LightVector,id:9485,x:32673,y:32729,varname:node_9485,prsc:2;n:type:ShaderForge.SFN_Dot,id:2507,x:32896,y:32657,varname:node_2507,prsc:2,dt:1|A-1849-OUT,B-9485-OUT;n:type:ShaderForge.SFN_Tex2d,id:130,x:33034,y:32060,ptovrint:False,ptlb:node_130,ptin:_node_130,varname:node_130,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:817d24285c011e24eb8085b8bb6b811e,ntxv:0,isnm:False|UVIN-6324-UVOUT;n:type:ShaderForge.SFN_Slider,id:1137,x:32673,y:32941,ptovrint:False,ptlb:node_1137,ptin:_node_1137,varname:node_1137,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.7692308,max:3;n:type:ShaderForge.SFN_Multiply,id:431,x:33076,y:32657,varname:node_431,prsc:2|A-2507-OUT,B-1137-OUT;n:type:ShaderForge.SFN_TexCoord,id:4112,x:32717,y:33158,varname:node_4112,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:7582,x:32919,y:33158,varname:node_7582,prsc:2,spu:0,spv:0.2|UVIN-4112-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:1659,x:33131,y:33158,ptovrint:False,ptlb:node_1659,ptin:_node_1659,varname:node_1659,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8b18d84ef57863049ae04994c30d773e,ntxv:0,isnm:False|UVIN-7582-UVOUT;n:type:ShaderForge.SFN_Panner,id:6324,x:32800,y:32060,varname:node_6324,prsc:2,spu:0,spv:-0.5|UVIN-1181-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:1181,x:32610,y:32060,varname:node_1181,prsc:2,uv:0;n:type:ShaderForge.SFN_Tex2d,id:6600,x:34060,y:32341,ptovrint:False,ptlb:node_6600,ptin:_node_6600,varname:node_6600,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7542b9521e250d245b3b66f36cec4405,ntxv:0,isnm:False|UVIN-4127-UVOUT;n:type:ShaderForge.SFN_Multiply,id:5149,x:33356,y:32057,varname:node_5149,prsc:2|A-130-RGB,B-6600-RGB;n:type:ShaderForge.SFN_TexCoord,id:9118,x:33647,y:32341,varname:node_9118,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:4127,x:33834,y:32341,varname:node_4127,prsc:2,spu:0,spv:0.1|UVIN-9118-UVOUT;n:type:ShaderForge.SFN_Vector1,id:2581,x:33002,y:32349,varname:node_2581,prsc:2,v1:0.54;n:type:ShaderForge.SFN_Slider,id:5840,x:32787,y:32459,ptovrint:False,ptlb:node_5840,ptin:_node_5840,varname:node_5840,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.01,max:1;n:type:ShaderForge.SFN_Panner,id:505,x:33957,y:32032,varname:node_505,prsc:2,spu:1,spv:1;n:type:ShaderForge.SFN_VertexColor,id:2803,x:33678,y:32036,varname:node_2803,prsc:2;proporder:130-1137-1659-6600-5840;pass:END;sub:END;*/

Shader "Shader Forge/Test2" {
    Properties {
        _node_130 ("node_130", 2D) = "white" {}
        _node_1137 ("node_1137", Range(0, 3)) = 0.7692308
        _node_1659 ("node_1659", 2D) = "white" {}
        _node_6600 ("node_6600", 2D) = "white" {}
        _node_5840 ("node_5840", Range(0, 1)) = 0.01
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float _node_5840;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_FOG_COORDS(0)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = mul(UNITY_MATRIX_MVP, float4(v.vertex.xyz + v.normal*_node_5840,1) );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                clip(0.54 - 0.5);
                return fixed4(float3(0,0,0),0);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _node_130; uniform float4 _node_130_ST;
            uniform float _node_1137;
            uniform sampler2D _node_1659; uniform float4 _node_1659_ST;
            uniform sampler2D _node_6600; uniform float4 _node_6600_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                clip(0.54 - 0.5);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float node_431 = (max(0,dot(i.normalDir,lightDirection))*_node_1137);
                float3 specularColor = float3(node_431,node_431,node_431);
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 node_9283 = _Time + _TimeEditor;
                float2 node_6324 = (i.uv0+node_9283.g*float2(0,-0.5));
                float4 _node_130_var = tex2D(_node_130,TRANSFORM_TEX(node_6324, _node_130));
                float2 node_4127 = (i.uv0+node_9283.g*float2(0,0.1));
                float4 _node_6600_var = tex2D(_node_6600,TRANSFORM_TEX(node_4127, _node_6600));
                float3 diffuseColor = (_node_130_var.rgb*_node_6600_var.rgb);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float2 node_7582 = (i.uv0+node_9283.g*float2(0,0.2));
                float4 _node_1659_var = tex2D(_node_1659,TRANSFORM_TEX(node_7582, _node_1659));
                float3 emissive = _node_1659_var.rgb;
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _node_130; uniform float4 _node_130_ST;
            uniform float _node_1137;
            uniform sampler2D _node_1659; uniform float4 _node_1659_ST;
            uniform sampler2D _node_6600; uniform float4 _node_6600_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                clip(0.54 - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float node_431 = (max(0,dot(i.normalDir,lightDirection))*_node_1137);
                float3 specularColor = float3(node_431,node_431,node_431);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 node_2122 = _Time + _TimeEditor;
                float2 node_6324 = (i.uv0+node_2122.g*float2(0,-0.5));
                float4 _node_130_var = tex2D(_node_130,TRANSFORM_TEX(node_6324, _node_130));
                float2 node_4127 = (i.uv0+node_2122.g*float2(0,0.1));
                float4 _node_6600_var = tex2D(_node_6600,TRANSFORM_TEX(node_4127, _node_6600));
                float3 diffuseColor = (_node_130_var.rgb*_node_6600_var.rgb);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                clip(0.54 - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
