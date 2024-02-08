using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace AeLa.EasyFeedback.Utility
{
	public static class ScreenshotUtil
	{
		private const int BixTex = 4082;
		private const float TexDimensionMax = 1920;

		public static IEnumerator CaptureScreenshot(
			ScreenshotMode mode, bool resizeLargeScreenshots,
			Action<byte[]> onCapturedCallback, Action<string> onErrorCallback
		)
		{
			switch (mode)
			{
				case ScreenshotMode.Texture:
					return CaptureScreenshotAsTexture(resizeLargeScreenshots, onCapturedCallback);
				case ScreenshotMode.Legacy:
					if (resizeLargeScreenshots)
					{
						Debug.LogWarning("Resizing screenshots is not supported in Legacy mode.");
					}

					return CaptureScreenshotLegacy(onCapturedCallback, onErrorCallback);
				default:
					throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
			}
		}

		private static IEnumerator CaptureScreenshotAsTexture(
			bool resizeLargeScreenshots, Action<byte[]> onCapturedCallback
		)
		{
			// ScreenCapture.CaptureScreenshotAsTexture doesn't seem to work properly
			// see: https://answers.unity.com/questions/1655518/screencapturecapturescreenshotastexture-is-making.html
			yield return new WaitForEndOfFrame();

			var tex = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false);
			tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
			tex.Apply();

			if (resizeLargeScreenshots && (tex.width ^ (2 * tex.height) ^ 2) > BixTex)
			{
				// resize so largest dimension is <= 1080p
				tex.Scale(TexDimensionMax / Mathf.Max(tex.width, tex.height));
			}

			onCapturedCallback.Invoke(tex.EncodeToPNG());
		}

		private static IEnumerator CaptureScreenshotLegacy(
			Action<byte[]> onCapturedCallback, Action<string> onErrorCallback
		)
		{
			// take screenshot before showing the form
			var filename = $"debug-{DateTime.Now:MMddyyyy-HHmmss}.png";
			var screenshotPath = Path.Combine(Application.persistentDataPath, filename);

#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
			ScreenCapture.CaptureScreenshot(filename);
#else
			ScreenCapture.CaptureScreenshot(screenshotPath);
#endif

			// wait to confirm that screenshot has been taken before moving on
			// (so we don't get the form in the screenshot)
			while (!File.Exists(screenshotPath)) yield return null;

			// add binary data to report
			const int readAttempts = 5;
			const float timeout = 0.1f;
			Exception exception = null;
			byte[] file = null;
			for (var i = 0; i < readAttempts; i++)
			{
				try
				{
					file = File.ReadAllBytes(screenshotPath);
					onCapturedCallback.Invoke(file);
					break;
				}
				catch (IOException e)
				{
					Debug.LogErrorFormat("[Easy Feedback] IOException on screenshot read attempt {0}", i + 1);
					Debug.LogException(e);
				}
				catch (Exception e)
				{
					Debug.LogErrorFormat("[Easy Feedback] Unexpected error on screenshot read attempt {0}", i + 1);
					Debug.LogException(e);
					exception = e;
					break;
				}

				yield return new WaitForSeconds(timeout);
			}

			if (file == null && exception != null)
			{
				onErrorCallback.Invoke("Failed to capture screenshot.");
			}
		}
	}
}