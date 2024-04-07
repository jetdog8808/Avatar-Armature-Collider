using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace JetDog.UserCollider
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class AvatarCollider : UdonSharpBehaviour
    {
        [SerializeField]
        private HumanBodyBones _bone;
        [SerializeField]
        private AvatarArmatureColliderSystem _colliderRoot;

        public HumanBodyBones Bone { get => _bone; }
        public AvatarArmatureColliderSystem ColliderRoot { get => _colliderRoot; }
        public VRCPlayerApi User { get => _colliderRoot.UserApi; }

        private void Start()
        {
            if(_colliderRoot == null)
            {
                _colliderRoot = GetComponentInParent<AvatarArmatureColliderSystem>();
            }
        }
    }
}