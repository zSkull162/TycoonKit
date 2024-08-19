#if UNITY_EDITOR && !COMPILER_UDONSHARP
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using zSkull162.TycoonKit;

[CustomEditor(typeof(UnlockButton)), CanEditMultipleObjects]
public class UnlockButtonEditor : Editor
{
    string originalName;
    #region Get Serialized Properties
    SerializedProperty moneyManager;
    SerializedProperty containerObject;
    SerializedProperty titleText;
    SerializedProperty costText;
    SerializedProperty unlockName;
    SerializedProperty cost;
    SerializedProperty unlocks;
    SerializedProperty previousObject;
    SerializedProperty audioSource;
    SerializedProperty buySound;
    SerializedProperty errorSound;
    SerializedProperty isBuySoundGlobal;
    SerializedProperty isErrorSoundGlobal;
    SerializedProperty useBuySound;
    SerializedProperty useErrorSound;
    SerializedProperty isUpgrade;

    private void OnEnable()
    {
        moneyManager = serializedObject.FindProperty("moneyManager");
        audioSource = serializedObject.FindProperty("audioSource");
        containerObject = serializedObject.FindProperty("containerObject");
        titleText = serializedObject.FindProperty("titleText");
        costText = serializedObject.FindProperty("costText");
        unlockName = serializedObject.FindProperty("unlockName");
        cost = serializedObject.FindProperty("cost");
        unlocks = serializedObject.FindProperty("unlocks");
        previousObject = serializedObject.FindProperty("previousObject");
        buySound = serializedObject.FindProperty("buySound");
        errorSound = serializedObject.FindProperty("errorSound");
        isBuySoundGlobal = serializedObject.FindProperty("isBuySoundGlobal");
        isErrorSoundGlobal = serializedObject.FindProperty("isErrorSoundGlobal");
        useBuySound = serializedObject.FindProperty("useBuySound");
        useErrorSound = serializedObject.FindProperty("useErrorSound");
        isUpgrade = serializedObject.FindProperty("isUpgrade");
        originalName = target.name;
    }
    #endregion

    public override void OnInspectorGUI()
    {
        UnlockButton _script = (UnlockButton)target;
        if (_script == null) return;

        GUIStyle helpBox = new GUIStyle(EditorStyles.helpBox);
        GUIStyle buttonLabel = new GUIStyle(GUI.skin.button);
        GUIStyle description = new GUIStyle(GUI.skin.label);
        description.richText = true;
        description.wordWrap = true;
        GUIStyle textField = new GUIStyle(EditorStyles.textField);
        textField.richText = true;

        GUIStyle foldoutStyle = EditorStyles.foldout;
        foldoutStyle.fontStyle = FontStyle.Bold;
        GUIStyle buttonStyle = EditorStyles.radioButton;
        buttonStyle.fontStyle = FontStyle.Bold;
        GUIStyle checkbox = EditorStyles.toggle;
        buttonStyle.fontStyle = FontStyle.Bold;

        buttonLabel.richText = true;
        buttonLabel.stretchWidth = true;

        serializedObject.Update();

        #region Editor Options
        InspectorUtils.TitleLabel(ThemeColor.Col1, "Editor", true);
        EditorGUILayout.Space(1);
        EditorGUILayout.BeginVertical(helpBox);
        _script.editorOptions = GUILayout.Toggle(_script.editorOptions, " Editor options", foldoutStyle);
        if (_script.editorOptions)
        {
            EditorGUILayout.Space(2);
            EditorGUILayout.BeginVertical(helpBox);
            InspectorUtils.SectionLabel(ThemeColor.Col3, "Text");
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Set Text"))
            {
                _script.TitleText.text = $"{_script.UnlockName}:";
                _script.CostText.text = _script.Cost.ToString("$#,###.#");
            }

            if (GUILayout.Button("Reset Text"))
            {
                _script.TitleText.text = "Unlock Name:";
                _script.CostText.text = "$Cost";
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(2);

            #region Find Objects
            EditorGUILayout.BeginVertical(helpBox);
            InspectorUtils.SectionLabel(ThemeColor.Col4, "Objects");
            EditorGUILayout.LabelField($"<size=11>These buttons assume your money manager is named exactly \"MoneyManager\", your audio source is named exactly \"ButtonAudio\", (capitalization and no space), and that the Container Object is the <i>first</i> child of the object with this script.</size>", description);
            EditorGUILayout.Space(1);
            EditorGUILayout.BeginHorizontal();
            float maxWidth = EditorGUIUtility.currentViewWidth / 3.33f;
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
                EditorGUILayout.LabelField("<color=grey>Find Money Manager</color>", buttonLabel, GUILayout.MaxWidth(maxWidth));
            }

            if (_script.ContainerObject == null)
            {
                if (GUILayout.Button("Find Container Object"))
                {
                    Transform tsfm = _script.transform.GetChild(0);
                    if (tsfm == null) { Debug.Log($"<color=red>No child object found</color>"); return; }
                    else Debug.Log($"<color=lime><b>Returning {tsfm}</b></color>");

                    containerObject.objectReferenceValue = tsfm.gameObject;
                }
            }
            else
            {
                EditorGUILayout.LabelField("<color=grey>Find Container Object</color>", buttonLabel, GUILayout.MaxWidth(maxWidth));
            }

            if (_script.AudioSource == null)
            {
                if (GUILayout.Button("Find Audio Source"))
                {
                    GameObject obj = InspectorUtils.FindObjectByName("ButtonAudio");
                    if (obj == null) { Debug.Log($"<color=red>No object found</color>"); return; }

                    audioSource.objectReferenceValue = obj.GetComponent<AudioSource>();
                }
            }
            else
            {
                EditorGUILayout.LabelField("<color=grey>Find Audio Source</color>", buttonLabel, GUILayout.MaxWidth(maxWidth));
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(3);
            EditorGUILayout.LabelField($"<size=11>Toggles the container object. Purely for the slight convenience. <color=grey><i>This button also assumes the Container Object is the first child of the object with this script.</i></color></size>", description);
            EditorGUILayout.Space(1);
            EditorGUILayout.BeginHorizontal();

            Transform containerObj = _script.transform.GetChild(0);
            bool isActive = containerObj.gameObject.activeSelf;
            if (GUILayout.Button("Toggle container object", GUILayout.MaxWidth(maxWidth * 1.5f)))
            {
                containerObj.gameObject.SetActive(!isActive);
            }

            if (isActive) {
                EditorGUILayout.LabelField("Container object is: <color=lime><i>Enabled</i></color>", textField, GUILayout.MaxWidth(maxWidth * 1.5f));
            }
            else {
                EditorGUILayout.LabelField("Container object is: <color=red><i>Disabled</i></color>", textField, GUILayout.MaxWidth(maxWidth * 1.5f));
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            #endregion
            EditorGUILayout.Space(2);

            EditorGUILayout.BeginVertical(helpBox);
            InspectorUtils.SectionLabel(ThemeColor.Col5, "Button Name");
            EditorGUILayout.LabelField($"<size=11>This will rename the object with this script in the hierarchy to be like: \"(UnlockName) Unlock\"</size>", description);
            EditorGUILayout.Space(1);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Rename button"))
            {
                RenameButton($"{_script.UnlockName} Unlock");
            }
            if (GUILayout.Button("Reset button name"))
            {
                Undo.RecordObject(_script.gameObject, "Reset name");
                _script.gameObject.name = originalName;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(5);
        #endregion

        InspectorUtils.TitleLabel(ThemeColor.Col2, "Unlock Button", true);
        EditorGUILayout.Space(1);
        EditorGUILayout.BeginVertical(helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col2, "System");
        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.PropertyField(moneyManager);
        EditorGUILayout.PropertyField(containerObject);
        EditorGUILayout.PropertyField(titleText);
        EditorGUILayout.PropertyField(costText);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(4);

        EditorGUILayout.BeginVertical(helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col3, "Main");
        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.PropertyField(unlockName);
        EditorGUILayout.PropertyField(cost);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(4);

        #region Audio
        EditorGUILayout.BeginVertical(helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col4, "Audio");
        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.PropertyField(audioSource);
        EditorGUILayout.Space(6);
        EditorGUILayout.BeginVertical(helpBox);
        _script.useBuySound = GUILayout.Toggle(_script.useBuySound, "Use Buy Sound", checkbox);
        if (_script.useBuySound)
        {
            useBuySound.boolValue = true;
            EditorGUILayout.PropertyField(buySound);
            EditorGUILayout.PropertyField(isBuySoundGlobal);
        }
        else useBuySound.boolValue = false;
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical(helpBox);
        _script.useErrorSound = GUILayout.Toggle(_script.useErrorSound, "Use Error Sound", checkbox);
        if (_script.useErrorSound)
        {
            useErrorSound.boolValue = true;
            EditorGUILayout.PropertyField(errorSound);
            EditorGUILayout.PropertyField(isErrorSoundGlobal);
        }
        else useErrorSound.boolValue = false;
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();
        #endregion

        EditorGUILayout.Space(4);

        EditorGUILayout.BeginVertical(helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col5, "Unlocks");
        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.PropertyField(unlocks);
        EditorGUILayout.Space(2);

        EditorGUILayout.BeginVertical(helpBox);
        _script.isUpgrade = GUILayout.Toggle(_script.isUpgrade, "  Is Upgrade", buttonStyle);
        if (_script.isUpgrade)
        {
            isUpgrade.boolValue = true;
            EditorGUILayout.PropertyField(previousObject);
        }
        else isUpgrade.boolValue = false;
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();

        void RenameButton(string name)
        {
            int count = 0;

            Transform containerObj = _script.transform.GetChild(0);
            GameObject[] objs = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            foreach (GameObject obj in objs)
            {
                if (obj.name.Contains(name) && obj != _script.gameObject)
                {
                    count += 1;
                } 
            }

            if (count <= 0) { _script.gameObject.name = $"{_script.UnlockName} Unlock"; }
            else { _script.gameObject.name = $"{_script.UnlockName} Unlock ({count})"; }

            containerObj.gameObject.name = _script.gameObject.name + " Container";
        }
    }
}
#endif