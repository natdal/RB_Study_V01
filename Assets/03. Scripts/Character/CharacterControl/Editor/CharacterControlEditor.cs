using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ver_01
{
    [CustomEditor(typeof(CharacterControl))]
    public class CharacterControlEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            CharacterControl control = (CharacterControl)target;

            if (GUILayout.Button("Setup RegdollParts (BodyParts)"))
            {
                control.SetRagdollParts();
            }
        }
    }
}

