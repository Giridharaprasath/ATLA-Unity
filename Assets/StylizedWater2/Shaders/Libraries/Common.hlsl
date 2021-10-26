//Stylized Water 2
//Staggart Creations (http://staggart.xyz)
//Copyright protected under Unity Asset Store EULA

#if defined(TESSELLATION_ON)
#if (defined(SHADER_API_D3D11) || defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE) || defined(SHADER_API_VULKAN) || defined(SHADER_API_METAL) || defined(SHADER_API_PSSL) || defined(SHADER_API_XBOXONE))
#define UNITY_CAN_COMPILE_TESSELLATION
#else
#error [Stylized Water] Current graphics API does not support tessellation (only Direct3D 11, OpenGL ES 3.0, OpenGL, Vulkan, Metal, PS4 and Xbox One)
#endif
#endif

//As per the "Shader" section of the documentation, this is primarily used to synchronizing animations in networked games
//#define USE_CUSTOM_TIME

#if !defined(USE_CUSTOM_TIME)
#define TIME_FRAG_INPUT input.uv.z
#define TIME_VERTEX_OUTPUT output.uv.z
#else
float _CustomTime;
#define TIME_FRAG_INPUT _CustomTime
#define TIME_VERTEX_OUTPUT _CustomTime
#endif

#define TIME ((TIME_FRAG_INPUT * _AnimationParams.z) * _AnimationParams.xy)
#define TIME_VERTEX ((TIME_VERTEX_OUTPUT * _AnimationParams.z) * _AnimationParams.xy)

#define HORIZONTAL_DISPLACEMENT_SCALAR 0.020
#define UP_VECTOR float3(0,1,0)

float2 GetSourceUV(float2 uv, float2 wPos, float state) 
{
	float2 output =  lerp(uv, wPos, state);
	//output.x = (int)((output.x / 0.5) + 0.5) * 0.5;
	//output.y = (int)((output.y / 0.5) + 0.5) * 0.5;

	#ifdef _RIVER
	//World-space tiling is useless in this case
	return uv;
	#endif
	
	return output;
}

float4 GetVertexColor(float4 inputColor, float4 mask)
{
	return inputColor * mask;
}

float DepthDistance(float3 wPos, float3 viewPos, float3 normal)
{
	return length((wPos - viewPos) * normal);
}

float4 PackedUV(float2 sourceUV, float2 time, float2 flowmap, float speed)
{
	#if _RIVER
	time *= flowmap;
	time.x = 0; //Only move in forward direction
	#endif
	
	float2 uv1 = sourceUV.xy + (time.xy * speed);
	#ifndef _RIVER
	//Second UV, 2x larger, twice as slow, in opposite direction
	float2 uv2 = (sourceUV.xy * 0.5) + ((1 - time.xy) * speed * 0.5);
	#else
	//2x larger, same direction/speed
	float2 uv2 = (sourceUV.xy * 0.5) + (time.xy * speed);
	#endif

	return float4(uv1.xy, uv2.xy);
}

float GetSlopeInverse(float3 normalWS)
{
	return saturate(pow(dot(UP_VECTOR, normalWS), 4));
}

struct DepthData
{
	float raw;
	float linear01;
	float eye;
};

//Return depth based on the used technique (buffer, vertex color, baked texture)
DepthData SampleDepth(float4 screenPos, float3 wPos)
{
	DepthData depth = (DepthData)0;

	screenPos /= screenPos.w;
	
#ifndef _DISABLE_DEPTH_TEX
	#ifndef UNITY_REVERSED_Z
	//screenPos.z = (UNITY_NEAR_CLIP_VALUE >= 0) ? screenPos.z : screenPos.z * 0.5 + 0.5;
	#endif
	depth.raw = SAMPLE_DEPTH(screenPos.xy);
	depth.eye = LINEAR_EYE_DEPTH(depth.raw);
	depth.linear01 = (depth.eye - LINEAR_EYE_DEPTH(screenPos.z)) * (lerp(1.0, _ProjectionParams.z, unity_OrthoParams.w)); //1=orthographic
#else
	depth.raw = 1;
	depth.eye = 1;
	depth.linear01 = 1;
#endif

	return depth;
}

float CheckPerspective(float x)
{
	return lerp(x, 1.0, unity_OrthoParams.w);
}

#define ORTHOGRAPHIC_SUPPORT

#if defined(USING_STEREO_MATRICES)
//Will never be used in VR, saves a per-fragment matrix multiplication
#undef ORTHOGRAPHIC_SUPPORT
#endif

//Reconstruct view-space position from depth.
float3 ReconstructViewPos(float4 screenPos, float3 viewDir, DepthData depth)
{
	#if defined(ORTHOGRAPHIC_SUPPORT)
	//View to world position
	float4 viewPos = float4((screenPos.xy/screenPos.w) * 2.0 - 1.0, depth.raw, 1.0);
	float4x4 viewToWorld = UNITY_MATRIX_I_VP;
	#if UNITY_REVERSED_Z //Wrecked since 7.3.1 "fix" and causes warping, invert second row https://issuetracker.unity3d.com/issues/shadergraph-inverse-view-projection-transformation-matrix-is-not-the-inverse-of-view-projection-transformation-matrix
	//Commit https://github.com/Unity-Technologies/Graphics/pull/374/files
	viewToWorld._12_22_32_42 = -viewToWorld._12_22_32_42;              
	#endif
	float4 viewWorld = mul(viewToWorld, viewPos);
	float3 viewWorldPos = viewWorld.xyz / viewWorld.w;
	#endif

	//Projection to world position
	float3 camPos = _WorldSpaceCameraPos.xyz;
	float3 worldPos = depth.eye * (viewDir/screenPos.w) - camPos;
	float3 perspWorldPos = -worldPos;

	#if defined(ORTHOGRAPHIC_SUPPORT)
	return lerp(perspWorldPos, viewWorldPos, unity_OrthoParams.w);
	#else
	return perspWorldPos;
	#endif

}
