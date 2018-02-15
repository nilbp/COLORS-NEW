using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MinionSpawn))]
public class EditorText : Editor {

  
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        string hello = "hello";

    }
}
