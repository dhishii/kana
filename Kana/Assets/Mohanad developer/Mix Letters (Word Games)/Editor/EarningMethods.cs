namespace editorspace
{
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEditor;
	using UnityEngine;

	public class EarningMethods : EditorWindow
	{
		[MenuItem("Window/Earning Methods")]
		public static void ShowWindow()
		{
			GetWindow<EarningMethods>("Earning Methods");

		}

		bool isAddAdmob;
		bool isPurchaser;

		private void OnEnable()
		{
			string ScriptingDefine = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
			if (ScriptingDefine.Contains("admob"))
			{
				isAddAdmob = true;
			}
			else
			{
				isAddAdmob = false;

			}
			if (ScriptingDefine.Contains("purchaser"))
			{
				isPurchaser = true;
			}
			else
			{
				isPurchaser = false;

			}

		}

		private void OnGUI()
		{
			isAddAdmob = GUILayout.Toggle(isAddAdmob, "Enable Admob");
			isPurchaser = GUILayout.Toggle(isPurchaser, "Enable In-App Purchaser");

			changeDefine();
		}

		void changeDefine()
		{

			if (isAddAdmob)
			{
				addDefineSymbol("admob");
			}
			else
			{
				removeDefineSymbol("admob");
			}

			if (isPurchaser)
			{
				addDefineSymbol("purchaser");
			}
			else
			{
				removeDefineSymbol("purchaser");
			}

		}

		void addDefineSymbol(string val)
		{
			string ScriptingDefine = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
			if (ScriptingDefine.Contains(val))
			{
				return;
			}

			string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
			List<string> allDefines = definesString.Split(';').ToList();
			allDefines.Add(val);
			PlayerSettings.SetScriptingDefineSymbolsForGroup(
				BuildTargetGroup.Android,
				string.Join(";", allDefines.ToArray()));

			PlayerSettings.SetScriptingDefineSymbolsForGroup(
				BuildTargetGroup.iOS,
				string.Join(";", allDefines.ToArray()));
		}
		void removeDefineSymbol(string val)
		{
			string ScriptingDefine = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
			if (!ScriptingDefine.Contains(val))
			{
				return;
			}
			string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
			List<string> allDefines = definesString.Split(';').ToList();
			allDefines.Remove(val);
			PlayerSettings.SetScriptingDefineSymbolsForGroup(
				BuildTargetGroup.Android,
				string.Join(";", allDefines.ToArray()));

			PlayerSettings.SetScriptingDefineSymbolsForGroup(
				BuildTargetGroup.iOS,
				string.Join(";", allDefines.ToArray()));
		}
	}
}