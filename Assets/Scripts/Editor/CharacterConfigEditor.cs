using UnityEditor;
using Shooting.Asset;
using UnityEngine;

[CustomEditor(typeof(CharacterConfig))]
public class CharacterConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CharacterConfig mTarget = (CharacterConfig)target;

        if(GUILayout.Button("Set GameObject Name"))
        {
            mTarget.AutoSet();
        }
    }
}
