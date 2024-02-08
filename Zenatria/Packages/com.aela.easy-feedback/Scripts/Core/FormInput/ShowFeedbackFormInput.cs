using AeLa.EasyFeedback.Utility;
using UnityEngine;

namespace AeLa.EasyFeedback.FormInput
{
	[RequireComponent(typeof(FeedbackForm))]
	public class ShowFeedbackFormInput : MonoBehaviour, IToggleFormInput
	{
		/// <summary>
		/// Key used to toggle the feedback form
		/// </summary>
		[Tooltip("Key used to toggle the feedback form")]
		public KeyCode ToggleKey = KeyCode.F12;

		/// <summary>
		/// Key used to show the feedback form
		/// </summary>
		[Tooltip("Key used to hide the feedback form")]
		public KeyCode ShowKey = KeyCode.None;

		/// <summary>
		/// Key used to hide the feedback form
		/// </summary>
		[Tooltip("Key used to hide the feedback form")]
		public KeyCode HideKey = KeyCode.Escape;

		private FeedbackForm form;

		public string Descriptor => ToggleKey != KeyCode.None ? ToggleKey.ToString() : ShowKey.ToString();

		private void Start()
		{
			form = GetComponent<FeedbackForm>();
		}

		private void Update()
		{
			// toggle
			if (Input.GetKeyDown(ToggleKey))
			{
				form.Toggle();
				return;
			}

			// show  
			if (Input.GetKeyDown(ShowKey))
			{
				form.Show();
				return;
			}

			// hide 
			if (Input.GetKeyDown(HideKey))
			{
				form.Hide();
			}
		}
	}
}