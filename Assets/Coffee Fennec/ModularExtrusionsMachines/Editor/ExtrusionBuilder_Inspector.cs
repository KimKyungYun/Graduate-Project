using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ModularExtrusionsMachines
{
    [CustomEditor(typeof(ExtrusionBuilder))]
    public class ExtrusionBuilder_Inspector : Editor
    {
        private VisualElement root;
        private Sizes size;
        ExtrusionBuilder builder;

        public override VisualElement CreateInspectorGUI()
        {
            builder = (ExtrusionBuilder)target;
            root = new VisualElement();
            VisualTreeAsset visualTree = ExtrusionAssetResources.instance.inspector;
            visualTree.CloneTree(root);
            ComputeSizeButtons();
            ComputeSizeButtonsState();
            ComputePartButtons();
            ComputePartList();
            ComputeMisc();
            return root;
        }

        private void ComputeSizeButtons()
        {
            root.Q<Button>("btnSize2020").clickable.clicked += () => { OnClickSizeButton(0); };
            root.Q<Button>("btnSize2040").clickable.clicked += () => { OnClickSizeButton(1); };
            root.Q<Button>("btnSize4040").clickable.clicked += () => { OnClickSizeButton(2); };
        }

        private void OnClickSizeButton(int index)
        {
            root.Query(className: "btnSize").ForEach((element) =>
            {
                element.RemoveFromClassList("selected");
            });
            size = (Sizes)index;
            switch (size)
            {
                case Sizes.TwentyByTwenty:
                    root.Q("btnSize2020").AddToClassList("selected");
                    break;
                case Sizes.TwentyByForty:
                    root.Q("btnSize2040").AddToClassList("selected");
                    break;
                case Sizes.FortyByForty:
                    root.Q("btnSize4040").AddToClassList("selected");
                    break;
                default:
                    break;
            }
            builder.usedSize = size;
            ComputePartButtons();
            ComputePartList();
            builder.Rebuild();
            ComputeMisc();
        }

        private void ComputeMisc()
        {
            root.Q<Label>("totalLenghtValue").text = builder.TotalLength().ToString();
        }

        private void ComputePartList()
        {
            switch (size)
            {
                case Sizes.TwentyByTwenty:
                    GeneratePartListItems(builder.twentyByTwentyDescriptors);
                    break;
                case Sizes.TwentyByForty:
                    GeneratePartListItems(builder.twentyByFortyDescriptors);
                    break;
                case Sizes.FortyByForty:
                    GeneratePartListItems(builder.fortyByFortyDescriptors);
                    break;
                default:
                    break;
            }
            ComputeMisc();
        }

        private void GeneratePartListItems(List<Part> parts)
        {
            VisualElement listRoot = root.Q("partList");
            listRoot.Clear();
            foreach (Part part in parts)
            {
                VisualElement newBtn;

                newBtn = ExtrusionAssetResources.instance.listItem.CloneTree();
                newBtn.Q("icon").style.backgroundImage = new StyleBackground(part.descriptor.Icon);

                newBtn.Q<IntegerField>("lengthInt").RegisterValueChangedCallback(x => { OnLengthChanged(part,x.newValue); });
                newBtn.Q<IntegerField>("lengthInt").EnableInClassList("hidden", !part.descriptor.allowCustomSize);
                newBtn.Q<IntegerField>("lengthInt").value = part.size;

                newBtn.Q<Button>("btnFlip").clickable.clicked += () => { OnClickListItemFlip(part, newBtn); };
                newBtn.Q<Button>("btnFlip").EnableInClassList("selected", part.isFlipped);
                newBtn.Q<Button>("btnFlip").EnableInClassList("hidden", !part.descriptor.allowFlip);

                newBtn.Q<Button>("btnRotate90").clickable.clicked += () => { OnClickListItemRotatedQuarter(part, newBtn); };
                newBtn.Q<Button>("btnRotate90").EnableInClassList("selected", part.isRotatedQuarter);
                newBtn.Q<Button>("btnRotate90").EnableInClassList("hidden", !part.descriptor.allowRotateQuarter);

                newBtn.Q<Button>("btnRotate180").clickable.clicked += () => { OnClickListItemRotatedHalf(part, newBtn); };
                newBtn.Q<Button>("btnRotate180").EnableInClassList("selected", part.isRotatedHalf);
                newBtn.Q<Button>("btnRotate180").EnableInClassList("hidden", !part.descriptor.allowRotateHalf);

                newBtn.Q<Button>("btnRemove").clickable.clicked += () => { OnClickListItemRemove(part); };
                newBtn.Q<Button>("btnHigher").clickable.clicked += () => { OnClickListItemHigher(part); };
                newBtn.Q<Button>("btnLower").clickable.clicked += () => { OnClickListItemLower(part); };
                listRoot.Add(newBtn);
            }
        }

        private void OnLengthChanged(Part part, int x)
        {
            if (x <= 0)
                x = 1;
            part.size = x;
            builder.Rebuild();
            ComputeMisc();
        }

        private void OnClickListItemLower(Part part)
        {
            List<Part> currentList = builder.GetCurrentList();
            int index = currentList.IndexOf(part);
            if (index + 1 > currentList.Count - 1)
                return;
            currentList.Remove(part);
            currentList.Insert(index + 1,part);
            builder.Rebuild();
            ComputePartList();
        }

        private void OnClickListItemHigher(Part part)
        {
            List<Part> currentList = builder.GetCurrentList();
            int index = currentList.IndexOf(part);
            if (index - 1 < 0)
                return;
            currentList.Remove(part);
            currentList.Insert(index - 1, part);
            builder.Rebuild();
            ComputePartList();
        }

        private void OnClickListItemRemove(Part part)
        {
            List<Part> currentList = builder.GetCurrentList();
            currentList.Remove(part);
            builder.Rebuild();
            ComputeMisc();
            ComputePartList();
        }

        private void OnClickListItemRotatedHalf(Part part, VisualElement newBtn)
        {
            part.isRotatedHalf = !part.isRotatedHalf;
            newBtn.Q<Button>("btnRotate180").EnableInClassList("selected", part.isRotatedHalf);
            builder.Rebuild();
        }

        private void OnClickListItemRotatedQuarter(Part part, VisualElement newBtn)
        {
            part.isRotatedQuarter = !part.isRotatedQuarter;
            newBtn.Q<Button>("btnRotate90").EnableInClassList("selected", part.isRotatedQuarter);
            builder.Rebuild();
        }

        private void OnClickListItemFlip(Part part, VisualElement newBtn)
        {
            part.isFlipped = !part.isFlipped;
            newBtn.Q<Button>("btnFlip").EnableInClassList("selected", part.isFlipped);
            builder.Rebuild();
        }

        private void ComputePartButtons()
        {
            switch (size)
            {
                case Sizes.TwentyByTwenty:
                    GeneratePartButtons(ExtrusionAssetResources.instance.twentyByTwentyDescriptors);
                    break;
                case Sizes.TwentyByForty:
                    GeneratePartButtons(ExtrusionAssetResources.instance.twentyByFortyDescriptors);
                    break;
                case Sizes.FortyByForty:
                    GeneratePartButtons(ExtrusionAssetResources.instance.fortyByFortyDescriptors);
                    break;
                default:
                    break;
            }
        }

        private void GeneratePartButtons(List<PartDescriptor> descriptors)
        {
            VisualElement listRoot = root.Q("partsSelector");
            listRoot.Clear();
            VisualElement newBtn;
            foreach (PartDescriptor desc in descriptors)
            {
                newBtn = ExtrusionAssetResources.instance.btnPart.CloneTree();
                newBtn.Q("background").style.backgroundImage = new StyleBackground(desc.Icon);
                newBtn.Q<Button>().clickable.clicked += () => { OnClickPartButton(desc); };
                listRoot.Add(newBtn);
            }
        }

        private void OnClickPartButton(PartDescriptor desc)
        {
            switch (size)
            {
                case Sizes.TwentyByTwenty:
                    builder.twentyByTwentyDescriptors.Add(new Part(desc));
                    break;
                case Sizes.TwentyByForty:
                    builder.twentyByFortyDescriptors.Add(new Part(desc));
                    break;
                case Sizes.FortyByForty:
                    builder.fortyByFortyDescriptors.Add(new Part(desc));
                    break;
                default:
                    break;
            }
            ComputePartList();
            builder.Rebuild();
        }

        private void ComputeSizeButtonsState()
        {
            root.Query(className: "btnSize").ForEach((element) =>
            {
                element.RemoveFromClassList("selected");
            });
            size = builder.usedSize;
            switch (builder.usedSize)
            {
                case Sizes.TwentyByTwenty:
                    root.Q("btnSize2020").AddToClassList("selected");
                    break;
                case Sizes.TwentyByForty:
                    root.Q("btnSize2040").AddToClassList("selected");
                    break;
                case Sizes.FortyByForty:
                    root.Q("btnSize4040").AddToClassList("selected");
                    break;
                default:
                    break;
            }
        }


    }

}

