using AeLa.EasyFeedback.UI;
using AeLa.EasyFeedback.UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace AeLa.EasyFeedback.FormInput
{
	public abstract class TabNextBase : MonoBehaviour
	{
		public Selectable Next;
		public Selectable Previous;
		
		/// <summary>
		/// Attached InputField (TMP or Unity)
		/// </summary>
		protected IInputField input;

		// InputField (TMP or Unity) attached to Next and Previous, respectively
		protected IInputField nextInput;
		protected IInputField previousInput;

		protected virtual void Start()
		{
			// get attached input field(s)
			input = UIInterop.GetInputField(gameObject);

			if (Next)
			{
				nextInput = UIInterop.GetInputField(Next.gameObject, true);
			}

			if (Previous)
			{
				previousInput = UIInterop.GetInputField(Previous.gameObject, true);
			}
		}

		protected virtual void Select(Selectable selectable)
		{
			if (!selectable)
			{
				Debug.LogWarning("Selectable is null");
				return;
			}
			
			input.DeactivateInputField();
			selectable.Select();

			if (TryGetInputField(selectable, out var inputField))
			{
				inputField.ActivateInputField();
			}
				
		}
		
		protected bool TryGetInputField(Selectable selectable, out IInputField field)
		{
			return (field = GetInputField(selectable)) != null;
		}

		protected IInputField GetInputField(Selectable selectable)
		{
			if (selectable == Next)
			{
				return nextInput;
			}

			if (selectable == Previous)
			{
				return previousInput;
			}

			return null;
		}

		/// <summary>
		/// Copies properties from <paramref name="other"/> to this instance
		/// </summary>
		public virtual void Copy(TabNextBase other)
		{
			Next = other.Next;
			Previous = other.Previous;
		}
	}
}