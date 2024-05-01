using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Components;

namespace JetDog.UserCollider
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class CollisionOwnershipTransfer : UdonSharpBehaviour
    {
        private VRCPlayerApi localPlayer;

        private void Start()
        {
            localPlayer = Networking.LocalPlayer;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!enabled || collision.rigidbody == null) return;

            VRCObjectSync objSync = (VRCObjectSync)collision.rigidbody.GetComponent(typeof(VRCObjectSync));
            if (objSync == null || !objSync.AllowCollisionOwnershipTransfer) return;

            GameObject collidedGOBJ = objSync.gameObject;
            if (Networking.IsOwner(collidedGOBJ)) return;

            VRCPickup pickup = (VRCPickup)objSync.GetComponent(typeof(VRCPickup));
            if (pickup != null && pickup.IsHeld) return;

            Networking.SetOwner(localPlayer, collidedGOBJ);
        }
    }
}
