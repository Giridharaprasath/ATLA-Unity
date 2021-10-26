using System;
using UnityEngine;

namespace StylizedWater2
{
    /// <summary>
    /// Helper class to retrieve and store a water material's wave settings
    /// </summary>
    [Serializable]
    public class WaveParameters
    {
        private const string WavesKeyword = "_WAVES";
        
        private static readonly int WaveDistID = Shader.PropertyToID("_WaveDistance");
        private static readonly int WaveSpeedID = Shader.PropertyToID("_WaveSpeed");
        private static readonly int WaveHeightID = Shader.PropertyToID("_WaveHeight");
        private static readonly int WaveSteepnessID = Shader.PropertyToID("_WaveSteepness");
        private static readonly int WaveCountID = Shader.PropertyToID("_WaveCount");
        private static readonly int WaveDirectionID = Shader.PropertyToID("_WaveDirection");
        private static readonly int AnimationParamID = Shader.PropertyToID("_AnimationParams");
        
        /// <summary>
        /// XY: Direction
        /// Z: Speed multiplier
        /// </summary>
        public Vector4 animationParams;

        public int count;
        public float distance;
        public float speed;
        public float height;
        public float steepness;
        public Vector4 direction;
        
        public static bool WavesEnabled(Material waterMat)
        {
            if (!waterMat) return false;
            
            return waterMat.IsKeywordEnabled(WavesKeyword);
        }

        public static float GetMaxWaveHeight(Material mat)
        {
            return mat.GetFloat(WaveHeightID);
        } 

        public void Update(Material mat)
        {
            distance = mat.GetFloat(WaveDistID);
            height = mat.GetFloat(WaveHeightID);
            speed = mat.GetFloat(WaveSpeedID);
            steepness = mat.GetFloat(WaveSteepnessID) + 0.1f;
            count = mat.GetInt(WaveCountID);
            direction = mat.GetVector(WaveDirectionID);
            animationParams = mat.GetVector(AnimationParamID);
            
            SetAsGlobal();
        }

        public void SetAsGlobal()
        {
            Shader.SetGlobalFloat(WaveDistID, distance);
            Shader.SetGlobalFloat(WaveHeightID, height);
            Shader.SetGlobalFloat(WaveSpeedID, speed);
            Shader.SetGlobalFloat(WaveSteepnessID, steepness + 0.1f);
            Shader.SetGlobalVector(WaveDirectionID, direction);
            Shader.SetGlobalVector(AnimationParamID, animationParams);
        }

        /* No real use case right now
        public void Set(Material mat)
        {
            mat.SetFloat(WaveDistID, distance);
            mat.SetFloat(WaveHeightID, height);
            mat.SetFloat(WaveSpeedID, speed);
            mat.SetFloat(WaveSteepnessID, speed  + 0.1f);
            mat.SetInt(WaveCountID, count);
            mat.SetVector(WaveDirectionID, direction);
            mat.SetVector(AnimationParamID, animationParams);
        }
        */
    }
}