using UnityEngine;
using UnityEditor;

namespace PHATASS.Utils.Types
{
	[CustomPropertyDrawer(typeof(AnimatorVariableIdentifier))]
	public class AnimatorVariableIdentifierPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);	

			//position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);		
			EditorGUI.PropertyField(position, property.FindPropertyRelative("variableName"), label);

			EditorGUI.EndProperty();
		}
	}
}