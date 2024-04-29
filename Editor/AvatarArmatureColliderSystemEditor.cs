using UnityEditor;
using UdonSharpEditor;

namespace JetDog.UserCollider
{
    [CustomEditor(typeof(AvatarArmatureColliderSystem))]
    public class AvatarArmatureColliderSystemEditor : Editor
    {
        #region Fields
        SerializedProperty getLocalUserProperty;
        SerializedProperty collisionTransferOwnershipProperty;
        SerializedProperty colliderLayerProperty;
        SerializedProperty colliderIsTriggerProperty;

        bool layerOverrideDropdown = false;
        SerializedProperty includeLayersProperty;
        SerializedProperty excludeLayersProperty;


        bool enabledSectionsDropdown = false;
        SerializedProperty fingerColliderEnable_R_Property,
            fingerColliderEnable_L_Property,
            handColliderEnable_R_Property,
            handColliderEnable_L_Property,
            armColliderEnable_R_Property,
            armColliderEnable_L_Property,
            legColliderEnable_R_Property,
            legColliderEnable_L_Property,
            torsoColliderEnableProperty,
            headColliderEnableProperty;

        bool referencesDropdown = false;
        SerializedProperty chest_BoneProperty,
            spine_BoneProperty,
            head_BoneProperty,
            upperLeg_L_BoneProperty, upperLeg_R_BoneProperty,
            lowerLeg_L_BoneProperty, lowerLeg_R_BoneProperty,
            foot_L_BoneProperty, foot_R_BoneProperty,
            upperArm_L_BoneProperty, upperArm_R_BoneProperty,
            lowerArm_L_BoneProperty, lowerArm_R_BoneProperty,
            hand_L_BoneProperty, hand_R_BoneProperty,
            index_L_BoneProperty, index_R_BoneProperty,
            middle_L_BoneProperty, middle_R_BoneProperty,
            ring_L_BoneProperty, ring_R_BoneProperty,
            little_L_BoneProperty, little_R_BoneProperty;

        SerializedProperty chest_ColliderProperty,
            spine_ColliderProperty,
            head_ColliderProperty,
            upperLeg_L_ColliderProperty, upperLeg_R_ColliderProperty,
            lowerLeg_L_ColliderProperty, lowerLeg_R_ColliderProperty,
            foot_L_ColliderProperty, foot_R_ColliderProperty,
            upperArm_L_ColliderProperty, upperArm_R_ColliderProperty,
            lowerArm_L_ColliderProperty, lowerArm_R_ColliderProperty,
            hand_L_ColliderProperty, hand_R_ColliderProperty,
            index_L_ColliderProperty, index_R_ColliderProperty,
            middle_L_ColliderProperty, middle_R_ColliderProperty,
            ring_L_ColliderProperty, ring_R_ColliderProperty,
            little_L_ColliderProperty, little_R_ColliderProperty;

        bool radiusSettingsDropdown = false;
        SerializedProperty upperLeg_radiusProperty,
            lowerLeg_radiusProperty,
            foot_radiusProperty,
            upperArm_radiuProperty,
            lowerArm_radiusProperty,
            SpineFactorProperty,
            head_radiusProperty,
            finger_radiusProperty;
        #endregion Fields

        #region Methods
        private void OnEnable()
        {
            getLocalUserProperty = serializedObject.FindProperty("getLocalUser");
            collisionTransferOwnershipProperty = serializedObject.FindProperty("_collisionTransferOwnership");
            colliderLayerProperty = serializedObject.FindProperty("_colliderLayer");
            colliderIsTriggerProperty = serializedObject.FindProperty("_colliderIsTrigger");
            includeLayersProperty = serializedObject.FindProperty("_includeLayers");
            excludeLayersProperty = serializedObject.FindProperty("_excludeLayers");

            fingerColliderEnable_R_Property = serializedObject.FindProperty("_fingerColliderEnable_R");
            fingerColliderEnable_L_Property = serializedObject.FindProperty("_fingerColliderEnable_L");
            handColliderEnable_R_Property = serializedObject.FindProperty("_handColliderEnable_R");
            handColliderEnable_L_Property = serializedObject.FindProperty("_handColliderEnable_L");
            armColliderEnable_R_Property = serializedObject.FindProperty("_armColliderEnable_R");
            armColliderEnable_L_Property = serializedObject.FindProperty("_armColliderEnable_L");
            legColliderEnable_R_Property = serializedObject.FindProperty("_legColliderEnable_R");
            legColliderEnable_L_Property = serializedObject.FindProperty("_legColliderEnable_L");
            torsoColliderEnableProperty = serializedObject.FindProperty("_torsoColliderEnable");
            headColliderEnableProperty = serializedObject.FindProperty("_headColliderEnable");

            chest_BoneProperty = serializedObject.FindProperty("chest_Bone");
            spine_BoneProperty = serializedObject.FindProperty("spine_Bone");
            head_BoneProperty = serializedObject.FindProperty("head_Bone");
            upperLeg_L_BoneProperty = serializedObject.FindProperty("upperLeg_L_Bone");
            upperLeg_R_BoneProperty = serializedObject.FindProperty("upperLeg_R_Bone");
            lowerLeg_L_BoneProperty = serializedObject.FindProperty("lowerLeg_L_Bone");
            lowerLeg_R_BoneProperty = serializedObject.FindProperty("lowerLeg_R_Bone");
            foot_L_BoneProperty = serializedObject.FindProperty("foot_L_Bone");
            foot_R_BoneProperty = serializedObject.FindProperty("foot_R_Bone");
            upperArm_L_BoneProperty = serializedObject.FindProperty("upperArm_L_Bone");
            upperArm_R_BoneProperty = serializedObject.FindProperty("upperArm_R_Bone");
            lowerArm_L_BoneProperty = serializedObject.FindProperty("lowerArm_L_Bone");
            lowerArm_R_BoneProperty = serializedObject.FindProperty("lowerArm_R_Bone");
            hand_L_BoneProperty = serializedObject.FindProperty("hand_L_Bone");
            hand_R_BoneProperty = serializedObject.FindProperty("hand_R_Bone");
            index_L_BoneProperty = serializedObject.FindProperty("index_L_Bone");
            index_R_BoneProperty = serializedObject.FindProperty("index_R_Bone");
            middle_L_BoneProperty = serializedObject.FindProperty("middle_L_Bone");
            middle_R_BoneProperty = serializedObject.FindProperty("middle_R_Bone");
            ring_L_BoneProperty = serializedObject.FindProperty("ring_L_Bone");
            ring_R_BoneProperty = serializedObject.FindProperty("ring_R_Bone");
            little_L_BoneProperty = serializedObject.FindProperty("little_L_Bone");
            little_R_BoneProperty = serializedObject.FindProperty("little_R_Bone");

            chest_ColliderProperty = serializedObject.FindProperty("chest_Collider");
            spine_ColliderProperty = serializedObject.FindProperty("spine_Collider");
            head_ColliderProperty = serializedObject.FindProperty("head_Collider");
            upperLeg_L_ColliderProperty = serializedObject.FindProperty("upperLeg_L_Collider");
            upperLeg_R_ColliderProperty = serializedObject.FindProperty("upperLeg_R_Collider");
            lowerLeg_L_ColliderProperty = serializedObject.FindProperty("lowerLeg_L_Collider");
            lowerLeg_R_ColliderProperty = serializedObject.FindProperty("lowerLeg_R_Collider");
            foot_L_ColliderProperty = serializedObject.FindProperty("foot_L_Collider");
            foot_R_ColliderProperty = serializedObject.FindProperty("foot_R_Collider");
            upperArm_L_ColliderProperty = serializedObject.FindProperty("upperArm_L_Collider");
            upperArm_R_ColliderProperty = serializedObject.FindProperty("upperArm_R_Collider");
            lowerArm_L_ColliderProperty = serializedObject.FindProperty("lowerArm_L_Collider");
            lowerArm_R_ColliderProperty = serializedObject.FindProperty("lowerArm_R_Collider");
            hand_L_ColliderProperty = serializedObject.FindProperty("hand_L_Collider");
            hand_R_ColliderProperty = serializedObject.FindProperty("hand_R_Collider");
            index_L_ColliderProperty = serializedObject.FindProperty("index_L_Collider");
            index_R_ColliderProperty = serializedObject.FindProperty("index_R_Collider");
            middle_L_ColliderProperty = serializedObject.FindProperty("middle_L_Collider");
            middle_R_ColliderProperty = serializedObject.FindProperty("middle_R_Collider");
            ring_L_ColliderProperty = serializedObject.FindProperty("ring_L_Collider");
            ring_R_ColliderProperty = serializedObject.FindProperty("ring_R_Collider");
            little_L_ColliderProperty = serializedObject.FindProperty("little_L_Collider");
            little_R_ColliderProperty = serializedObject.FindProperty("little_R_Collider");

            upperLeg_radiusProperty = serializedObject.FindProperty("upperLeg_radius");
            lowerLeg_radiusProperty = serializedObject.FindProperty("lowerLeg_radius");
            foot_radiusProperty = serializedObject.FindProperty("foot_radius");
            upperArm_radiuProperty = serializedObject.FindProperty("upperArm_radius");
            lowerArm_radiusProperty = serializedObject.FindProperty("lowerArm_radius");
            SpineFactorProperty = serializedObject.FindProperty("SpineFactor");
            head_radiusProperty = serializedObject.FindProperty("head_radius");
            finger_radiusProperty = serializedObject.FindProperty("finger_radius");

        }
        public override void OnInspectorGUI()
        {
            if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;

            EditorGUILayout.PropertyField(getLocalUserProperty);
            EditorGUILayout.PropertyField(collisionTransferOwnershipProperty);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(colliderIsTriggerProperty);
            colliderLayerProperty.intValue = EditorGUILayout.LayerField("Layer", colliderLayerProperty.intValue);

            layerOverrideDropdown = EditorGUILayout.BeginFoldoutHeaderGroup(layerOverrideDropdown, "Layer Overrides");

            if (layerOverrideDropdown)
            {
                EditorGUILayout.PropertyField(includeLayersProperty);
                EditorGUILayout.PropertyField(excludeLayersProperty);
            }

            EditorGUILayout.EndFoldoutHeaderGroup();

            EditorGUILayout.Space();

            enabledSectionsDropdown = EditorGUILayout.BeginFoldoutHeaderGroup(enabledSectionsDropdown, "Enabled collider sections");

            if (enabledSectionsDropdown)
            {
                fingerColliderEnable_R_Property.boolValue = EditorGUILayout.Toggle("Finger Collider Enabled", fingerColliderEnable_R_Property.boolValue);
                fingerColliderEnable_L_Property.boolValue = fingerColliderEnable_R_Property.boolValue;

                handColliderEnable_R_Property.boolValue = EditorGUILayout.Toggle("Hand Collider Enabled", handColliderEnable_R_Property.boolValue);
                handColliderEnable_L_Property.boolValue = handColliderEnable_R_Property.boolValue;

                armColliderEnable_R_Property.boolValue = EditorGUILayout.Toggle("Arm Collider Enabled", armColliderEnable_R_Property.boolValue);
                armColliderEnable_L_Property.boolValue = armColliderEnable_R_Property.boolValue;

                legColliderEnable_R_Property.boolValue = EditorGUILayout.Toggle("Leg Collider Enabled", legColliderEnable_R_Property.boolValue);
                legColliderEnable_L_Property.boolValue = legColliderEnable_R_Property.boolValue;

                EditorGUILayout.PropertyField(torsoColliderEnableProperty);
                EditorGUILayout.PropertyField(headColliderEnableProperty);
            }

            EditorGUILayout.EndFoldoutHeaderGroup();

            referencesDropdown = EditorGUILayout.BeginFoldoutHeaderGroup(referencesDropdown, "Bone and Collider references");

            if (referencesDropdown)
            {
                EditorGUILayout.PropertyField(chest_BoneProperty);
                EditorGUILayout.PropertyField(spine_BoneProperty);
                EditorGUILayout.PropertyField(head_BoneProperty);
                EditorGUILayout.PropertyField(upperLeg_L_BoneProperty);
                EditorGUILayout.PropertyField(upperLeg_R_BoneProperty);
                EditorGUILayout.PropertyField(lowerLeg_L_BoneProperty);
                EditorGUILayout.PropertyField(lowerLeg_R_BoneProperty);
                EditorGUILayout.PropertyField(foot_L_BoneProperty);
                EditorGUILayout.PropertyField(foot_R_BoneProperty);
                EditorGUILayout.PropertyField(upperArm_L_BoneProperty);
                EditorGUILayout.PropertyField(upperArm_R_BoneProperty);
                EditorGUILayout.PropertyField(lowerArm_L_BoneProperty);
                EditorGUILayout.PropertyField(lowerArm_R_BoneProperty);
                EditorGUILayout.PropertyField(hand_L_BoneProperty);
                EditorGUILayout.PropertyField(hand_R_BoneProperty);
                EditorGUILayout.PropertyField(index_L_BoneProperty);
                EditorGUILayout.PropertyField(index_R_BoneProperty);
                EditorGUILayout.PropertyField(middle_L_BoneProperty);
                EditorGUILayout.PropertyField(middle_R_BoneProperty);
                EditorGUILayout.PropertyField(ring_L_BoneProperty);
                EditorGUILayout.PropertyField(ring_R_BoneProperty);
                EditorGUILayout.PropertyField(little_L_BoneProperty);
                EditorGUILayout.PropertyField(little_R_BoneProperty);

                EditorGUILayout.PropertyField(chest_ColliderProperty);
                EditorGUILayout.PropertyField(spine_ColliderProperty);
                EditorGUILayout.PropertyField(head_ColliderProperty);
                EditorGUILayout.PropertyField(upperLeg_L_ColliderProperty);
                EditorGUILayout.PropertyField(upperLeg_R_ColliderProperty);
                EditorGUILayout.PropertyField(lowerLeg_L_ColliderProperty);
                EditorGUILayout.PropertyField(lowerLeg_R_ColliderProperty);
                EditorGUILayout.PropertyField(foot_L_ColliderProperty);
                EditorGUILayout.PropertyField(foot_R_ColliderProperty);
                EditorGUILayout.PropertyField(upperArm_L_ColliderProperty);
                EditorGUILayout.PropertyField(upperArm_R_ColliderProperty);
                EditorGUILayout.PropertyField(lowerArm_L_ColliderProperty);
                EditorGUILayout.PropertyField(lowerArm_R_ColliderProperty);
                EditorGUILayout.PropertyField(hand_L_ColliderProperty);
                EditorGUILayout.PropertyField(hand_R_ColliderProperty);
                EditorGUILayout.PropertyField(index_L_ColliderProperty);
                EditorGUILayout.PropertyField(index_R_ColliderProperty);
                EditorGUILayout.PropertyField(middle_L_ColliderProperty);
                EditorGUILayout.PropertyField(middle_R_ColliderProperty);
                EditorGUILayout.PropertyField(ring_L_ColliderProperty);
                EditorGUILayout.PropertyField(ring_R_ColliderProperty);
                EditorGUILayout.PropertyField(little_L_ColliderProperty);
                EditorGUILayout.PropertyField(little_R_ColliderProperty);
            }

            EditorGUILayout.EndFoldoutHeaderGroup();

            radiusSettingsDropdown = EditorGUILayout.BeginFoldoutHeaderGroup(radiusSettingsDropdown, "Collider size tweaks");

            if (radiusSettingsDropdown)
            {
                EditorGUILayout.PropertyField(upperLeg_radiusProperty);
                EditorGUILayout.PropertyField(lowerLeg_radiusProperty);
                EditorGUILayout.PropertyField(foot_radiusProperty);
                EditorGUILayout.PropertyField(upperArm_radiuProperty);
                EditorGUILayout.PropertyField(lowerArm_radiusProperty);
                EditorGUILayout.PropertyField(SpineFactorProperty);
                EditorGUILayout.PropertyField(head_radiusProperty);
                EditorGUILayout.PropertyField(finger_radiusProperty);
            }

            EditorGUILayout.EndFoldoutHeaderGroup();

            serializedObject.ApplyModifiedProperties();
        }
        #endregion Methods
    }
}