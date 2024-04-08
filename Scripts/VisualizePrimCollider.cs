using UdonSharp;
using UnityEngine;

namespace JetDog.UserCollider
{
    [RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshFilter)), UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class VisualizePrimCollider : UdonSharpBehaviour
    {
        public bool IsOn
        {
            get => _isOn;
        }

        private MeshFilter _meshFilterRef;
        private MeshRenderer _meshRendererRef;
        private Collider _primCollider;

        [SerializeField]
        private Mesh capsulePrim;
        [SerializeField]
        private Mesh boxPrim;
        [SerializeField]
        private Mesh spherePrim;
        private Mesh visualMesh;
        //can replace with onawake
        private bool initalized = false;
        private bool retry = false;

        private bool _isOn = false;

        void Start()
        {
            _meshFilterRef = GetComponent<MeshFilter>();
            _meshRendererRef = GetComponent<MeshRenderer>();
            _primCollider = GetComponent<Collider>();

            _meshRendererRef.enabled = false;
            visualMesh = new Mesh();
            _meshFilterRef.mesh = visualMesh;

            initalized = true;
            if (retry) VisualizeCollider(_isOn);
        }

        public void VisualizeCollider(bool state)
        {
            if (!initalized)
            {
                retry = true;
                _isOn = state;
                return;
            }

            if (_primCollider == null)
            {
                Debug.LogWarning("collider not found");
                _isOn = false;
                _meshRendererRef.enabled = false;
                return;
            }

            if (visualMesh == null)
            {
                visualMesh = new Mesh();
            }

            _meshFilterRef.mesh = visualMesh;
            _isOn = state;
            _meshRendererRef.enabled = state;

            if (state)
            {
                switch (_primCollider.GetType().Name)
                {
                    case nameof(BoxCollider):
                        CreateBoxMesh((BoxCollider)_primCollider);
                        break;
                    case nameof(SphereCollider):
                        CreateSphereMesh((SphereCollider)_primCollider);
                        break;
                    case nameof(CapsuleCollider):
                        CreateCapsuleMesh((CapsuleCollider)_primCollider);
                        break;
                    case nameof(MeshCollider):
                        _meshFilterRef.mesh = ((MeshCollider)_primCollider).sharedMesh;
                        break;
                    default:
                        _isOn = false;
                        _meshRendererRef.enabled = false;
                        return;
                }
            }

            //not working correctly in udon
            /*
            switch (primCollider)
            {
                case BoxCollider b:
                    Debug.Log("was a box");
                    meshFilterRef.mesh = CreateBoxMesh(b);
                    break;
                case SphereCollider s:
                    Debug.Log("was a sphere");
                    meshFilterRef.mesh = CreateSphereMesh(s);
                    break;
                case CapsuleCollider c:
                    Debug.Log("was a capsule");
                    meshFilterRef.mesh = CreateCapsuleMesh(c);
                    break;
                case MeshCollider m:
                    meshFilterRef.mesh = m.sharedMesh;
                    break;
            }*/
        }

        private void CreateBoxMesh(BoxCollider box)
        {
            CopyMesh(boxPrim);
            Vector3[] verts = visualMesh.vertices;
            for (var i = 0; i < verts.Length; i++)
            {
                verts[i] = Vector3.Scale(verts[i], box.size) + box.center;
            }
            visualMesh.vertices = verts;

            visualMesh.RecalculateBounds();
        }

        private void CreateSphereMesh(SphereCollider sphere)
        {
            CopyMesh(spherePrim);
            Vector3[] verts = visualMesh.vertices;
            for (var i = 0; i < verts.Length; i++)
            {
                verts[i] = (verts[i] * (sphere.radius / 0.5f)) + sphere.center;
            }
            visualMesh.vertices = verts;

            visualMesh.RecalculateBounds();
        }

        private void CreateCapsuleMesh(CapsuleCollider capsule)
        {
            CopyMesh(capsulePrim);
            Vector3[] verts = visualMesh.vertices;
            float radiusScale = capsule.radius / 0.5f;
            for (var i = 0; i < verts.Length; i++)
            {
                Vector3 temppos = (verts[i] * radiusScale) + (new Vector3(0f, (Mathf.Sign(verts[i].y) * ((Mathf.Max(capsule.radius * 2f, capsule.height) - (2f * radiusScale)) * .5f)), 0f));
                switch (capsule.direction)
                {
                    case 0:
                        temppos = Quaternion.Euler(0f, 0f, 90f) * temppos;
                        break;
                    case 2:
                        temppos = Quaternion.Euler(90f, 0f, 0f) * temppos;
                        break;
                }
                verts[i] = temppos + capsule.center;
            }
            visualMesh.vertices = verts;
            visualMesh.RecalculateNormals();
            visualMesh.RecalculateTangents();
            visualMesh.RecalculateBounds();
        }

        private void CopyMesh(Mesh meshCopy)
        {
            visualMesh.vertices = meshCopy.vertices;
            visualMesh.triangles = meshCopy.triangles;
            visualMesh.normals = meshCopy.normals;
            visualMesh.tangents = meshCopy.tangents;
            visualMesh.uv = meshCopy.uv;
        }
    }
}