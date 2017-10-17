using UnityEditor;
using UnityEngine;

namespace CustomUnityEffects.Editor
{
	[CustomEditor(typeof(VisualEffectBase), true)]
	public class SimpleVisualEffectEditor : UnityEditor.Editor
	{
		private Transform _previewPoint;
		private Vector3 _previewPosition;
		private Vector3 _previewDirection;

		private readonly string[] _popupStrings = {"Preview By Point", "Preview By Position"};
		private int _selectedPopupIndex;

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

			_selectedPopupIndex = EditorGUILayout.Popup("Preview Mode", _selectedPopupIndex, _popupStrings);

			var position = Vector3.zero;
			var direction = Vector3.right;
			if (_selectedPopupIndex == 0)
			{
				_previewPoint = (Transform)EditorGUILayout.ObjectField("Preview position", _previewPoint, typeof (Transform), true);
				position = _previewPoint == null ? Vector3.zero : _previewPoint.position;
			}
			else
			{
				_previewPosition = EditorGUILayout.Vector3Field("Preview position", _previewPosition);
				position = _previewPosition;
			}

			_previewDirection = EditorGUILayout.Vector3Field("Preview direction", _previewPosition);
			direction = _previewPosition;

			EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
			if (GUILayout.Button("Preview"))
			{
				((VisualEffectBase)target).Play(position, direction, null);
			}
			EditorGUI.EndDisabledGroup();
		}
	}
}
