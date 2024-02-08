using System;
using AeLa.EasyFeedback.FormInput;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

namespace AeLa.EasyFeedback.InputSystemSupport.Editor
{
	public static class InputSystemMigration
	{
		/// <summary>
		/// Migrates relevant components on the target GameObject and all of its children to the new input system versions.
		/// </summary>
		/// <param name="target"></param>
		/// <param name="showFormInputActions">InputActionReferences for the <see cref="ShowFeedbackFormInputSystem"/> component</param>
		public static void MigrateTarget(
			GameObject target, (InputActionReference, InputActionReference, InputActionReference) showFormInputActions
		)
		{
			
			// need to get a prefab instance to remove components
			string prefabPath = null;
			if (target.scene.rootCount == 0)
			{
				prefabPath = AssetDatabase.GetAssetPath(target);
				target = PrefabUtility.LoadPrefabContents(prefabPath);
			}
			
			// replace all ShowFeedbackFormInput components with ShowFeedbackFormInputSystem
			ReplaceComponents<ShowFeedbackFormInput, ShowFeedbackFormInputSystem>(
				target, (o, n) => (n.Toggle, n.Show, n.Hide) = showFormInputActions
			);

			// replace all TabNext components with TabNextInputSystem
			ReplaceComponents<TabNext, TabNextInputSystem>(target, (o, n) => n.Copy(o));

			// apply changes to prefab asset if we're working with one
			if (prefabPath != null)
			{
				PrefabUtility.SaveAsPrefabAsset(target, prefabPath);
			}
		}

		private static void ReplaceComponents<TOld, TNew>(GameObject target, Action<TOld, TNew> migrationFunc = null)
			where TOld : MonoBehaviour
			where TNew : MonoBehaviour
		{
			var oldComponents = target.GetComponentsInChildren<TOld>(true);
			foreach (var old in oldComponents)
			{
				var replacement = old.gameObject.AddComponent<TNew>();
				migrationFunc?.Invoke(old, replacement);
				Object.DestroyImmediate(old);
			}
		}
	}
}