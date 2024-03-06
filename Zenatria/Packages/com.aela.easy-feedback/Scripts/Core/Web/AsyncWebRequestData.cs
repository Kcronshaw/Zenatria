using UnityEngine;
using UnityEngine.Networking;

namespace AeLa.EasyFeedback.Web
{
	internal readonly struct AsyncWebRequestData
	{
		/// <summary>
		/// The underlying UnityWebRequest
		/// </summary>
		public UnityWebRequest Request { get; }

		/// <summary>
		/// The AsyncOperation for this request
		/// </summary>
		public AsyncOperation Operation { get; }

		/// <summary>
		/// Whether or not the async operation is finished
		/// </summary>
		public bool OperationIsDone => Operation.isDone;

		/// <summary>
		/// Whether or not the request has resulted in an error
		/// </summary>
		public bool RequestIsError => Request.error != null;

		public string ErrorText
		{
			get
			{
#if UNITY_2020_1_OR_NEWER
				if (Request.result == UnityWebRequest.Result.ProtocolError)
#else
				if (Request.isHttpError)
#endif
				{
					return Request.downloadHandler.text;
				}

				if (RequestIsError)
				{
					return Request.error;
				}

				return string.Empty;
			}
		}

		public AsyncWebRequestData(
			UnityWebRequest request, AsyncOperation operation
		)
		{
			Request = request;
			Operation = operation;
		}
	}
}