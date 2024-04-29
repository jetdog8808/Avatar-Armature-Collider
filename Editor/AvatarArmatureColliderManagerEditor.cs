using UnityEngine;
using UnityEditor;
using UdonSharpEditor;

namespace JetDog.UserCollider
{
    [CustomEditor(typeof(AvatarArmatureColliderManager))]
    public class AvatarArmatureColliderManagerEditor : Editor
    {
        #region Fields
        SerializedProperty prefabRefProperty;
        SerializedProperty remoteCollidersEnabledProperty,
            localCollidersEnabledProperty;
        SerializedProperty localCollisionTransferOwnershipProperty;
        SerializedProperty remoteIsTriggerProperty,
            localIstriggerProperty;
        SerializedProperty localLayerProperty,
            remoteLayerProperty;
        SerializedProperty remoteIncludeLayersProperty,
            localIncludeLayersProperty,
            remoteExcludeLayersProperty,
            localExcludeLayersProperty;
        SerializedProperty fingerCollisionProperty,
            handCollisionProperty,
            armCollisionProperty,
            legCollisionProperty,
            torsoCollisionProperty,
            headCollisionProperty;
        SerializedProperty distanceFactors,
            distanceUpdateRates;

        bool lodDropdown = false;
        bool remoteLayerOverrideDropdown = false,
            localLayerOverrideDropdown = false;
        bool enabledSectionsDropdown = false;
        #endregion Fields

        #region Methods
        private void OnEnable()
        {
            prefabRefProperty = serializedObject.FindProperty("prefabRef");
            localCollisionTransferOwnershipProperty = serializedObject.FindProperty("localCollisionTransferOwnership");
            remoteCollidersEnabledProperty = serializedObject.FindProperty("_remoteCollidersEnabled");
            localCollidersEnabledProperty = serializedObject.FindProperty("_localCollidersEnabled");

            remoteIsTriggerProperty = serializedObject.FindProperty("remoteIsTrigger");
            localIstriggerProperty = serializedObject.FindProperty("localIsTrigger");

            localLayerProperty = serializedObject.FindProperty("localLayer");
            remoteLayerProperty = serializedObject.FindProperty("remoteLayer");

            remoteIncludeLayersProperty = serializedObject.FindProperty("remoteIncludeLayers");
            localIncludeLayersProperty = serializedObject.FindProperty("localIncludeLayers");
            remoteExcludeLayersProperty = serializedObject.FindProperty("remoteExcludeLayers");
            localExcludeLayersProperty = serializedObject.FindProperty("localExcludeLayers");

            fingerCollisionProperty = serializedObject.FindProperty("fingerCollision");
            handCollisionProperty = serializedObject.FindProperty("handCollision");
            armCollisionProperty = serializedObject.FindProperty("armCollision");
            legCollisionProperty = serializedObject.FindProperty("legCollision");
            torsoCollisionProperty = serializedObject.FindProperty("torsoCollision");
            headCollisionProperty = serializedObject.FindProperty("headCollision");

            distanceFactors = serializedObject.FindProperty("distanceFactors");
            distanceUpdateRates = serializedObject.FindProperty("distanceUpdateRates");
        }
        public override void OnInspectorGUI()
        {
            if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;

            EditorGUILayout.PropertyField(prefabRefProperty);
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(localCollidersEnabledProperty);
            EditorGUILayout.PropertyField(localCollisionTransferOwnershipProperty);
            EditorGUILayout.PropertyField(localIstriggerProperty);
            localLayerProperty.intValue = EditorGUILayout.LayerField("Local Collider Layer", localLayerProperty.intValue);
            localLayerOverrideDropdown = EditorGUILayout.BeginFoldoutHeaderGroup(localLayerOverrideDropdown, "Layer Overrides");

            if (localLayerOverrideDropdown)
            {
                EditorGUILayout.PropertyField(localIncludeLayersProperty);
                EditorGUILayout.PropertyField(localExcludeLayersProperty);
            }

            EditorGUILayout.EndFoldoutHeaderGroup();

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(remoteCollidersEnabledProperty);
            EditorGUILayout.PropertyField(remoteIsTriggerProperty);
            remoteLayerProperty.intValue = EditorGUILayout.LayerField("Remote Collider Layer", remoteLayerProperty.intValue);
            remoteLayerOverrideDropdown = EditorGUILayout.BeginFoldoutHeaderGroup(remoteLayerOverrideDropdown, "Layer Overrides");

            if (remoteLayerOverrideDropdown)
            {
                EditorGUILayout.PropertyField(remoteIncludeLayersProperty);
                EditorGUILayout.PropertyField(remoteExcludeLayersProperty);
            }

            EditorGUILayout.EndFoldoutHeaderGroup();

            EditorGUILayout.Space();

            enabledSectionsDropdown = EditorGUILayout.BeginFoldoutHeaderGroup(enabledSectionsDropdown, "Enabled collider sections");

            if (enabledSectionsDropdown)
            {
                EditorGUILayout.PropertyField(fingerCollisionProperty);
                EditorGUILayout.PropertyField(handCollisionProperty);
                EditorGUILayout.PropertyField(armCollisionProperty);
                EditorGUILayout.PropertyField(legCollisionProperty);
                EditorGUILayout.PropertyField(torsoCollisionProperty);
                EditorGUILayout.PropertyField(headCollisionProperty);
            }

            EditorGUILayout.EndFoldoutHeaderGroup();

            EditorGUILayout.Space();

            lodDropdown = EditorGUILayout.BeginFoldoutHeaderGroup(lodDropdown, "LOD");

            if (lodDropdown)
            {
                EditorGUI.BeginChangeCheck();

                EditorGUILayout.LabelField("Lod 0", EditorStyles.boldLabel);
                float distance_x = EditorGUILayout.FloatField("Meters", distanceFactors.vector3Value.x);
                int rate_x = EditorGUILayout.IntSlider("Update frame", distanceUpdateRates.vector3IntValue.x, 1, 15);
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Lod 1", EditorStyles.boldLabel);
                float distance_y = EditorGUILayout.FloatField("Meters", distanceFactors.vector3Value.y);
                int rate_y = EditorGUILayout.IntSlider("Update frame", distanceUpdateRates.vector3IntValue.y, 1, 15);
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Lod 2", EditorStyles.boldLabel);
                float distance_z = EditorGUILayout.FloatField("Meters", distanceFactors.vector3Value.z);
                int rate_z = EditorGUILayout.IntSlider("Update frame", distanceUpdateRates.vector3IntValue.z, 1, 15);

                if (EditorGUI.EndChangeCheck())
                {
                    if ((distance_x + 1f) > distance_y) distance_y = distance_x + 1f;
                    if ((distance_y + 1f) > distance_z) distance_z = distance_y + 1f;

                    distanceFactors.vector3Value = new Vector3(distance_x, distance_y, distance_z);
                    distanceUpdateRates.vector3IntValue = new Vector3Int(rate_x, rate_y, rate_z);
                }
            }

            EditorGUILayout.EndFoldoutHeaderGroup();

            serializedObject.ApplyModifiedProperties();
        }
        #endregion Methods
    }
}