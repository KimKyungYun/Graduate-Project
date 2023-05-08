using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Splines;
namespace ModularExtrusionsMachines
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(SplineContainer))]
    public class CableDrawer : MonoBehaviour
    {
        public GameObject endGo;
        private Mesh _mesh;
        private MeshFilter meshFilter;
        public float normalIntensity = 1f;
        private Vector3 endGoLastPos;
        private Vector3 lastPos;
        private SplineContainer splineC;

        public float cableRadius = 0.002f;
        public int cableSides = 6;
        public int cableSegments = 20;
        public bool cableCapped = true;
        public bool alwaysUpdate = false;
        void Start()
        {
            Init();
            RefreshCurve();
            if (meshFilter.sharedMesh == null || _mesh == null)
                _mesh = new Mesh();
            meshFilter.sharedMesh = _mesh;
        }

        private void Init()
        {
            if (endGo == null)
                return;
            meshFilter = GetComponent<MeshFilter>();
            splineC = GetComponent<SplineContainer>();
            if (splineC.Splines.Count == 0)
                splineC.AddSpline();
        }

        void Update()
        {
            if (endGo == null)
                return;
            if (endGoLastPos != endGo.transform.position || lastPos != transform.position || alwaysUpdate == true)
            {
                endGoLastPos = endGo.transform.position;
                lastPos = transform.position;
                RefreshCurve();
                SplineMesh.Extrude(splineC.Spline, _mesh, cableRadius, cableSides, cableSegments, cableCapped);
            }

        }
        private void RefreshCurve()
        {
            if (splineC.Splines.Count == 0)
                splineC.AddSpline();
            splineC.Spline.Clear();
            splineC.Spline.Knots = new BezierKnot[2];
            splineC.Spline.SetKnot(0, new BezierKnot(Vector3.zero, Vector3.forward * 0.1f * normalIntensity, Vector3.forward * 0.1f * normalIntensity));
            splineC.Spline.SetKnot(1, new BezierKnot(transform.InverseTransformPoint(endGo.transform.position), transform.InverseTransformDirection(endGo.transform.forward) * 0.1f * normalIntensity, transform.InverseTransformDirection(endGo.transform.forward) * 0.1f * normalIntensity));
        }

        void OnDrawGizmos()
        {
            if (!Application.isPlaying && endGo != null)
            {
                Init();
                if (splineC == null)
                    splineC = GetComponent<SplineContainer>();
                RefreshCurve();
                if (endGoLastPos != endGo.transform.position || lastPos != transform.position || meshFilter.sharedMesh == null || _mesh == null || alwaysUpdate == true)
                {
                    _mesh = new Mesh();
                    meshFilter.sharedMesh = _mesh;
                    endGoLastPos = endGo.transform.position;
                    lastPos = transform.position;
                    SplineMesh.Extrude(splineC.Spline, _mesh, cableRadius, cableSides, cableSegments, cableCapped);
                }
            }
        }
    }

}
