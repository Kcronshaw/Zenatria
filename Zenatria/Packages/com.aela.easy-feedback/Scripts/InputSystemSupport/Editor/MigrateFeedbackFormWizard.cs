using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AeLa.EasyFeedback.InputSystemSupport.Editor
{
	public class MigrateFeedbackFormWizard : EditorWindow
	{
		/// <summary>
		/// The target feedback prefab
		/// </summary>
		public GameObject Target;

		// ShowFeedbackFormInputSystem properties
		private InputActionReference toggleInput;
		private InputActionReference showInput;
		private InputActionReference hideInput;

		public static MigrateFeedbackFormWizard GetWindow()
		{
			return EditorWindow.GetWindow<MigrateFeedbackFormWizard>(true, "Migrate Feedback Form to Input System");
		}

		private void Awake()
		{
			maxSize = minSize = new Vector2(400,200);
		}

		private void OnGUI()
		{
			EditorGUILayout.HelpBox("Replaces any legacy input manager components with their Input System counterparts on the target form prefab.", MessageType.None);
			EditorGUILayout.Space();

			// todo: warn if instance of prefab and give option to select prefab instead
			Target = (GameObject)EditorGUILayout.ObjectField("Target", Target, typeof(GameObject), true);

			if (PrefabUtility.GetPrefabInstanceStatus(Target) != PrefabInstanceStatus.NotAPrefab)
			{
				EditorGUILayout.HelpBox(
					"Target is a prefab instance. You may want to migrate the prefab asset instead.",
					MessageType.Warning
				);
			}

			if (!Target)
			{
				return;
			}

			FormComponentFields();

			if (GUILayout.Button("Migrate!"))
			{
				Migrate();
				Close();
			}
		}

		private void FormComponentFields()
		{
			InputActionReference InputActionReferenceField(string label, InputActionReference value) =>
				(InputActionReference)EditorGUILayout.ObjectField(label, value, typeof(InputActionReference), false);

			toggleInput = InputActionReferenceField("Toggle Input", toggleInput);
			showInput = InputActionReferenceField("Show Input", showInput);
			hideInput = InputActionReferenceField("Hide Input", hideInput);
		}

		private void Migrate()
		{
			Undo.RecordObject(Target, $"Migrate {Target.name} to new Input System");
			InputSystemMigration.MigrateTarget(Target, (toggleInput, showInput, hideInput));
		}
	}
}