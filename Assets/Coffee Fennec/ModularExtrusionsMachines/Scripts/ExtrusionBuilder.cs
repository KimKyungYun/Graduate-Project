using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using UnityEditor;
using UnityEngine;
namespace ModularExtrusionsMachines
{
    [ExecuteInEditMode]
    public class ExtrusionBuilder : MonoBehaviour
    {
        [SerializeField]
        public List<Part> twentyByTwentyDescriptors = new List<Part>();
        [SerializeField]
        public List<Part> twentyByFortyDescriptors = new List<Part>();
        [SerializeField]
        public List<Part> fortyByFortyDescriptors = new List<Part>();
        public Sizes usedSize;


        public void Rebuild()
        {
            DeleteChilds();
            int offset = 0;
            foreach (Part part in GetCurrentList())
            {
                GameObject go = Instantiate(part.descriptor.prefab, transform);
                go.transform.localRotation = Quaternion.identity;
                go.transform.localPosition = (part.descriptor.allowFlip && part.isFlipped) ? new Vector3((offset + part.size) * 0.01f, 0, 0) : new Vector3(offset * 0.01f, 0, 0);
                if (part.descriptor.allowCustomSize)
                    go.transform.localScale = new Vector3(part.size, 1, 1);
                offset += part.size;
                int xAxisRot = 0;
                int yAxisRot = 0;
                if (part.descriptor.allowRotateHalf && part.isRotatedHalf)
                    xAxisRot += 180;
                if (part.descriptor.allowRotateQuarter && part.isRotatedQuarter)
                    xAxisRot += 90;
                if (part.descriptor.allowFlip && part.isFlipped)
                    yAxisRot += 180;

                Vector3 rotation = new Vector3(xAxisRot, yAxisRot, 0);
                go.transform.localRotation = Quaternion.Euler(rotation);
                go.name = part.descriptor.prefab.name;
            }
            switch (usedSize)
            {
                case Sizes.TwentyByTwenty:
                    gameObject.name = "Extrusion_20x20_"+TotalLength()+"cm";
                    break;
                case Sizes.TwentyByForty:
                    gameObject.name = "Extrusion_20x40_" + TotalLength() + "cm"; 
                    break;
                case Sizes.FortyByForty:
                    gameObject.name = "Extrusion_40x40_" + TotalLength() + "cm"; 
                    break;
            }
        }

        public int TotalLength()
        {
            int length = 0;
            foreach (Part part in GetCurrentList())
                length += part.size;
            return length;
        }

        public void DeleteChilds()
        {
            for (int i = this.transform.childCount; i > 0; --i)
                DestroyImmediate(this.transform.GetChild(0).gameObject);
        }

        public List<Part> GetCurrentList()
        {
            switch (usedSize)
            {
                case Sizes.TwentyByTwenty:
                    return twentyByTwentyDescriptors;
                case Sizes.TwentyByForty:
                    return twentyByFortyDescriptors;
                case Sizes.FortyByForty:
                    return fortyByFortyDescriptors;
                default:
                    return twentyByTwentyDescriptors;
            }
        }
    }

    public enum Sizes
    {
        TwentyByTwenty = 0,
        TwentyByForty = 1,
        FortyByForty = 2
    }

    [System.Serializable]
    public class PartDescriptor
    {
        [SerializeField]
        public GameObject prefab;
        [SerializeField]
        public Sprite Icon;
        [SerializeField]
        public int defaultSize;
        [SerializeField]
        public bool allowCustomSize;
        [SerializeField]
        public bool allowRotateQuarter;
        [SerializeField]
        public bool allowRotateHalf;
        [SerializeField]
        public bool allowFlip;

        public override string ToString()
        {
            return prefab != null ? prefab.name : "Missing Prefab";
        }
    }

    [System.Serializable]
    public class Part
    {
        [SerializeField]
        public PartDescriptor descriptor;
        [SerializeField]
        public int size = 1;
        [SerializeField]
        public bool isRotatedHalf;
        [SerializeField]
        public bool isRotatedQuarter;
        [SerializeField]
        public bool isFlipped;

        public Part(PartDescriptor descriptor)
        {
            this.descriptor = descriptor;
            size = descriptor.defaultSize;
        }
    }
}