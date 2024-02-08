using UnityEditor;
using UnityEngine;

namespace AeLa.EasyFeedback.Editor
{
	[CustomEditor(typeof(FeedbackForm))]
	internal class FeedbackFormEditor : UnityEditor.Editor
	{
		private SerializedProperty config;

		private void Awake()
		{
			config = serializedObject.FindProperty("Config");
		}

		public override void OnInspectorGUI()
		{
			// prompt to configure if not already set up
			if (config == null || !config.objectReferenceValue)
			{
				EditorGUILayout.LabelField("Easy Feedback is not yet configured!");
				if (GUILayout.Button("Configure Now"))
				{
					SettingsService.OpenProjectSettings(Constants.ProjectSettingsPath);
				}
			}
			else
			{
				var config = this.config.objectReferenceValue as EFConfig;
				if (string.IsNullOrEmpty(config.Token))
				{
					EditorGUILayout.LabelField("Not authenticated with Trello!");
					if (GUILayout.Button("Authenticate Now"))
					{
						SettingsService.OpenProjectSettings(Constants.ProjectSettingsPath);
					}
				}
				else
				{
					// show filesize warning
					EditorGUILayout.HelpBox(Constants.FilesizeWarning, MessageType.Warning);
				}
			}

			base.OnInspectorGUI();
		}
	}
}