using System.Collections;
using System.Collections.Generic;
using UnityEditor;
//using UnityEditor.TerrainTools;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using ModularExtrusionsMachines;

namespace ModularExtrusionsMachines
{
    [RequireComponent(typeof(AudioSource))]
    public class DeltaXYZ : MonoBehaviour
    {

        public float armsLenght = 0.24f;
        public GameObject headObject;
        public GameObject headAnchor;
        public GameObject bedAnchor;
        [Space]
        public GameObject XHeadAxis;
        public GameObject XCarriageAxis;
        public GameObject XCarriage;
        [Space]
        public GameObject YHeadAxis;
        public GameObject YCarriageAxis;
        public GameObject YCarriage;
        [Space]
        public GameObject ZHeadAxis;
        public GameObject ZCarriageAxis;
        public GameObject ZCarriage;
        [Space]
        public float bedRadius = 0.2f;
        public float height = 0.2f;
        public float speed = 0.1f;

        public Vector3 targetPos = new Vector3(0, 0, 0);
        private bool onTarget = false;
        public Vector3[] targets = new Vector3[1];
        public int posIndex = 0;
        public bool dontUseList = false;

        public bool randomHorizontalTarget = false;
        public bool randomVerticalTarget = false;

        [SerializeField]
        List<ScrollMaterialMapper> scrollMats = new List<ScrollMaterialMapper>();

        private AudioSource audiSrco;

        [Range(0f, 2f)]
        public float averageNoisePitch = 0.8f;
        [Range(0f, 1f)]
        public float volume = 1;

        private Vector3 rightAngleX
        {
            get => XCarriageAxis.transform.position + transform.up * Vector3.Dot(transform.up, XHeadAxis.transform.position - XCarriageAxis.transform.position);
        }
        private Vector3 rightAngleY
        {
            get => YCarriageAxis.transform.position + transform.up * Vector3.Dot(transform.up, YHeadAxis.transform.position - YCarriageAxis.transform.position);
        }
        private Vector3 rightAngleZ
        {
            get => ZCarriageAxis.transform.position + transform.up * Vector3.Dot(transform.up, ZHeadAxis.transform.position - ZCarriageAxis.transform.position);
        }

        private float legX;
        private float legY;
        private float legZ;

        private bool XOnTarget;
        private bool YOnTarget;
        private bool ZOnTarget;

        void Start()
        {
            foreach (ScrollMaterialMapper mat in scrollMats)
                mat.Init();
            audiSrco = GetComponent<AudioSource>();
            audiSrco.clip = ExtrusionAssetResources.instance.StepperMotorSound;
            audiSrco.loop = true;
            audiSrco.playOnAwake = false;
            audiSrco.spatialBlend = 1f;
        }

        void Update()
        {
            if (onTarget)
            {
                audiSrco.Pause();
                XOnTarget = false;
                YOnTarget = false;
                ZOnTarget = false;
                audiSrco.pitch = Random.Range(averageNoisePitch - 0.4f, averageNoisePitch + 0.4f);
                if (!dontUseList)
                    posIndex++;
                posIndex = (posIndex >= targets.Length) ? 0 : posIndex;
                onTarget = false;
                if (targets.Length > 0 && !dontUseList)
                    targetPos = targets[posIndex];
                else
                    targetPos = Vector3.zero;
                Vector2 rdm = Random.insideUnitCircle * bedRadius;
                if (randomHorizontalTarget)
                    targetPos = new Vector3(rdm.x, targetPos.y, rdm.y);
                if (randomVerticalTarget)
                    targetPos = new Vector3(targetPos.x, Random.Range(0, height), targetPos.z);
            }
            MoveHeadTarget();

            if ((XHeadAxis != null && XCarriageAxis != null && YHeadAxis != null && YCarriageAxis != null && ZHeadAxis != null && ZCarriageAxis != null))
            {
                legX = Vector3.Distance(XHeadAxis.transform.position, rightAngleX);
                legY = Vector3.Distance(YHeadAxis.transform.position, rightAngleY);
                legZ = Vector3.Distance(ZHeadAxis.transform.position, rightAngleZ);

                if (legX < armsLenght && legY < armsLenght && legX < armsLenght)
                {
                    float vX, vY, vZ, yOffset;
                    yOffset = headObject.transform.localPosition.y + headObject.transform.parent.localPosition.y;

                    vX = (armsLenght * armsLenght) - (legX * legX);
                    ComputeAxisStates(Axis.X, XCarriage.transform.localPosition.y, yOffset + Mathf.Sqrt(vX));
                    if (!float.IsNaN(yOffset + Mathf.Sqrt(vX))) 
                        XCarriage.transform.localPosition = new Vector3(XCarriage.transform.localPosition.x, yOffset + Mathf.Sqrt(vX), XCarriage.transform.localPosition.z);

                    vY = (armsLenght * armsLenght) - (legY * legY);
                    ComputeAxisStates(Axis.Y, YCarriage.transform.localPosition.y, yOffset + Mathf.Sqrt(vY));
                    if (!float.IsNaN(yOffset + Mathf.Sqrt(vY))) 
                        YCarriage.transform.localPosition = new Vector3(YCarriage.transform.localPosition.x, yOffset + Mathf.Sqrt(vY), YCarriage.transform.localPosition.z);

                    vZ = (armsLenght * armsLenght) - (legZ * legZ);
                    ComputeAxisStates(Axis.Z, ZCarriage.transform.localPosition.y, yOffset + Mathf.Sqrt(vZ));
                    if (!float.IsNaN(yOffset + Mathf.Sqrt(vZ)))
                        ZCarriage.transform.localPosition = new Vector3(ZCarriage.transform.localPosition.x, yOffset + Mathf.Sqrt(vZ), ZCarriage.transform.localPosition.z);

                }
                XHeadAxis.transform.LookAt(XCarriageAxis.transform, XHeadAxis.transform.up);
                YHeadAxis.transform.LookAt(YCarriageAxis.transform, YHeadAxis.transform.up);
                ZHeadAxis.transform.LookAt(ZCarriageAxis.transform, ZHeadAxis.transform.up);
            }
        }

        public void MoveHeadTarget()
        {
            headAnchor.transform.localPosition = Vector3.MoveTowards(headAnchor.transform.localPosition, targetPos, speed * Time.deltaTime);
            if (headAnchor.transform.localPosition == targetPos)
            {
                onTarget = true;
                foreach (ScrollMaterialMapper mat in scrollMats)
                    mat.SetMaterialState(false, true, 0.1f);
            }
        }

        private void ComputeAxisStates(Axis axis, float current, float target)
        {
            bool IsReverse = false;
            bool IsOnPoint = false;
            if (current > target)
                IsReverse = true;
            if (current == target)
                IsOnPoint = true;
            if (axis == Axis.X && IsOnPoint == true && !XOnTarget)
            {
                audiSrco.pitch = Random.Range(averageNoisePitch - 0.1f, averageNoisePitch + 0.1f);
                XOnTarget = true;
            }
            if (axis == Axis.Y && IsOnPoint == true && !YOnTarget)
            {
                audiSrco.pitch = Random.Range(averageNoisePitch - 0.1f, averageNoisePitch + 0.1f);
                YOnTarget = true;
            }
            if (axis == Axis.Z && IsOnPoint == true && !ZOnTarget)
            {
                audiSrco.pitch = Random.Range(averageNoisePitch - 0.1f, averageNoisePitch + 0.1f);
                ZOnTarget = true;
            }
            float factor = current - target;
            factor = factor * 1000;
            ComputeScrollMaterials(axis, factor, IsReverse, IsOnPoint);
            ComputeAudio(axis, IsReverse, IsOnPoint);
        }

        private void ComputeScrollMaterials(Axis axis, float factor, bool IsReverse, bool IsOnPoint)
        {
            foreach (ScrollMaterialMapper mat in scrollMats)
                if (mat.triggersOnAxis == axis)
                    mat.SetMaterialState(IsReverse, IsOnPoint, 0.1f);
        }

        private void ComputeAudio(Axis axis, bool IsReverse, bool IsOnPoint)
        {
            audiSrco.volume = volume;
            if (audiSrco.isPlaying == false)
            {
                audiSrco.Play();
                audiSrco.UnPause();
            }
        }


        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta - new Color(0, 0, 0, .5f);
            Gizmos.DrawCube(PivotRotation(bedAnchor.transform.position + targetPos, bedAnchor.transform.position), Vector3.one * 0.01f);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(XHeadAxis.transform.position, XCarriageAxis.transform.position);
            Gizmos.DrawLine(rightAngleX, XCarriageAxis.transform.position);
            Gizmos.DrawLine(rightAngleX, XHeadAxis.transform.position);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(YHeadAxis.transform.position, YCarriageAxis.transform.position);
            Gizmos.DrawLine(rightAngleY, YCarriageAxis.transform.position);
            Gizmos.DrawLine(rightAngleY, YHeadAxis.transform.position);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(ZHeadAxis.transform.position, ZCarriageAxis.transform.position);
            Gizmos.DrawLine(rightAngleZ, ZCarriageAxis.transform.position);
            Gizmos.DrawLine(rightAngleZ, ZHeadAxis.transform.position);

            Handles.color = Color.yellow;
            Gizmos.color = Color.yellow;
            Handles.DrawWireDisc(PivotRotation(bedAnchor.transform.position + (Vector3.up * 0.005f), bedAnchor.transform.position), bedAnchor.transform.up, bedRadius);
            Gizmos.DrawLine(PivotRotation(bedAnchor.transform.position, bedAnchor.transform.position), bedAnchor.transform.position + (bedAnchor.transform.up * height));
            Handles.DrawWireDisc(PivotRotation(bedAnchor.transform.position + (Vector3.up * height), bedAnchor.transform.position), bedAnchor.transform.up, bedRadius);

        }   
        #endif
        private Vector3 PivotRotation(Vector3 point, Vector3 pivot)
        {
            return transform.transform.rotation * (point - pivot) + pivot;
        }

    }
}