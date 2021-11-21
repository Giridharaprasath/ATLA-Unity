using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Broccoli.Controller {
	/// <summary>
	/// Controls a Tree Broccoli Instances.
	/// </summary>
	[ExecuteInEditMode]
	public class BroccoTreeController_1_2_5 : MonoBehaviour {
		#region Vars
		/// <summary>
		/// Version of Broccoli Tree Creator issuing this controller.
		/// </summary>
		public string version = "";
		/// <summary>
		/// Type of shaders available.
		/// </summary>
		public enum ShaderType {
			Standard,
			TreeCreatorOrCompatible,
			SpeedTree7OrCompatible,
			SpeedTree8OrCompatible,
			Billboard
		}
		/// <summary>
		/// Type of shader used to process this instance.
		/// </summary>
		public ShaderType shaderType = ShaderType.Standard;
		/// <summary>
		/// True if this instance has wind data based on SpeedTree.
		/// </summary>
		private bool hasSpeedTreeWind { 
			get { return shaderType == ShaderType.SpeedTree7OrCompatible || shaderType == ShaderType.SpeedTree8OrCompatible; } 
		}
		/// <summary>
		/// True if this instance has wind data based on Unity Tree Creator.
		/// </summary>
		private bool hasTreeCreatorWind {
			get { return shaderType == ShaderType.TreeCreatorOrCompatible; }
		}
		/// <summary>
		/// True to preview wind on the editor at all times.
		/// </summary>
		[HideInInspector]
		public bool editorWindAlways = false;
		/// <summary>
		/// The renderer of the tree.
		/// </summary>
		Renderer _renderer;
		/// <summary>
		/// Material property block to set shader values.
		/// </summary>
		MaterialPropertyBlock _propBlock;
		/// <summary>
		/// The wind vector affecting this instance.
		/// </summary>
		Vector4 wind = Vector4.zero;
		/// <summary>
		/// True to preview wind on the editor when requested.
		/// </summary>
		[HideInInspector]
		public bool editorWindEnabled = false;
		public enum WindType {
			None,
			TreeCreator,
			ST7,
			ST8
		}
		public WindType windType = WindType.None;
		public enum WindQuality {
			None,
			Fastest,
			Fast,
			Better,
			Best,
			Palm
		}
		public WindQuality windQuality = WindQuality.Better;
		private float baseWindAmplitude = 0.2752f;
		public float localWindAmplitude = 1f;
		public static float globalWindAmplitude = 1f;
		public float windMain = 0f;
		#endregion

		#region Shader values
		float valueTime = 0f;
		Vector4 valueWindDirection = Vector4.zero;
		Vector4 valueSTWindVector = Vector4.zero;
		Vector4 valueSTWindGlobal = Vector4.zero;
		Vector4 valueSTWindBranch = Vector4.zero;
		Vector4 valueSTWindBranchTwitch = Vector4.zero;
		Vector4 valueSTWindBranchWhip = Vector4.zero;
		Vector4 valueSTWindBranchAnchor = Vector4.zero;
		Vector4 valueSTWindBranchAdherences = Vector4.zero;
		Vector4 valueSTWindTurbulences = Vector4.zero;
		Vector4 valueSTWindLeaf1Ripple = Vector4.zero;
		Vector4 valueSTWindLeaf1Tumble = Vector4.zero;
		Vector4 valueSTWindLeaf1Twitch = Vector4.zero;
		Vector4 valueSTWindLeaf2Ripple = Vector4.zero;
		Vector4 valueSTWindLeaf2Tumble = Vector4.zero;
		Vector4 valueSTWindLeaf2Twitch = Vector4.zero;
		Vector4 valueSTWindFrondRipple = Vector4.zero;
		#endregion

		#region Shader Property Ids
		static int propWindEnabled = 0;
		static int propWindQuality = 0;
		static int propSTWindVector = 0;
		static int propSTWindGlobal = 0;
		static int propSTWindBranch = 0;
		static int propSTWindBranchTwitch = 0;
		static int propSTWindBranchWhip = 0;
		static int propSTWindBranchAnchor = 0;
		static int propSTWindBranchAdherences = 0;
		static int propSTWindTurbulences = 0;
		static int propSTWindLeaf1Ripple = 0;
		static int propSTWindLeaf1Tumble = 0;
		static int propSTWindLeaf1Twitch = 0;
		static int propSTWindLeaf2Ripple = 0;
		static int propSTWindLeaf2Tumble = 0;
		static int propSTWindLeaf2Twitch = 0;
		static int propSTWindFrondRipple = 0;
		#endregion

		#region Static Constructor
		#if UNITY_2017_2_OR_NEWER
		static BroccoTreeController_1_2_5 () {
			propWindEnabled = Shader.PropertyToID ("_WindEnabled");
			propWindQuality = Shader.PropertyToID ("_WindQuality");
			propSTWindVector = Shader.PropertyToID ("_ST_WindVector");
			propSTWindVector = Shader.PropertyToID ("_ST_WindVector");
			propSTWindGlobal = Shader.PropertyToID ("_ST_WindGlobal");
			propSTWindBranch = Shader.PropertyToID ("_ST_WindBranch");
			propSTWindBranchTwitch = Shader.PropertyToID ("_ST_WindBranchTwitch");
			propSTWindBranchWhip = Shader.PropertyToID ("_ST_WindBranchWhip");
			propSTWindBranchAnchor = Shader.PropertyToID ("_ST_WindBranchAnchor");
			propSTWindBranchAdherences = Shader.PropertyToID ("_ST_WindBranchAdherences");
			propSTWindTurbulences = Shader.PropertyToID ("_ST_WindTurbulences");
			propSTWindLeaf1Ripple = Shader.PropertyToID ("_ST_WindLeaf1Ripple");
			propSTWindLeaf1Tumble = Shader.PropertyToID ("_ST_WindLeaf1Tumble");
			propSTWindLeaf1Twitch = Shader.PropertyToID ("_ST_WindLeaf1Twitch");
			propSTWindLeaf2Ripple = Shader.PropertyToID ("_ST_WindLeaf2Ripple");
			propSTWindLeaf2Tumble = Shader.PropertyToID ("_ST_WindLeaf2Tumble");
			propSTWindLeaf2Twitch = Shader.PropertyToID ("_ST_WindLeaf2Twitch");
			propSTWindFrondRipple = Shader.PropertyToID ("_ST_WindFrondRipple");
		}
		#endif
		#endregion

		#region Events
		public void Awake () {
			#if !UNITY_2017_2_OR_NEWER
			propWindEnabled = Shader.PropertyToID ("_WindEnabled");
			propWindQuality = Shader.PropertyToID ("_WindQuality");
			propSTWindVector = Shader.PropertyToID ("_ST_WindVector");
			propSTWindVector = Shader.PropertyToID ("_ST_WindVector");
			propSTWindGlobal = Shader.PropertyToID ("_ST_WindGlobal");
			propSTWindBranch = Shader.PropertyToID ("_ST_WindBranch");
			propSTWindBranchTwitch = Shader.PropertyToID ("_ST_WindBranchTwitch");
			propSTWindBranchWhip = Shader.PropertyToID ("_ST_WindBranchWhip");
			propSTWindBranchAnchor = Shader.PropertyToID ("_ST_WindBranchAnchor");
			propSTWindBranchAdherences = Shader.PropertyToID ("_ST_WindBranchAdherences");
			propSTWindTurbulences = Shader.PropertyToID ("_ST_WindTurbulences");
			propSTWindLeaf1Ripple = Shader.PropertyToID ("_ST_WindLeaf1Ripple");
			propSTWindLeaf1Tumble = Shader.PropertyToID ("_ST_WindLeaf1Tumble");
			propSTWindLeaf1Twitch = Shader.PropertyToID ("_ST_WindLeaf1Twitch");
			propSTWindLeaf2Ripple = Shader.PropertyToID ("_ST_WindLeaf2Ripple");
			propSTWindLeaf2Tumble = Shader.PropertyToID ("_ST_WindLeaf2Tumble");
			propSTWindLeaf2Twitch = Shader.PropertyToID ("_ST_WindLeaf2Twitch");
			propSTWindFrondRipple = Shader.PropertyToID ("_ST_WindFrondRipple");
			#endif
		}
		/// <summary>
		/// Start this instance.
		/// </summary>
		public void Start () {
			// Get renderer component.
			_renderer = GetComponent<Renderer> ();
			if (_renderer != null && shaderType != ShaderType.Standard) {
				_propBlock = new MaterialPropertyBlock ();
				_renderer.GetPropertyBlock (_propBlock);
				if (hasSpeedTreeWind) {
					SetupSpeedTreeWind ();
				} else if (hasTreeCreatorWind) {
					SetupTreeCreatorWind ();
				}
			}
		}
		/// <summary>
		/// Update this instance.
		/// </summary>
		void Update () {
			if (_renderer != null && _renderer.isVisible) {
				#if UNITY_EDITOR
				if (hasSpeedTreeWind && EditorApplication.isPlaying) {
				#else
				if (hasSpeedTreeWind) {
				#endif
					UpdateSpeedTreeWind ();
				}
			}
		}
		void EditorUpdate () {
			#if UNITY_EDITOR
			if (editorWindEnabled && hasSpeedTreeWind && !EditorApplication.isPlaying) {
				UpdateSpeedTreeWind ();
			}
			#endif
		}
		#if UNITY_EDITOR
		/// <summary>
		/// Raises the enable event.
		/// </summary>
		void OnEnable () {
			#if UNITY_2017_2_OR_NEWER
			EditorApplication.playModeStateChanged += StateChange;
			#else
			EditorApplication.playmodeStateChanged += StateChangeFormer;
			#endif
			#if UNITY_EDITOR
			EditorApplication.update += EditorUpdate;
			#endif
		}
		/// <summary>
		/// Raises the disable event.
		/// </summary>
		void OnDisable () {
			#if UNITY_2017_2_OR_NEWER
			EditorApplication.playModeStateChanged += StateChange;
			#else
			EditorApplication.playmodeStateChanged += StateChangeFormer;
			#endif
			#if UNITY_EDITOR
			EditorApplication.update -= EditorUpdate;
			#endif
		}
		/// <summary>
		/// Editor state has changed.
		/// </summary>
		void StateChangeFormer () {
			if (EditorApplication.isPlayingOrWillChangePlaymode && EditorApplication.isPlaying) {
				editorWindEnabled = windType != WindType.None;
			} else {
				editorWindEnabled = false;
			}
		}
		#if UNITY_2017_2_OR_NEWER
		void StateChange (PlayModeStateChange state) {
			if (state == PlayModeStateChange.EnteredPlayMode || state == PlayModeStateChange.ExitingEditMode) {
				editorWindEnabled = windType != WindType.None;
			} else {
				editorWindEnabled = false;
			}
		}
		#endif
		#endif
		#endregion

		#region Wind
		public void UpdateWind () {
			bool isEnabled = windQuality != WindQuality.None;
			if (hasSpeedTreeWind) {
				SetupSpeedTreeWind (isEnabled);
			} else {
				SetupTreeCreatorWind (isEnabled);
			}
		}
		public void SetupTreeCreatorWind (bool enable = true) {
			wind = Vector4.zero;
			float factor = 1f;
			float freq = 1f;
			Vector4 windZoneDirection = Vector4.zero;
			if (enable) {
				Vector4 windZoneFactor = Vector4.zero;
				WindZone[] windZones = FindObjectsOfType<WindZone> ();
				for (int i = 0; i < windZones.Length; i++) {
					if (windZones [i].gameObject.activeSelf && windZones[i].mode == WindZoneMode.Directional) {
						windZoneDirection = new Vector4 (windZones [i].transform.forward.x, windZones [i].transform.forward.y, windZones [i].transform.forward.z, 1f);
						factor = windZones [i].windMain * (windZones [i].windPulseMagnitude + 1f);
						// Since the frequency solving function in Unity Tree Creator is obscured we use this one instead.
						freq = (float)(Mathf.Cos (windZones [i].windPulseFrequency * Mathf.PI) *
							Mathf.Cos (windZones [i].windPulseFrequency * 3 * Mathf.PI) *
							Mathf.Cos (windZones [i].windPulseFrequency * 5 * Mathf.PI) + 
							Mathf.Sin (windZones [i].windPulseFrequency * 25 * Mathf.PI) * 0.1f);
						factor *= freq;

						windZoneFactor = new Vector4 (windZoneDirection.x * factor, 
							windZoneDirection.y * factor, 
							windZoneDirection.z * factor, 
							windZones [i].windTurbulence * (windZones[i].windPulseMagnitude + 1f));
						wind += windZoneFactor;
					}
				}
			}
			if (_propBlock != null) {
				_propBlock.SetVector ("_Wind", wind);
				_renderer.SetPropertyBlock (_propBlock);
			}
		}
		public void SetupSpeedTreeWind (bool enable = true) {
			/*
			_ST_WindGlobal.x *= _WindSpeed;
			_ST_WindGlobal.y *= _WindAmplitude;
			_ST_WindGlobal.z *= _WindDegreeSlope;
			_ST_WindBranchAdherences.x *= _WindConstantTilt;
			_ST_WindLeaf1Ripple.y *= _LeafRipple;
			_ST_WindLeaf2Ripple.y *= _LeafRipple;
			_ST_WindLeaf1Ripple.x *= _LeafRippleSpeed;
			_ST_WindLeaf2Ripple.x *= _LeafRippleSpeed;
			_ST_WindLeaf1Tumble.yz *= _LeafTumble;
			_ST_WindLeaf2Tumble.yz *= _LeafTumble;
			_ST_WindLeaf1Tumble.x *= _LeafTumbleSpeed;
			_ST_WindLeaf2Tumble.x *= _LeafTumbleSpeed;
			_ST_WindBranch.y *= _BranchRipple;
			_ST_WindBranch.x *= _BranchRippleSpeed;
			_ST_WindBranchTwitch.x *= _BranchTwitch;
			_ST_WindBranchWhip.x *= _BranchWhip;
			_ST_WindTurbulences.x *= _BranchTurbulences;
			_ST_WindBranchAnchor.xyz *= float3(1, _BranchHeaviness, 1);
			_ST_WindBranchAnchor.w *= _BranchForceHeaviness;
			*/
			if (_propBlock == null) return;
			if (!enable) {
				_propBlock.SetFloat (propWindEnabled, 0f);
				_renderer.SetPropertyBlock (_propBlock);
				return;
			}
			GetWindZoneValues ();
			// WindEnabled
			_propBlock.SetFloat (propWindEnabled, (enable?1f:0f));
			// WindQuality
			_propBlock.SetFloat (propWindQuality, (float)windQuality); // TODO: set quality from var
			// STWindVector
			valueSTWindVector = valueWindDirection;
			_propBlock.SetVector (propSTWindVector, valueSTWindVector);
			// STWindGlobal (time / 2, 0.3, 0.1, 1.7)
			valueSTWindGlobal = new Vector4 (Time.time * 0.36f, 
				baseWindAmplitude * localWindAmplitude * globalWindAmplitude * windMain, 
				0.0655f, 1.728f);
			_propBlock.SetVector (propSTWindGlobal, valueSTWindGlobal);
			// STWindBranch (time / 1.5, 0.4f, time * 1.5, 0f)
			valueSTWindBranch = new Vector4 (Time.time * 0.65f, 0.4102f, Time.time * 1.5f, 0f);
			_propBlock.SetVector (propSTWindBranch, valueSTWindBranch);
			// STWindBranchTwitch (0.6, 0.1, 0.8, 0.3)
			valueSTWindBranchTwitch = new Vector4 (0.603f, 0.147f, 0.75f, 0.3f);
			_propBlock.SetVector (propSTWindBranchTwitch, valueSTWindBranchTwitch);
			// STWindBranchWhip
			valueSTWindBranchWhip = new Vector4 (0.0f, 0.0f, 0.0f, 0.0f);
			_propBlock.SetVector (propSTWindBranchWhip, valueSTWindBranchWhip);
			// STWindBranchAnchor
			valueSTWindBranchAnchor = new Vector4 (0.034f, 0.4773f, 0.878f, 11.081f);
			_propBlock.SetVector (propSTWindBranchAnchor, valueSTWindBranchAnchor);
			// STWindBranchAdherences
			valueSTWindBranchAdherences = new Vector4 (0.09295f, 0.1f, 0f, 0f);
			_propBlock.SetVector (propSTWindBranchAdherences, valueSTWindBranchAdherences);
			// STWindTurbulences
			valueSTWindTurbulences = new Vector4 (0.7f, 0.3f, 0f, 0f);
			_propBlock.SetVector (propSTWindTurbulences, valueSTWindTurbulences);
			// STWindLeaf1Ripple (time * 3.2, 0, 0.5, 0)
			valueSTWindLeaf1Ripple = new Vector4 (Time.time * 3.18f, 0.044f, 0.5f, 0f);
			_propBlock.SetVector (propSTWindLeaf1Ripple, valueSTWindLeaf1Ripple);
			// STWindLeaf2Ripple (time * 4.7, 0, 0.5, 0)
			valueSTWindLeaf2Ripple = new Vector4 (Time.time * 4.7f, 0f, 0.5f, 0f); // USE time sin 4.6-4.8
			_propBlock.SetVector (propSTWindLeaf2Ripple, valueSTWindLeaf2Ripple);
			// STWindLeaf1Tumble (time, 0.1, 0.1, 0.1)
			valueSTWindLeaf1Tumble = new Vector4 (Time.time* 0.84f, 0.1298f, 0.11403f, 0.11f);
			_propBlock.SetVector (propSTWindLeaf1Tumble, valueSTWindLeaf1Tumble);
			// STWindLeaf2Tumble (time, 0.1, 0.1, 0.1)
			valueSTWindLeaf2Tumble = new Vector4 (Time.time, 0.035f, 0.035f, 0.5f);
			_propBlock.SetVector (propSTWindLeaf2Tumble, valueSTWindLeaf2Tumble);
			// STWindLeaf1Twitch (0.3, 0.3, time * 1.5, 0.0)
			valueSTWindLeaf1Twitch = new Vector4 (0.3315f, 0.3246f, Time.time * 1.56f, 0f);
			_propBlock.SetVector (propSTWindLeaf1Twitch, valueSTWindLeaf1Twitch);
			// STWindLeaf2Twitch (0, 33.3, time / 1.5, 0.0)
			valueSTWindLeaf2Twitch = new Vector4 (0.01745f, 33.3333f, Time.time * 0.31f, 12.896f);
			_propBlock.SetVector (propSTWindLeaf2Twitch, valueSTWindLeaf2Twitch);
			// STWindFrondRipple (time * -40, 1.2, 10.3, 0)
			valueSTWindFrondRipple = new Vector4 (Time.time * -40.5f, 1.2192f, 10.34f, 0.0f);
			_propBlock.SetVector (propSTWindFrondRipple, valueSTWindFrondRipple);

			_renderer.SetPropertyBlock (_propBlock);
		}
		void SetWindQuality (bool enable = true) {
			if (shaderType == ShaderType.SpeedTree8OrCompatible) {
				foreach (Material material in _renderer.sharedMaterials) {
					material.DisableKeyword ("_WINDQUALITY_NONE");
					material.DisableKeyword ("_WINDQUALITY_FASTEST");
					material.DisableKeyword ("_WINDQUALITY_FAST");
					material.DisableKeyword ("_WINDQUALITY_BETTER");
					material.DisableKeyword ("_WINDQUALITY_BEST");
					material.DisableKeyword ("_WINDQUALITY_PALM");
					if (enable) {
						switch (windQuality) {
							case WindQuality.None:
								material.EnableKeyword ("_WINDQUALITY_NONE");
								break;
							case WindQuality.Fastest:
								material.EnableKeyword ("_WINDQUALITY_FASTEST");
								break;
							case WindQuality.Fast:
								material.EnableKeyword ("_WINDQUALITY_FAST");
								break;
							case WindQuality.Better:
								material.EnableKeyword ("_WINDQUALITY_BETTER");
								break;
							case WindQuality.Best:
								material.EnableKeyword ("_WINDQUALITY_BEST");
								break;
							case WindQuality.Palm:
								material.EnableKeyword ("_WINDQUALITY_PALM");
								break;
						}
					}
				}
			} else if (shaderType == ShaderType.SpeedTree7OrCompatible) {
				foreach (Material material in _renderer.sharedMaterials) {
					if (enable) {
						material.EnableKeyword ("ENABLE_WIND");
					} else {
						material.DisableKeyword ("ENABLE_WIND");
					}
				}
			}
			_renderer.GetPropertyBlock (_propBlock);
			_propBlock.SetFloat (propWindEnabled, (enable?1f:0f));
			_propBlock.SetFloat (propWindQuality, (float)windQuality);
			_renderer.SetPropertyBlock (_propBlock);
		}
		public void UpdateSpeedTreeWind () {
			if (_propBlock == null) return;
			#if UNITY_EDITOR
			valueTime = (EditorApplication.isPlaying)?Time.time:(float)EditorApplication.timeSinceStartup;
			#else
			valueTime = Time.time;
			#endif
			// STWindGlobal
			valueSTWindGlobal.x = valueTime * 0.36f;
			valueSTWindGlobal.y = baseWindAmplitude * localWindAmplitude * globalWindAmplitude * windMain;
			_propBlock.SetVector (propSTWindGlobal, valueSTWindGlobal);
			_renderer.SetPropertyBlock (_propBlock);
			// STWindBranch
			valueSTWindBranch = new Vector4 (valueTime * 0.65f, 0.4102f, valueTime * 1.5f, 0f);
			_propBlock.SetVector (propSTWindBranch, valueSTWindBranch);
			// STWindLeaf1Ripple (time * 3.2, 0, 0.5, 0)
			valueSTWindLeaf1Ripple = new Vector4 (valueTime * 3.18f, 0.044f, 0.5f, 0f);
			_propBlock.SetVector (propSTWindLeaf1Ripple, valueSTWindLeaf1Ripple);
			// STWindLeaf2Ripple (time * 4.7, 0, 0.5, 0)
			valueSTWindLeaf2Ripple = new Vector4 (valueTime * 4.7f, 0f, 0.5f, 0f);
			_propBlock.SetVector (propSTWindLeaf2Ripple, valueSTWindLeaf2Ripple);
			// STWindLeaf1Tumble (time, 0.1, 0.1, 0.1)
			valueSTWindLeaf1Tumble = new Vector4 (valueTime * 0.84f, 0.1298f, 0.11403f, 0.11f);
			_propBlock.SetVector (propSTWindLeaf1Tumble, valueSTWindLeaf1Tumble);
			// STWindLeaf2Tumble (time, 0.1, 0.1, 0.1)
			valueSTWindLeaf2Tumble = new Vector4 (valueTime, 0.035f, 0.035f, 0.5f);
			_propBlock.SetVector (propSTWindLeaf2Tumble, valueSTWindLeaf2Tumble);
			// STWindLeaf1Twitch (0.3, 0.3, time * 1.5, 0.0)
			valueSTWindLeaf1Twitch = new Vector4 (0.3315f, 0.3246f, valueTime * 1.56f, 0f);
			_propBlock.SetVector (propSTWindLeaf1Twitch, valueSTWindLeaf1Twitch);
			// STWindLeaf2Twitch (0, 33.3, time / 1.5, 0.0)
			valueSTWindLeaf2Twitch = new Vector4 (0.01745f, 33.3333f, valueTime * 0.31f, 12.896f);
			_propBlock.SetVector (propSTWindLeaf2Twitch, valueSTWindLeaf2Twitch);
			// STWindFrondRipple (time * -40, 1.2, 10.3, 0)
			valueSTWindFrondRipple = new Vector4 (valueTime * -40.5f, 1.2192f, 10.34f, 0.0f);
			_propBlock.SetVector (propSTWindFrondRipple, valueSTWindFrondRipple);
		}
		public void GetWindZoneValues () {
			valueWindDirection = new Vector4 (1f, 0f, 0f, 0f);
			WindZone[] windZones = FindObjectsOfType<WindZone> ();
			for (int i = 0; i < windZones.Length; i++) {
				if (windZones [i].gameObject.activeSelf && windZones[i].mode == WindZoneMode.Directional) {
					windMain = windZones [i].windMain;
					valueWindDirection = new Vector4 (windZones [i].transform.forward.x, windZones [i].transform.forward.y, windZones [i].transform.forward.z, 1f);
				}
			}
		}
		/// <summary>
		/// Enables or disables wind animation in the editor.
		/// </summary>
		/// <param name="enabled">If set to <c>true</c> wind animation is enabled.</param>
		public void EditorWindAnimate (bool enabled) {
			#if UNITY_EDITOR
			editorWindEnabled = enabled | editorWindAlways;
			if (_propBlock == null) { Start (); }
			if (hasSpeedTreeWind) {
				SetWindQuality (editorWindEnabled);
				SetupSpeedTreeWind (editorWindEnabled);
			} else {
				SetupTreeCreatorWind (editorWindEnabled);
			}
			#endif
		}
		#endregion

		/*
		#ifdef ENABLE_WIND

		#define WIND_QUALITY_NONE       0
		#define WIND_QUALITY_FASTEST    1
		#define WIND_QUALITY_FAST       2
		#define WIND_QUALITY_BETTER     3
		#define WIND_QUALITY_BEST       4
		#define WIND_QUALITY_PALM       5

		uniform half _WindQuality;
		uniform half _WindEnabled;

		#include "SpeedTreeWind.cginc"

		#endif
		*/

		/*
		https://docs.unity3d.com/Manual/SL-UnityShaderVariables.html
		https://forum.unity.com/threads/description-of-packed-speedtree-wind-shader-inputs.575182/
		CBUFFER_START(SpeedTreeWind)
		float4 _ST_WindVector;
		float4 _ST_WindGlobal;  // (_Time.y, ?, ?, ?)
		float4 _ST_WindBranch; // (_Time.y, ?, ?, ?)
		float4 _ST_WindBranchTwitch;
		float4 _ST_WindBranchWhip;
		float4 _ST_WindBranchAnchor;
		float4 _ST_WindBranchAdherences;
		float4 _ST_WindTurbulences;
		float4 _ST_WindLeaf1Ripple; // (_Time.y, ?, ?, ?)
		float4 _ST_WindLeaf1Tumble; // (_Time.z, ?, ?, ?)
		float4 _ST_WindLeaf1Twitch; // (?, ?, _Time.y, ?)
		float4 _ST_WindLeaf2Ripple; // (_Time.y, ?, ?, ?)
		float4 _ST_WindLeaf2Tumble; // (_Time.z, ?, ?, ?)
		float4 _ST_WindLeaf2Twitch; // (?, ?, _Time.y, ?)
		float4 _ST_WindFrondRipple; // (_Time.y, ?, ?, ?)
		float4 _ST_WindAnimation;
		CBUFFER_END
		*/
	}
}