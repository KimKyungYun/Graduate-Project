using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace ModularExtrusionsMachines
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(LineRenderer))]
    public class LaserDrawer : MonoBehaviour
    {
        public List<GameObject> points;
        public float raycastDistance = 10f;
        private LineRenderer lr;
        private void Start()
        {
            lr = GetComponent<LineRenderer>();
        }

        void Update()
        {
            if (points.Count > 0)
            {
                Vector3[] pos = new Vector3[points.Count + 1];
                for (int i = 0; i < points.Count; i++)
                    pos[i] = points[i].transform.position;
                RaycastHit hit;
                GameObject lastItem = points[points.Count - 1];
                if (Physics.Raycast(lastItem.transform.position, lastItem.transform.forward, out hit, raycastDistance))
                    pos[pos.Length - 1] = hit.point;
                else
                    pos[pos.Length - 1] = lastItem.transform.position + (lastItem.transform.forward * raycastDistance);
                lr.positionCount = points.Count + 1;
                lr.SetPositions(pos);
            }
        }
    }
}