using System;
using AeLa.EasyFeedback.FormInput;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AeLa.EasyFeedback.InputSystemSupport
{
	public class ShowFeedbackFormInputSystem : MonoBehaviour, IToggleFormInput
	{
		/// <summary>
		/// Input action used to toggle the feedback form
		/// </summary>
		[Tooltip("Input used to toggle the feedback form")]
        public InputActionReference Toggle;

		/// <summary>
		/// Input action used to show the feedback form
		/// </summary>
		[Tooltip("Input used to show the feedback form")]
		public InputActionReference Show;

		/// <summary>
		/// Input action used to hide the feedback form
		/// </summary>
		[Tooltip("Input used to hide the feedback form")]
		public InputActionReference Hide;
        
		private FeedbackForm form;

		public string Descriptor
		{
			get
			{
				if (Toggle && Toggle.action != null)
				{
					return Toggle.action.GetBindingDisplayString();
				}

				if (Show && Show.action != null)
				{
					return Show.action.GetBindingDisplayString();
				}

				return null;
			}
		}

		private void OnEnable()
		{
			form = GetComponent<FeedbackForm>();

			EnableAction(Toggle, OnToggle);
			EnableAction(Show, OnOpen);
			EnableAction(Hide, OnClose);
		}

		private void OnDisable()
		{
			DisableAction(Toggle, OnToggle);
			DisableAction(Show, OnOpen);
			DisableAction(Hide, OnClose);
		}

		private void EnableAction(InputAction action, Action<InputAction.CallbackContext> callback)
		{
			if (action == null) return;
			action.performed += callback;
			action.Enable();
		}

		private void DisableAction(InputAction action, Action<InputAction.CallbackContext> callback)
		{
			if (action == null) return;
			action.performed -= callback;
			action.Disable();
		}

		private void OnToggle(InputAction.CallbackContext ctx)
		{
			form.Toggle();
		}

		private void OnOpen(InputAction.CallbackContext ctx)
		{
			form.Show();
		}

		private void OnClose(InputAction.CallbackContext ctx)
		{
			form.Hide();
		}
	}
}
