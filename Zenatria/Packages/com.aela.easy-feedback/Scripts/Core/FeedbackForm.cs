using System;
using System.Collections;
using System.IO;
using AeLa.EasyFeedback.APIs;
using AeLa.EasyFeedback.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AeLa.EasyFeedback
{
	public class FeedbackForm : MonoBehaviour
	{
		[Tooltip("Easy Feedback configuration file")]
		public EFConfig Config;

		[Tooltip("Include screenshot with reports?")]
		public bool IncludeScreenshot = true;

		/// <summary>
		/// Method used to capture the screenshot.
		/// </summary>
		[Tooltip("Method used to capture the screenshot.")]
		public ScreenshotMode ScreenshotCaptureMode = ScreenshotMode.Texture;

		/// <summary>
		/// Resizes screenshots larger than 1080p to help with Trello's filesize limit.
		/// </summary>
		/// <remarks>
		/// Not supported in Legacy screenshot capture mode.
		/// </remarks>
		[Tooltip(
			"Resizes screenshots larger than 1080p to help with Trello's filesize limit.\nNOTE: Not supported in Legacy screenshot mode."
		)]
		public bool ResizeLargeScreenshots = true;

		public Transform Form;

		/// <summary name="OnFormOpened">
		/// Called when the form is first opened, right before it is shown on screen
		/// </summary>
		[Tooltip("Functions to be called when the form is first opened")]
		public UnityEvent OnFormOpened;

		/// <summary name="OnFormSubmitted">
		/// Called right before the report is sent to Trello,
		/// so additional information may be added.
		/// </summary>
		[Tooltip("Functions to be called when the form is submitted")]
		public UnityEvent OnFormSubmitted;

		/// <summary name="OnFormClosed">
		/// Called when the form is closed, whether or not it was submitted
		/// </summary>
		[Tooltip("Functions to be called when the form is closed")]
		public UnityEvent OnFormClosed;

		/// <summary>
		/// Called to notify of any errors during submission
		/// </summary>
		[Tooltip("Called to notify of any errors during submission")]
		public SubmissionMessageEvent OnSubmissionError;

		/// <summary>
		/// Called when the submission has successfully completed
		/// </summary>
		[Tooltip("Called when the submission has successfully completed")]
		public UnityEvent OnSubmissionSucceeded;

		/// <summary>
		/// Called if the submission fails
		/// </summary>
		[Tooltip("Called if the submission fails")]
		public UnityEvent OnSubmissionFailed;
		
		/// <summary>
		/// A submission event including a message
		/// </summary>
		[Serializable]
		public class SubmissionMessageEvent : UnityEvent<string>
		{
		}

		/// <summary>
		/// The current report being built.
		/// Will be sent as next report
		/// </summary>
		public Report CurrentReport;

		// form metadata
		private string screenshotPath;

		private Coroutine ssCoroutine;

		// api handler
		private Trello trello;

		/// <summary>
		/// Whether or not the form is currently being displayed
		/// </summary>
		public bool IsOpen => Form.gameObject.activeSelf;
		
		public void Awake()
		{
			if (!Config.StoreLocal)
			{
				InitTrelloAPI();
			}

			// initialize current report
			InitCurrentReport();
		}


		public void InitTrelloAPI()
		{
			// initialize api handler
			trello = new Trello(Config.Token);
		}

		/// <summary>
		/// Replaces currentReport with a new instance of Report
		/// </summary>
		private void InitCurrentReport()
		{
			CurrentReport = new Report();
		}

		/// <summary>
		/// Toggles the open state of the form
		/// </summary>
		public void Toggle()
		{
			if (IsOpen)
			{
				Hide();
			}
			else
			{
				Show();
			}
		}


		/// <summary>
		/// Takes a screenshot, then opens the form
		/// </summary>
		public void Show()
		{
			if (!IsOpen && ssCoroutine == null)
			{
				// Set up a new report
				InitCurrentReport();
			
				ssCoroutine = StartCoroutine(ScreenshotAndOpenForm());
			}
		}

		/// <summary>
		/// Called by the submit button, submits the form.
		/// </summary>
		public void Submit()
		{
			StartCoroutine(SubmitAsync());
		}

		private IEnumerator SubmitAsync()
		{
			// disable form
			DisableForm();

			// call OnFormSubmitted
			OnFormSubmitted.Invoke();

			// close form
			Hide();

			if (!Config.StoreLocal)
			{
				// add card to board
				yield return trello.AddCard(
					CurrentReport.Title ?? "[no summary]",
					CurrentReport.ToString() ?? "[no detail]",
					CurrentReport.Labels,
					CurrentReport.List.id ?? Config.Board.ListIds[0]
				);

				// send up attachments 
				if (trello.LastAddCardResponse != null && !trello.UploadError)
				{
					yield return AttachFilesAsync(
						trello.LastAddCardResponse.id
					);
				}
			}
			else
			{
				// store entire report locally, then return
				var localPath = WriteLocal(CurrentReport);
				Debug.Log(localPath);
			}

			if (!Config.StoreLocal && trello.UploadError)
			{
				// preserve report locally if there's an issue during upload
				Debug.Log(WriteLocal(CurrentReport));

				// notify failure
				OnSubmissionError.Invoke(
					"Trello upload failed.\n" +
					"Reason: " + trello.ErrorMessage
				);

				if (trello.UploadException != null)
				{
					Debug.LogException(trello.UploadException);
				}
				else
				{
					Debug.LogError(trello.ErrorMessage);
				}

				OnSubmissionFailed.Invoke();
			}
			else
			{
				// report success
				OnSubmissionSucceeded.Invoke();
			}
		}

		/// <summary>
		/// Attaches files on current report to card
		/// </summary>
		/// <param name="cardID"></param>
		/// <returns></returns>
		private IEnumerator AttachFilesAsync(string cardID)
		{
			foreach(var attachment in CurrentReport.Attachments)
			{
				yield return trello.AddAttachmentAsync(
					cardID, attachment.Data, null, attachment.Name
				);

				if (trello.UploadError) // failed to add attachment
				{
					OnSubmissionError.Invoke(
						"Failed to attach file to report.\n" +
						"Reason: " + trello.ErrorMessage
					);
				}
			}
		}

		/// <summary>
		/// Saves the report in a local directory
		/// </summary>
		private string WriteLocal(Report report)
		{
			// create the report directory
			var feedbackDirectory = Application.persistentDataPath +
			                        "/feedback-" +
			                        DateTime.Now.ToString("MMddyyyy-HHmmss");
			Directory.CreateDirectory(feedbackDirectory);

			// save the report
			File.WriteAllText(
				feedbackDirectory + "/report.txt", report.GetLocalFileText()
			);

			// save attachments
			foreach(var attachment in CurrentReport.Attachments)
			{
				File.WriteAllBytes(
					feedbackDirectory + "/" + attachment.Name, attachment.Data
				);
			}

			return feedbackDirectory;
		}

		/// <summary>
		/// Disables all the Selectable elements on the form.
		/// </summary>
		public void DisableForm()
		{
			foreach (Transform child in Form)
			{
				var selectable = child.GetComponent<Selectable>();
				if (selectable != null)
				{
					selectable.interactable = false;
				}
			}
		}

		/// <summary>
		/// Enables all the Selectable elements on the form.
		/// </summary>
		public void EnableForm()
		{
			foreach (Transform child in Form)
			{
				var selectable = child.GetComponent<Selectable>();
				if (selectable != null)
				{
					selectable.interactable = true;
				}
			}
		}

		/// <summary>
		/// Hides the form, called by the Close button.
		/// </summary>
		public void Hide()
		{
			// don't do anything if the form is already hidden
			if (!IsOpen)
			{
				return;
			}

			// hide form
			Form.gameObject.SetActive(false);

			// delete temporary screenshot
			if (!Config.StoreLocal && IncludeScreenshot
			                       && File.Exists(screenshotPath))
			{
				if (ssCoroutine != null)
				{
					StopCoroutine(ssCoroutine);
				}

				File.Delete(screenshotPath);
			}

			screenshotPath = string.Empty;
			
			// clear screenshot coroutine
			ssCoroutine = null;

			// call OnFormClosed
			OnFormClosed.Invoke();
		}

		private IEnumerator ScreenshotAndOpenForm()
		{
			if (IncludeScreenshot)
			{
				yield return ScreenshotUtil.CaptureScreenshot(
					ScreenshotCaptureMode, ResizeLargeScreenshots,
					ss => CurrentReport.AttachFile("screenshot.png", ss),
					err => OnSubmissionError.Invoke(err)
				);
			}

			// show form
			EnableForm();
			Form.gameObject.SetActive(true);

			// call OnFormOpened
			OnFormOpened.Invoke();
		}
	}
}