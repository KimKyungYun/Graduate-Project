using System.Collections.Generic;
using UnityEngine;

namespace ModularExtrusionsMachines
{

    [RequireComponent(typeof(AudioSource))]
    public class CartesianXYZ : MonoBehaviour
    {
        public GameObject XAxisAnchor;
        public GameObject YAxisAnchor;
        public GameObject ZAxisAnchor;
        public GameObject BedAnchor;

        public Vector3 XAxisPos { get => XAxisAnchor.transform.localPosition; }
        public Vector3 YAxisPos { get => YAxisAnchor.transform.localPosition; }
        public Vector3 ZAxisPos { get => ZAxisAnchor.transform.localPosition; }
        public Vector3 BedAnchorPos { get => BedAnchor.transform.localPosition; }

        public Vector2 bedSize = new Vector2(0.2f, 0.2f);
        public float height = 0.2f;
        public float speed = 0.1f;
        public bool bedNotMoving = false;

        private Vector3 XTarget { get => new Vector3(target.x, 0, 0); }
        private Vector3 YTarget { get => new Vector3(0, target.y, 0); }
        private Vector3 ZTarget { get => new Vector3(0, 0, target.z); }

        private Vector3 Xoffset;
        private Vector3 Yoffset;
        private Vector3 Zoffset;

        private Vector3 target = new Vector3(0, 0, 0);
        private bool onTarget = false;
        public Vector3[] targets = new Vector3[1];
        public int posIndex = 0;
        public bool dontUseList = false;

        public bool randomHorizontalTarget = false;
        public bool randomVerticalTarget = false;

        [SerializeField]
        List<ScrollMaterialMapper> scrollMats = new List<ScrollMaterialMapper>();

        private AudioSource audioSrc;

        [Range(0f, 2f)]
        public float averageNoisePitch = 0.8f;
        [Range(0f, 1f)]
        public float volume = 1f;

        void Start()
        {
            audioSrc = GetComponent<AudioSource>();
            audioSrc.clip = ExtrusionAssetResources.instance.StepperMotorSound;
            audioSrc.loop = true;
            audioSrc.playOnAwake = false;
            audioSrc.spatialBlend = 1f;
            Xoffset = XAxisAnchor.transform.localPosition;
            Yoffset = YAxisAnchor.transform.localPosition;
            Zoffset = ZAxisAnchor.transform.localPosition;
            foreach (ScrollMaterialMapper mat in scrollMats)
                mat.Init();
            target = new Vector3(-target.x, target.y, -target.z);
        }

        private bool XOnTarget = false;
        private bool YOnTarget = false;
        private bool ZOnTarget = false;

        void Update()
        {
            if (onTarget)
            {
                audioSrc.Pause();
                XOnTarget = false;
                YOnTarget = false;
                ZOnTarget = false;
                if (!dontUseList)
                    posIndex++;
                posIndex = (posIndex >= targets.Length) ? 0 : posIndex;
                onTarget = false;
                if (targets.Length > 0 && !dontUseList)
                    SetHeadTarget(targets[posIndex]);
                else
                    target = Vector3.zero;
                if (randomHorizontalTarget)
                    target = new Vector3(Random.Range(0, bedSize.x), target.y, Random.Range(0, bedSize.y));
                if (randomVerticalTarget)
                    target = new Vector3(target.x, Random.Range(0, height), target.z);
                target = new Vector3(-target.x, target.y, -target.z);
            }
            MoveHeadTarget();
        }

        public void MoveHeadTarget()
        {
            if (!bedNotMoving)
            {
                XAxisAnchor.transform.localPosition = Vector3.MoveTowards(XAxisPos, Xoffset + XTarget, speed * Time.deltaTime);
                ComputeAxisStates(Axis.X, XAxisPos, Xoffset + XTarget);
            }
            else
            {
                XAxisAnchor.transform.localPosition = Vector3.MoveTowards(XAxisPos, BedAnchor.transform.localPosition - XTarget, speed * Time.deltaTime);
                ComputeAxisStates(Axis.X, XAxisPos, BedAnchor.transform.localPosition - XTarget);
            }
            YAxisAnchor.transform.localPosition = Vector3.MoveTowards(YAxisPos, Yoffset + YTarget, speed * Time.deltaTime);
            ComputeAxisStates(Axis.Y, YAxisPos, Yoffset + YTarget);
            ZAxisAnchor.transform.localPosition = Vector3.MoveTowards(ZAxisPos, Zoffset + ZTarget, speed * Time.deltaTime);
            ComputeAxisStates(Axis.Z, ZAxisPos, Zoffset + ZTarget);
            if (!bedNotMoving && XAxisPos == Xoffset + XTarget && YAxisPos == Yoffset + YTarget && ZAxisPos == Zoffset + ZTarget)
                onTarget = true;
            else if (bedNotMoving && XAxisPos == BedAnchor.transform.localPosition - XTarget && YAxisPos == Yoffset + YTarget && ZAxisPos == Zoffset + ZTarget)
                onTarget = true;
        }

        private void ComputeAxisStates(Axis axis, Vector3 current, Vector3 target)
        {
            bool IsReverse = false;
            bool IsOnPoint = false;
            if (axis == Axis.X && current.x > target.x)
                IsReverse = true;
            if (axis == Axis.Y && current.y > target.y)
                IsReverse = true;
            if (axis == Axis.Z && current.z > target.z)
                IsReverse = true;
            if (axis == Axis.X && current.x == target.x)
                IsOnPoint = true;
            if (axis == Axis.Y && current.y == target.y)
                IsOnPoint = true;
            if (axis == Axis.Z && current.z == target.z)
                IsOnPoint = true;
            if (axis == Axis.X && IsOnPoint == true && !XOnTarget) {
                audioSrc.pitch = Random.Range(averageNoisePitch - 0.1f, averageNoisePitch + 0.1f);
                XOnTarget = true;
            }
            if (axis == Axis.Y && IsOnPoint == true && !YOnTarget) {
                audioSrc.pitch = Random.Range(averageNoisePitch - 0.1f, averageNoisePitch + 0.1f);
                YOnTarget = true;
            }
            if (axis == Axis.Z && IsOnPoint == true && !ZOnTarget) {
                audioSrc.pitch = Random.Range(averageNoisePitch - 0.1f, averageNoisePitch + 0.1f);
                ZOnTarget = true;
            }
            ComputeAudio();
            ComputeScrollMaterials(axis, IsReverse, IsOnPoint);
        }

        private void ComputeScrollMaterials(Axis axis, bool IsReverse, bool IsOnPoint)
        {
            foreach (ScrollMaterialMapper mat in scrollMats)
                if (mat.triggersOnAxis == axis)
                    mat.SetMaterialState(IsReverse, IsOnPoint, speed);
        }

        private void ComputeAudio()
        {
            audioSrc.volume = volume;
            if (audioSrc.isPlaying == false)
            {
                audioSrc.Play();
                audioSrc.UnPause();
            }
        }

        public void SetHeadTarget(Vector3 pos)
        {
            pos = CorrectTarget(pos);
            target = pos;
        }

        public Vector3 CorrectTarget(Vector3 pos)
        {
            pos.x = pos.x > bedSize.x ? bedSize.x : pos.x;
            pos.x = pos.x < 0 ? 0 : pos.x;
            pos.y = pos.y > height ? height : pos.y;
            pos.y = pos.y < 0 ? 0 : pos.y;
            pos.z = pos.z > bedSize.y ? bedSize.y : pos.z;
            pos.z = pos.z < 0 ? 0 : pos.z;
            return pos;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta - new Color(0, 0, 0, .5f);
            if (bedNotMoving)
                Gizmos.DrawCube(RotatePointAroundPivot(BedAnchor.transform.position - (XTarget - YTarget - ZTarget)), Vector3.one * 0.01f);
            else
                Gizmos.DrawCube(RotatePointAroundPivot(XAxisAnchor.transform.position - (XTarget - YTarget - ZTarget)), Vector3.one * 0.01f);
            Gizmos.color = Color.red - new Color(0, 0, 0, .5f);
            Gizmos.DrawSphere(RotatePointAroundPivot(XAxisAnchor.transform.position, XAxisAnchor.transform.position), 0.005f);
            Gizmos.color = Color.yellow - new Color(0, 0, 0, .5f);
            Vector3 A, B, C, D;
            if (bedNotMoving)
            {
                A = BedAnchor.transform.position + new Vector3(bedSize.x, 0, 0);
                B = BedAnchor.transform.position + new Vector3(bedSize.x, 0, -bedSize.y);
                C = BedAnchor.transform.position + new Vector3(0, 0, -bedSize.y);
                D = BedAnchor.transform.position + new Vector3(0, 0, 0);
            }
            else
            {
                A = XAxisAnchor.transform.position + new Vector3(bedSize.x, 0, 0);
                B = XAxisAnchor.transform.position + new Vector3(bedSize.x, 0, -bedSize.y);
                C = XAxisAnchor.transform.position + new Vector3(0, 0, -bedSize.y);
                D = XAxisAnchor.transform.position + new Vector3(0, 0, 0);
            }
            Gizmos.DrawLine(RotatePointAroundPivot(A), RotatePointAroundPivot(B));
            Gizmos.DrawLine(RotatePointAroundPivot(B), RotatePointAroundPivot(C));
            Gizmos.DrawLine(RotatePointAroundPivot(C), RotatePointAroundPivot(D));
            Gizmos.DrawLine(RotatePointAroundPivot(D), RotatePointAroundPivot(A));
            Gizmos.DrawLine(RotatePointAroundPivot(B), RotatePointAroundPivot(D));
            Gizmos.DrawLine(RotatePointAroundPivot(A), RotatePointAroundPivot(C));
            Gizmos.color = Color.green - new Color(0, 0, 0, .5f);
            Gizmos.DrawSphere(RotatePointAroundPivot(YAxisAnchor.transform.position, YAxisAnchor.transform.position), 0.005f);
            Gizmos.DrawLine(RotatePointAroundPivot(XAxisAnchor.transform.position, XAxisAnchor.transform.position), RotatePointAroundPivot(XAxisAnchor.transform.position + new Vector3(0, height, 0), XAxisAnchor.transform.position));
            Gizmos.color = Color.blue - new Color(0, 0, 0, .5f);
            Gizmos.DrawSphere(RotatePointAroundPivot(ZAxisAnchor.transform.position, ZAxisAnchor.transform.position), 0.005f);
            Gizmos.DrawLine(RotatePointAroundPivot(
                ZAxisAnchor.transform.position + new Vector3(0, 0.05f, 0), ZAxisAnchor.transform.position)
                , RotatePointAroundPivot(new Vector3(ZAxisAnchor.transform.position.x, ZAxisAnchor.transform.position.y + 0.05f, XAxisAnchor.transform.position.z - bedSize.y), ZAxisAnchor.transform.position));
        }

        private Vector3 RotatePointAroundPivot(Vector3 point)
        {
            if (bedNotMoving)
                return transform.rotation * (point - BedAnchor.transform.position) + BedAnchor.transform.position;
            else
                return transform.rotation * (point - XAxisAnchor.transform.position) + XAxisAnchor.transform.position;
        }
        private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot)
        {
            return transform.rotation * (point - pivot) + pivot;
        }
    }

    [System.Serializable]
    public class ScrollMaterialMapper
    {
        [SerializeField]
        public Renderer Renderer;
        [SerializeField]
        public bool invertDirection;
        [SerializeField]
        public Axis triggersOnAxis;
        private Material instance;

        public void Init()
        {
            instance = new Material(Renderer.material);
            Material[] mat = new Material[1];
            mat[0] = instance;
            Renderer.materials = mat;
        }

        public void SetMaterialState(bool isReverse, bool isOnPoint, float speed)
        {
            int invertedFactor = invertDirection ? -1 : 1;
            if (isOnPoint)
                instance.SetFloat("_ScrollSpeed", 0);
            else if (isReverse)
                instance.SetFloat("_ScrollSpeed", (-speed * 100) * invertedFactor);
            else
                instance.SetFloat("_ScrollSpeed", (speed * 100) * invertedFactor);
        }
    }
    [System.Serializable]
    public enum Axis
    {
        X, Y, Z
    }
}