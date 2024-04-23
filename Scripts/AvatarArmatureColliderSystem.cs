using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using JetBrains.Annotations;

namespace JetDog.UserCollider
{
    [DefaultExecutionOrder(-2000000000), UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class AvatarArmatureColliderSystem : UdonSharpBehaviour
    {
        #region Properties
        [PublicAPI]
        //collider target public api.
        public VRCPlayerApi UserApi { get => _userApi; }
        [PublicAPI]
        public bool FingerColliderEnable { get => _fingerColliderEnable; }
        [PublicAPI]
        public bool HandColliderEnable { get => _handColliderEnable; }
        [PublicAPI]
        public bool ArmColliderEnable { get => _armColliderEnable; }
        [PublicAPI]
        public bool LegColliderEnable { get => _legColliderEnable; }
        [PublicAPI]
        public bool TorsoColliderEnable { get => _torsoColliderEnable; }
        [PublicAPI]
        public bool HeadColliderEnable { get => _headColliderEnable; }
        [PublicAPI]
        public bool AvatarCalibrated { get => _avatarCalibrated; }
        [PublicAPI]
        public bool AvatarValid { get => humanoidValid; }
        [PublicAPI]
        public int UpdateEveryNthFrame
        {
            get => _nThFrame;
            set => Mathf.Clamp(value, 1, 15);
        }
        [PublicAPI]
        public int ColliderLayer
        {
            get => _colliderLayer;

            set
            {
                if (value < 0 || value > 31) return;

                _colliderLayer = value;

                gameObject.layer = value;

                spine_Collider.gameObject.layer = value;
                chest_Collider.gameObject.layer = value;
                head_Collider.gameObject.layer = value;
                index_L_Collider.gameObject.layer = value;
                middle_L_Collider.gameObject.layer = value;
                ring_L_Collider.gameObject.layer = value;
                little_L_Collider.gameObject.layer = value;
                index_R_Collider.gameObject.layer = value;
                middle_R_Collider.gameObject.layer = value;
                ring_R_Collider.gameObject.layer = value;
                little_R_Collider.gameObject.layer = value;
                upperLeg_L_Collider.gameObject.layer = value;
                lowerLeg_L_Collider.gameObject.layer = value;
                foot_L_Collider.gameObject.layer = value;
                upperLeg_R_Collider.gameObject.layer = value;
                lowerLeg_R_Collider.gameObject.layer = value;
                foot_R_Collider.gameObject.layer = value;
                upperArm_L_Collider.gameObject.layer = value;
                lowerArm_L_Collider.gameObject.layer = value;
                hand_L_Collider.gameObject.layer = value;
                upperArm_R_Collider.gameObject.layer = value;
                lowerArm_R_Collider.gameObject.layer = value;
                hand_R_Collider.gameObject.layer = value;

                spine_Bone.gameObject.layer = value;
                chest_Bone.gameObject.layer = value;
                head_Bone.gameObject.layer = value;
                index_L_Bone.gameObject.layer = value;
                middle_L_Bone.gameObject.layer = value;
                ring_L_Bone.gameObject.layer = value;
                little_L_Bone.gameObject.layer = value;
                index_R_Bone.gameObject.layer = value;
                middle_R_Bone.gameObject.layer = value;
                ring_R_Bone.gameObject.layer = value;
                little_R_Bone.gameObject.layer = value;
                upperLeg_L_Bone.gameObject.layer = value;
                lowerLeg_L_Bone.gameObject.layer = value;
                foot_L_Bone.gameObject.layer = value;
                upperLeg_R_Bone.gameObject.layer = value;
                lowerLeg_R_Bone.gameObject.layer = value;
                foot_R_Bone.gameObject.layer = value;
                upperArm_L_Bone.gameObject.layer = value;
                lowerArm_L_Bone.gameObject.layer = value;
                hand_L_Bone.gameObject.layer = value;
                upperArm_R_Bone.gameObject.layer = value;
                lowerArm_R_Bone.gameObject.layer = value;
                hand_R_Bone.gameObject.layer = value;
            }
        }
        [PublicAPI]
        public Rigidbody[] BoneRigidbodies
        {
            get
            {
                return new Rigidbody[]
                {
                    spine_Bone,
                    chest_Bone,
                    head_Bone,
                    upperLeg_L_Bone,
                    upperLeg_R_Bone,
                    lowerLeg_L_Bone,
                    lowerLeg_R_Bone,
                    foot_L_Bone,
                    foot_R_Bone,
                    upperArm_L_Bone,
                    upperArm_R_Bone,
                    lowerArm_L_Bone,
                    lowerArm_R_Bone,
                    hand_L_Bone,
                    hand_R_Bone,
                    index_L_Bone,
                    middle_L_Bone,
                    ring_L_Bone,
                    little_L_Bone,
                    index_R_Bone,
                    middle_R_Bone,
                    ring_R_Bone,
                    little_R_Bone
                };
            }
        }
        [PublicAPI]
        public Collider[] BoneColliders
        {
            get
            {
                return new Collider[]
                {
                    spine_Collider,
                    chest_Collider,
                    head_Collider,
                    upperLeg_L_Collider,
                    upperLeg_R_Collider,
                    lowerLeg_L_Collider,
                    lowerLeg_R_Collider,
                    foot_L_Collider,
                    foot_R_Collider,
                    upperArm_L_Collider,
                    upperArm_R_Collider,
                    lowerArm_L_Collider,
                    lowerArm_R_Collider,
                    hand_L_Collider,
                    hand_R_Collider,
                    index_L_Collider,
                    middle_L_Collider,
                    ring_L_Collider,
                    little_L_Collider,
                    index_R_Collider,
                    middle_R_Collider,
                    ring_R_Collider,
                    little_R_Collider
                };
            }
        }
        [PublicAPI]
        public bool DetectCollision
        {
            get => _detectCollisions;
            set
            {
                index_L_Bone.detectCollisions = value;
                index_R_Bone.detectCollisions = value;
                middle_L_Bone.detectCollisions = value;
                middle_R_Bone.detectCollisions = value;
                ring_L_Bone.detectCollisions = value;
                ring_R_Bone.detectCollisions = value;
                little_L_Bone.detectCollisions = value;
                little_R_Bone.detectCollisions = value;
                hand_L_Bone.detectCollisions = value;
                hand_R_Bone.detectCollisions = value;
                upperArm_L_Bone.detectCollisions = value;
                lowerArm_L_Bone.detectCollisions = value;
                upperArm_R_Bone.detectCollisions = value;
                lowerArm_R_Bone.detectCollisions = value;
                upperLeg_L_Bone.detectCollisions = value;
                lowerLeg_L_Bone.detectCollisions = value;
                foot_L_Bone.detectCollisions = value;
                upperLeg_R_Bone.detectCollisions = value;
                lowerLeg_R_Bone.detectCollisions = value;
                foot_R_Bone.detectCollisions = value;
                spine_Bone.detectCollisions = value;
                chest_Bone.detectCollisions = value;
                head_Bone.detectCollisions = value;

                _detectCollisions = value;
            }
        }
        [PublicAPI]
        public bool ColliderIsTrigger
        {
            get => _colliderIsTrigger;
            set
            {
                spine_Collider.isTrigger = value;
                chest_Collider.isTrigger = value;
                head_Collider.isTrigger = value;
                upperLeg_L_Collider.isTrigger = value;
                upperLeg_R_Collider.isTrigger = value;
                lowerLeg_L_Collider.isTrigger = value;
                lowerLeg_R_Collider.isTrigger = value;
                foot_L_Collider.isTrigger = value;
                foot_R_Collider.isTrigger = value;
                upperArm_L_Collider.isTrigger = value;
                upperArm_R_Collider.isTrigger = value;
                lowerArm_L_Collider.isTrigger = value;
                lowerArm_R_Collider.isTrigger = value;
                hand_L_Collider.isTrigger = value;
                hand_R_Collider.isTrigger = value;
                index_L_Collider.isTrigger = value;
                middle_L_Collider.isTrigger = value;
                ring_L_Collider.isTrigger = value;
                little_L_Collider.isTrigger = value;
                index_R_Collider.isTrigger = value;
                middle_R_Collider.isTrigger = value;
                ring_R_Collider.isTrigger = value;
                little_R_Collider.isTrigger = value;

                _colliderIsTrigger = value;
            }
        }
        [PublicAPI]
        public LayerMask IncludeLayers
        {
            get => _includeLayers;
            set
            {
                _includeLayers = value;

                chest_Bone.includeLayers = _includeLayers;
                spine_Bone.includeLayers = _includeLayers;
                head_Bone.includeLayers = _includeLayers;
                upperLeg_L_Bone.includeLayers = _includeLayers;
                lowerLeg_L_Bone.includeLayers = _includeLayers;
                foot_L_Bone.includeLayers = _includeLayers;
                upperLeg_R_Bone.includeLayers = _includeLayers;
                lowerLeg_R_Bone.includeLayers = _includeLayers;
                foot_R_Bone.includeLayers = _includeLayers;
                upperArm_L_Bone.includeLayers = _includeLayers;
                lowerArm_L_Bone.includeLayers = _includeLayers;
                hand_L_Bone.includeLayers = _includeLayers;
                upperArm_R_Bone.includeLayers = _includeLayers;
                lowerArm_R_Bone.includeLayers = _includeLayers;
                hand_R_Bone.includeLayers = _includeLayers;
                index_L_Bone.includeLayers = _includeLayers;
                middle_L_Bone.includeLayers = _includeLayers;
                ring_L_Bone.includeLayers = _includeLayers;
                little_L_Bone.includeLayers = _includeLayers;
                index_R_Bone.includeLayers = _includeLayers;
                middle_R_Bone.includeLayers = _includeLayers;
                ring_R_Bone.includeLayers = _includeLayers;
                little_R_Bone.includeLayers = _includeLayers;

                chest_Collider.includeLayers = _includeLayers;
                spine_Collider.includeLayers = _includeLayers;
                head_Collider.includeLayers = _includeLayers;
                upperLeg_L_Collider.includeLayers = _includeLayers;
                lowerLeg_L_Collider.includeLayers = _includeLayers;
                foot_L_Collider.includeLayers = _includeLayers;
                upperLeg_R_Collider.includeLayers = _includeLayers;
                lowerLeg_R_Collider.includeLayers = _includeLayers;
                foot_R_Collider.includeLayers = _includeLayers;
                upperArm_L_Collider.includeLayers = _includeLayers;
                lowerArm_L_Collider.includeLayers = _includeLayers;
                hand_L_Collider.includeLayers = _includeLayers;
                upperArm_R_Collider.includeLayers = _includeLayers;
                lowerArm_R_Collider.includeLayers = _includeLayers;
                hand_R_Collider.includeLayers = _includeLayers;
                index_L_Collider.includeLayers = _includeLayers;
                middle_L_Collider.includeLayers = _includeLayers;
                ring_L_Collider.includeLayers = _includeLayers;
                little_L_Collider.includeLayers = _includeLayers;
                index_R_Collider.includeLayers = _includeLayers;
                middle_R_Collider.includeLayers = _includeLayers;
                ring_R_Collider.includeLayers = _includeLayers;
                little_R_Collider.includeLayers = _includeLayers;
            }
        }
        [PublicAPI]
        public LayerMask ExcludeLayers
        {
            get => _excludeLayers;
            set
            {
                _excludeLayers = value;

                chest_Bone.excludeLayers = _excludeLayers;
                spine_Bone.excludeLayers = _excludeLayers;
                head_Bone.excludeLayers = _excludeLayers;
                upperLeg_L_Bone.excludeLayers = _excludeLayers;
                lowerLeg_L_Bone.excludeLayers = _excludeLayers;
                foot_L_Bone.excludeLayers = _excludeLayers;
                upperLeg_R_Bone.excludeLayers = _excludeLayers;
                lowerLeg_R_Bone.excludeLayers = _excludeLayers;
                foot_R_Bone.excludeLayers = _excludeLayers;
                upperArm_L_Bone.excludeLayers = _excludeLayers;
                lowerArm_L_Bone.excludeLayers = _excludeLayers;
                hand_L_Bone.excludeLayers = _excludeLayers;
                upperArm_R_Bone.excludeLayers = _excludeLayers;
                lowerArm_R_Bone.excludeLayers = _excludeLayers;
                hand_R_Bone.excludeLayers = _excludeLayers;
                index_L_Bone.excludeLayers = _excludeLayers;
                middle_L_Bone.excludeLayers = _excludeLayers;
                ring_L_Bone.excludeLayers = _excludeLayers;
                little_L_Bone.excludeLayers = _excludeLayers;
                index_R_Bone.excludeLayers = _excludeLayers;
                middle_R_Bone.excludeLayers = _excludeLayers;
                ring_R_Bone.excludeLayers = _excludeLayers;
                little_R_Bone.excludeLayers = _excludeLayers;

                chest_Collider.excludeLayers = _excludeLayers;
                spine_Collider.excludeLayers = _excludeLayers;
                head_Collider.excludeLayers = _excludeLayers;
                upperLeg_L_Collider.excludeLayers = _excludeLayers;
                lowerLeg_L_Collider.excludeLayers = _excludeLayers;
                foot_L_Collider.excludeLayers = _excludeLayers;
                upperLeg_R_Collider.excludeLayers = _excludeLayers;
                lowerLeg_R_Collider.excludeLayers = _excludeLayers;
                foot_R_Collider.excludeLayers = _excludeLayers;
                upperArm_L_Collider.excludeLayers = _excludeLayers;
                lowerArm_L_Collider.excludeLayers = _excludeLayers;
                hand_L_Collider.excludeLayers = _excludeLayers;
                upperArm_R_Collider.excludeLayers = _excludeLayers;
                lowerArm_R_Collider.excludeLayers = _excludeLayers;
                hand_R_Collider.excludeLayers = _excludeLayers;
                index_L_Collider.excludeLayers = _excludeLayers;
                middle_L_Collider.excludeLayers = _excludeLayers;
                ring_L_Collider.excludeLayers = _excludeLayers;
                little_L_Collider.excludeLayers = _excludeLayers;
                index_R_Collider.excludeLayers = _excludeLayers;
                middle_R_Collider.excludeLayers = _excludeLayers;
                ring_R_Collider.excludeLayers = _excludeLayers;
                little_R_Collider.excludeLayers = _excludeLayers;
            }
        }
        [PublicAPI]
        public bool VisualizerState { get => _visualizerState; }
        //isActiveAndEnabled not whitelisted so recreated till whitelisted
        private new bool isActiveAndEnabled
        {
            get
            {
                if (_gobjCache == null) _gobjCache = gameObject;
                return enabled && _gobjCache.activeInHierarchy;
            }
        }
        #endregion Properties

        #region Fields
        [SerializeField]
        //get local api on start
        private bool getLocalUser = false;
        //collider target
        private VRCPlayerApi _userApi;

        [SerializeField]
        //collider layers 10 is local, 9 is remote.
        private int _colliderLayer = 0;
        [SerializeField]
        private bool _colliderIsTrigger = false;
        [SerializeField]
        private LayerMask _includeLayers;
        [SerializeField]
        private LayerMask _excludeLayers;

        private const float averageEyeHeight = 1.64f;

        [SerializeField]
        private bool _fingerColliderEnable = true,
            _handColliderEnable = true,
            _armColliderEnable = true,
            _legColliderEnable = true,
            _torsoColliderEnable = true,
            _headColliderEnable = true;

        private bool _avatarCalibrated = false,
            _initialized = false,
            _updateCollider = false,
            _teleportCollider = false,
            _delayHeightChangeState = false,
            _delaySetHeight = false,
            _detectCollisions = true;

        private bool _visualizerState = false;

        private GameObject _gobjCache;

        private int _nThFrame = 1;

        #region Bone_Refs
        [SerializeField]
        //rigidbodys for each bone collider
        private Rigidbody chest_Bone,
            spine_Bone,
            head_Bone,
            upperLeg_L_Bone, upperLeg_R_Bone,
            lowerLeg_L_Bone, lowerLeg_R_Bone,
            foot_L_Bone, foot_R_Bone,
            upperArm_L_Bone, upperArm_R_Bone,
            lowerArm_L_Bone, lowerArm_R_Bone,
            hand_L_Bone, hand_R_Bone,
            index_L_Bone, index_R_Bone,
            middle_L_Bone, middle_R_Bone,
            ring_L_Bone, ring_R_Bone,
            little_L_Bone, little_R_Bone;
        //bone Transforsm
        private Transform chest_Bone_T,
            spine_Bone_T,
            head_Bone_T,
            upperLeg_L_Bone_T, upperLeg_R_Bone_T,
            lowerLeg_L_Bone_T, lowerLeg_R_Bone_T,
            foot_L_Bone_T, foot_R_Bone_T,
            upperArm_L_Bone_T, upperArm_R_Bone_T,
            lowerArm_L_Bone_T, lowerArm_R_Bone_T,
            hand_L_Bone_T, hand_R_Bone_T,
            index_L_Bone_T, index_R_Bone_T,
            middle_L_Bone_T, middle_R_Bone_T,
            ring_L_Bone_T, ring_R_Bone_T,
            little_L_Bone_T, little_R_Bone_T;
        [SerializeField]
        //collider for head
        private SphereCollider head_Collider;
        [SerializeField]
        //colliders for bones
        private CapsuleCollider chest_Collider,
            spine_Collider,
            upperLeg_L_Collider, upperLeg_R_Collider,
            lowerLeg_L_Collider, lowerLeg_R_Collider,
            foot_L_Collider, foot_R_Collider,
            upperArm_L_Collider, upperArm_R_Collider,
            lowerArm_L_Collider, lowerArm_R_Collider,
            index_L_Collider, index_R_Collider,
            middle_L_Collider, middle_R_Collider,
            ring_L_Collider, ring_R_Collider,
            little_L_Collider, little_R_Collider;
        [SerializeField]
        //colliders for hands
        private BoxCollider hand_L_Collider, hand_R_Collider;
        //transform of the colliders for calibration
        private Transform chest_Collider_T,
            spine_Collider_T,
            head_Collider_T,
            upperLeg_L_Collider_T, upperLeg_R_Collider_T,
            lowerLeg_L_Collider_T, lowerLeg_R_Collider_T,
            foot_L_Collider_T, foot_R_Collider_T,
            upperArm_L_Collider_T, upperArm_R_Collider_T,
            lowerArm_L_Collider_T, lowerArm_R_Collider_T,
            hand_L_Collider_T, hand_R_Collider_T,
            index_L_Collider_T, index_R_Collider_T,
            middle_L_Collider_T, middle_R_Collider_T,
            ring_L_Collider_T, ring_R_Collider_T,
            little_L_Collider_T, little_R_Collider_T;
        private VisualizePrimCollider[] colliderVisualizers;

        [SerializeField]//how big colliders should be
        private float upperLeg_radius = .12f,
            lowerLeg_radius = .09f,
            foot_radius = .07f,
            upperArm_radius = .1f,
            lowerArm_radius = .08f,
            SpineFactor = 1.97f,
            head_radius = .25f,
            finger_radius = .02f;
        //if bones are valid
        private bool foot_Valid = false,
            index_Valid = false,
            middle_Valid = false,
            ring_Valid = false,
            little_Valid = false,
            eye_Valid = false,
            humanoidValid = false;
        //furthest valid finger.
        private HumanBodyBones index_L_Furthest, index_R_Furthest,
            middle_L_Furthest, middle_R_Furthest,
            ring_L_Furthest, ring_R_Furthest,
            little_L_Furthest, little_R_Furthest;
        #endregion Bone_Refs

        #endregion Fields

        #region Methods

        #region Public Methods
        [PublicAPI]
        public void SetUser(VRCPlayerApi player)
        {
            if (!Utilities.IsValid(player))
            {
                _userApi = null;
                return;
            }

            _userApi = player;
            _CheckBoneValidity();

            if (!isActiveAndEnabled || !_initialized) return;

            _CalibrateToAvatar();
            _SetCollidersEnabled(true);
        }
        [PublicAPI]
        public void SetFingerColliderState(bool state)
        {
            if (state == _fingerColliderEnable) return;

            if (state)
            {
                _teleportCollider = true;
                index_L_Bone.detectCollisions = false;
                index_R_Bone.detectCollisions = false;

                middle_L_Bone.detectCollisions = false;
                middle_R_Bone.detectCollisions = false;

                ring_L_Bone.detectCollisions = false;
                ring_R_Bone.detectCollisions = false;

                little_L_Bone.detectCollisions = false;
                little_R_Bone.detectCollisions = false;
            }

            _fingerColliderEnable = state;

            index_L_Bone.gameObject.SetActive(state && index_Valid);
            index_R_Bone.gameObject.SetActive(state && index_Valid);

            middle_L_Bone.gameObject.SetActive(state && middle_Valid);
            middle_R_Bone.gameObject.SetActive(state && middle_Valid);

            ring_L_Bone.gameObject.SetActive(state && ring_Valid);
            ring_R_Bone.gameObject.SetActive(state && ring_Valid);

            little_L_Bone.gameObject.SetActive(state && little_Valid);
            little_R_Bone.gameObject.SetActive(state && little_Valid);
        }
        [PublicAPI]
        public void SetHandColliderState(bool state)
        {
            if (state == _handColliderEnable) return;

            if (state)
            {
                _teleportCollider = true;
                hand_L_Bone.detectCollisions = false;
                hand_R_Bone.detectCollisions = false;
            }

            _handColliderEnable = state;

            hand_L_Bone.gameObject.SetActive(state && humanoidValid);
            hand_R_Bone.gameObject.SetActive(state && humanoidValid);
        }
        [PublicAPI]
        public void SetArmColliderState(bool state)
        {
            if (state == _armColliderEnable) return;

            if (state)
            {
                _teleportCollider = true;
                upperArm_L_Bone.detectCollisions = false;
                lowerArm_L_Bone.detectCollisions = false;

                upperArm_R_Bone.detectCollisions = false;
                lowerArm_R_Bone.detectCollisions = false;
            }

            _armColliderEnable = state;

            upperArm_L_Bone.gameObject.SetActive(state && humanoidValid);
            lowerArm_L_Bone.gameObject.SetActive(state && humanoidValid);

            upperArm_R_Bone.gameObject.SetActive(state && humanoidValid);
            lowerArm_R_Bone.gameObject.SetActive(state && humanoidValid);
        }
        [PublicAPI]
        public void SetLegColliderState(bool state)
        {
            if (state == _legColliderEnable) return;

            if (state)
            {
                _teleportCollider = true;
                upperLeg_L_Bone.detectCollisions = false;
                lowerLeg_L_Bone.detectCollisions = false;
                foot_L_Bone.detectCollisions = false;

                upperLeg_R_Bone.detectCollisions = false;
                lowerLeg_R_Bone.detectCollisions = false;
                foot_R_Bone.detectCollisions = false;
            }

            _legColliderEnable = state;

            upperLeg_L_Bone.gameObject.SetActive(state && humanoidValid);
            lowerLeg_L_Bone.gameObject.SetActive(state && humanoidValid);
            foot_L_Bone.gameObject.SetActive(state && humanoidValid);

            upperLeg_R_Bone.gameObject.SetActive(state && humanoidValid);
            lowerLeg_R_Bone.gameObject.SetActive(state && humanoidValid);
            foot_R_Bone.gameObject.SetActive(state && humanoidValid);
        }
        [PublicAPI]
        public void SetTorsoColliderState(bool state)
        {
            if (state == _torsoColliderEnable) return;

            if (state)
            {
                _teleportCollider = true;
                spine_Bone.detectCollisions = false;
                chest_Bone.detectCollisions = false;
            }

            _torsoColliderEnable = state;

            spine_Bone.gameObject.SetActive(state && humanoidValid);
            chest_Bone.gameObject.SetActive(state && humanoidValid);
        }
        [PublicAPI]
        public void SetHeadColliderState(bool state)
        {
            if (state == _headColliderEnable) return;

            if (state)
            {
                _teleportCollider = true;
                head_Bone.detectCollisions = false;
            }

            _headColliderEnable = state;

            head_Bone.gameObject.SetActive(state && humanoidValid);
        }
        [PublicAPI]
        public void TeleportCollider()
        {
            index_L_Bone.detectCollisions = false;
            index_R_Bone.detectCollisions = false;

            middle_L_Bone.detectCollisions = false;
            middle_R_Bone.detectCollisions = false;

            ring_L_Bone.detectCollisions = false;
            ring_R_Bone.detectCollisions = false;

            little_L_Bone.detectCollisions = false;
            little_R_Bone.detectCollisions = false;

            hand_L_Bone.detectCollisions = false;
            hand_R_Bone.detectCollisions = false;

            upperArm_L_Bone.detectCollisions = false;
            lowerArm_L_Bone.detectCollisions = false;

            upperArm_R_Bone.detectCollisions = false;
            lowerArm_R_Bone.detectCollisions = false;

            upperLeg_L_Bone.detectCollisions = false;
            lowerLeg_L_Bone.detectCollisions = false;
            foot_L_Bone.detectCollisions = false;

            upperLeg_R_Bone.detectCollisions = false;
            lowerLeg_R_Bone.detectCollisions = false;

            spine_Bone.detectCollisions = false;
            chest_Bone.detectCollisions = false;
            foot_R_Bone.detectCollisions = false;

            head_Bone.detectCollisions = false;

            _teleportCollider = true;
        }
        [PublicAPI]
        public Rigidbody GetBoneRigidbody(HumanBodyBones bone)
        {
            Rigidbody boneRigidbody;

            switch (bone)
            {
                case HumanBodyBones.Hips:
                    boneRigidbody = spine_Bone;
                    break;
                case HumanBodyBones.LeftUpperLeg:
                    boneRigidbody = upperLeg_L_Bone;
                    break;
                case HumanBodyBones.RightUpperLeg:
                    boneRigidbody = upperLeg_R_Bone;
                    break;
                case HumanBodyBones.LeftLowerLeg:
                    boneRigidbody = lowerLeg_L_Bone;
                    break;
                case HumanBodyBones.RightLowerLeg:
                    boneRigidbody = lowerLeg_R_Bone;
                    break;
                case HumanBodyBones.LeftFoot:
                    boneRigidbody = foot_L_Bone;
                    break;
                case HumanBodyBones.RightFoot:
                    boneRigidbody = foot_R_Bone;
                    break;
                case HumanBodyBones.Spine:
                    boneRigidbody = spine_Bone;
                    break;
                case HumanBodyBones.Chest:
                    boneRigidbody = chest_Bone;
                    break;
                case HumanBodyBones.UpperChest:
                    boneRigidbody = chest_Bone;
                    break;
                case HumanBodyBones.Neck:
                    boneRigidbody = head_Bone;
                    break;
                case HumanBodyBones.Head:
                    boneRigidbody = head_Bone;
                    break;
                case HumanBodyBones.LeftShoulder:
                    boneRigidbody = upperArm_L_Bone;
                    break;
                case HumanBodyBones.RightShoulder:
                    boneRigidbody = upperArm_R_Bone;
                    break;
                case HumanBodyBones.LeftUpperArm:
                    boneRigidbody = upperArm_L_Bone;
                    break;
                case HumanBodyBones.RightUpperArm:
                    boneRigidbody = upperArm_R_Bone;
                    break;
                case HumanBodyBones.LeftLowerArm:
                    boneRigidbody = lowerArm_L_Bone;
                    break;
                case HumanBodyBones.RightLowerArm:
                    boneRigidbody = lowerArm_R_Bone;
                    break;
                case HumanBodyBones.LeftHand:
                    boneRigidbody = hand_L_Bone;
                    break;
                case HumanBodyBones.RightHand:
                    boneRigidbody = hand_R_Bone;
                    break;
                case HumanBodyBones.LeftToes:
                    boneRigidbody = foot_L_Bone;
                    break;
                case HumanBodyBones.RightToes:
                    boneRigidbody = foot_R_Bone;
                    break;
                case HumanBodyBones.LeftEye:
                    boneRigidbody = head_Bone;
                    break;
                case HumanBodyBones.RightEye:
                    boneRigidbody = head_Bone;
                    break;
                case HumanBodyBones.Jaw:
                    boneRigidbody = head_Bone;
                    break;
                case HumanBodyBones.LeftIndexProximal:
                    boneRigidbody = index_L_Bone;
                    break;
                case HumanBodyBones.LeftIndexIntermediate:
                    boneRigidbody = index_L_Bone;
                    break;
                case HumanBodyBones.LeftIndexDistal:
                    boneRigidbody = index_L_Bone;
                    break;
                case HumanBodyBones.LeftMiddleProximal:
                    boneRigidbody = middle_L_Bone;
                    break;
                case HumanBodyBones.LeftMiddleIntermediate:
                    boneRigidbody = middle_L_Bone;
                    break;
                case HumanBodyBones.LeftMiddleDistal:
                    boneRigidbody = middle_L_Bone;
                    break;
                case HumanBodyBones.LeftRingProximal:
                    boneRigidbody = ring_L_Bone;
                    break;
                case HumanBodyBones.LeftRingIntermediate:
                    boneRigidbody = ring_L_Bone;
                    break;
                case HumanBodyBones.LeftRingDistal:
                    boneRigidbody = ring_L_Bone;
                    break;
                case HumanBodyBones.LeftLittleProximal:
                    boneRigidbody = little_L_Bone;
                    break;
                case HumanBodyBones.LeftLittleIntermediate:
                    boneRigidbody = little_L_Bone;
                    break;
                case HumanBodyBones.LeftLittleDistal:
                    boneRigidbody = little_L_Bone;
                    break;
                case HumanBodyBones.RightIndexProximal:
                    boneRigidbody = index_R_Bone;
                    break;
                case HumanBodyBones.RightIndexIntermediate:
                    boneRigidbody = index_R_Bone;
                    break;
                case HumanBodyBones.RightIndexDistal:
                    boneRigidbody = index_R_Bone;
                    break;
                case HumanBodyBones.RightMiddleProximal:
                    boneRigidbody = middle_R_Bone;
                    break;
                case HumanBodyBones.RightMiddleIntermediate:
                    boneRigidbody = middle_R_Bone;
                    break;
                case HumanBodyBones.RightMiddleDistal:
                    boneRigidbody = middle_R_Bone;
                    break;
                case HumanBodyBones.RightRingProximal:
                    boneRigidbody = ring_R_Bone;
                    break;
                case HumanBodyBones.RightRingIntermediate:
                    boneRigidbody = ring_R_Bone;
                    break;
                case HumanBodyBones.RightRingDistal:
                    boneRigidbody = ring_R_Bone;
                    break;
                case HumanBodyBones.RightLittleProximal:
                    boneRigidbody = little_R_Bone;
                    break;
                case HumanBodyBones.RightLittleIntermediate:
                    boneRigidbody = little_R_Bone;
                    break;
                case HumanBodyBones.RightLittleDistal:
                    boneRigidbody = little_R_Bone;
                    break;
                default:
                    boneRigidbody = null;
                    break;
            }

            return boneRigidbody;
        }
        [PublicAPI]
        public Collider GetBoneCollider(HumanBodyBones bone)
        {
            Collider boneCollider;

            switch (bone)
            {
                case HumanBodyBones.Hips:
                    boneCollider = spine_Collider;
                    break;
                case HumanBodyBones.LeftUpperLeg:
                    boneCollider = upperLeg_L_Collider;
                    break;
                case HumanBodyBones.RightUpperLeg:
                    boneCollider = upperLeg_R_Collider;
                    break;
                case HumanBodyBones.LeftLowerLeg:
                    boneCollider = lowerLeg_L_Collider;
                    break;
                case HumanBodyBones.RightLowerLeg:
                    boneCollider = lowerLeg_R_Collider;
                    break;
                case HumanBodyBones.LeftFoot:
                    boneCollider = foot_L_Collider;
                    break;
                case HumanBodyBones.RightFoot:
                    boneCollider = foot_R_Collider;
                    break;
                case HumanBodyBones.Spine:
                    boneCollider = spine_Collider;
                    break;
                case HumanBodyBones.Chest:
                    boneCollider = chest_Collider;
                    break;
                case HumanBodyBones.UpperChest:
                    boneCollider = chest_Collider;
                    break;
                case HumanBodyBones.Neck:
                    boneCollider = head_Collider;
                    break;
                case HumanBodyBones.Head:
                    boneCollider = head_Collider;
                    break;
                case HumanBodyBones.LeftShoulder:
                    boneCollider = upperArm_L_Collider;
                    break;
                case HumanBodyBones.RightShoulder:
                    boneCollider = upperArm_R_Collider;
                    break;
                case HumanBodyBones.LeftUpperArm:
                    boneCollider = upperArm_L_Collider;
                    break;
                case HumanBodyBones.RightUpperArm:
                    boneCollider = upperArm_R_Collider;
                    break;
                case HumanBodyBones.LeftLowerArm:
                    boneCollider = lowerArm_L_Collider;
                    break;
                case HumanBodyBones.RightLowerArm:
                    boneCollider = lowerArm_R_Collider;
                    break;
                case HumanBodyBones.LeftHand:
                    boneCollider = hand_L_Collider;
                    break;
                case HumanBodyBones.RightHand:
                    boneCollider = hand_R_Collider;
                    break;
                case HumanBodyBones.LeftToes:
                    boneCollider = foot_L_Collider;
                    break;
                case HumanBodyBones.RightToes:
                    boneCollider = foot_R_Collider;
                    break;
                case HumanBodyBones.LeftEye:
                    boneCollider = head_Collider;
                    break;
                case HumanBodyBones.RightEye:
                    boneCollider = head_Collider;
                    break;
                case HumanBodyBones.Jaw:
                    boneCollider = head_Collider;
                    break;
                case HumanBodyBones.LeftIndexProximal:
                    boneCollider = index_L_Collider;
                    break;
                case HumanBodyBones.LeftIndexIntermediate:
                    boneCollider = index_L_Collider;
                    break;
                case HumanBodyBones.LeftIndexDistal:
                    boneCollider = index_L_Collider;
                    break;
                case HumanBodyBones.LeftMiddleProximal:
                    boneCollider = middle_L_Collider;
                    break;
                case HumanBodyBones.LeftMiddleIntermediate:
                    boneCollider = middle_L_Collider;
                    break;
                case HumanBodyBones.LeftMiddleDistal:
                    boneCollider = middle_L_Collider;
                    break;
                case HumanBodyBones.LeftRingProximal:
                    boneCollider = ring_L_Collider;
                    break;
                case HumanBodyBones.LeftRingIntermediate:
                    boneCollider = ring_L_Collider;
                    break;
                case HumanBodyBones.LeftRingDistal:
                    boneCollider = ring_L_Collider;
                    break;
                case HumanBodyBones.LeftLittleProximal:
                    boneCollider = little_L_Collider;
                    break;
                case HumanBodyBones.LeftLittleIntermediate:
                    boneCollider = little_L_Collider;
                    break;
                case HumanBodyBones.LeftLittleDistal:
                    boneCollider = little_L_Collider;
                    break;
                case HumanBodyBones.RightIndexProximal:
                    boneCollider = index_R_Collider;
                    break;
                case HumanBodyBones.RightIndexIntermediate:
                    boneCollider = index_R_Collider;
                    break;
                case HumanBodyBones.RightIndexDistal:
                    boneCollider = index_R_Collider;
                    break;
                case HumanBodyBones.RightMiddleProximal:
                    boneCollider = middle_R_Collider;
                    break;
                case HumanBodyBones.RightMiddleIntermediate:
                    boneCollider = middle_R_Collider;
                    break;
                case HumanBodyBones.RightMiddleDistal:
                    boneCollider = middle_R_Collider;
                    break;
                case HumanBodyBones.RightRingProximal:
                    boneCollider = ring_R_Collider;
                    break;
                case HumanBodyBones.RightRingIntermediate:
                    boneCollider = ring_R_Collider;
                    break;
                case HumanBodyBones.RightRingDistal:
                    boneCollider = ring_R_Collider;
                    break;
                case HumanBodyBones.RightLittleProximal:
                    boneCollider = little_R_Collider;
                    break;
                case HumanBodyBones.RightLittleIntermediate:
                    boneCollider = little_R_Collider;
                    break;
                case HumanBodyBones.RightLittleDistal:
                    boneCollider = little_R_Collider;
                    break;
                default:
                    boneCollider = null;
                    break;
            }

            return boneCollider;
        }
        [PublicAPI]
        public void VisualizeColliders(bool state)
        {
            _visualizerState = state;
            if (colliderVisualizers == null) return;
            foreach (VisualizePrimCollider visualizer in colliderVisualizers)
            {
                visualizer.VisualizeCollider(state);
            }
        }
        #endregion Public Methods

        #region Built-in Events

        #region Unity
        private void OnEnable()
        {
            //awake not currently implimented till udon2
            if (!_initialized)
            {
                _TransformInit();
                _ColliderInit();
                _RigidbodyInit();
                _ColliderIgnoreSelf();
                if (getLocalUser) _userApi = Networking.LocalPlayer;

                _UpdateLimbStates();

                ColliderLayer = _colliderLayer;
                ColliderIsTrigger = _colliderIsTrigger;
                IncludeLayers = _includeLayers;
                ExcludeLayers = _excludeLayers;
                colliderVisualizers = GetComponentsInChildren<VisualizePrimCollider>(true);
                if (_visualizerState && _avatarCalibrated) VisualizeColliders(true);

                _initialized = true;
            }

            _avatarCalibrated = false;
            if (Utilities.IsValid(_userApi))
            {
                _CalibrateToAvatar();
                _SetCollidersEnabled(true);

            }
            else
            {
                _SetCollidersEnabled(false);
            }

        }
        private void OnDisable()
        {
            _avatarCalibrated = false;
            _updateCollider = false;
        }
        private void FixedUpdate()
        {
            if (!_updateCollider || !_avatarCalibrated) return;

            if (_teleportCollider)
            {
                _TeleportPose();
            }
            else
            {
                _UpdatePose();
            }
            _updateCollider = false;
        }
        #endregion Unity

        #region VRC
        public override void OnAvatarChanged(VRCPlayerApi player)
        {
            if (player != _userApi) return;
            _SetCollidersEnabled(false);
            _avatarCalibrated = false;
            _CheckBoneValidity();

            if (!isActiveAndEnabled) return;

            _CalibrateToAvatar();
            _SetCollidersEnabled(true);
        }
        public override void OnAvatarEyeHeightChanged(VRCPlayerApi player, float prevEyeHeightAsMeters)
        {
            if (player != _userApi || !isActiveAndEnabled || !_avatarCalibrated) return;

            if (_userApi.isLocal)
            {
                //calibrates first frame but adds delay if you are changing height every frame.
                if (_delayHeightChangeState)
                {
                    _delaySetHeight = true;
                }
                else
                {
                    _delayHeightChangeState = true;
                    _CalibrateToAvatar();
                    SendCustomEventDelayedSeconds(nameof(_DelayedHeightChange), 0.25f, VRC.Udon.Common.Enums.EventTiming.LateUpdate);
                }
            }
            else
            {
                _CalibrateToAvatar();
            }

        }
        public override void PostLateUpdate()
        {
            if (!_avatarCalibrated || !Utilities.IsValid(_userApi) || Time.frameCount % _nThFrame != (UserApi.playerId % _nThFrame)) return;

            _updateCollider = true;
        }
        #endregion VRC

        #endregion Built-in Events

        #region Setup System
        private void _TransformInit()
        {
            Transform rootCache = transform;
            rootCache.parent = null;
            rootCache.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            rootCache.localScale = Vector3.one;

            chest_Collider_T = chest_Collider.transform;
            spine_Collider_T = spine_Collider.transform;
            head_Collider_T = head_Collider.transform;
            index_L_Collider_T = index_L_Collider.transform;
            middle_L_Collider_T = middle_L_Collider.transform;
            ring_L_Collider_T = ring_L_Collider.transform;
            little_L_Collider_T = little_L_Collider.transform;
            index_R_Collider_T = index_R_Collider.transform;
            middle_R_Collider_T = middle_R_Collider.transform;
            ring_R_Collider_T = ring_R_Collider.transform;
            little_R_Collider_T = little_R_Collider.transform;
            upperLeg_L_Collider_T = upperLeg_L_Collider.transform;
            lowerLeg_L_Collider_T = lowerLeg_L_Collider.transform;
            foot_L_Collider_T = foot_L_Collider.transform;
            upperLeg_R_Collider_T = upperLeg_R_Collider.transform;
            lowerLeg_R_Collider_T = lowerLeg_R_Collider.transform;
            foot_R_Collider_T = foot_R_Collider.transform;
            upperArm_L_Collider_T = upperArm_L_Collider.transform;
            lowerArm_L_Collider_T = lowerArm_L_Collider.transform;
            upperArm_R_Collider_T = upperArm_R_Collider.transform;
            lowerArm_R_Collider_T = lowerArm_R_Collider.transform;
            hand_L_Collider_T = hand_L_Collider.transform;
            hand_R_Collider_T = hand_R_Collider.transform;

            chest_Bone_T = chest_Bone.transform;
            spine_Bone_T = spine_Bone.transform;
            head_Bone_T = head_Bone.transform;
            upperLeg_L_Bone_T = upperLeg_L_Bone.transform;
            lowerLeg_L_Bone_T = lowerLeg_L_Bone.transform;
            foot_L_Bone_T = foot_L_Bone.transform;
            upperLeg_R_Bone_T = upperLeg_R_Bone.transform;
            lowerLeg_R_Bone_T = lowerLeg_R_Bone.transform;
            foot_R_Bone_T = foot_R_Bone.transform;
            upperArm_L_Bone_T = upperArm_L_Bone.transform;
            lowerArm_L_Bone_T = lowerArm_L_Bone.transform;
            hand_L_Bone_T = hand_L_Bone.transform;
            upperArm_R_Bone_T = upperArm_R_Bone.transform;
            lowerArm_R_Bone_T = lowerArm_R_Bone.transform;
            hand_R_Bone_T = hand_R_Bone.transform;
            index_L_Bone_T = index_L_Bone.transform;
            middle_L_Bone_T = middle_L_Bone.transform;
            ring_L_Bone_T = ring_L_Bone.transform;
            little_L_Bone_T = little_L_Bone.transform;
            index_R_Bone_T = index_R_Bone.transform;
            middle_R_Bone_T = middle_R_Bone.transform;
            ring_R_Bone_T = ring_R_Bone.transform;
            little_R_Bone_T = little_R_Bone.transform;

            chest_Bone_T.localScale = Vector3.one;
            spine_Bone_T.localScale = Vector3.one;
            head_Bone_T.localScale = Vector3.one;
            upperLeg_L_Bone_T.localScale = Vector3.one;
            lowerLeg_L_Bone_T.localScale = Vector3.one;
            foot_L_Bone_T.localScale = Vector3.one;
            upperLeg_R_Bone_T.localScale = Vector3.one;
            lowerLeg_R_Bone_T.localScale = Vector3.one;
            foot_R_Bone_T.localScale = Vector3.one;
            upperArm_L_Bone_T.localScale = Vector3.one;
            lowerArm_L_Bone_T.localScale = Vector3.one;
            hand_L_Bone_T.localScale = Vector3.one;
            upperArm_R_Bone_T.localScale = Vector3.one;
            lowerArm_R_Bone_T.localScale = Vector3.one;
            hand_R_Bone_T.localScale = Vector3.one;
            index_L_Bone_T.localScale = Vector3.one;
            middle_L_Bone_T.localScale = Vector3.one;
            ring_L_Bone_T.localScale = Vector3.one;
            little_L_Bone_T.localScale = Vector3.one;
            index_R_Bone_T.localScale = Vector3.one;
            middle_R_Bone_T.localScale = Vector3.one;
            ring_R_Bone_T.localScale = Vector3.one;
            little_R_Bone_T.localScale = Vector3.one;


            chest_Collider_T.localScale = Vector3.one;
            spine_Collider_T.localScale = Vector3.one;
            head_Collider_T.localScale = Vector3.one;
            index_L_Collider_T.localScale = Vector3.one;
            middle_L_Collider_T.localScale = Vector3.one;
            ring_L_Collider_T.localScale = Vector3.one;
            little_L_Collider_T.localScale = Vector3.one;
            index_R_Collider_T.localScale = Vector3.one;
            middle_R_Collider_T.localScale = Vector3.one;
            ring_R_Collider_T.localScale = Vector3.one;
            little_R_Collider_T.localScale = Vector3.one;
            upperLeg_L_Collider_T.localScale = Vector3.one;
            lowerLeg_L_Collider_T.localScale = Vector3.one;
            foot_L_Collider_T.localScale = Vector3.one;
            upperLeg_R_Collider_T.localScale = Vector3.one;
            lowerLeg_R_Collider_T.localScale = Vector3.one;
            foot_R_Collider_T.localScale = Vector3.one;
            upperArm_L_Collider_T.localScale = Vector3.one;
            lowerArm_L_Collider_T.localScale = Vector3.one;
            upperArm_R_Collider_T.localScale = Vector3.one;
            lowerArm_R_Collider_T.localScale = Vector3.one;
            hand_L_Collider_T.localScale = Vector3.one;
            hand_R_Collider_T.localScale = Vector3.one;

        }
        private void _ColliderInit()
        {
            int direction = 2;
            chest_Collider.direction = direction;
            spine_Collider.direction = direction;
            index_L_Collider.direction = direction;
            middle_L_Collider.direction = direction;
            ring_L_Collider.direction = direction;
            little_L_Collider.direction = direction;
            index_R_Collider.direction = direction;
            middle_R_Collider.direction = direction;
            ring_R_Collider.direction = direction;
            little_R_Collider.direction = direction;
            upperLeg_L_Collider.direction = direction;
            lowerLeg_L_Collider.direction = direction;
            foot_L_Collider.direction = direction;
            upperLeg_R_Collider.direction = direction;
            lowerLeg_R_Collider.direction = direction;
            foot_R_Collider.direction = direction;
            upperArm_L_Collider.direction = direction;
            lowerArm_L_Collider.direction = direction;
            upperArm_R_Collider.direction = direction;
            lowerArm_R_Collider.direction = direction;

            chest_Collider.center = Vector3.zero;
            spine_Collider.center = Vector3.zero;
            head_Collider.center = Vector3.zero;
            index_L_Collider.center = Vector3.zero;
            middle_L_Collider.center = Vector3.zero;
            ring_L_Collider.center = Vector3.zero;
            little_L_Collider.center = Vector3.zero;
            index_R_Collider.center = Vector3.zero;
            middle_R_Collider.center = Vector3.zero;
            ring_R_Collider.center = Vector3.zero;
            little_R_Collider.center = Vector3.zero;
            upperLeg_L_Collider.center = Vector3.zero;
            lowerLeg_L_Collider.center = Vector3.zero;
            foot_L_Collider.center = Vector3.zero;
            upperLeg_R_Collider.center = Vector3.zero;
            lowerLeg_R_Collider.center = Vector3.zero;
            foot_R_Collider.center = Vector3.zero;
            upperArm_L_Collider.center = Vector3.zero;
            lowerArm_L_Collider.center = Vector3.zero;
            upperArm_R_Collider.center = Vector3.zero;
            lowerArm_R_Collider.center = Vector3.zero;
        }
        private void _RigidbodyInit()
        {
            bool boneKinematic = true;
            chest_Bone.isKinematic = boneKinematic;
            spine_Bone.isKinematic = boneKinematic;
            head_Bone.isKinematic = boneKinematic;
            upperLeg_L_Bone.isKinematic = boneKinematic;
            lowerLeg_L_Bone.isKinematic = boneKinematic;
            foot_L_Bone.isKinematic = boneKinematic;
            upperLeg_R_Bone.isKinematic = boneKinematic;
            lowerLeg_R_Bone.isKinematic = boneKinematic;
            foot_R_Bone.isKinematic = boneKinematic;
            upperArm_L_Bone.isKinematic = boneKinematic;
            lowerArm_L_Bone.isKinematic = boneKinematic;
            hand_L_Bone.isKinematic = boneKinematic;
            upperArm_R_Bone.isKinematic = boneKinematic;
            lowerArm_R_Bone.isKinematic = boneKinematic;
            hand_R_Bone.isKinematic = boneKinematic;
            index_L_Bone.isKinematic = boneKinematic;
            middle_L_Bone.isKinematic = boneKinematic;
            ring_L_Bone.isKinematic = boneKinematic;
            little_L_Bone.isKinematic = boneKinematic;
            index_R_Bone.isKinematic = boneKinematic;
            middle_R_Bone.isKinematic = boneKinematic;
            ring_R_Bone.isKinematic = boneKinematic;
            little_R_Bone.isKinematic = boneKinematic;

            RigidbodyInterpolation interpMode = RigidbodyInterpolation.None;
            chest_Bone.interpolation = interpMode;
            spine_Bone.interpolation = interpMode;
            head_Bone.interpolation = interpMode;
            upperLeg_L_Bone.interpolation = interpMode;
            lowerLeg_L_Bone.interpolation = interpMode;
            foot_L_Bone.interpolation = interpMode;
            upperLeg_R_Bone.interpolation = interpMode;
            lowerLeg_R_Bone.interpolation = interpMode;
            foot_R_Bone.interpolation = interpMode;
            upperArm_L_Bone.interpolation = interpMode;
            lowerArm_L_Bone.interpolation = interpMode;
            hand_L_Bone.interpolation = interpMode;
            upperArm_R_Bone.interpolation = interpMode;
            lowerArm_R_Bone.interpolation = interpMode;
            hand_R_Bone.interpolation = interpMode;
            index_L_Bone.interpolation = interpMode;
            middle_L_Bone.interpolation = interpMode;
            ring_L_Bone.interpolation = interpMode;
            little_L_Bone.interpolation = interpMode;
            index_R_Bone.interpolation = interpMode;
            middle_R_Bone.interpolation = interpMode;
            ring_R_Bone.interpolation = interpMode;
            little_R_Bone.interpolation = interpMode;

            CollisionDetectionMode cDM = CollisionDetectionMode.ContinuousSpeculative;
            foot_L_Bone.collisionDetectionMode = cDM;
            foot_R_Bone.collisionDetectionMode = cDM;
            hand_L_Bone.collisionDetectionMode = cDM;
            hand_R_Bone.collisionDetectionMode = cDM;
            index_L_Bone.collisionDetectionMode = cDM;
            middle_L_Bone.collisionDetectionMode = cDM;
            ring_L_Bone.collisionDetectionMode = cDM;
            little_L_Bone.collisionDetectionMode = cDM;
            index_R_Bone.collisionDetectionMode = cDM;
            middle_R_Bone.collisionDetectionMode = cDM;
            ring_R_Bone.collisionDetectionMode = cDM;
            little_R_Bone.collisionDetectionMode = cDM;

            cDM = CollisionDetectionMode.Discrete;
            chest_Bone.collisionDetectionMode = cDM;
            spine_Bone.collisionDetectionMode = cDM;
            head_Bone.collisionDetectionMode = cDM;
            upperLeg_L_Bone.collisionDetectionMode = cDM;
            lowerLeg_L_Bone.collisionDetectionMode = cDM;
            upperLeg_R_Bone.collisionDetectionMode = cDM;
            lowerLeg_R_Bone.collisionDetectionMode = cDM;
            upperArm_L_Bone.collisionDetectionMode = cDM;
            lowerArm_L_Bone.collisionDetectionMode = cDM;
            upperArm_R_Bone.collisionDetectionMode = cDM;
            lowerArm_R_Bone.collisionDetectionMode = cDM;

        }
        //ignore self collision
        private void _ColliderIgnoreSelf()
        {
            Collider[] colliderArray = BoneColliders;

            int n = colliderArray.Length - 1;
            for (int i = 0; i < n; i++)
            {
                for (int e = n; e > i; e--)
                {
                    Physics.IgnoreCollision(colliderArray[i], colliderArray[e], true);
                }
            }
        }
        #endregion Setup System

        #region Setup Colliders
        //check what avatar bones are valid
        private void _CheckBoneValidity()
        {
            if (!Utilities.IsValid(_userApi)) return;

            humanoidValid = _userApi.GetBonePosition(HumanBodyBones.Hips) != Vector3.zero
                && _userApi.GetBonePosition(HumanBodyBones.Chest) != Vector3.zero
                && _userApi.GetBonePosition(HumanBodyBones.Neck) != Vector3.zero
                && _userApi.GetBonePosition(HumanBodyBones.Head) != Vector3.zero
                && _userApi.GetBonePosition(HumanBodyBones.RightUpperLeg) != Vector3.zero
                && _userApi.GetBonePosition(HumanBodyBones.RightUpperLeg) != Vector3.zero
                && _userApi.GetBonePosition(HumanBodyBones.RightLowerLeg) != Vector3.zero
                && _userApi.GetBonePosition(HumanBodyBones.LeftUpperLeg) != Vector3.zero
                && _userApi.GetBonePosition(HumanBodyBones.LeftUpperLeg) != Vector3.zero
                && _userApi.GetBonePosition(HumanBodyBones.LeftLowerLeg) != Vector3.zero
                && _userApi.GetBonePosition(HumanBodyBones.RightFoot) != Vector3.zero
                && _userApi.GetBonePosition(HumanBodyBones.LeftFoot) != Vector3.zero
                && _userApi.GetBonePosition(HumanBodyBones.LeftUpperArm) != Vector3.zero
                && _userApi.GetBonePosition(HumanBodyBones.LeftLowerArm) != Vector3.zero
                && _userApi.GetBonePosition(HumanBodyBones.LeftHand) != Vector3.zero
                && _userApi.GetBonePosition(HumanBodyBones.LeftUpperArm) != Vector3.zero
                && _userApi.GetBonePosition(HumanBodyBones.LeftLowerArm) != Vector3.zero
                && _userApi.GetBonePosition(HumanBodyBones.LeftHand) != Vector3.zero
                && _userApi.GetBonePosition(HumanBodyBones.RightUpperArm) != Vector3.zero
                && _userApi.GetBonePosition(HumanBodyBones.RightLowerArm) != Vector3.zero
                && _userApi.GetBonePosition(HumanBodyBones.RightHand) != Vector3.zero;

            if (humanoidValid)
            {
                foot_Valid = (_userApi.GetBonePosition(HumanBodyBones.LeftToes) != Vector3.zero) && (_userApi.GetBonePosition(HumanBodyBones.RightToes) != Vector3.zero);

                eye_Valid = (_userApi.GetBonePosition(HumanBodyBones.LeftEye) != Vector3.zero) && (_userApi.GetBonePosition(HumanBodyBones.RightEye) != Vector3.zero);

                index_Valid = (_userApi.GetBonePosition(HumanBodyBones.LeftIndexProximal) != Vector3.zero) && (_userApi.GetBonePosition(HumanBodyBones.RightIndexProximal) != Vector3.zero);
                if (_userApi.GetBonePosition(HumanBodyBones.LeftIndexDistal) != Vector3.zero)
                {
                    index_L_Furthest = HumanBodyBones.LeftIndexDistal;
                }
                else if (_userApi.GetBonePosition(HumanBodyBones.LeftIndexIntermediate) != Vector3.zero)
                {
                    index_L_Furthest = HumanBodyBones.LeftIndexIntermediate;
                }
                else
                {
                    index_L_Furthest = HumanBodyBones.LeftIndexProximal;
                }

                if (_userApi.GetBonePosition(HumanBodyBones.RightIndexDistal) != Vector3.zero)
                {
                    index_R_Furthest = HumanBodyBones.RightIndexDistal;
                }
                else if (_userApi.GetBonePosition(HumanBodyBones.RightIndexIntermediate) != Vector3.zero)
                {
                    index_R_Furthest = HumanBodyBones.RightIndexIntermediate;
                }
                else
                {
                    index_R_Furthest = HumanBodyBones.RightIndexProximal;
                }

                middle_Valid = (_userApi.GetBonePosition(HumanBodyBones.LeftMiddleProximal) != Vector3.zero) && (_userApi.GetBonePosition(HumanBodyBones.RightMiddleProximal) != Vector3.zero);
                if (_userApi.GetBonePosition(HumanBodyBones.LeftMiddleDistal) != Vector3.zero)
                {
                    middle_L_Furthest = HumanBodyBones.LeftMiddleDistal;
                }
                else if (_userApi.GetBonePosition(HumanBodyBones.LeftMiddleIntermediate) != Vector3.zero)
                {
                    middle_L_Furthest = HumanBodyBones.LeftMiddleIntermediate;
                }
                else
                {
                    middle_L_Furthest = HumanBodyBones.LeftMiddleProximal;
                }

                if (_userApi.GetBonePosition(HumanBodyBones.RightMiddleDistal) != Vector3.zero)
                {
                    middle_R_Furthest = HumanBodyBones.RightMiddleDistal;
                }
                else if (_userApi.GetBonePosition(HumanBodyBones.RightMiddleIntermediate) != Vector3.zero)
                {
                    middle_R_Furthest = HumanBodyBones.RightMiddleIntermediate;
                }
                else
                {
                    middle_R_Furthest = HumanBodyBones.RightMiddleProximal;
                }

                ring_Valid = (_userApi.GetBonePosition(HumanBodyBones.LeftRingProximal) != Vector3.zero) && (_userApi.GetBonePosition(HumanBodyBones.RightRingProximal) != Vector3.zero);
                if (_userApi.GetBonePosition(HumanBodyBones.LeftRingDistal) != Vector3.zero)
                {
                    ring_L_Furthest = HumanBodyBones.LeftRingDistal;
                }
                else if (_userApi.GetBonePosition(HumanBodyBones.LeftRingIntermediate) != Vector3.zero)
                {
                    ring_L_Furthest = HumanBodyBones.LeftRingIntermediate;
                }
                else
                {
                    ring_L_Furthest = HumanBodyBones.LeftRingProximal;
                }

                if (_userApi.GetBonePosition(HumanBodyBones.RightRingDistal) != Vector3.zero)
                {
                    ring_R_Furthest = HumanBodyBones.RightRingDistal;
                }
                else if (_userApi.GetBonePosition(HumanBodyBones.RightRingIntermediate) != Vector3.zero)
                {
                    ring_R_Furthest = HumanBodyBones.RightRingIntermediate;
                }
                else
                {
                    ring_R_Furthest = HumanBodyBones.RightRingProximal;
                }

                little_Valid = (_userApi.GetBonePosition(HumanBodyBones.LeftLittleProximal) != Vector3.zero) && (_userApi.GetBonePosition(HumanBodyBones.RightLittleProximal) != Vector3.zero);
                if (_userApi.GetBonePosition(HumanBodyBones.LeftLittleDistal) != Vector3.zero)
                {
                    little_L_Furthest = HumanBodyBones.LeftLittleDistal;
                }
                else if (_userApi.GetBonePosition(HumanBodyBones.LeftLittleIntermediate) != Vector3.zero)
                {
                    little_L_Furthest = HumanBodyBones.LeftLittleIntermediate;
                }
                else
                {
                    little_L_Furthest = HumanBodyBones.LeftLittleProximal;
                }

                if (_userApi.GetBonePosition(HumanBodyBones.RightLittleDistal) != Vector3.zero)
                {
                    little_R_Furthest = HumanBodyBones.RightLittleDistal;
                }
                else if (_userApi.GetBonePosition(HumanBodyBones.RightLittleIntermediate) != Vector3.zero)
                {
                    little_R_Furthest = HumanBodyBones.RightLittleIntermediate;
                }
                else
                {
                    little_R_Furthest = HumanBodyBones.RightLittleProximal;
                }


            }
            else
            {
                foot_Valid = humanoidValid;
                eye_Valid = humanoidValid;
                index_Valid = humanoidValid;
                middle_Valid = humanoidValid;
                ring_Valid = humanoidValid;
                little_Valid = humanoidValid;
            }

            _UpdateLimbStates();
        }
        //setup colliders to avatar
        private void _CalibrateToAvatar()
        {
            if (!humanoidValid || !Utilities.IsValid(_userApi)) return;

            float scaleFactor = _userApi.GetAvatarEyeHeightAsMeters() / averageEyeHeight;

            //calibrate head
            head_Bone_T.SetPositionAndRotation(_userApi.GetBonePosition(HumanBodyBones.Head), _userApi.GetBoneRotation(HumanBodyBones.Head));

            if (eye_Valid)
            {
                Vector3 headEyeMidPos = Vector3.Lerp(head_Bone_T.position, Vector3.Lerp(_userApi.GetBonePosition(HumanBodyBones.LeftEye), _userApi.GetBonePosition(HumanBodyBones.RightEye), .5f), .5f);
                Vector3 eyeDirection = headEyeMidPos - head_Bone_T.position;

                Vector3 headEyeMidCrossPos = headEyeMidPos + (Vector3.Cross(eyeDirection, Vector3.Cross(head_Bone_T.position - _userApi.GetBonePosition(HumanBodyBones.Neck), eyeDirection)).normalized * eyeDirection.magnitude);
                head_Collider_T.SetPositionAndRotation(headEyeMidCrossPos + (headEyeMidCrossPos - head_Bone_T.position), Quaternion.identity);

                head_Collider.radius = (Mathf.Max(Vector3.Distance(head_Collider_T.position, head_Bone_T.position), Mathf.Max(Vector3.Distance(head_Collider_T.position, _userApi.GetBonePosition(HumanBodyBones.LeftEye)), Vector3.Distance(head_Collider_T.position, _userApi.GetBonePosition(HumanBodyBones.RightEye))))) * 1.5f;

            }
            else
            {
                head_Collider_T.SetPositionAndRotation(head_Bone_T.position, head_Bone_T.rotation);
                head_Collider.radius = head_radius * scaleFactor;
            }
            //Debug.Log($"head radius set to: {head_Collider.radius} || scale factor was{scaleFactor} || head radius presize is {head_radius}");

            //calibrate torso
            spine_Bone_T.SetPositionAndRotation(_userApi.GetBonePosition(HumanBodyBones.Hips), Quaternion.LookRotation(_userApi.GetBonePosition(HumanBodyBones.Chest) - _userApi.GetBonePosition(HumanBodyBones.Hips), _userApi.GetBoneRotation(HumanBodyBones.Hips) * Vector3.right));
            chest_Bone_T.SetPositionAndRotation(_userApi.GetBonePosition(HumanBodyBones.Chest), Quaternion.LookRotation(_userApi.GetBonePosition(HumanBodyBones.Neck) - _userApi.GetBonePosition(HumanBodyBones.Chest), _userApi.GetBoneRotation(HumanBodyBones.Chest) * Vector3.right));

            _EncapsulateToPoint(spine_Collider_T, spine_Collider, spine_Bone_T.position, chest_Bone_T.position - spine_Bone_T.position, spine_Bone_T.right, (Vector3.Distance(_userApi.GetBonePosition(HumanBodyBones.LeftUpperLeg), _userApi.GetBonePosition(HumanBodyBones.RightUpperLeg)) * 0.5f) * SpineFactor);
            _EncapsulateToPoint(chest_Collider_T, chest_Collider, chest_Bone_T.position, _userApi.GetBonePosition(HumanBodyBones.Neck) - chest_Bone_T.position, chest_Bone_T.right, (Vector3.Distance(_userApi.GetBonePosition(HumanBodyBones.LeftUpperArm), _userApi.GetBonePosition(HumanBodyBones.RightUpperArm)) * 0.5f) + (upperArm_radius * scaleFactor));

            //calibrate left leg
            upperLeg_L_Bone_T.SetPositionAndRotation(_userApi.GetBonePosition(HumanBodyBones.LeftUpperLeg), _userApi.GetBoneRotation(HumanBodyBones.LeftUpperLeg));
            lowerLeg_L_Bone_T.SetPositionAndRotation(_userApi.GetBonePosition(HumanBodyBones.LeftLowerLeg), _userApi.GetBoneRotation(HumanBodyBones.LeftLowerLeg));
            foot_L_Bone_T.SetPositionAndRotation(_userApi.GetBonePosition(HumanBodyBones.LeftFoot), _userApi.GetBoneRotation(HumanBodyBones.LeftFoot));

            _Encapsulate(upperLeg_L_Collider_T, upperLeg_L_Collider, upperLeg_L_Bone_T.position, lowerLeg_L_Bone_T.position - upperLeg_L_Bone_T.position, upperLeg_L_Bone_T.right, upperLeg_radius * scaleFactor);
            _Encapsulate(lowerLeg_L_Collider_T, lowerLeg_L_Collider, lowerLeg_L_Bone_T.position, foot_L_Bone_T.position - lowerLeg_L_Bone_T.position, lowerLeg_L_Bone_T.right, lowerLeg_radius * scaleFactor);
            _Encapsulate(foot_L_Collider_T, foot_L_Collider, foot_L_Bone_T.position, (foot_Valid ? _userApi.GetBonePosition(HumanBodyBones.LeftToes) : foot_L_Bone_T.position) - foot_L_Bone_T.position, foot_L_Bone_T.right, foot_radius * scaleFactor);

            //calibrate right leg
            upperLeg_R_Bone_T.SetPositionAndRotation(_userApi.GetBonePosition(HumanBodyBones.RightUpperLeg), _userApi.GetBoneRotation(HumanBodyBones.RightUpperLeg));
            lowerLeg_R_Bone_T.SetPositionAndRotation(_userApi.GetBonePosition(HumanBodyBones.RightLowerLeg), _userApi.GetBoneRotation(HumanBodyBones.RightLowerLeg));
            foot_R_Bone_T.SetPositionAndRotation(_userApi.GetBonePosition(HumanBodyBones.RightFoot), _userApi.GetBoneRotation(HumanBodyBones.RightFoot));

            _Encapsulate(upperLeg_R_Collider_T, upperLeg_R_Collider, upperLeg_R_Bone_T.position, lowerLeg_R_Bone_T.position - upperLeg_R_Bone_T.position, upperLeg_R_Bone_T.right, upperLeg_radius * scaleFactor);
            _Encapsulate(lowerLeg_R_Collider_T, lowerLeg_R_Collider, lowerLeg_R_Bone_T.position, foot_R_Bone_T.position - lowerLeg_R_Bone_T.position, lowerLeg_R_Bone_T.right, lowerLeg_radius * scaleFactor);
            _Encapsulate(foot_R_Collider_T, foot_R_Collider, foot_R_Bone_T.position, (foot_Valid ? _userApi.GetBonePosition(HumanBodyBones.RightToes) : foot_R_Bone_T.position) - foot_R_Bone_T.position, foot_R_Bone_T.right, foot_radius * scaleFactor);

            //calibrate left arm
            upperArm_L_Bone_T.SetPositionAndRotation(_userApi.GetBonePosition(HumanBodyBones.LeftUpperArm), _userApi.GetBoneRotation(HumanBodyBones.LeftUpperArm));
            lowerArm_L_Bone_T.SetPositionAndRotation(_userApi.GetBonePosition(HumanBodyBones.LeftLowerArm), _userApi.GetBoneRotation(HumanBodyBones.LeftLowerArm));
            hand_L_Bone_T.SetPositionAndRotation(_userApi.GetBonePosition(HumanBodyBones.LeftHand), _userApi.GetBoneRotation(HumanBodyBones.LeftHand));

            _Encapsulate(upperArm_L_Collider_T, upperArm_L_Collider, upperArm_L_Bone_T.position, lowerArm_L_Bone_T.position - upperArm_L_Bone_T.position, upperArm_L_Bone_T.right, upperArm_radius * scaleFactor);
            _EncapsulateToPoint(lowerArm_L_Collider_T, lowerArm_L_Collider, lowerArm_L_Bone_T.position, hand_L_Bone_T.position - lowerArm_L_Bone_T.position, lowerArm_L_Bone_T.right, lowerArm_radius * scaleFactor);

            //calibrate right arm
            upperArm_R_Bone_T.SetPositionAndRotation(_userApi.GetBonePosition(HumanBodyBones.RightUpperArm), _userApi.GetBoneRotation(HumanBodyBones.RightUpperArm));
            lowerArm_R_Bone_T.SetPositionAndRotation(_userApi.GetBonePosition(HumanBodyBones.RightLowerArm), _userApi.GetBoneRotation(HumanBodyBones.RightLowerArm));
            hand_R_Bone_T.SetPositionAndRotation(_userApi.GetBonePosition(HumanBodyBones.RightHand), _userApi.GetBoneRotation(HumanBodyBones.RightHand));

            _Encapsulate(upperArm_R_Collider_T, upperArm_R_Collider, upperArm_R_Bone_T.position, lowerArm_R_Bone_T.position - upperArm_R_Bone_T.position, upperArm_R_Bone_T.right, upperArm_radius * scaleFactor);
            _EncapsulateToPoint(lowerArm_R_Collider_T, lowerArm_R_Collider, lowerArm_R_Bone_T.position, hand_R_Bone_T.position - lowerArm_R_Bone_T.position, lowerArm_R_Bone_T.right, lowerArm_radius * scaleFactor);

            //calibrate hand
            if (index_Valid || middle_Valid || ring_Valid || little_Valid)
            {
                _BoxHandFit(hand_L_Collider_T, hand_L_Bone_T, hand_L_Collider, false);
                _BoxHandFit(hand_R_Collider_T, hand_R_Bone_T, hand_R_Collider, true);

                float Radiuscache = finger_radius * scaleFactor;

                if (index_Valid)
                {
                    index_L_Bone_T.SetPositionAndRotation(_userApi.GetBonePosition(HumanBodyBones.LeftIndexProximal), Quaternion.LookRotation(_userApi.GetBonePosition(index_L_Furthest) - _userApi.GetBonePosition(HumanBodyBones.LeftIndexProximal), _userApi.GetBoneRotation(HumanBodyBones.LeftIndexProximal) * Vector3.right));
                    index_R_Bone_T.SetPositionAndRotation(_userApi.GetBonePosition(HumanBodyBones.RightIndexProximal), Quaternion.LookRotation(_userApi.GetBonePosition(index_R_Furthest) - _userApi.GetBonePosition(HumanBodyBones.RightIndexProximal), _userApi.GetBoneRotation(HumanBodyBones.RightIndexProximal) * Vector3.right));

                    _Encapsulate(index_L_Collider_T, index_L_Collider, index_L_Bone_T.position, _userApi.GetBonePosition(index_L_Furthest) - index_L_Bone_T.position, index_L_Bone_T.right, Radiuscache);
                    _Encapsulate(index_R_Collider_T, index_R_Collider, index_R_Bone_T.position, _userApi.GetBonePosition(index_R_Furthest) - index_R_Bone_T.position, index_R_Bone_T.right, Radiuscache);
                }

                if (middle_Valid)
                {
                    middle_L_Bone_T.SetPositionAndRotation(_userApi.GetBonePosition(HumanBodyBones.LeftMiddleProximal), Quaternion.LookRotation(_userApi.GetBonePosition(middle_L_Furthest) - _userApi.GetBonePosition(HumanBodyBones.LeftMiddleProximal), _userApi.GetBoneRotation(HumanBodyBones.LeftMiddleProximal) * Vector3.right));
                    middle_R_Bone_T.SetPositionAndRotation(_userApi.GetBonePosition(HumanBodyBones.RightMiddleProximal), Quaternion.LookRotation(_userApi.GetBonePosition(middle_R_Furthest) - _userApi.GetBonePosition(HumanBodyBones.RightMiddleProximal), _userApi.GetBoneRotation(HumanBodyBones.RightMiddleProximal) * Vector3.right));

                    _Encapsulate(middle_L_Collider_T, middle_L_Collider, middle_L_Bone_T.position, _userApi.GetBonePosition(middle_L_Furthest) - middle_L_Bone_T.position, middle_L_Bone_T.right, Radiuscache);
                    _Encapsulate(middle_R_Collider_T, middle_R_Collider, middle_R_Bone_T.position, _userApi.GetBonePosition(middle_R_Furthest) - middle_R_Bone_T.position, middle_R_Bone_T.right, Radiuscache);
                }

                if (ring_Valid)
                {
                    ring_L_Bone_T.SetPositionAndRotation(_userApi.GetBonePosition(HumanBodyBones.LeftRingProximal), Quaternion.LookRotation(_userApi.GetBonePosition(ring_L_Furthest) - _userApi.GetBonePosition(HumanBodyBones.LeftRingProximal), _userApi.GetBoneRotation(HumanBodyBones.LeftRingProximal) * Vector3.right));
                    ring_R_Bone_T.SetPositionAndRotation(_userApi.GetBonePosition(HumanBodyBones.RightRingProximal), Quaternion.LookRotation(_userApi.GetBonePosition(ring_R_Furthest) - _userApi.GetBonePosition(HumanBodyBones.RightRingProximal), _userApi.GetBoneRotation(HumanBodyBones.RightRingProximal) * Vector3.right));

                    _Encapsulate(ring_L_Collider_T, ring_L_Collider, ring_L_Bone_T.position, _userApi.GetBonePosition(ring_L_Furthest) - ring_L_Bone_T.position, ring_L_Bone_T.right, Radiuscache);
                    _Encapsulate(ring_R_Collider_T, ring_R_Collider, ring_R_Bone_T.position, _userApi.GetBonePosition(ring_R_Furthest) - ring_R_Bone_T.position, ring_R_Bone_T.right, Radiuscache);
                }

                if (little_Valid)
                {
                    little_L_Bone_T.SetPositionAndRotation(_userApi.GetBonePosition(HumanBodyBones.LeftLittleProximal), Quaternion.LookRotation(_userApi.GetBonePosition(little_L_Furthest) - _userApi.GetBonePosition(HumanBodyBones.LeftLittleProximal), _userApi.GetBoneRotation(HumanBodyBones.LeftLittleProximal) * Vector3.right));
                    little_R_Bone_T.SetPositionAndRotation(_userApi.GetBonePosition(HumanBodyBones.RightLittleProximal), Quaternion.LookRotation(_userApi.GetBonePosition(little_R_Furthest) - _userApi.GetBonePosition(HumanBodyBones.RightLittleProximal), _userApi.GetBoneRotation(HumanBodyBones.RightLittleProximal) * Vector3.right));

                    _Encapsulate(little_L_Collider_T, little_L_Collider, little_L_Bone_T.position, _userApi.GetBonePosition(little_L_Furthest) - little_L_Bone_T.position, little_L_Bone_T.right, Radiuscache);
                    _Encapsulate(little_R_Collider_T, little_R_Collider, little_R_Bone_T.position, _userApi.GetBonePosition(little_R_Furthest) - little_R_Bone_T.position, little_R_Bone_T.right, Radiuscache);
                }
            }
            else
            {
                _BoxHand(hand_L_Collider_T, hand_L_Collider, scaleFactor);
                _BoxHand(hand_R_Collider_T, hand_R_Collider, scaleFactor);
            }

            _avatarCalibrated = true;

            if (_visualizerState) VisualizeColliders(true);
        }
        //dissable all colliders
        private void _SetCollidersEnabled(bool state)
        {

            index_L_Collider.enabled = state;
            index_R_Collider.enabled = state;

            middle_L_Collider.enabled = state;
            middle_R_Collider.enabled = state;

            ring_L_Collider.enabled = state;
            ring_R_Collider.enabled = state;

            little_L_Collider.enabled = state;
            little_R_Collider.enabled = state;

            hand_L_Collider.enabled = state;
            hand_R_Collider.enabled = state;

            upperArm_L_Collider.enabled = state;
            lowerArm_L_Collider.enabled = state;

            upperArm_R_Collider.enabled = state;
            lowerArm_R_Collider.enabled = state;

            upperLeg_L_Collider.enabled = state;
            lowerLeg_L_Collider.enabled = state;
            foot_L_Collider.enabled = state;

            upperLeg_R_Collider.enabled = state;
            lowerLeg_R_Collider.enabled = state;
            foot_R_Collider.enabled = state;

            spine_Collider.enabled = state;
            chest_Collider.enabled = state;

            head_Collider.enabled = state;
        }
        private void _UpdateLimbStates()
        {
            hand_L_Bone.gameObject.SetActive(humanoidValid && _handColliderEnable);
            hand_R_Bone.gameObject.SetActive(humanoidValid && _handColliderEnable);

            upperArm_L_Bone.gameObject.SetActive(humanoidValid && _armColliderEnable);
            lowerArm_L_Bone.gameObject.SetActive(humanoidValid && _armColliderEnable);

            upperArm_R_Bone.gameObject.SetActive(humanoidValid && _armColliderEnable);
            lowerArm_R_Bone.gameObject.SetActive(humanoidValid && _armColliderEnable);

            upperLeg_L_Bone.gameObject.SetActive(humanoidValid && _legColliderEnable);
            lowerLeg_L_Bone.gameObject.SetActive(humanoidValid && _legColliderEnable);
            foot_L_Bone.gameObject.SetActive(humanoidValid && _legColliderEnable);

            upperLeg_R_Bone.gameObject.SetActive(humanoidValid && _legColliderEnable);
            lowerLeg_R_Bone.gameObject.SetActive(humanoidValid && _legColliderEnable);
            foot_R_Bone.gameObject.SetActive(humanoidValid && _legColliderEnable);

            spine_Bone.gameObject.SetActive(humanoidValid && _torsoColliderEnable);
            chest_Bone.gameObject.SetActive(humanoidValid && _torsoColliderEnable);

            head_Bone.gameObject.SetActive(humanoidValid && _headColliderEnable);

            index_L_Bone.gameObject.SetActive(index_Valid && _fingerColliderEnable);
            index_R_Bone.gameObject.SetActive(index_Valid && _fingerColliderEnable);

            middle_L_Bone.gameObject.SetActive(middle_Valid && _fingerColliderEnable);
            middle_R_Bone.gameObject.SetActive(middle_Valid && _fingerColliderEnable);

            ring_L_Bone.gameObject.SetActive(ring_Valid && _fingerColliderEnable);
            ring_R_Bone.gameObject.SetActive(ring_Valid && _fingerColliderEnable);

            little_L_Bone.gameObject.SetActive(little_Valid && _fingerColliderEnable);
            little_R_Bone.gameObject.SetActive(little_Valid && _fingerColliderEnable);
        }
        [System.Obsolete("should never be called externally")]
        public void _DelayedHeightChange()
        {
            _delayHeightChangeState = false;
            if (isActiveAndEnabled)
            {
                if (!_delaySetHeight) return;
                _CalibrateToAvatar();
            }

            _delaySetHeight = false;
        }
        //sizes a capsule collider to cover two points 
        private void _Encapsulate(Transform colliderT, CapsuleCollider collider, Vector3 startPoint, Vector3 relativeVector, Vector3 upDirection, float capRadius)
        {
            colliderT.SetPositionAndRotation(startPoint + (relativeVector * 0.5f), Quaternion.LookRotation(relativeVector, upDirection));
            collider.height = relativeVector.magnitude + (capRadius * 2);
            collider.radius = Mathf.Clamp(capRadius, 0, collider.height * 0.5f);
        }
        //sizes a capsule collider to cover the starting point to the destination
        private void _EncapsulateToPoint(Transform colliderT, CapsuleCollider collider, Vector3 startPoint, Vector3 relativeVector, Vector3 upDirection, float capRadius)
        {
            float vectorMagnitude = relativeVector.magnitude;
            colliderT.SetPositionAndRotation(startPoint + (relativeVector.normalized * ((vectorMagnitude - capRadius) * .5f)), Quaternion.LookRotation(relativeVector, upDirection));
            collider.height = vectorMagnitude + capRadius;
            collider.radius = Mathf.Clamp(capRadius, 0, collider.height * 0.5f);
        }
        //sizes a box to your hand/palm
        private void _BoxHandFit(Transform handcolliderT, Transform handT, BoxCollider handcollider, bool rightHand)
        {
            Vector3 finger1 = _userApi.GetBonePosition(index_Valid ? (rightHand ? HumanBodyBones.RightIndexProximal : HumanBodyBones.LeftIndexProximal) : (middle_Valid ? (rightHand ? HumanBodyBones.RightMiddleProximal : HumanBodyBones.LeftMiddleProximal) : (ring_Valid ? (rightHand ? HumanBodyBones.RightRingProximal : HumanBodyBones.LeftRingProximal) : (little_Valid ? (rightHand ? HumanBodyBones.RightLittleProximal : HumanBodyBones.LeftLittleProximal) : (rightHand ? HumanBodyBones.RightHand : HumanBodyBones.LeftHand))))) - handT.position;
            Vector3 finger2 = _userApi.GetBonePosition(little_Valid ? (rightHand ? HumanBodyBones.RightLittleProximal : HumanBodyBones.LeftLittleProximal) : (ring_Valid ? (rightHand ? HumanBodyBones.RightRingProximal : HumanBodyBones.LeftRingProximal) : (middle_Valid ? (rightHand ? HumanBodyBones.RightMiddleProximal : HumanBodyBones.LeftMiddleProximal) : (index_Valid ? (rightHand ? HumanBodyBones.RightIndexProximal : HumanBodyBones.LeftIndexProximal) : (rightHand ? HumanBodyBones.RightHand : HumanBodyBones.LeftHand))))) - handT.position;

            handcolliderT.localPosition = Vector3.zero;
            handcolliderT.rotation = Quaternion.LookRotation(Vector3.Slerp(finger1, finger2, .5f), Vector3.Cross(finger1, finger2));

            Vector3[] handpoints = new Vector3[5];
            handpoints[0] = handT.position;
            handpoints[1] = index_Valid ? (rightHand ? _userApi.GetBonePosition(HumanBodyBones.RightIndexProximal) : _userApi.GetBonePosition(HumanBodyBones.LeftIndexProximal)) : handpoints[0];
            handpoints[2] = middle_Valid ? (rightHand ? _userApi.GetBonePosition(HumanBodyBones.RightMiddleProximal) : _userApi.GetBonePosition(HumanBodyBones.LeftMiddleProximal)) : handpoints[0];
            handpoints[3] = ring_Valid ? (rightHand ? _userApi.GetBonePosition(HumanBodyBones.RightRingProximal) : _userApi.GetBonePosition(HumanBodyBones.LeftRingProximal)) : handpoints[0];
            handpoints[4] = little_Valid ? (rightHand ? _userApi.GetBonePosition(HumanBodyBones.RightLittleProximal) : _userApi.GetBonePosition(HumanBodyBones.LeftLittleProximal)) : handpoints[0];

            Bounds bounds = GeometryUtility.CalculateBounds(handpoints, handcolliderT.worldToLocalMatrix);
            handcollider.center = bounds.center;
            handcollider.size = new Vector3(bounds.size.x * 1.33333f, bounds.size.y + (finger_radius * 2f * (_userApi.GetAvatarEyeHeightAsMeters() / averageEyeHeight)), bounds.size.z);
        }
        //makes a cube around your hand bone if there are no fingers.
        private void _BoxHand(Transform colliderT, BoxCollider collider, float scaleFactor)
        {
            colliderT.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            collider.center = Vector3.zero;
            float size = (lowerArm_radius * 2f) * scaleFactor;
            collider.size = new Vector3(size, size, size);
        }

        #endregion Setup Colliders

        #region Move Colliders
        private void _UpdatePose()
        {
            if (!_avatarCalibrated || !Utilities.IsValid(_userApi)) return;

            if (_headColliderEnable) head_Bone.Move(_userApi.GetBonePosition(HumanBodyBones.Head), _userApi.GetBoneRotation(HumanBodyBones.Head));
            Vector3 tempposition1;
            Vector3 tempposition2;

            if (_torsoColliderEnable)
            {
                tempposition1 = _userApi.GetBonePosition(HumanBodyBones.Hips);
                tempposition2 = _userApi.GetBonePosition(HumanBodyBones.Chest);
                spine_Bone.Move(tempposition1, Quaternion.LookRotation(tempposition2 - tempposition1, _userApi.GetBoneRotation(HumanBodyBones.Hips) * Vector3.right));
                chest_Bone.Move(tempposition2, Quaternion.LookRotation(_userApi.GetBonePosition(HumanBodyBones.Neck) - tempposition2, _userApi.GetBoneRotation(HumanBodyBones.Chest) * Vector3.right));
            }

            if (_legColliderEnable)
            {
                upperLeg_L_Bone.Move(_userApi.GetBonePosition(HumanBodyBones.LeftUpperLeg), _userApi.GetBoneRotation(HumanBodyBones.LeftUpperLeg));
                lowerLeg_L_Bone.Move(_userApi.GetBonePosition(HumanBodyBones.LeftLowerLeg), _userApi.GetBoneRotation(HumanBodyBones.LeftLowerLeg));
                foot_L_Bone.Move(_userApi.GetBonePosition(HumanBodyBones.LeftFoot), _userApi.GetBoneRotation(HumanBodyBones.LeftFoot));

                upperLeg_R_Bone.Move(_userApi.GetBonePosition(HumanBodyBones.RightUpperLeg), _userApi.GetBoneRotation(HumanBodyBones.RightUpperLeg));
                lowerLeg_R_Bone.Move(_userApi.GetBonePosition(HumanBodyBones.RightLowerLeg), _userApi.GetBoneRotation(HumanBodyBones.RightLowerLeg));
                foot_R_Bone.Move(_userApi.GetBonePosition(HumanBodyBones.RightFoot), _userApi.GetBoneRotation(HumanBodyBones.RightFoot));
            }

            if (_armColliderEnable)
            {
                upperArm_L_Bone.Move(_userApi.GetBonePosition(HumanBodyBones.LeftUpperArm), _userApi.GetBoneRotation(HumanBodyBones.LeftUpperArm));
                lowerArm_L_Bone.Move(_userApi.GetBonePosition(HumanBodyBones.LeftLowerArm), _userApi.GetBoneRotation(HumanBodyBones.LeftLowerArm));

                upperArm_R_Bone.Move(_userApi.GetBonePosition(HumanBodyBones.RightUpperArm), _userApi.GetBoneRotation(HumanBodyBones.RightUpperArm));
                lowerArm_R_Bone.Move(_userApi.GetBonePosition(HumanBodyBones.RightLowerArm), _userApi.GetBoneRotation(HumanBodyBones.RightLowerArm));
            }

            if (_handColliderEnable)
            {
                hand_L_Bone.Move(_userApi.GetBonePosition(HumanBodyBones.LeftHand), _userApi.GetBoneRotation(HumanBodyBones.LeftHand));
                hand_R_Bone.Move(_userApi.GetBonePosition(HumanBodyBones.RightHand), _userApi.GetBoneRotation(HumanBodyBones.RightHand));
            }

            if (_fingerColliderEnable)
            {
                if (index_Valid)
                {
                    tempposition1 = _userApi.GetBonePosition(HumanBodyBones.LeftIndexProximal);
                    index_L_Bone.Move(tempposition1, Quaternion.LookRotation(_userApi.GetBonePosition(index_L_Furthest) - tempposition1, _userApi.GetBoneRotation(HumanBodyBones.LeftIndexProximal) * Vector3.right));

                    tempposition1 = _userApi.GetBonePosition(HumanBodyBones.RightIndexProximal);
                    index_R_Bone.Move(tempposition1, Quaternion.LookRotation(_userApi.GetBonePosition(index_R_Furthest) - tempposition1, _userApi.GetBoneRotation(HumanBodyBones.RightIndexProximal) * Vector3.right));
                }

                if (middle_Valid)
                {
                    tempposition1 = _userApi.GetBonePosition(HumanBodyBones.LeftMiddleProximal);
                    middle_L_Bone.Move(tempposition1, Quaternion.LookRotation(_userApi.GetBonePosition(middle_L_Furthest) - tempposition1, _userApi.GetBoneRotation(HumanBodyBones.LeftMiddleProximal) * Vector3.right));

                    tempposition1 = _userApi.GetBonePosition(HumanBodyBones.RightMiddleProximal);
                    middle_R_Bone.Move(tempposition1, Quaternion.LookRotation(_userApi.GetBonePosition(middle_R_Furthest) - tempposition1, _userApi.GetBoneRotation(HumanBodyBones.RightMiddleProximal) * Vector3.right));
                }

                if (ring_Valid)
                {
                    tempposition1 = _userApi.GetBonePosition(HumanBodyBones.LeftRingProximal);
                    ring_L_Bone.Move(tempposition1, Quaternion.LookRotation(_userApi.GetBonePosition(ring_L_Furthest) - tempposition1, _userApi.GetBoneRotation(HumanBodyBones.LeftRingProximal) * Vector3.right));

                    tempposition1 = _userApi.GetBonePosition(HumanBodyBones.RightRingProximal);
                    ring_R_Bone.Move(tempposition1, Quaternion.LookRotation(_userApi.GetBonePosition(ring_R_Furthest) - tempposition1, _userApi.GetBoneRotation(HumanBodyBones.RightRingProximal) * Vector3.right));
                }

                if (little_Valid)
                {
                    tempposition1 = _userApi.GetBonePosition(HumanBodyBones.LeftLittleProximal);
                    little_L_Bone.Move(tempposition1, Quaternion.LookRotation(_userApi.GetBonePosition(little_L_Furthest) - tempposition1, _userApi.GetBoneRotation(HumanBodyBones.LeftLittleProximal) * Vector3.right));

                    tempposition1 = _userApi.GetBonePosition(HumanBodyBones.RightLittleProximal);
                    little_R_Bone.Move(tempposition1, Quaternion.LookRotation(_userApi.GetBonePosition(little_R_Furthest) - tempposition1, _userApi.GetBoneRotation(HumanBodyBones.RightLittleProximal) * Vector3.right));
                }
            }

        }
        private void _TeleportPose()
        {
            if (!_avatarCalibrated || !Utilities.IsValid(_userApi)) return;

            if (_headColliderEnable)
            {
                head_Bone.position = _userApi.GetBonePosition(HumanBodyBones.Head);
                head_Bone.rotation = _userApi.GetBoneRotation(HumanBodyBones.Head);
            }
            Vector3 tempposition1;
            Vector3 tempposition2;

            if (_torsoColliderEnable)
            {
                tempposition1 = _userApi.GetBonePosition(HumanBodyBones.Hips);
                tempposition2 = _userApi.GetBonePosition(HumanBodyBones.Chest);
                spine_Bone.position = tempposition1;
                spine_Bone.rotation = Quaternion.LookRotation(tempposition2 - tempposition1, _userApi.GetBoneRotation(HumanBodyBones.Hips) * Vector3.right);
                chest_Bone.position = tempposition2;
                chest_Bone.rotation = Quaternion.LookRotation(_userApi.GetBonePosition(HumanBodyBones.Neck) - tempposition2, _userApi.GetBoneRotation(HumanBodyBones.Chest) * Vector3.right);
            }

            if (_legColliderEnable)
            {
                upperLeg_L_Bone.position = _userApi.GetBonePosition(HumanBodyBones.LeftUpperLeg);
                upperLeg_L_Bone.rotation = _userApi.GetBoneRotation(HumanBodyBones.LeftUpperLeg);
                lowerLeg_L_Bone.position = _userApi.GetBonePosition(HumanBodyBones.LeftLowerLeg);
                lowerLeg_L_Bone.rotation = _userApi.GetBoneRotation(HumanBodyBones.LeftLowerLeg);
                foot_L_Bone.position = _userApi.GetBonePosition(HumanBodyBones.LeftFoot);
                foot_L_Bone.rotation = _userApi.GetBoneRotation(HumanBodyBones.LeftFoot);

                upperLeg_R_Bone.position = _userApi.GetBonePosition(HumanBodyBones.RightUpperLeg);
                upperLeg_R_Bone.rotation = _userApi.GetBoneRotation(HumanBodyBones.RightUpperLeg);
                lowerLeg_R_Bone.position = _userApi.GetBonePosition(HumanBodyBones.RightLowerLeg);
                lowerLeg_R_Bone.rotation = _userApi.GetBoneRotation(HumanBodyBones.RightLowerLeg);
                foot_R_Bone.position = _userApi.GetBonePosition(HumanBodyBones.RightFoot);
                foot_R_Bone.rotation = _userApi.GetBoneRotation(HumanBodyBones.RightFoot);
            }

            if (_armColliderEnable)
            {
                upperArm_L_Bone.position = _userApi.GetBonePosition(HumanBodyBones.LeftUpperArm);
                upperArm_L_Bone.rotation = _userApi.GetBoneRotation(HumanBodyBones.LeftUpperArm);
                lowerArm_L_Bone.position = _userApi.GetBonePosition(HumanBodyBones.LeftLowerArm);
                lowerArm_L_Bone.rotation = _userApi.GetBoneRotation(HumanBodyBones.LeftLowerArm);

                upperArm_R_Bone.position = _userApi.GetBonePosition(HumanBodyBones.RightUpperArm);
                upperArm_R_Bone.rotation = _userApi.GetBoneRotation(HumanBodyBones.RightUpperArm);
                lowerArm_R_Bone.position = _userApi.GetBonePosition(HumanBodyBones.RightLowerArm);
                lowerArm_R_Bone.rotation = _userApi.GetBoneRotation(HumanBodyBones.RightLowerArm);
            }

            if (_handColliderEnable)
            {
                hand_L_Bone.position = _userApi.GetBonePosition(HumanBodyBones.LeftHand);
                hand_L_Bone.rotation = _userApi.GetBoneRotation(HumanBodyBones.LeftHand);

                hand_R_Bone.position = _userApi.GetBonePosition(HumanBodyBones.RightHand);
                hand_R_Bone.rotation = _userApi.GetBoneRotation(HumanBodyBones.RightHand);
            }

            if (_fingerColliderEnable)
            {
                if (index_Valid)
                {
                    tempposition1 = _userApi.GetBonePosition(HumanBodyBones.LeftIndexProximal);
                    index_L_Bone.position = tempposition1;
                    index_L_Bone.rotation = Quaternion.LookRotation(_userApi.GetBonePosition(index_L_Furthest) - tempposition1, _userApi.GetBoneRotation(HumanBodyBones.LeftIndexProximal) * Vector3.right);

                    tempposition1 = _userApi.GetBonePosition(HumanBodyBones.RightIndexProximal);
                    index_R_Bone.position = tempposition1;
                    index_R_Bone.rotation = Quaternion.LookRotation(_userApi.GetBonePosition(index_R_Furthest) - tempposition1, _userApi.GetBoneRotation(HumanBodyBones.RightIndexProximal) * Vector3.right);
                }

                if (middle_Valid)
                {
                    tempposition1 = _userApi.GetBonePosition(HumanBodyBones.LeftMiddleProximal);
                    middle_L_Bone.position = tempposition1;
                    middle_L_Bone.rotation = Quaternion.LookRotation(_userApi.GetBonePosition(middle_L_Furthest) - tempposition1, _userApi.GetBoneRotation(HumanBodyBones.LeftMiddleProximal) * Vector3.right);

                    tempposition1 = _userApi.GetBonePosition(HumanBodyBones.RightMiddleProximal);
                    middle_R_Bone.position = tempposition1;
                    middle_R_Bone.rotation = Quaternion.LookRotation(_userApi.GetBonePosition(middle_R_Furthest) - tempposition1, _userApi.GetBoneRotation(HumanBodyBones.RightMiddleProximal) * Vector3.right);
                }

                if (ring_Valid)
                {
                    tempposition1 = _userApi.GetBonePosition(HumanBodyBones.LeftRingProximal);
                    ring_L_Bone.position = tempposition1;
                    ring_L_Bone.rotation = Quaternion.LookRotation(_userApi.GetBonePosition(ring_L_Furthest) - tempposition1, _userApi.GetBoneRotation(HumanBodyBones.LeftRingProximal) * Vector3.right);

                    tempposition1 = _userApi.GetBonePosition(HumanBodyBones.RightRingProximal);
                    ring_R_Bone.position = tempposition1;
                    ring_R_Bone.rotation = Quaternion.LookRotation(_userApi.GetBonePosition(ring_R_Furthest) - tempposition1, _userApi.GetBoneRotation(HumanBodyBones.RightRingProximal) * Vector3.right);
                }

                if (little_Valid)
                {
                    tempposition1 = _userApi.GetBonePosition(HumanBodyBones.LeftLittleProximal);
                    little_L_Bone.position = tempposition1;
                    little_L_Bone.rotation = Quaternion.LookRotation(_userApi.GetBonePosition(little_L_Furthest) - tempposition1, _userApi.GetBoneRotation(HumanBodyBones.LeftLittleProximal) * Vector3.right);

                    tempposition1 = _userApi.GetBonePosition(HumanBodyBones.RightLittleProximal);
                    little_R_Bone.position = tempposition1;
                    little_R_Bone.rotation = Quaternion.LookRotation(_userApi.GetBonePosition(little_R_Furthest) - tempposition1, _userApi.GetBoneRotation(HumanBodyBones.RightLittleProximal) * Vector3.right);
                }
            }

            if (_detectCollisions)
            {
                index_L_Bone.detectCollisions = true;
                index_R_Bone.detectCollisions = true;

                middle_L_Bone.detectCollisions = true;
                middle_R_Bone.detectCollisions = true;

                ring_L_Bone.detectCollisions = true;
                ring_R_Bone.detectCollisions = true;

                little_L_Bone.detectCollisions = true;
                little_R_Bone.detectCollisions = true;

                hand_L_Bone.detectCollisions = true;
                hand_R_Bone.detectCollisions = true;

                upperArm_L_Bone.detectCollisions = true;
                lowerArm_L_Bone.detectCollisions = true;

                upperArm_R_Bone.detectCollisions = true;
                lowerArm_R_Bone.detectCollisions = true;

                upperLeg_L_Bone.detectCollisions = true;
                lowerLeg_L_Bone.detectCollisions = true;
                foot_L_Bone.detectCollisions = true;

                upperLeg_R_Bone.detectCollisions = true;
                lowerLeg_R_Bone.detectCollisions = true;
                foot_R_Bone.detectCollisions = true;

                spine_Bone.detectCollisions = true;
                chest_Bone.detectCollisions = true;
                head_Bone.detectCollisions = true;
            }

            _teleportCollider = false;

        }
        #endregion Move Colliders

        #endregion Methods
    }
}