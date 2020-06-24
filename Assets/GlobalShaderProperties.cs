using UnityEngine;

namespace Crumbling
{
    [CreateAssetMenu(fileName = "GlobalShaderProperties", menuName = "Crumbling/Shader properties")]
    public class GlobalShaderProperties : ScriptableObject
    {
        [SerializeField] private float _holeSize;
        [SerializeField] private bool _enableDither;
        [SerializeField] private Texture2D _ditherTexture;
        [SerializeField] private Vector2 _ditherScale;
        [SerializeField] private float _holeOpacity;
        [SerializeField] private float _holeEdgeWidth;
        
        private static readonly int HoleSizeID = Shader.PropertyToID("_HoleSize");
        private static readonly int HoleEdgeWidthID = Shader.PropertyToID("_HoleBlend");
        private static readonly int HoleOpacityID = Shader.PropertyToID("_MinimumVisibility");
        private static readonly int DitheringID = Shader.PropertyToID("_Dithering");
        private static readonly int DitherScaleID = Shader.PropertyToID("_DitherScale");
        private const string DitherKeyword = "DITHER_ON";

        public float HoleSize => _holeSize;

        private void OnValidate()
        {
            Apply();
        }

        public void Apply()
        {
            Shader.SetGlobalFloat(HoleSizeID, HoleSize);
            Shader.SetGlobalFloat(HoleEdgeWidthID, _holeEdgeWidth);
            Shader.SetGlobalFloat(HoleOpacityID, _holeOpacity);
            if (_enableDither) Shader.EnableKeyword(DitherKeyword);
            if (!_enableDither) Shader.DisableKeyword(DitherKeyword);
            Shader.SetGlobalTexture(DitheringID, _ditherTexture);
            Shader.SetGlobalVector(DitherScaleID, _ditherScale);
        }
    }
}