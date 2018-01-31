namespace NendUnityPlugin
{
	using UnityEngine;
	using UnityEditor;
	using System.IO;
	using System.Xml;
	using System.Linq;

	public class NendAndroidSetup : EditorWindow
	{
		private static bool isImportGooglePlayServiceBase = false;
		private static bool isImportGooglePlayServiceBasement = false;
		private static bool isImportGooglePlayServiceLocation = false;
		private static bool isImportV7CardviewLibrary = false;
		private static bool isImportPercentSupportLibrary = false;
		private static bool isImportSupportCompatLibrary = false;
		private static bool isImportSupportCoreUtilLibrary = false;
		private static bool isOutputDebugLog = false;

		private const string AndroidSDKRoot = "AndroidSdkRoot";
		private const string GmsDirectoryPath = "extras/google/m2repository/com/google/android/gms";
		private const string SupportDirectoryPath = "extras/android/m2repository/com/android/support";
		private const string GmsBasementArtifactName = "play-services-basement";
		private const string GmsBaseArtifactName = "play-services-base";
		private const string CardviewArtifactName = "cardview-v7";
		private const string PercentArtifactName = "percent";
		private const string CompatArtifactName = "support-compat";
		private const string LocationArtifactName = "play-services-location";
		private const string CoreUtilsArtifactName = "support-core-utils";
		private const string AndroidLibraryDirectoryPath = "NendAd/Plugins/Android";

		private bool m_ShowGooglePlayServiceMenu = true;
		private bool m_ShowAndroidSupportLibraryMenu = true;
		private bool m_ShowDebugMenu = true;
		private Vector2 m_ScrollPosition = Vector2.zero;

		private static class Ja
		{
			internal const string ImportGooglePlayServices = "nendSDKで利用するGoogle Play Servicesライブラリをプロジェクトに追加します。既にプロジェクトに追加済みの場合はチェックを外してください。";
			internal const string TitleRequired = "Required\n";
			internal const string DescriptionRequiredGooglePlayServices = "広告IDの取得に使用します。";
			internal const string TitleOptional = "Optional\n";
			internal const string DescriptionOptionalGooglePlayServices = "nendSDKの一部広告では位置情報を使用することで、より適切な広告ターゲティングが行えます。\nアプリケーションが位置情報を利用しており、かつ位置情報を利用することを許可される場合、以下をインポートしてください。";
			internal const string WarningPlayServiceLocation = "Location ライブラリがダウンロードされていません。\nAndroid SDK ManagerでAndroid Support Repositoryをダウンロードしてください。";
			internal const string WarningAndroidSDKPath = "AndroidSDKのパスが設定されていません。\nUnityのPreferences... > External Toolsより設定を行ってください。";
			internal const string WarningGooglePlayServices = "Google Play Servicesがダウンロードされていません。\nAndroid SDK ManagerでGoogle Repositoryをダウンロードしてください。";
			internal const string WarningV7CardviewLibrary = "v7 Cardview ライブラリがダウンロードされていません。\nAndroid SDK ManagerでAndroid Support Repositoryをダウンロードしてください。";
			internal const string WarningPercentSupportLibrary = "Percent Support ライブラリがダウンロードされていません。\nAndroid SDK ManagerでAndroid Support Repositoryをダウンロードしてください。";
			internal const string WarningSupportCompatLibrary = "Support Compat ライブラリがダウンロードされていません。\nAndroid SDK ManagerでAndroid Support Repositoryをダウンロードしてください。";
			internal const string WarningSupportCoreUtils = "Support Core Util ライブラリがダウンロードされていません。\nAndroid SDK ManagerでAndroid Support Repositoryをダウンロードしてください。";
			internal const string AboutUnityPreferences = "Preferences設定について";
			internal const string AboutAndroidSDKManager = "Android SDK Managerについて";
			internal const string AboutGoogleRepository = "Google Repositoryについて";
			internal const string AboutAndroidSupportRepository = "Android Support Repositoryについて";
			internal const string ImportAndroidSupportLibrary = "nendSDKで利用するサポートライブラリをプロジェクトに追加します。既にプロジェクトに追加済みの場合はチェックを外してください。";
			internal const string OutputDebugLog = "nendSDKのデバッグログを出力するかどうかを設定します。";
			internal const string UnityPreferencesURL = "https://docs.unity3d.com/ja/current/Manual/Preferences.html";
			internal const string AndroidSDKManagerURL = "https://developer.android.com/studio/intro/update.html?hl=ja#sdk-manager";
			internal const string GoogleRepositoryURL = "https://developer.android.com/studio/intro/update.html?hl=ja#recommended";
		}

		private static class En
		{
			internal const string ImportGooglePlayServices = "Add the Google Play Services to your project for using nendSDK. Uncheck the library which has already been added to the project.";
			internal const string TitleRequired = "Required\n";
			internal const string DescriptionRequiredGooglePlayServices = "Use to get advertisement identifier.";
			internal const string TitleOptional = "Optional\n";
			internal const string DescriptionOptionalGooglePlayServices = "In nendSDK, some advertisements takes advertisement targeting properly if could use location information.\nYour app has used location & grantes us to use location information, please import these libraries.";
			internal const string WarningPlayServiceLocation = "Location Library has not been downloaded.\nDownload Android Support Repository with Android SDK Manager.";
			internal const string WarningAndroidSDKPath = "The Android SDK path is not set.\nPlease make settings from Unity's \"Preferences ...> External Tools\".";
			internal const string WarningGooglePlayServices = "Google Play Services has not been downloaded.\nDownload Google Repository with Android SDK Manager.";
			internal const string WarningV7CardviewLibrary = "v7 Cardview Library has not been downloaded.\nDownload Android Support Repository with Android SDK Manager.";
			internal const string WarningPercentSupportLibrary = "Percent Support Library has not been downloaded.\nDownload Android Support Repository with Android SDK Manager.";
			internal const string WarningSupportCompatLibrary = "Support Core Util Library has not been downloaded.\nDownload Android Support Repository with Android SDK Manager.";
			internal const string WarningSupportCoreUtils = "Support Core Util Library has not been downloaded.\nDownload Android Support Repository with Android SDK Manager.";
			internal const string AboutUnityPreferences = "About Preferences";
			internal const string AboutAndroidSDKManager = "About Android SDK Manager";
			internal const string AboutGoogleRepository = "About Google Repository";
			internal const string AboutAndroidSupportRepository = "About Android Support Repository";
			internal const string ImportAndroidSupportLibrary = "Add the android support libraries to your project for using nendSDK. Uncheck the library which has already been added to the project.";
			internal const string OutputDebugLog = "Sets whether to output debug log of nendSDK or not.";
			internal const string UnityPreferencesURL = "https://docs.unity3d.com/Manual/Preferences.html";
			internal const string AndroidSDKManagerURL = "https://developer.android.com/studio/intro/update.html#sdk-manager";
			internal const string GoogleRepositoryURL = "https://developer.android.com/studio/intro/update.html#recommended";
		}

		[MenuItem ("NendSDK/Android Setup", false, 1)]
		public static void MenuItemAndroidSetup ()
		{
			NendAndroidSetup window = (NendAndroidSetup)EditorWindow.GetWindow (typeof(NendAndroidSetup));
			var titleContent = new GUIContent ();
			titleContent.text = "Android Setup";
			window.titleContent = titleContent;

			var vec2 = new Vector2 (700, 440);
			window.minSize = vec2;
			window.Show ();
		}

		void OnGUI ()
		{
			GUIStyle buttonStyle;
			var isJapanese = IsJapanese ();

			m_ScrollPosition = EditorGUILayout.BeginScrollView (m_ScrollPosition);
			{
				m_ShowGooglePlayServiceMenu = EditorGUILayout.Foldout (m_ShowGooglePlayServiceMenu, "Google Play Services");
				if (m_ShowGooglePlayServiceMenu) {
					EditorGUI.indentLevel = 1;
					EditorGUILayout.HelpBox (isJapanese ? Ja.ImportGooglePlayServices : En.ImportGooglePlayServices, MessageType.Info, true);

					EditorGUI.indentLevel = 2;

					string requiredTitle = (isJapanese ? Ja.TitleRequired : En.TitleRequired);
					string requiredDescription = (isJapanese ? Ja.DescriptionRequiredGooglePlayServices : En.DescriptionRequiredGooglePlayServices);
					SetCustomHelpBoxStyle (requiredTitle, requiredDescription);
					isImportGooglePlayServiceBasement = EditorGUILayout.ToggleLeft ("Import \"play-services-basement\"", isImportGooglePlayServiceBasement);
					if (isImportGooglePlayServiceBasement) {
						if (!CheckAndroidSDKPath ()) {
							ShowAndroidSDKWarning (isJapanese);
						} else if (!CheckLibrary (GmsDirectoryPath)) {
							ShowAndroidLibraryWarning (isJapanese ? Ja.WarningGooglePlayServices : En.WarningGooglePlayServices, isJapanese ? Ja.AboutGoogleRepository : En.AboutGoogleRepository, isJapanese);
						}
					}

					string optionalTitle = (isJapanese ? Ja.TitleOptional : En.TitleOptional);
					string optionalDescription = (isJapanese ? Ja.DescriptionOptionalGooglePlayServices : En.DescriptionOptionalGooglePlayServices);
					SetCustomHelpBoxStyle (optionalTitle, optionalDescription);
					isImportGooglePlayServiceLocation = EditorGUILayout.ToggleLeft ("Import \"play-services-location\"", isImportGooglePlayServiceLocation);
					if (isImportGooglePlayServiceLocation) {
						if (!CheckAndroidSDKPath ()) {
							ShowAndroidSDKWarning (isJapanese);
						} else if (!CheckLibrary (GmsDirectoryPath)) {
							ShowAndroidLibraryWarning (isJapanese ? Ja.WarningPlayServiceLocation : En.WarningPlayServiceLocation, isJapanese ? Ja.AboutGoogleRepository : En.AboutGoogleRepository, isJapanese);
						}
					}
					isImportGooglePlayServiceBase = EditorGUILayout.ToggleLeft ("Import \"play-services-base\"", (isImportGooglePlayServiceBase));
					if (isImportGooglePlayServiceBase) {
						if (!CheckAndroidSDKPath ()) {
							ShowAndroidSDKWarning (isJapanese);
						} else if (!CheckLibrary (GmsDirectoryPath)) {
							ShowAndroidLibraryWarning (isJapanese ? Ja.WarningGooglePlayServices : En.WarningGooglePlayServices, isJapanese ? Ja.AboutGoogleRepository : En.AboutGoogleRepository, isJapanese);
						}
					}
				}
				EditorGUI.indentLevel = 0;

				m_ShowAndroidSupportLibraryMenu = EditorGUILayout.Foldout (m_ShowAndroidSupportLibraryMenu, "Android Support Library");
				if (m_ShowAndroidSupportLibraryMenu) {
					EditorGUI.indentLevel = 1;
					EditorGUILayout.HelpBox (isJapanese ? Ja.ImportAndroidSupportLibrary : En.ImportAndroidSupportLibrary, MessageType.Info, true);

					EditorGUI.indentLevel = 2;
					isImportV7CardviewLibrary = EditorGUILayout.ToggleLeft ("Import \"cardview-v7\"", isImportV7CardviewLibrary);
					if (isImportV7CardviewLibrary) {
						if (!CheckAndroidSDKPath ()) {
							ShowAndroidSDKWarning (isJapanese);
						} else if (!CheckLibrary (SupportDirectoryPath)) {
							ShowAndroidLibraryWarning (isJapanese ? Ja.WarningV7CardviewLibrary : En.WarningV7CardviewLibrary, isJapanese ? Ja.AboutAndroidSupportRepository : En.AboutAndroidSupportRepository, isJapanese);
						}
					}

					isImportPercentSupportLibrary = EditorGUILayout.ToggleLeft ("Import \"percent\"", isImportPercentSupportLibrary);
					if (isImportPercentSupportLibrary) {
						if (!CheckAndroidSDKPath ()) {
							ShowAndroidSDKWarning (isJapanese);
						} else if (!CheckLibrary (SupportDirectoryPath)) {
							ShowAndroidLibraryWarning (isJapanese ? Ja.WarningPercentSupportLibrary : En.WarningPercentSupportLibrary, isJapanese ? Ja.AboutAndroidSupportRepository : En.AboutAndroidSupportRepository, isJapanese);
						}
					}

					isImportSupportCompatLibrary = EditorGUILayout.ToggleLeft ("Import \"support-compat\"", isImportSupportCompatLibrary);
					if (isImportSupportCompatLibrary) {
						if (!CheckAndroidSDKPath ()) {
							ShowAndroidSDKWarning (isJapanese);
						} else if (!CheckLibrary (SupportDirectoryPath)) {
							ShowAndroidLibraryWarning (isJapanese ? Ja.WarningSupportCompatLibrary : En.WarningSupportCompatLibrary, isJapanese ? Ja.AboutAndroidSupportRepository : En.AboutAndroidSupportRepository, isJapanese);
						}
					}

					isImportSupportCoreUtilLibrary = EditorGUILayout.ToggleLeft ("Import \"support-core-utils\"", isImportSupportCoreUtilLibrary);
					if (isImportSupportCoreUtilLibrary) {
						if (!CheckAndroidSDKPath ()) {
							ShowAndroidSDKWarning (isJapanese);
						} else if (!CheckLibrary (SupportDirectoryPath)) {
							ShowAndroidLibraryWarning (isJapanese ? Ja.WarningSupportCoreUtils : En.WarningSupportCoreUtils, isJapanese ? Ja.AboutAndroidSupportRepository : En.AboutAndroidSupportRepository, isJapanese);
						}
					}
				}

				EditorGUI.indentLevel = 0;

				m_ShowDebugMenu = EditorGUILayout.Foldout (m_ShowDebugMenu, "Debug");
				if (m_ShowDebugMenu) {
					EditorGUI.indentLevel = 1;
					EditorGUILayout.HelpBox (isJapanese ? Ja.OutputDebugLog : En.OutputDebugLog, MessageType.Info, true);
					EditorGUI.indentLevel = 2;
					isOutputDebugLog = EditorGUILayout.ToggleLeft ("Output Debug Log", isOutputDebugLog);
				}
			}
			EditorGUILayout.EndScrollView ();

			buttonStyle = new GUIStyle (GUI.skin.button);
			buttonStyle.margin = new RectOffset (20, 20, 10, 10);
			if (GUILayout.Button ("Configure", buttonStyle, GUILayout.Height (24))) {
				Configure ();
			}
		}

		public void Configure ()
		{
			Debug.Log ("Processing...");
			if (isImportGooglePlayServiceBase) {
				AddLibrary (GmsDirectoryPath, GmsBaseArtifactName);
			}
			if (isImportGooglePlayServiceBasement) {
				AddLibrary (GmsDirectoryPath, GmsBasementArtifactName);
			}
			if (isImportGooglePlayServiceLocation) {
				AddLibrary (GmsDirectoryPath, LocationArtifactName);
			}
			if (isImportV7CardviewLibrary) {
				AddLibrary (SupportDirectoryPath, CardviewArtifactName);
			}
			if (isImportPercentSupportLibrary) {
				AddLibrary (SupportDirectoryPath, PercentArtifactName);
			}
			if (isImportSupportCompatLibrary) {
				AddLibrary (SupportDirectoryPath, CompatArtifactName);
			}
			if (isImportSupportCoreUtilLibrary) {
				AddLibrary (SupportDirectoryPath, CoreUtilsArtifactName);
			}
				
			ConfigureAndroidManifest ();
			AssetDatabase.Refresh ();
			Debug.Log ("Done!");
			Close ();
		}

		private static bool IsJapanese ()
		{
			return Application.systemLanguage == SystemLanguage.Japanese; 
		}

		private static bool CheckAndroidSDKPath ()
		{
			string androidSDKPath = EditorPrefs.GetString (AndroidSDKRoot, null);
			return !string.IsNullOrEmpty (androidSDKPath);
		}

		private static bool CheckLibrary (string libraryPath)
		{
			string androidSDKPath = EditorPrefs.GetString (AndroidSDKRoot, null);
			string path = Path.Combine (androidSDKPath, ToPlatformDirectorySeparator (libraryPath));
			return Directory.Exists (path);
		}

		private static string ToPlatformDirectorySeparator (string path)
		{
			return path.Replace ("/", Path.DirectorySeparatorChar.ToString ());
		}

		private static void AddLibrary (string path, string artifactName)
		{
			string libraryDirectoryPath = Path.Combine (Application.dataPath, ToPlatformDirectorySeparator (AndroidLibraryDirectoryPath));
			string[] archives = Directory.GetFiles (libraryDirectoryPath, artifactName + "*.?.?.aar");
			if (null != archives && 0 < archives.Length) {
				Debug.Log ("The " + artifactName + " is already exist.");
				return;
			}
			string artifactPath = Path.Combine (EditorPrefs.GetString (AndroidSDKRoot, null), Path.Combine (path, artifactName));
			var directoryInfo = new DirectoryInfo (artifactPath);
			if (directoryInfo.Exists) {
				DirectoryInfo[] infos = directoryInfo.GetDirectories ("*.?.?");

				if (null == infos || 0 == infos.Length) {
					Debug.LogWarning ("The " + artifactName + " is not installed.");
					return;
				}
				var max = infos
					.Select (di => di.Name)
					.Aggregate ((current, next) => {
					int currentVersion = int.Parse (current.Replace (".", ""));
					int nextVersion = int.Parse (next.Replace (".", ""));
					return nextVersion > currentVersion ? next : current;
				});
				string archiveName = string.Format (artifactName + "-{0}.aar", max);
				string aarPathFrom = Path.Combine (artifactPath, Path.Combine (max, archiveName));
				string aarPathTo = Path.Combine (libraryDirectoryPath, archiveName);
				FileUtil.CopyFileOrDirectory (aarPathFrom, aarPathTo);
				Debug.Log ("Added: " + aarPathTo);
			} else {
				Debug.LogWarning ("The " + artifactName + " is not installed.");
			}
		}

		private static void ConfigureAndroidManifest ()
		{
			string manifestPathDest = Path.Combine (Application.dataPath, ToPlatformDirectorySeparator (AndroidLibraryDirectoryPath + "/AndroidManifest.xml"));
			if (!File.Exists (manifestPathDest)) {
				if (!isOutputDebugLog) {
					Debug.Log ("There is no need to change the AndroidManifest.");
					return;
				}

				string[] UnityAndroidManifestPathList = {
					Path.Combine (EditorApplication.applicationPath, ToPlatformDirectorySeparator ("../PlaybackEngines/AndroidPlayer/Apk/AndroidManifest.xml")),
					Path.Combine (EditorApplication.applicationContentsPath, ToPlatformDirectorySeparator ("PlaybackEngines/AndroidPlayer/Apk/AndroidManifest.xml")),
					Path.Combine (EditorApplication.applicationContentsPath, ToPlatformDirectorySeparator ("PlaybackEngines/AndroidPlayer/AndroidManifest.xml"))
				};
					
				string defaultManifestPath = null;
				foreach (string path in UnityAndroidManifestPathList) {
					if (File.Exists (path)) {
						defaultManifestPath = path;
						Debug.Log ("Found AndroidManifest at " + path);
						break;
					}
				}
				if (null == defaultManifestPath) {
					Debug.LogWarning ("Couldn't find default AndroidManifest.");
					return;
				}
				FileUtil.CopyFileOrDirectory (defaultManifestPath, manifestPathDest);
			} else {
				Debug.Log ("The AndroidManifest is already exist.");
			}
		
			var doc = new XmlDocument ();
			doc.Load (manifestPathDest);
		
			XmlNode applicationNode = doc.SelectSingleNode ("manifest/application");
			if (null == applicationNode) {
				Debug.LogWarning ("The application tag is not found.");
				return;
			}
		
			string ns = applicationNode.GetNamespaceOfPrefix ("android");
			var nsManager = new XmlNamespaceManager (doc.NameTable);
			nsManager.AddNamespace ("android", ns);
				
			XmlNode nendDebuggableNode = applicationNode.SelectSingleNode (@"//meta-data[@android:name='NendDebuggable']", nsManager);
			if (null != nendDebuggableNode) {
				XmlElement element = (XmlElement)nendDebuggableNode;
				element.SetAttribute ("value", ns, isOutputDebugLog.ToString ().ToLower ());
				Debug.Log ("Modified: " + element.OuterXml);
			} else if (isOutputDebugLog) { 
				XmlElement element = CreateNendDebuggableElement (doc, ns);
				applicationNode.AppendChild (element);
				Debug.Log ("Added: " + element.OuterXml);
			} else {
				Debug.Log ("There is no need to create a NendDebuggable element.");
			}
			doc.Save (manifestPathDest);
		}

		private static XmlElement CreateLocaionPermissionElement (XmlDocument doc, string ns, string permission)
		{
			XmlElement element = doc.CreateElement ("uses-permission");
			element.SetAttribute ("name", ns, permission);
			return element;
		}

		private static XmlElement CreateNendDebuggableElement (XmlDocument doc, string ns)
		{
			XmlElement element = doc.CreateElement ("meta-data");
			element.SetAttribute ("name", ns, "NendDebuggable");
			element.SetAttribute ("value", ns, "true");
			return element;
		}

		private static void ShowAndroidSDKWarning (bool isJapanese)
		{
			GUIStyle buttonStyle;

			EditorGUILayout.HelpBox (isJapanese ? Ja.WarningAndroidSDKPath : En.WarningAndroidSDKPath, MessageType.Warning, true);
			buttonStyle = new GUIStyle (GUI.skin.button);
			buttonStyle.margin = new RectOffset (20, 0, 0, 0);
			if (GUILayout.Button (isJapanese ? Ja.AboutUnityPreferences : En.AboutUnityPreferences, buttonStyle, GUILayout.ExpandWidth (false))) {
				Application.OpenURL (isJapanese ? Ja.UnityPreferencesURL : En.UnityPreferencesURL);
			}
		}

		private static void ShowAndroidLibraryWarning (string WarningLibrary, string WarningRepository, bool isJapanese)
		{
			GUIStyle buttonStyle;

			EditorGUILayout.HelpBox (WarningLibrary, MessageType.Warning, true);
			EditorGUILayout.BeginHorizontal ();
			{
				buttonStyle = new GUIStyle (GUI.skin.button);
				buttonStyle.margin = new RectOffset (20, 0, 0, 0);
				if (GUILayout.Button (isJapanese ? Ja.AboutAndroidSDKManager : En.AboutAndroidSDKManager, buttonStyle, GUILayout.ExpandWidth (false))) {
					Application.OpenURL (isJapanese ? Ja.AndroidSDKManagerURL : En.AndroidSDKManagerURL);
				}
				buttonStyle = new GUIStyle (GUI.skin.button);
				buttonStyle.margin = new RectOffset (10, 0, 0, 0);
				if (GUILayout.Button (WarningRepository, buttonStyle, GUILayout.ExpandWidth (false))) {
					Application.OpenURL (isJapanese ? Ja.GoogleRepositoryURL : En.GoogleRepositoryURL);
				}
			}
			EditorGUILayout.EndHorizontal ();
		}

		private static void SetCustomHelpBoxStyle(string title, string description) 
		{
			GUIStyle helpStyle = GUI.skin.GetStyle("HelpBox");
			helpStyle.richText = true;
			EditorGUILayout.TextArea("<b>" + title + "</b>" + description, helpStyle);
		}

	}
}