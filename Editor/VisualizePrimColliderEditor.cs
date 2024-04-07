using UnityEngine;
using UnityEditor;
using UdonSharpEditor;

namespace JetDog.UserCollider
{
    [CustomEditor(typeof(VisualizePrimCollider)), CanEditMultipleObjects]
    public class VisualizePrimColliderEditor : Editor
    {
        #region Fields
        [SerializeField]
        private Mesh capsulePrimRef;
        [SerializeField]
        private Mesh boxPrimRef;
        [SerializeField]
        private Mesh spherePrimRef;

        SerializedProperty capsulePrim,
            boxPrim,
            spherePrim;
        #endregion Fields

        #region Methods
        private void OnEnable()
        {
            capsulePrim = serializedObject.FindProperty("capsulePrim");
            boxPrim = serializedObject.FindProperty("boxPrim");
            spherePrim = serializedObject.FindProperty("spherePrim");

            RefNullCheck();
            serializedObject.ApplyModifiedProperties();
        }
        public override void OnInspectorGUI()
        {
            if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(capsulePrim);
            EditorGUILayout.PropertyField(boxPrim);
            EditorGUILayout.PropertyField(spherePrim);

            if (EditorGUI.EndChangeCheck())
            {
                RefNullCheck();
            }
            
            serializedObject.ApplyModifiedProperties();
        }
        
        private void RefNullCheck()
        {
            if (capsulePrim.objectReferenceValue == null) capsulePrim.objectReferenceValue = capsulePrimRef;
            if (boxPrim.objectReferenceValue == null) boxPrim.objectReferenceValue = boxPrimRef;
            if (spherePrim.objectReferenceValue == null) spherePrim.objectReferenceValue = spherePrimRef;
        }
        #endregion Methods
    }
}