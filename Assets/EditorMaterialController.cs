using UnityEditor;
using UnityEngine;

namespace Crumbling.Editor
{
    [InitializeOnLoad]
    public class EditorMaterialController
    {
        private static readonly int HmdPosID = Shader.PropertyToID("_HMDPos");
        private static readonly int CharPosID = Shader.PropertyToID("_CharPos");
        private static readonly int CamToCharID = Shader.PropertyToID("_CamToChar");
        private static readonly int CamToCharSquaredID = Shader.PropertyToID("_CamToCharSquared");
        private const string VRKeyword = "VR_ON";

        static EditorMaterialController()
        {
            EditorApplication.update += Update;
        }

        private static void Update()
        {
            if (Application.isPlaying) return;
            GameObject player = GameObject.FindWithTag("Player");
            Vector3 playerPosition = player == null ? Vector3.zero : player.transform.position;
            if (Camera.main != null)
            {
                Vector3 cameraPosition = Camera.main.transform.position;
                Vector3 camToPlayer = playerPosition - cameraPosition;
                Shader.SetGlobalVector(HmdPosID, cameraPosition);
                Shader.SetGlobalVector(CamToCharID, camToPlayer);
                Shader.SetGlobalFloat(CamToCharSquaredID, Vector3.SqrMagnitude(camToPlayer));
            }
            if (Camera.current != null)
            {
                Vector3 cameraPosition = Camera.current.transform.position;
                Vector3 camToPlayer = playerPosition - cameraPosition;
                Shader.SetGlobalVector(HmdPosID, cameraPosition);
                Shader.SetGlobalVector(CamToCharID, camToPlayer);
                Shader.SetGlobalFloat(CamToCharSquaredID, Vector3.SqrMagnitude(camToPlayer));
            }

            if (Shader.IsKeywordEnabled(VRKeyword)) Shader.DisableKeyword(VRKeyword);
            Shader.SetGlobalVector(CharPosID, playerPosition);
        }
    }
}