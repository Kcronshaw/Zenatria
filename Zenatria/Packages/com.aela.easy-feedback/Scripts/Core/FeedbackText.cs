using AeLa.EasyFeedback.FormInput;
using AeLa.EasyFeedback.UI;
using AeLa.EasyFeedback.UI.Interfaces;
using UnityEngine;

namespace AeLa.EasyFeedback
{
#if UNITY_EDITOR
	[ExecuteInEditMode]
#endif
	public class FeedbackText : MonoBehaviour
	{
		public string Message = "Press {0} to submit feedback.";
		public FeedbackForm Form;
		private IToggleFormInput input;
		private IText text;

		private string currentDescriptor;

		private void Awake()
		{
			text = UIInterop.GetText(gameObject);
			input = Form.GetComponent<IToggleFormInput>();
			if (input == null)
			{
				Debug.LogError($"Form is missing {nameof(IToggleFormInput)} component");
			}
		}

		private void Update()
		{
			if (input != null && currentDescriptor != input.Descriptor)
			{
				UpdateText();
			}
		}

		private void UpdateText()
		{
			currentDescriptor = input.Descriptor;
			text.Text = string.Format(Message, currentDescriptor);
		}
	}
}