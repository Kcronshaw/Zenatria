using AeLa.EasyFeedback.APIs;
using AeLa.EasyFeedback.Editor.Utility;
using UnityEditor;
using UnityEngine;

namespace AeLa.EasyFeedback.Editor
{
	internal class NewBoardWindow : EditorWindow
	{
		private const string WindowTitle = "New Feedback Board";
		private const int Width = 312;
		private const int Height = 46;

		private Trello trello;
		private string boardName = "My Feedback Board";

		public static NewBoardWindow GetWindow()
		{
			var window =
				GetWindow<NewBoardWindow>(true, WindowTitle);

			// set window size
			window.minSize = new Vector2(Width, Height);
			window.maxSize = window.minSize;

			return window;
		}

		private void OnEnable()
		{
			if (trello == null)
			{
				// init trello API handler
				var config = EFConfigProvider.GetEFConfig();
				trello = new Trello(config.Token);
			}
		}

		private void OnGUI()
		{
			if (trello == null)
			{
				return;
			}

			boardName = EditorGUILayout.TextField("Board Name", boardName);

			if (GUILayout.Button("Create Board"))
			{
				// add board
				SetupBoard(boardName);

				if (EditorUtility.DisplayDialog(
					    "Board created!",
					    "The board " + boardName +
					    " has been successfully created!", "Ok"
				    ))
				{
					// refresh boards in configuration
					Settings.ScheduleRefresh();

					// close self
					Close();
				}
			}
		}

		/// <summary>
		/// Clones the default feedback board
		/// </summary>
		/// <param name="boardName"></param>
		/// <returns></returns>
		public void SetupBoard(string boardName)
		{
			trello.AddBoard(
				boardName, true, true, null, null, Trello.TemplateBoardID
			);
		}
	}
}