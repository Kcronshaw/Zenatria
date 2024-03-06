using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AeLa.EasyFeedback.Editor.Utility
{
	/// <summary>
	/// Provides a single point of access for the EFConfig asset
	/// </summary>
	public static class EFConfigProvider
	{
		private const string configAssetsDirectory = "Settings";
		private const string configAssetName = "EasyFeedbackConfig.asset";

		private static EFConfig config;

		/// <summary>
		/// Attempts to get the <see cref="EFConfig"/> asset in the project (if any)
		/// </summary>
		/// <returns><see cref="EFConfig"/> asset if it exists in the project</returns>
		public static EFConfig GetEFConfig()
		{
			if (config)
			{
				return config;
			}

			// try to find the config asset at the default path
			config = AssetDatabase.LoadAssetAtPath<EFConfig>(GetDefaultConfigPathProject());
			if (config)
			{
				return config;
			}

			// try to find the config asset by type
			var results = AssetDatabase.FindAssets($"t:{nameof(EFConfig)}");
			var guid = results.FirstOrDefault();
			var path = !string.IsNullOrEmpty(guid) ? AssetDatabase.GUIDToAssetPath(guid) : null;

			if (results.Length > 1)
			{
				Debug.LogWarning($"Found more than one {nameof(EFConfig)} asset in project. Using {path}");
			}

			if (path == null)
			{
				return null;
			}

			return config = AssetDatabase.LoadAssetAtPath<EFConfig>(path);
		}

		/// <summary>
		/// Attempts to find an <see cref="EFConfig"/> asset in the project and creates one at the default path if not
		/// </summary>
		/// <returns></returns>
		public static EFConfig GetOrCreateEFConfig()
		{
			// try to get config first
			config = GetEFConfig();
			if (config)
			{
				return config;
			}

			// create config 
			config = ScriptableObject.CreateInstance<EFConfig>();
			EnsureConfigRootExists();
			AssetDatabase.CreateAsset(config, GetDefaultConfigPathProject());
			EditorUtility.SetDirty(config);
			AssetDatabase.SaveAssets();
			return config;
		}

		/// <summary>
		/// Returns the project-relative path where the EFConfig lives by default
		/// </summary>
		/// <returns></returns>
		private static string GetDefaultConfigPathProject()
		{
			return Path.Combine("Assets", configAssetsDirectory, configAssetName);
		}

		/// <summary>
		/// Returns the fully qualified directory where the EFConfig lives by default
		/// </summary>
		/// <returns></returns>
		private static string GetDefaultConfigDirectoryFull()
		{
			return Path.Combine(Application.dataPath, configAssetsDirectory);
		}

		/// <summary>
		/// Gets the fully qualified path where the EFConfig lives by default
		/// </summary>
		/// <returns></returns>
		private static string GetDefaultConfigPathFull()
		{
			return Path.Combine(GetDefaultConfigDirectoryFull(), configAssetName);
		}

		private static void EnsureConfigRootExists()
		{
			var directory = GetDefaultConfigDirectoryFull();
			if (Directory.Exists(directory))
			{
				return;
			}

			Directory.CreateDirectory(directory);
		}
	}
}