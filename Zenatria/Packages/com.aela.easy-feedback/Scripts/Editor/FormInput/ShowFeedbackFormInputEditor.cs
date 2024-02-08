using AeLa.EasyFeedback.FormInput;
using UnityEditor;

namespace AeLa.EasyFeedback.Editor.FormInput
{
	[CustomEditor(typeof(ShowFeedbackFormInput))]
	[CanEditMultipleObjects]
	public class ShowFeedbackFormInputEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			InputSystemWarning.Draw();
			base.OnInspectorGUI();
		}
	}
}