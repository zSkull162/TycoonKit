#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;
using zSkull162.TycoonKit;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators;

[CustomEditor(typeof(Collector)), CanEditMultipleObjects]
public class CollectorEditor : Editor
{
    #region Get Serialized Properties
    SerializedProperty moneyManager;

    private void OnEnable()
    {
        moneyManager = serializedObject.FindProperty("moneyManager");
    }
    #endregion

    public override void OnInspectorGUI()
    {
        Collector _script = (Collector)target;
        if (_script == null) return;

        GUIStyle helpBox = new GUIStyle(EditorStyles.helpBox);
        GUIStyle buttonLabel = new GUIStyle(GUI.skin.button);
        buttonLabel.richText = true;

        serializedObject.Update();

        InspectorUtils.TitleLabel(ThemeColor.Col2, "Collector", true);
        EditorGUILayout.Space(1);

        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.Space(2);
        EditorGUILayout.PropertyField(moneyManager);
        EditorGUILayout.Space(2);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(2);
        EditorGUILayout.BeginVertical(helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col1, "Editor");
        EditorGUILayout.BeginVertical(helpBox);
        InspectorUtils.Description("This button assumes your money manager is named exactly \"MoneyManager\" (capitalization and no space)");
        EditorGUILayout.Space(1);
        if (_script.MoneyManager == null)
        {
            if (GUILayout.Button("Find Money Manager"))
            {
                GameObject obj = InspectorUtils.FindObjectByName("MoneyManager");
                if (obj == null) { Debug.Log($"<color=red>No object found</color>"); return; }

                moneyManager.objectReferenceValue = obj.GetComponent<MoneyManager>();
            }
        }
        else
        {
            EditorGUILayout.LabelField("<color=grey>Find Money Manager</color>", buttonLabel);
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif