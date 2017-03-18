using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AutomaticVerticalSize))]
public class AutomaticVerticalSizeEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if( GUILayout.Button("Resize"))
        {
            AutomaticVerticalSize script = (AutomaticVerticalSize)target;
            script.AdjustSize();
        }
    }
}
