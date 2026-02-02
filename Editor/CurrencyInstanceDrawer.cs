using Fsi.Ui;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.Currencies
{
    [CustomPropertyDrawer(typeof(CurrencyInstance<,>), true)]
    public class CurrencyInstanceDrawer : PropertyDrawer
    {
        private const string USS = "Packages/com.fallingsnowinteractive.currencies/Editor/CurrentInstanceDrawer.uss";
        
        private static bool IsInListView(VisualElement element)
        {
            for (VisualElement current = element; current != null; current = current.parent)
            {
                if (current is MultiColumnListView || current is ListView)
                {
                    return true;
                }

                if (current.ClassListContains("unity-collection-view__item"))
                {
                    return true;
                }
            }

            return false;
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new();
            
            StyleSheet stylesheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(USS);
            if (stylesheet)
            {
                root.styleSheets.Add(stylesheet);
            }
            
            root.AddToClassList("currency");
            FsiUiEditorUtility.AddUss(root);
            
            SerializedProperty currencyProp = property.FindPropertyRelative("currency");
            SerializedProperty amountProp = property.FindPropertyRelative("amount");
            
            Label label = new(property.displayName);
            label.AddToClassList("currency_label");
            label.RegisterCallback<AttachToPanelEvent>(_ =>
                                                       {
                                                           label.style.display = IsInListView(root) 
                                                                                     ? DisplayStyle.None 
                                                                                     : DisplayStyle.Flex;
                                                       });
            root.Add(label);
            
            VisualElement data = new();
            data.AddToClassList("currency_value");
            root.Add(data);
            
            PropertyField amountField = new(amountProp, string.Empty);
            amountField.AddToClassList("currency_amount");
            data.Add(amountField);

            PropertyField currencyField = new(currencyProp);
            currencyField.AddToClassList("currency_data");
            data.Add(currencyField);
            
            return root;
        }
    }
}
