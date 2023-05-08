using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ModularExtrusionsMachines
{
    [CreateAssetMenu(fileName = "Extrusion Parts Descriptors", menuName = "ExtrusionAsset/PartsDescriptors")]
    public class ExtrusionAssetResources : ScriptableObject
    {
        private const string assetpath = "ExtrusionAssetResources";

        private static ExtrusionAssetResources _instance;
        public static ExtrusionAssetResources instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                _instance = Resources.Load<ExtrusionAssetResources>(assetpath);
                return _instance;
            }
        }


        [SerializeField]
        public List<PartDescriptor> twentyByTwentyDescriptors;
        [SerializeField]
        public List<PartDescriptor> twentyByFortyDescriptors;
        [SerializeField]
        public List<PartDescriptor> fortyByFortyDescriptors;
        [SerializeField]
        public VisualTreeAsset btnPart;
        [SerializeField]
        public VisualTreeAsset listItem;
        [SerializeField]
        public VisualTreeAsset inspector;
        [SerializeField]
        public AudioClip StepperMotorSound;

    }
}