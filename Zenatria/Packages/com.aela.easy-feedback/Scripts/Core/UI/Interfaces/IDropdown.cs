namespace AeLa.EasyFeedback.UI.Interfaces
{
	public interface IDropdown
	{
		IText CaptionText { get; }
		int Value { get; set; }

		void ClearOptions();
		void RefreshShownValue();
		void AddOption(string text);
	}
}