using Fsi.Ui;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.Currencies
{
    public class CurrencyInstanceElement : VisualElement
    {
        public CurrencyInstanceElement(SerializedProperty property)
        {
            AddToClassList("fsi-property-row");
            FsiUiEditorUtility.AddUss(this);

            VisualElement data = new();
            data.AddToClassList("fsi-property-row");
            Add(data);

            SerializedProperty currencyProp = property.FindPropertyRelative("currency");
            SerializedProperty amountProp = property.FindPropertyRelative("amount");

            PropertyField currencyField = new(currencyProp);
            currencyField.AddToClassList("fsi-property-field");

            IntegerField amountField = new() { label = property.displayName };
            amountField.BindProperty(amountProp);
            amountField.AddToClassList("fsi-property-field");

            data.Add(amountField);
            data.Add(currencyField);
        }
    }
}
