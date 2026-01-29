using Fsi.Ui;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.Currencies
{
    [CustomPropertyDrawer(typeof(CurrencyInstance<,>), true)]
    public class CurrencyInstanceDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new();
            root.AddToClassList("fsi-property-row");
            FsiUiEditorUtility.AddUss(root);

            VisualElement data = new();
            data.AddToClassList("fsi-property-row");
            data.AddToClassList("unity-base-field__aligned");
            data.style.flexDirection = FlexDirection.Row;
            data.style.columnGap = 4;

            root.Add(data);
            
            SerializedProperty currencyProp = property.FindPropertyRelative("currency");
            SerializedProperty amountProp = property.FindPropertyRelative("amount");

            PropertyField currencyField = new(currencyProp);
            currencyField.AddToClassList("fsi-property-field");
            
            bool showLabel = !property.propertyPath.Contains("Array.data[");
            if (showLabel)
            {
                Label label = new(property.displayName);
                label.AddToClassList("unity-base-field__label");
                label.AddToClassList("unity-base-field__aligned");
                data.Add(label);
            }

            PropertyField amountField = new(amountProp, string.Empty);
            amountField.AddToClassList("fsi-property-field");
            amountField.AddToClassList("unity-base-field__aligned");
            
            data.Add(amountField);
            data.Add(currencyField);
            
            return root;
        }
    }
}
