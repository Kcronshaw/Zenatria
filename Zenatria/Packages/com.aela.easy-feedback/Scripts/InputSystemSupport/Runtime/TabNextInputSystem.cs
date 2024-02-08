using AeLa.EasyFeedback.FormInput;
using UnityEngine.InputSystem;

namespace AeLa.EasyFeedback.InputSystemSupport
{
	/// <summary>
	/// A drop-in replacement for the <see cref="TabNext"/> component using the Input System.
	/// </summary>
	/// <remarks>
	/// This is just a simple helper component with hard-coded input bindings for tab/shift.
	/// You may use this as an example if you need something more bespoke.
	/// Always feel free to reach out to our support email if you'd like help extending Easy Feedback!
	/// </remarks>
	public class TabNextInputSystem : TabNextBase
	{
		private InputAction tabAction;
		private InputAction shiftAction;
		
		private void OnEnable()
		{
			if (tabAction == null)
			{
				tabAction = new InputAction(binding: "<Keyboard>/tab");
				tabAction.performed += OnTab;
			}

			if (shiftAction == null)
			{
				shiftAction = new InputAction(binding: "<Keyboard>/shift");
			}
			
			tabAction.Enable();
			shiftAction.Enable();
		}

		private void OnDisable()
		{
			tabAction.Disable();
			shiftAction.Disable();
		}

		private void OnTab(InputAction.CallbackContext ctx)
		{
			if (!input.IsFocused)
			{
				return;
			}

			var shift = shiftAction.IsPressed();
			
			if (Next && !shift)
			{
				Select(Next);
			}
			else if (Previous && shift)
			{
				Select(Previous);
			}
		}
	}
}