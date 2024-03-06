using System.Diagnostics;
using UnityEditor;

namespace AeLa.EasyFeedback.Editor.FormInput
{
	public static class InputSystemWarning
	{

		[Conditional("ENABLE_INPUT_SYSTEM")]
		public static void Draw() => EditorGUILayout.HelpBox(
			"This component uses the legacy input manager. You may need to replace this with the new input system component.",
			MessageType.Warning
		);
	}
}