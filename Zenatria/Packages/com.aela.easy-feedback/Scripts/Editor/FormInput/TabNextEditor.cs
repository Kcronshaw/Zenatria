using AeLa.EasyFeedback.FormInput;
using UnityEditor;

namespace AeLa.EasyFeedback.Editor.FormInput
{
	[CustomEditor(typeof(TabNext))]
	[CanEditMultipleObjects]
	public class TabNextEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			InputSystemWarning.Draw();
			base.OnInspectorGUI();
		}
	}
}