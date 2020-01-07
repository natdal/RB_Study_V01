using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ver_01
{

    [CustomEditor(typeof(CharacterControl))]
    public class MaterialChanger : Editor // 인스펙터에서만 동작
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            CharacterControl control = (CharacterControl)target;

            if (GUILayout.Button("Change Material"))
            {
                control.ChangeMaterial();
            }
        }
    }
}

