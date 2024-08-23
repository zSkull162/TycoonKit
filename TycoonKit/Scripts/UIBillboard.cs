
#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;
#endif
using UnityEngine;
using UdonSharp;
using VRC.SDKBase;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class UIBillboard : UdonSharpBehaviour
{
    private VRCPlayerApi localPlayer;

    private void Start()
    {
        localPlayer = Networking.LocalPlayer;
    }

    void LateUpdate()
    {
        Quaternion head = localPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation;
        this.transform.LookAt(this.transform.position + head * Vector3.forward, Vector3.up);
    }
}


#region Custom Inspector

#if UNITY_EDITOR && !COMPILER_UDONSHARP
[CustomEditor(typeof(UIBillboard)), CanEditMultipleObjects]
public class UIBillboardInspector : Editor
{
    public override void OnInspectorGUI()
    {
        GUIStyle style = new GUIStyle(EditorStyles.helpBox);
        style.richText = true;

        EditorGUILayout.Space(4);
        EditorGUILayout.LabelField("<size=14><color=white><b>Makes the object which this script is attatched to face the player's head.</b></color></size><size=13>\nMainly used for UI Canvases.</size>", style);
        EditorGUILayout.Space(4);
    }
}
#endif

#endregion