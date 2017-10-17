using UnityEditor;
using UnityEngine;

namespace CustomUnityEffects.Editor
{
	[CustomEditor(typeof(AudioEffectBase), true)]
	public class SingleAudioEffectEditor : UnityEditor.Editor
	{
		[SerializeField]
		private AudioSource _previewer;

		public void OnEnable()
		{
			_previewer = EditorUtility.CreateGameObjectWithHideFlags("Audio preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
		}

		public void OnDisable()
		{
			DestroyImmediate(_previewer.gameObject);
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
			if (GUILayout.Button("Preview"))
			{
				((AudioEffectBase)target).Play(_previewer);
			}
			EditorGUI.EndDisabledGroup();
		}
	}
}
