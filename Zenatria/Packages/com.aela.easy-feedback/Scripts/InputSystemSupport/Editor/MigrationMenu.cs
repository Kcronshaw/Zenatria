using UnityEditor;

namespace AeLa.EasyFeedback.InputSystemSupport.Editor
{
	public static class MigrationMenu
	{
		private const string SceneMenuName = "GameObject/Migrate form to Input System";
		private const string ProjectMenuName = "Assets/Migrate form to Input System";
		
		[MenuItem(SceneMenuName, false, 10)]
		[MenuItem(ProjectMenuName, false, 100)]
		private static void Context()
		{
			var wizard = MigrateFeedbackFormWizard.GetWindow();
			wizard.Target = Selection.activeGameObject;
		}

		[MenuItem(SceneMenuName, true)]
		[MenuItem(ProjectMenuName, true)]
		private static bool ContextValidation()
		{
			var selection = Selection.activeGameObject;
			return selection && selection.GetComponent<FeedbackForm>();
		}
	}
}