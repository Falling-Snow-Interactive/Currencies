using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.Currencies
{
    public class CurrencyInstanceElement : VisualElement
    {
        private const string USS = "Packages/com.fallingsnowinteractive.currencies/Editor/CurrentInstanceElement.uss";
        
        #region USS Classes

        private const string RootClass = "currency_instance_element__root";
        private const string LabelClass = "currency_instance_element__label";
        private const string FieldClass = "currency_instance_element__property";
        private const string FieldAmountClass = "currency_instance_element__amount";
        private const string FieldCurrencyClass = "currency_instance_element__data";

        #endregion
        
        private readonly Label rootLabel;
        
        public CurrencyInstanceElement(string label, SerializedProperty property)
        {
            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(USS);
            if (styleSheet)
            {
                styleSheets.Add(styleSheet);
            }
            
            VisualElement root = new();
            root.AddToClassList(RootClass);
            Add(root);

            rootLabel = new Label(label);
            rootLabel.AddToClassList(LabelClass);
            root.Add(rootLabel);

            VisualElement fieldGroup = new();
            fieldGroup.AddToClassList(FieldClass);
            root.Add(fieldGroup);

            SerializedProperty amountProp = property.FindPropertyRelative("amount");
            PropertyField minField = new(amountProp) { label = "" };
            minField.AddToClassList(FieldAmountClass);
            fieldGroup.Add(minField);

            // VisualElement fieldSpacer = new();
            // fieldSpacer.AddToClassList(FieldSpacerClass);
            // fieldGroup.Add(fieldSpacer);

            SerializedProperty currencyProp = property.FindPropertyRelative("currency");
            PropertyField currencyField = new(currencyProp) { label = "" };
            currencyField.AddToClassList(FieldCurrencyClass);
            fieldGroup.Add(currencyField);

            // When used inside a MultiColumnListView cell, hide the top label.
            RegisterCallback<AttachToPanelEvent>(_ => UpdateRootLabelVisibility());
        }
        
        private void UpdateRootLabelVisibility()
        {
            bool hide = IsInMultiColumnListView();
            rootLabel.style.display = hide ? DisplayStyle.None : DisplayStyle.Flex;
        }

        private bool IsInMultiColumnListView()
        {
            for (VisualElement p = this; p != null; p = p.parent)
            {
                if (p is MultiColumnListView)
                    return true;

                // Fallback: Unity's internal root class name for the control.
                if (p.ClassListContains("unity-multi-column-list-view"))
                    return true;
            }

            return false;
        }
    }
}