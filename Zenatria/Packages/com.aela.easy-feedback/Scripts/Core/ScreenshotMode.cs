namespace AeLa.EasyFeedback
{
	public enum ScreenshotMode
	{
		/// <summary>
		/// Captures the screen to memory using Texture2D.ReadPixels.
		/// </summary>
		Texture,

		/// <summary>
		/// Captures the screen using ScreenCapture.CaptureScreenshot. Use if screenshots are not captured correctly by
		/// <see cref="Texture"/> mode.
		/// </summary>
		/// <remarks>Will not work on some platforms.</remarks>
		Legacy
	}
}