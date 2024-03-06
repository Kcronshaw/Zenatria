using UnityEngine;

namespace AeLa.EasyFeedback.FormInput
{
	public class TabNext : TabNextBase
	{
		private void Update()
		{
			if (!input.IsFocused)
			{
				return;
			}

			var shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
			var tabDown = Input.GetKeyDown(KeyCode.Tab);
			
			if (Next && tabDown && !shift)
			{
				Select(Next);
			}
			else if (Previous && tabDown && shift)
			{
				Select(Previous);
			}
		}
	}
}