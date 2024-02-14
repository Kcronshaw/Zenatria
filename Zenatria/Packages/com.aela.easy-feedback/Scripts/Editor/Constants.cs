namespace AeLa.EasyFeedback.Editor
{
	internal static class Constants
	{
		public const string ProjectSettingsPath = "Project/Easy Feedback";

		public const string FilesizeWarning =
			"Trello has a per-attachment file size limit of 10MB for free accounts and 250MB for paid accounts. Files larger than this limit will fail to upload.";

		/// <summary>
		/// Forward slash (/) as unicode sequence. 
		/// Used as a workaround for forward slashes in Trello board names
		/// causing popups to erroneously display submenus.
		/// </summary>
		public const string UnicodeForwardSlash = "\u2215";

		public const string DocumentationURL = "https://aesthetic.games/easy-feedback/docs/release/";
		public const string HelpURL = "mailto:help@aesthetic.games";
		public const string AssetStoreURL = "https://assetstore.unity.com/packages/tools/integration/easy-feedback-form-81608";
	}
}