using System;
using UnityEngine;

namespace AeLa.EasyFeedback.Utility
{
	/// <summary>
	/// Shows or hides the mouse when the feedback form is opened or closed.
	/// Remove this component from your form if you do not want the mouse to be automatically managed.
	/// </summary>
	[RequireComponent(typeof(FeedbackForm))]
	public class ShowHideMouse : MonoBehaviour
	{
		private FeedbackForm form;
		
		private CursorLockMode previousLockState;
		private bool previousVisibility;

		private void Awake()
		{
			form = GetComponent<FeedbackForm>();
		}

		private void OnEnable()
		{
			form.OnFormOpened.AddListener(FormOpened);
			form.OnFormClosed.AddListener(FormClosed);

			if (form.IsOpen)
			{
				FormOpened();
			}
		}

		private void OnDisable()
		{
			if (!form) return;
			form.OnFormOpened.RemoveListener(FormOpened);
			form.OnFormClosed.RemoveListener(FormClosed);
		}

		private void FormOpened()
		{
			previousVisibility = Cursor.visible;
			previousLockState = Cursor.lockState;
			
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}

		private void FormClosed()
		{
			Cursor.visible = previousVisibility;
			Cursor.lockState = previousLockState;
		}
	}
}