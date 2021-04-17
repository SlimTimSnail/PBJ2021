using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(WordTagAttribute))]
public class WordTagDrawer : PropertyDrawer
{
    private static WordTags s_tagsData = null;
    private static SerializedProperty s_currentProperty = null;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (s_tagsData == null)
        {
            s_tagsData = AssetDatabase.LoadAssetAtPath<WordTags>("Assets/Data/WordTags.asset");
        }

        GUIContent myLabel = EditorGUI.BeginProperty(position, label, property);
        Rect pos = EditorGUI.PrefixLabel(position, myLabel);
        Rect dropButton = new Rect(pos.xMax - 20f, pos.y, 20f, pos.height);
        pos.width -= 20f;
        EditorGUI.PropertyField(pos, property, GUIContent.none);
        if(EditorGUI.DropdownButton(dropButton, GUIContent.none, FocusType.Passive))
        {
            s_currentProperty = property;
            GenericMenu menu = new GenericMenu();
            string current = property.stringValue;
            if (!string.IsNullOrEmpty(current))
            {
                menu.AddItem(new GUIContent(current), true, TagSelected, current);
            }
            foreach (string tag in s_tagsData)
            {
                menu.AddItem(new GUIContent(tag), tag == current, TagSelected, tag);
            }
            menu.ShowAsContext();
        }
    }

    private void TagSelected(object data)
    {
        if (s_currentProperty != null)
        {
            s_currentProperty.stringValue = (string)data;
            s_currentProperty.serializedObject.ApplyModifiedProperties();
        }
    }

}
