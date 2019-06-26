using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CookieCutterBase), editorForChildClasses: true)]
public class CutterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var cookieCutter = (CookieCutterBase)target;
        if (cookieCutter != null)
        {
            if (GUILayout.Button("Execute"))
            {
                cookieCutter.Execute();
            }
        }
    }
}
