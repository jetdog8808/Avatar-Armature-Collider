﻿using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.SDK3.Data;
using JetBrains.Annotations;

namespace JetDog.UserCollider
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class AvatarArmatureColliderManager : UdonSharpBehaviour
    {
        #region Fields
        [SerializeField]
        private AvatarArmatureColliderSystem prefabRef;

        [SerializeField]
        private bool _remoteCollidersEnabled = true,
            _localCollidersEnabled = true;

        [SerializeField]
        private bool remoteIsTrigger = false,
            localIsTrigger = false;

        [SerializeField]
        //collider layers 10 is local, 9 is remote.
        private int localLayer = 10,
            remoteLayer = 9;

        [SerializeField]
        private LayerMask remoteIncludeLayers,
            localIncludeLayers,
            remoteExcludeLayers,
            localExcludeLayers;

        [SerializeField]
        private bool fingerCollision = false,
            handCollision = true,
            armCollision = true,
            legCollision = false,
            torsoCollision = true,
            headCollision = false;
        private DataDictionary colliderDictionary = new DataDictionary();
        private DataList UserIdList = new DataList();
        private VRCPlayerApi localuser;
        private AvatarArmatureColliderSystem localCollider;
        [SerializeField]
        private Vector3 distanceFactors = new Vector3(2f, 8.5f, 15f);
        [SerializeField]
        private Vector3Int distanceUpdateRates = new Vector3Int(1, 3, 6);
        private int updateRemainder = 0;
        private bool visualizerIsOn = false;
        [SerializeField]
        private bool localCollisionTransferOwnership = true;
        #endregion Fields

        #region Properties
        /// <summary>
        /// Get state of Remote Colliders.
        /// </summary>
        [PublicAPI]
        public bool RemoteCollidersEnabled { get => _remoteCollidersEnabled; }
        /// <summary>
        /// Get state of Local Collider.
        /// </summary>
        [PublicAPI]
        public bool LocalCollidersEnabled { get => _localCollidersEnabled; }
        /// <summary>
        /// Reference of Instantiated Prefab.
        /// </summary>
        [PublicAPI]
        public AvatarArmatureColliderSystem PrefabRef { get => prefabRef; }
        /// <summary>
        /// Reference to Local Players Collider.
        /// </summary>
        [PublicAPI]
        public AvatarArmatureColliderSystem LocalCollider { get => localCollider; }

        #endregion Properties

        #region Methods

        #region Public Methods
        /// <summary>
        /// Sets the state of Remote Colliders.
        /// </summary>
        /// <param name="state">Enable State</param>
        [PublicAPI]
        public void SetRemoteCollidersActive(bool state)
        {
            _remoteCollidersEnabled = state;

            for (int i = 0; i < UserIdList.Count; i++)
            {
                if (!colliderDictionary.TryGetValue(UserIdList[i], TokenType.Reference, out DataToken colliderSystemRef)) continue;

                ((AvatarArmatureColliderSystem)colliderSystemRef.Reference).gameObject.SetActive(state);
            }

        }
        /// <summary>
        /// Sets the state of Local Collider.
        /// </summary>
        /// <param name="state">Enable State</param>
        [PublicAPI]
        public void SetLocalColliderActive(bool state)
        {
            _localCollidersEnabled = state;
            localCollider.gameObject.SetActive(state);
        }
        /// <summary>
        /// Sets state for debug visualizer of the colliders.
        /// </summary>
        /// <param name="state">Enable State</param>
        [PublicAPI]
        public void VisualizeAllColliders(bool state)
        {
            visualizerIsOn = state;

            for (int i = 0; i < UserIdList.Count; i++)
            {
                if (!colliderDictionary.TryGetValue(UserIdList[i], TokenType.Reference, out DataToken colliderSystemRef)) continue;

                ((AvatarArmatureColliderSystem)colliderSystemRef.Reference).VisualizeColliders(state);
            }

            localCollider.VisualizeColliders(state);
        }
        /// <summary>
        /// Toggles state of debug visualizer of the colliders.
        /// </summary>
        [PublicAPI]
        public void ToggleVisualizerColliders()
        {
            VisualizeAllColliders(!visualizerIsOn);
        }
        /// <summary>
        /// Method to get reference of players collider
        /// </summary>
        /// <param name="playerId">VRCPlayerApi of the player</param>
        /// <returns>Reference to users <see cref="AvatarArmatureColliderSystem"/></returns>
        [PublicAPI]
        public AvatarArmatureColliderSystem RemoteCollider(int playerId)
        {
            if (colliderDictionary.TryGetValue(new DataToken(playerId), TokenType.Reference, out DataToken colliderSystemRef))
            {
                return (AvatarArmatureColliderSystem)colliderSystemRef.Reference;
            }
            return null;
        }
        /// <summary>
        /// Gives array of all remote colliders.
        /// </summary>
        /// <returns>Array of <see cref="AvatarArmatureColliderSystem"/></returns>
        [PublicAPI]
        public AvatarArmatureColliderSystem[] RemoteColliders()
        {
            var list = new AvatarArmatureColliderSystem[UserIdList.Count];
            var j = 0;
            for (int i = 0; i < UserIdList.Count; i++)
            {
                if (!colliderDictionary.TryGetValue(UserIdList[i], TokenType.Reference, out DataToken colliderSystemRef)) continue;

                list[j] = (AvatarArmatureColliderSystem)colliderSystemRef.Reference;
                j++;
            }
            if (j == list.Length)
            {
                return list;
            }
            var resizedList = new AvatarArmatureColliderSystem[j];
            System.Array.Copy(list, resizedList, j);
            return resizedList;
        }
        #endregion Public Methods

        #region Built-in Events
        private void Start()
        {
            localuser = Networking.LocalPlayer;
            distanceFactors = Vector3.Scale(distanceFactors, distanceFactors);

            //start loop
            SendCustomEventDelayedSeconds(nameof(DistanceUpdateLoop), 0.05f);
        }
        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            AvatarArmatureColliderSystem newCollider = Instantiate(prefabRef.gameObject, Vector3.zero, Quaternion.identity).GetComponent<AvatarArmatureColliderSystem>();
            newCollider.SetUser(player);


            if (player.isLocal)
            {
                newCollider.gameObject.SetActive(_localCollidersEnabled);
                newCollider.ColliderLayer = localLayer;
                newCollider.CollisionTransferOwnership = localCollisionTransferOwnership;
                newCollider.ColliderIsTrigger = localIsTrigger;
                newCollider.IncludeLayers = localIncludeLayers;
                newCollider.ExcludeLayers = localExcludeLayers;
                localCollider = newCollider;
            }
            else
            {
                newCollider.gameObject.SetActive(_remoteCollidersEnabled);

                newCollider.ColliderLayer = remoteLayer;
                newCollider.ColliderIsTrigger = remoteIsTrigger;
                newCollider.IncludeLayers = remoteIncludeLayers;
                newCollider.ExcludeLayers = remoteExcludeLayers;

                colliderDictionary.Add(player.playerId, newCollider);
                UserIdList = colliderDictionary.GetKeys();

                ProximityUpdate(player);

                if (visualizerIsOn) newCollider.VisualizeColliders(true);
            }
        }
        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            if (player.isLocal) return;
            Destroy(((AvatarArmatureColliderSystem)colliderDictionary[player.playerId].Reference).gameObject);
            colliderDictionary.Remove(player.playerId);
            UserIdList = colliderDictionary.GetKeys();
        }
        #endregion Built-in Events

        #region Delayed Events
        //can be updated to coroutine or unitask in future
        [System.Obsolete("should never be called externally")]
        //updates 20 times per second (each collider will update 2 times per second)
        public void DistanceUpdateLoop()
        {
            for (int i = 0; i < UserIdList.Count; i++)
            {
                if (i % 10 == updateRemainder)
                {
                    ProximityUpdate(VRCPlayerApi.GetPlayerById(UserIdList[i].Int));
                }
            }

            if (++updateRemainder > 9)
            {
                updateRemainder = 0;
            }

            SendCustomEventDelayedSeconds(nameof(DistanceUpdateLoop), 0.05f);
        }
        #endregion Delayed Events
        //changes collider quality depending how close they are.
        private void ProximityUpdate(VRCPlayerApi player)
        {
            if (!colliderDictionary.TryGetValue(player.playerId, TokenType.Reference, out DataToken colliderSystemRef)) return;

            VRCPlayerApi.TrackingData cameraInfo = localuser.GetTrackingData(VRCPlayerApi.TrackingDataType.Head);
            Vector3 playerHips = player.GetBonePosition(HumanBodyBones.Hips);
            if (playerHips == Vector3.zero) playerHips = player.GetPosition();

            bool isInfront = Vector3.Dot(cameraInfo.rotation * Vector3.forward, (playerHips - cameraInfo.position).normalized) > 0.5f;
            float playerDistanceFactor = ((playerHips - localuser.GetPosition()).sqrMagnitude) / (Mathf.Pow(player.GetAvatarEyeHeightAsMeters(), 2f));

            AvatarArmatureColliderSystem colliderSystem = ((AvatarArmatureColliderSystem)colliderSystemRef.Reference);

            bool distanceCheck;
            int zone = 0;

            distanceCheck = playerDistanceFactor < (isInfront ? distanceFactors.x : distanceFactors.x * .6f);
            if (distanceCheck != colliderSystem.FingerColliderEnable)
            {
                colliderSystem.SetFingerColliderState(distanceCheck && fingerCollision);
            }
            zone += distanceCheck ? 0 : 1;

            distanceCheck = playerDistanceFactor < (isInfront ? distanceFactors.y : distanceFactors.y * .6f);
            if ((distanceCheck != colliderSystem.ArmColliderEnable) || (distanceCheck != colliderSystem.LegColliderEnable))
            {
                colliderSystem.SetArmColliderState(distanceCheck && armCollision);
                colliderSystem.SetLegColliderState(distanceCheck && legCollision);
            }
            zone += distanceCheck ? 0 : 1;

            distanceCheck = playerDistanceFactor < (isInfront ? distanceFactors.z : distanceFactors.z * .6f);
            if ((distanceCheck != colliderSystem.HeadColliderEnable) || (distanceCheck != colliderSystem.TorsoColliderEnable) || (distanceCheck != colliderSystem.HandColliderEnable))
            {
                colliderSystem.SetHandColliderState(distanceCheck && handCollision);
                colliderSystem.SetTorsoColliderState(distanceCheck && torsoCollision);
                colliderSystem.SetHeadColliderState(distanceCheck && headCollision);
            }

            switch (zone)
            {
                case 0:
                    colliderSystem.UpdateEveryNthFrame = distanceUpdateRates.x;
                    break;
                case 1:
                    colliderSystem.UpdateEveryNthFrame = distanceUpdateRates.y;
                    break;
                default:
                    colliderSystem.UpdateEveryNthFrame = distanceUpdateRates.z;
                    break;
            }
        }
        #endregion Methods
    }
}
